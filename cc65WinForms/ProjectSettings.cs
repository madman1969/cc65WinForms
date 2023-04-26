using cc65Wrapper;
using cc65Wrapper.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cc65WinForms
{
    // TODO: Handle 'Set Path' buttons for 'Project Settings' dialog

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
        public Cc65Project Project { get; set; }

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
        /// Handles the Click event of the okButtom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void okButtom_Click(Object sender, EventArgs e)
        {
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
                outputFileLabel.Text = Project.OutputFile;
                optimiseCodeCheckBox.Checked = Project.OptimiseCode;
                versionTextBox.Text = Project.Version.ToString();
                outputPathTextBox.Text = Project.FullOutputFilePath;
            }
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
