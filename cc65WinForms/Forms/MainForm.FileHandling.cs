using FarsiLibrary.Win;
using System.IO;
using System.Windows.Forms;

namespace cc65WinForms
{
    public partial class MainForm : Form
    {
        #region File Handling Methods

        /// <summary>
        /// Displays the 'Open File' dialog and loads the selected file into a new editor tab.
        /// </summary>
        /// <remarks>
        /// Uses the existing __OpenFileDialog__ instance named <c>ofdMain</c>. If the dialog
        /// returns <see cref="DialogResult.OK"/>, the selected file path (<c>ofdMain.FileName</c>)
        /// is passed to <c>CreateTab</c> to create and load a new editor tab.
        /// </remarks>
        private void OpenFile()
        {
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                CreateTab(ofdMain.FileName);
            }
        }

        /// <summary>
        /// Saves the contents of the currently selected text editor tab.
        /// </summary>
        /// <remarks>
        /// If no editor tab is selected (<c>tsFiles.SelectedItem</c> is <c>null</c>), this method
        /// returns immediately. The actual save logic (including prompts for file name or error
        /// handling) is performed by the <c>Save(FATabStripItem)</c> method.
        /// </remarks>
        private void SaveFile()
        {
            if (tsFiles.SelectedItem != null)
            {
                Save(tsFiles.SelectedItem);
            }
        }

        /// <summary>
        /// Saves the contents of all open text editor tabs.
        /// </summary>
        /// <remarks>
        /// Iterates every <c>FATabStripItem</c> in <c>tsFiles.Items</c> and invokes <c>Save</c>.
        /// If a tab requires a file name, the <c>Save</c> implementation is expected to prompt the user.
        /// </remarks>
        private void SaveOpenFiles()
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                Save(tab);
            }
        }

        /// <summary>
        /// Saves the contents of the current text editor tab, forcing the user to specify a file name.
        /// </summary>
        /// <remarks>
        /// This method implements a "Save As" behavior:
        /// - If no tab is selected, it returns immediately.
        /// - It temporarily clears the selected tab's <c>Tag</c> (which holds the associated file path as a <c>string</c>)
        ///   so that <c>Save</c> will treat the tab as untitled and prompt for a file name.
        /// - If the save operation fails or is cancelled (<c>Save</c> returns <c>false</c>), the original
        ///   <c>Tag</c> and display <c>Title</c> are restored.
        /// </remarks>
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
        /// Closes the currently selected text editor tab after attempting to save its contents.
        /// </summary>
        /// <remarks>
        /// Calls <c>SaveFile</c> first to allow the user to save changes. After saving (or if saving is not required),
        /// the currently selected tab is removed from <c>tsFiles</c> via <c>RemoveTab</c>.
        /// </remarks>
        private void CloseFile()
        {
            SaveFile();

            if (tsFiles.SelectedItem != null)
            {
                tsFiles.RemoveTab(tsFiles.SelectedItem);
            }
        }

        /// <summary>
        /// Closes all open text editor tabs.
        /// </summary>
        /// <remarks>
        /// Removes tabs one-by-one until <c>tsFiles.Items</c> is empty. Any per-tab save/confirmation behavior
        /// is performed by the tab removal logic and/or by <c>RemoveTab</c>.
        /// </remarks>
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
