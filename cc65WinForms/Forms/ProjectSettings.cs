using cc65Wrapper;
using cc65Wrapper.Enumerations;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cc65WinForms
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ProjectSettings : Form
    {
        #region Fields and properties

        /// <summary>
        /// Gets or sets the project used by the dialog.
        /// </summary>
        /// <value>
        /// A <c>Cc65Project</c> instance.
        /// </value>
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

            if (
                string.Compare(
                    Project.TargetPlatform,
                    TargetPlatformComboBox.SelectedItem as string,
                    StringComparison.Ordinal
                ) != 0
            )
            {
                Project.TargetPlatform = TargetPlatformComboBox.SelectedItem as string;
                Project.IsModified = true;
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
                TargetPlatformComboBox.SelectedIndex = (int)
                    Enum.Parse(typeof(CC65ProjectTypes), Project.TargetPlatform);
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
