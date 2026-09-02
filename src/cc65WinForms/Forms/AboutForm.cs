using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace cc65WinForms
{
    /// <summary>
    /// A simple "About" dialog showing the application version, copyright and a
    /// clickable link to the current session's log file (<see cref="Program.LogFilePath"/>).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AboutForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class,
        /// populating version/copyright text from assembly attributes and the
        /// log link from <see cref="Program.LogFilePath"/>.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();

            versionLabel.Text = $"Version {assembly.GetName().Version}";

            if (Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute))
                is AssemblyCopyrightAttribute copyrightAttribute
                && !string.IsNullOrEmpty(copyrightAttribute.Copyright))
            {
                copyrightLabel.Text = copyrightAttribute.Copyright;
            }

            logLinkLabel.Text = Program.LogFilePath;
        }

        /// <summary>
        /// Swaps the placeholder icon for the owning form's icon (the application's
        /// actual icon) once <see cref="Form.Owner"/> is available.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Owner?.Icon != null)
            {
                iconPictureBox.Image = Owner.Icon.ToBitmap();
            }
        }

        /// <summary>
        /// Opens the current log file in the OS-associated application, or
        /// informs the user if it does not exist yet.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LogLinkLabel_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
        {
            var logFilePath = Program.LogFilePath;

            if (string.IsNullOrEmpty(logFilePath) || !File.Exists(logFilePath))
            {
                MessageBox.Show(
                    this,
                    $"The log file has not been created yet:{Environment.NewLine}{logFilePath}",
                    "Log File Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            logLinkLabel.LinkVisited = true;

            try
            {
                Process.Start(new ProcessStartInfo(logFilePath) { UseShellExecute = true });
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Could not open the log file:{Environment.NewLine}{ex.Message}",
                    "Unable to Open Log File",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
