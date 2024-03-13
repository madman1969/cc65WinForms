using cc65Wrapper;
using cc65Wrapper.Enumerations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cc65WinForms
{
    public partial class MainForm : Form
    {
        #region Project routines

        /// <summary>
        /// Displays the 'Open Project' dialog
        /// </summary>
        /// <remarks>
        /// If a project is opened it will populate the 'Target Platform', project tree view and update the status bar
        /// </remarks>
        private void OpenProject()
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Project Files|*.json",
                CheckFileExists = true
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Load the project JSON ...
                ProjectFile = dlg.FileNames[0];
                var json = File.ReadAllText(ProjectFile);
                Project = CC65Project.FromJson(json);

                // Select the correct target for the project ...
                Enum.TryParse(Project.TargetPlatform, out CC65ProjectTypes target);
                cbTargetPlatform.SelectedIndex = (int)target;

                // Clear the modified flag ...
                Project.IsModified = false;

                // Update status bar items ...
                // DisplayLoadedProject();
                // DisplayTargetPlatform();
            }

            // Populate the tree view
            PopulateTreeView();

            tbOutput.AppendText($"Loaded project: {ProjectFile} ...{Environment.NewLine}");

            UpdateProjectStatusLabel(Project.ProjectName);
        }

        /// <summary>
        /// Updates the status bar to show the currently loaded project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <remarks>Will display 'No Project Loaded' if passed null</remarks>
        private void UpdateProjectStatusLabel(string projectName)
        {
            string message;

            if (projectName != null)
            {
                message = $"Project {projectName} loaded";
            }
            else
            {
                message = "No Project Loaded";
            }

            ProjectLabel.Text = message;
        }

        /// <summary>
        /// Updates the status bar to show the current target platform selection.
        /// </summary>
        /// <remarks>If passed null assumes target platform is C128</remarks>
        private void UpdateTargetPlatformLabel()
        {
            string message;

            if (Project == null)
            {
                message = "Target: C128";
            }
            else
            {
                message = $"Target: {Project.TargetPlatform.ToUpper()}";
            }

            PlatformTargetLabel.Text = message;
        }

        // TODO 3: Handle adding new project
        // TODO 4: Update cursor position readout when cursor keys used
        // TODO 5: Add emulator configuration support

        /// <summary>
        /// Closes the currently open project.
        /// </summary>
        /// <remarks>Will close any open files and clear the project tree</remarks>
        private void CloseProject()
        {
            tbOutput.AppendText($"Closed project: {ProjectFile} ...{Environment.NewLine}");

            // Unload the project ...
            this.Project = null;

            // Close any open files ...
            CloseAllFiles();

            // Clear the project tree ...
            PopulateTreeView();

            // Reset the selected platform target ...
            cbTargetPlatform.SelectedIndex = 0;

            UpdateProjectStatusLabel(null);
        }

        /// <summary>
        /// Helper method which returns a boolean if a project is loaded and has been modified
        /// </summary>
        /// <returns>
        ///   <c>true</c> if there is a modified loaded project; otherwise, <c>false</c>.
        /// </returns>
        private bool CanSaveProject()
        {
            if (this.Project == null)
            {
                return false;
            }

            return Project.IsModified;
        }

        /// <summary>
        /// Saves the current project settings
        /// </summary>
        private void SaveProject()
        {
            // Bail if no project loaded or un-named ...
            if (Project == null || string.IsNullOrEmpty(Project.ProjectName))
            {
                return;
            }

            // Convert project to JSON ...
            var asJSON = Project.AsJson();

            // Do we have a project file path ? ...
            if (string.IsNullOrEmpty(ProjectFile))
            {
                var dlg = new SaveFileDialog
                {
                    Filter = "Project Files|*.json",
                    DefaultExt = ".json"
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ProjectFile = dlg.FileName;
                }
                else
                {
                    return;
                }
            }

            // Write project details to file ...
            File.WriteAllText(ProjectFile, asJSON);

            // Clear the modified flag ...
            Project.IsModified = false;
        }

        /// <summary>
        /// Builds the currently loaded project
        /// </summary>
        /// <returns><c>true</c> if build successful; else <c>false</c></returns>
        /// <remarks>Will save any open files first</remarks>
        private async Task<bool> BuildProject()
        {
            SaveOpenFiles();

            var builtOK = false;

            tbOutput.AppendText(
                $"Building {Project.InputFiles.Count} files for project [{Project.ProjectName}] targeting [{Project.TargetPlatform}]...{Environment.NewLine}"
            );

            // Compile the project ...
            CliWrap.Models.ExecutionResult result = await Cc65Build.Compile(Project);

            List<Cc65Error> errorList = new List<Cc65Error>();

            if (result.ExitCode != 0)
            {
                errorList = Cc65Build.ErrorsAsErrorList(result);

                // Force the 'Errors List' to be selected ...
                tsOutput.SelectedItem = tsOutput.Items[1];

                PopulateErrorsDataGridView(errorList);

                tbOutput.AppendText(
                    $"Build failed, found {errorList.Count} errors{Environment.NewLine}"
                );
            }
            else
            {
                builtOK = true;
                tbOutput.AppendText($"Build successful{Environment.NewLine}");
            }

            PopulateErrorsDataGridView(errorList);

            return builtOK;
        }

        /// <summary>
        /// Populates the error list from the supplied list of <c>Cc65Error</c> instances
        /// </summary>
        /// <param name="errorList"></param>
        /// <remarks>The error rows will be highlighted according to their <c>Cc65Error.Type</c> value</remarks>
        private void PopulateErrorsDataGridView(List<Cc65Error> errorList)
        {
            // Populate the data grid view
            errorsDataGridView.DataSource = errorList;

            // Set the appropriate background colour based on error type ...
            foreach (var item in errorsDataGridView.Rows)
            {
                var error = (Cc65Error)((item as DataGridViewRow).DataBoundItem);

                switch (error.Type)
                {
                    case "Warning":
                        (item as DataGridViewRow).DefaultCellStyle.ForeColor = Color.Purple;
                        break;
                    case "Error":
                        (item as DataGridViewRow).DefaultCellStyle.ForeColor = Color.Orange;
                        break;
                    case "Fatal":
                        (item as DataGridViewRow).DefaultCellStyle.ForeColor = Color.Red;
                        break;
                }
            }
        }

        /// <summary>
        /// Launches the current project in WinVICE using the current target platform selection.
        /// </summary>
        private async Task ExecuteProject()
        {
            var builtOK = await BuildProject();

            if (builtOK)
            {
                tbOutput.AppendText(
                    $"Launching {Project.ProjectName} in emulator ...{Environment.NewLine}"
                );

                _ = await Cc65Emulators.LaunchEmulator(Project, emulators);
            }
        }

        /// <summary>
        /// Displays the project settings dialog.
        /// </summary>
        /// <remarks>Allows the user to modified the project settings/// </remarks>
        private void DisplayProjectSettingsDialog()
        {
            var dlg = new ProjectSettings { Project = Project };

            // Use the updated project settings ...
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Project = dlg.Project;
            }
        }

        #endregion
    }
}
