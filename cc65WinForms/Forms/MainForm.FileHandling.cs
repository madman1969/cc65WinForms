using FarsiLibrary.Win;
using System.IO;
using System.Windows.Forms;

namespace cc65WinForms
{
    public partial class MainForm : Form
    {
        #region File Handling Methods

        /// <summary>
        /// Displays the 'Open File' dialog and loads the selection into a new editor tab
        /// </summary>
        private void OpenFile()
        {
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                CreateTab(ofdMain.FileName);
            }
        }

        /// <summary>
        /// Save the contents of the current text editor tab.
        ///
        /// Exits if now text editor tab is current.
        /// </summary>
        private void SaveFile()
        {
            if (tsFiles.SelectedItem != null)
            {
                Save(tsFiles.SelectedItem);
            }
        }

        /// <summary>
        /// Saves the contents of all open text editor tabs.
        ///
        /// Will prompt if a file name is required
        /// </summary>
        private void SaveOpenFiles()
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                Save(tab);
            }
        }

        /// <summary>
        /// Saves the contents of the current text editor tab as the specified name.
        ///
        /// If save operation cancelled, then restores the original details
        /// </summary>
        private void SaveFileAs()
        {
            // Bail if no editor tab selected ...
            if (tsFiles.SelectedItem == null)
            {
                return;
            }

            // Note the original filename and reset it ...
            var oldFile = tsFiles.SelectedItem.Tag as string;
            tsFiles.SelectedItem.Tag = null;

            // Did we managed to save the file ? ...
            if (!Save(tsFiles.SelectedItem))
            {
                // Nope, so restore the tag and title ...
                if (oldFile != null)
                {
                    tsFiles.SelectedItem.Tag = oldFile;
                    tsFiles.SelectedItem.Title = Path.GetFileName(oldFile);
                }
            }
        }

        /// <summary>
        /// Closes the current text editor tab. Automatically saves the current contents before closing the tab
        /// </summary>
        private void CloseFile()
        {
            SaveFile();

            if (tsFiles.SelectedItem != null)
            {
                tsFiles.RemoveTab(tsFiles.SelectedItem);
            }
        }

        /// <summary>
        /// Closes all open text editor tabs
        /// </summary>
        private void CloseAllFiles()
        {
            while (tsFiles.Items.Count > 0)
            {
                tsFiles.RemoveTab(tsFiles.Items[0]);
            }
        }

        #endregion
    }
}
