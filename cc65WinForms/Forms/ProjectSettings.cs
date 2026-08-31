// ProjectSettings.cs
// Documentation:
// This file implements the `ProjectSettings` Windows Form which provides a UI
// for viewing and editing properties of a `CC65Project` instance.
//
// The `using` directives below import the namespaces required by this form:
// - `cc65Wrapper`
//   Contains the core project model types (for example `CC65Project`) used by
//   the dialog to read and write project data.
// - `cc65Wrapper.Enumerations`
//   Defines project-related enums (for example `CC65ProjectTypes`) used to
//   populate and parse the target platform selection.
// - `System`
//   Provides base types such as `Object`, `String` and `EventArgs`.
// - `System.ComponentModel`
//   Contains attributes used by WinForms designer and serialization features
//   (for example `DesignerSerializationVisibility`).
using cc65Wrapper;
using cc65Wrapper.Enumerations;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cc65WinForms
{
    /// <summary>
    /// A dialog that displays and edits settings for a <see cref="CC65Project"/>.
    /// 
    /// Responsibilities:
    /// - Bind current project values to UI controls when the dialog loads.
    /// - Allow the user to change project name, working directory, target
    ///   platform, output file and basic build options.
    /// - Validate and apply changes back to the <see cref="CC65Project"/> instance,
    ///   marking the project as modified when values change.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ProjectSettings : Form
    {
        #region Fields and properties

        /// <summary>
        /// Gets or sets the project used by the dialog.
        /// </summary>
        /// <value>
        /// A <c>CC65Project</c> instance.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CC65Project Project { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSettings"/> class.
        /// </summary>
        public ProjectSettings()
        {
            // Assume empty project ...
            Project = null;

            InitializeComponent();

            PopulateTargetPlatformComboBox();
        }

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the ok Button control.
        /// Commits changes from the UI to the bound <see cref="CC65Project"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void okButtom_Click(Object sender, EventArgs e)
        {
            // Check if project settings have changed ...

            if (
                string.Compare(
                    Project.ProjectName,
                    projectNameTextBox.Text,
                    StringComparison.Ordinal
                ) != 0
            )
            {
                Project.ProjectName = projectNameTextBox.Text;
                Project.IsModified = true;
            }

            if (
                string.Compare(
                    Project.WorkingDirectory,
                    workingDirLabel.Text,
                    StringComparison.Ordinal
                ) != 0
            )
            {
                Project.WorkingDirectory = workingDirLabel.Text;
                Project.IsModified = true;
            }

            // Parse the selected platform string to enum
            var selectedPlatform = TargetPlatformComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedPlatform) && 
                Enum.TryParse<CC65ProjectTypes>(selectedPlatform, true, out var newPlatform))
            {
                if (Project.TargetPlatform != newPlatform)
                {
                    Project.TargetPlatform = newPlatform;
                    Project.IsModified = true;
                }
            }

            if (
                string.Compare(Project.OutputFile, outputFileTextBox.Text, StringComparison.Ordinal)
                != 0
            )
            {
                Project.OutputFile = outputFileTextBox.Text;
                Project.IsModified = true;
            }

            if (Project.OptimiseCode != optimiseCodeCheckBox.Checked)
            {
                Project.OptimiseCode = optimiseCodeCheckBox.Checked;
                Project.IsModified = true;
            }

            CloseProjectSettings();
        }

        /// <summary>
        /// Handles the Load event of the ProjectSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>This method assumes the classes Project property points at the currently loaded project</remarks>
        private void ProjectSettings_Load(Object sender, EventArgs e)
        {
            if (Project != null)
            {
                this.Text = $"Project Settings - {Project.ProjectName}";
                projectNameTextBox.Text = Project.ProjectName;
                workingDirLabel.Text = Project.WorkingDirectory;
                TargetPlatformComboBox.SelectedIndex = (int)Project.TargetPlatform;
                outputFileTextBox.Text = Project.OutputFile;
                optimiseCodeCheckBox.Checked = Project.OptimiseCode;
                versionTextBox.Text = Project.Version.ToString();
                outputPathTextBox.Text = Project.FullOutputFilePath;
            }
        }

        /// <summary>
        /// Handles the Click event of the setWorkingDirButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void setWorkingDirButton_Click(Object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                workingDirLabel.Text = folderBrowserDialog.SelectedPath;
                outputPathTextBox.Text = Path.Combine(workingDirLabel.Text, outputFileTextBox.Text);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the outputFileTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void outputFileTextBox_TextChanged(Object sender, EventArgs e)
        {
            outputPathTextBox.Text = Path.Combine(Project.WorkingDirectory, outputFileTextBox.Text);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Populates the target platform ComboBox.
        /// </summary>
        private void PopulateTargetPlatformComboBox()
        {
            foreach (var value in Enum.GetValues(typeof(CC65ProjectTypes)))
            {
                TargetPlatformComboBox.Items.Add(value.ToString());
            }

            TargetPlatformComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Closes the project settings.
        /// </summary>
        private void CloseProjectSettings()
        {
            this.Close();
        }

        #endregion
    }
}
