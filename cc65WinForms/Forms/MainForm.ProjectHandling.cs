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
        /// Displays the 'Open Project' dialog and loads the selected project file.
        /// </summary>
        /// <remarks>
        /// If the user selects a valid project JSON file this method:
        /// - Reads the file contents and deserializes into a <c>CC65Project</c> instance.
        /// - Updates the UI target platform selection to match the project.
        /// - Clears the project's modified flag.
        /// - Populates the project tree view and writes a message to the output pane.
        /// If the dialog is cancelled no project is loaded; the tree view and UI are still refreshed.
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
                cbTargetPlatform.SelectedIndex = (int)Project.TargetPlatform;

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
        /// <param name="projectName">Name of the project to display. If <c>null</c>, "No Project Loaded" will be shown.</param>
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
        /// <remarks>
        /// If no project is loaded this method defaults the displayed platform to C128.
        /// When a project is loaded the project's <c>TargetPlatform</c> value is displayed in upper-case.
        /// </remarks>
        private void UpdateTargetPlatformLabel()
        {
            string message;

            if (Project == null)
            {
                message = "Target: C128";
            }
            else
            {
                message = $"Target: {Project.TargetPlatform.ToString().ToUpper()}";
            }

            PlatformTargetLabel.Text = message;
        }

        // TODO 3: Handle adding new project
        // TODO 4: Switch focus to 'Output' tab when no compilation errors.
        // TODO 5: Add emulator configuration support

        /// <summary>
        /// Closes the currently open project and clears related UI state.
        /// </summary>
        /// <remarks>
        /// This method:
        /// - Writes a message to the output pane.
        /// - Unloads the in-memory <c>Project</c>.
        /// - Closes all open editor files.
        /// - Clears and repopulates the project tree view.
        /// - Resets the target platform selection to the default (index 0).
        /// - Updates the project status label to indicate no project is loaded.
        /// </remarks>
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
        /// Determines whether the currently loaded project can be saved.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if a project is loaded and its <c>IsModified</c> flag is <c>true</c>; otherwise, <c>false</c>.
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
        /// Saves the current project settings to disk in JSON format.
        /// </summary>
        /// <remarks>
        /// If the project has no file path (<c>ProjectFile</c>) the user will be prompted with a __SaveFileDialog__.
        /// After a successful save the project's <c>IsModified</c> flag is cleared.
        /// If no project is loaded or the project has no name the save is skipped.
        /// </remarks>
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
        /// Builds the currently loaded project by invoking the external cc65 toolchain.
        /// </summary>
        /// <returns><c>true</c> if build successful; otherwise <c>false</c>.</returns>
        /// <remarks>
        /// This method:
        /// - Saves any open files prior to building.
        /// - Invokes <c>Cc65Build.CompileAsync(Project)</c> and collects errors.
        /// - If the build fails the error list is displayed in the errors grid and the Errors tab is selected.
        /// - On success a success message is written to the output pane.
        /// The method returns <c>false</c> if the build failed or if no project is set up to build.
        /// </remarks>
        private async Task<bool> BuildProjectAsync()
        {
            SaveOpenFiles();

            var builtOK = false;

            tbOutput.AppendText(
                $"Building {Project.InputFiles.Count} files for project [{Project.ProjectName}] targeting [{Project.TargetPlatform}]...{Environment.NewLine}"
            );

            // CompileAsync the project ...
            var result = await Cc65Build.CompileAsync(Project);

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
        /// Populates the error list grid from the supplied list of <c>Cc65Error</c> instances.
        /// </summary>
        /// <param name="errorList">List of errors returned from a cc65 compilation run.</param>
        /// <remarks>
        /// Each row's foreground color is updated based on the error's <c>Type</c>:
        /// - "Warning": Purple
        /// - "Error": Orange
        /// - "Fatal": Red
        /// The grid's <c>DataSource</c> is set to the provided list which binds each <c>Cc65Error</c> to a row.
        /// </remarks>
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
        /// Builds and, on success, launches the current project in the configured emulator.
        /// </summary>
        /// <remarks>
        /// This method will first call <see cref="BuildProjectAsync"/>. If that build succeeds the project
        /// is launched via <c>Cc65Emulators.LaunchEmulatorAsync(Project, emulators)</c>. A message is written to the output pane before launching.
        /// </remarks>
        private async Task ExecuteProjectAsync()
        {
            var builtOK = await BuildProjectAsync();

            if (builtOK)
            {
                tbOutput.AppendText(
                    $"Launching {Project.ProjectName} in emulator ...{Environment.NewLine}"
                );

                _ = await Cc65Emulators.LaunchEmulatorAsync(Project, emulators);
            }
        }

        /// <summary>
        /// Displays the project settings dialog and updates the active project with any changes.
        /// </summary>
        /// <remarks>
        /// A <c>ProjectSettings</c> dialog is created and passed the current <c>Project</c>.
        /// If the user accepts the dialog (<see cref="DialogResult.OK"/>) the in-memory project instance is replaced with the dialog's updated <c>Project</c>.
        /// </remarks>
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
