using cc65Wrapper;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cc65WinForms
{
    public partial class MainForm : Form
    {
        #region Event Handlers

        /// <summary>
        /// Creates a new editor tab.
        /// </summary>
        /// <remarks>
        /// Invoked by the "New" tool-strip button. Uses <see cref="CreateTab(string)"/> with a null
        /// argument to create an untitled/empty tab.
        /// </remarks>
        /// <param name="sender">Event source (toolbar button).</param>
        /// <param name="e">Event arguments.</param>
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        /// <summary>
        /// Delayed text-changed handler for the editor.
        /// </summary>
        /// <remarks>
        /// Called after a short delay when editor text changes. Responsible for UI-related updates
        /// triggered by edits such as updating invisible-character highlighting. There is commented
        /// code for rebuilding an object explorer asynchronously; that behavior is currently disabled.
        /// </remarks>
        /// <param name="sender">Event source (editor control).</param>
        /// <param name="e">Text changed event data containing the changed range.</param>
        void Tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            //rebuild object explorer
            //ThreadPool.QueueUserWorkItem(
            //    (o) => ReBuildObjectExplorer(text)
            //);

            //show invisible chars
            HighlightInvisibleChars(e.ChangedRange);
        }

        /// <summary>
        /// Handles immediate selection changes in the editor.
        /// </summary>
        /// <remarks>
        /// Updates the UI cursor/position label to reflect the current caret position.
        /// </remarks>
        /// <param name="sender">Event source (editor control).</param>
        /// <param name="e">Event arguments.</param>
        private void Tb_SelectionChanged(Object sender, EventArgs e)
        {
            var tb = sender as FastColoredTextBox;

            UpdateCursorPositionLabel(tb.Selection.Start);
            // int cursorPosition = tb.Selection.Start.iChar;
        }

        /// <summary>
        /// Handles delayed selection changes in the editor.
        /// </summary>
        /// <remarks>
        /// Performs lower-priority tasks that should not run on every single selection change,
        /// such as recording the last visit timestamp for the current line and highlighting all
        /// occurrences of the word under the caret within the visible range.
        /// </remarks>
        /// <param name="sender">Event source (editor control).</param>
        /// <param name="e">Event arguments.</param>
        void Tb_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var tb = sender as FastColoredTextBox;

            // Remember last visit time ...
            if (tb.Selection.IsEmpty && tb.Selection.Start.iLine < tb.LinesCount)
            {
                if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
                {
                    tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
                    lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
                }
            }

            // Highlight same words ...
            tb.VisibleRange.ClearStyle(sameWordsStyle);

            if (!tb.Selection.IsEmpty)
            {
                return; // user selected a range; don't highlight single-word occurrences
            }

            // Get fragment around caret ...
            FastColoredTextBoxNS.Range fragment = tb.Selection.GetFragment(@"\w");
            var text = fragment.Text;

            if (text.Length == 0)
            {
                return;
            }

            // Highlight same words in visible range ...
            FastColoredTextBoxNS.Range[] ranges = tb.VisibleRange.GetRanges($"\\b{text}\\b").ToArray();

            if (ranges.Length > 1)
            {
                foreach (FastColoredTextBoxNS.Range r in ranges)
                {
                    r.SetStyle(sameWordsStyle);
                }
            }
        }

        /// <summary>
        /// Handles key-down events in the editor to support editor shortcuts.
        /// </summary>
        /// <remarks>
        /// Supported shortcuts:
        /// - Ctrl + OemMinus : Navigate backward
        /// - Ctrl + Shift + OemMinus : Navigate forward
        /// - Ctrl + K : Force-show context popup menu (ignores minimum fragment length)
        /// Handled keys set <see cref="KeyEventArgs.Handled"/> to true to prevent further processing.
        /// </remarks>
        /// <param name="sender">Event source (editor control).</param>
        /// <param name="e">Key event data.</param>
        void Tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.OemMinus)
            {
                NavigateBackward();
                e.Handled = true;
            }

            if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.OemMinus)
            {
                NavigateForward();
                e.Handled = true;
            }

            if (e.KeyData == (Keys.K | Keys.Control))
            {
                // forced show (MinFragmentLength will be ignored)
                (CurrentTB.Tag as TbInfo).popupMenu.Show(true);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Updates the cursor/position display as the mouse moves over the editor.
        /// </summary>
        /// <param name="sender">Event source (editor control).</param>
        /// <param name="e">Mouse event data containing the mouse location.</param>
        void Tb_MouseMove(object sender, MouseEventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            Place place = tb.PointToPlace(e.Location);

            UpdateCursorPositionLabel(place);

            //var r = new Range(tb, place, place);
            //var text = r.GetFragment("[a-zA-Z]").Text;
            //lbWordUnderMouse.Text = text;
        }

        /// <summary>
        /// Refreshes invisible-character highlighting for every open file.
        /// </summary>
        /// <remarks>
        /// Iterates through all open editor tabs and re-applies invisible-character highlighting,
        /// then invalidates the current editor to force a repaint.
        /// </remarks>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event arguments.</param>
        private void BtInvisibleChars_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                HighlightInvisibleChars((tab.Controls[0] as FastColoredTextBox).Range);
            }

            CurrentTB?.Invalidate();
        }

        /// <summary>
        /// Opens a file via the Open file action.
        /// </summary>
        /// <param name="sender">Event source (toolbar button).</param>
        /// <param name="e">Event arguments.</param>
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Saves the current file.
        /// </summary>
        /// <param name="sender">Event source (toolbar button).</param>
        /// <param name="e">Event arguments.</param>
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Shows the Save As dialog for the current file.
        /// </summary>
        /// <param name="sender">Event source (menu item).</param>
        /// <param name="e">Event arguments.</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        /// <summary>
        /// Quits the application by closing the main form.
        /// </summary>
        /// <param name="sender">Event source (menu item).</param>
        /// <param name="e">Event arguments.</param>
        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Periodic timer tick that updates the enabled/disabled state of UI commands.
        /// </summary>
        /// <remarks>
        /// Keeps the toolbar/menu state consistent with the editor and project state: e.g. enables
        /// Save when the current editor has unsaved changes, enables project actions when a project
        /// is loaded, and updates project-save availability via <see cref="CanSaveProject"/>.
        /// Exceptions are caught and written to the console to avoid timer termination.
        /// </remarks>
        /// <param name="sender">Event source (timer).</param>
        /// <param name="e">Event arguments.</param>
        private void TmUpdateInterface_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentTB != null && tsFiles.Items.Count > 0)
                {
                    FastColoredTextBox tb = CurrentTB;
                    //undoStripButton.Enabled = undoToolStripMenuItem.Enabled = tb.UndoEnabled;
                    //redoStripButton.Enabled = redoToolStripMenuItem.Enabled = tb.RedoEnabled;
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = tb.IsChanged;
                    closeFileToolStripMenuItem.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    //pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = true;
                    //cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled =
                    //copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = !tb.Selection.IsEmpty;
                    //printToolStripButton.Enabled = true;
                }
                else
                {
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                    closeFileToolStripMenuItem.Enabled = false;
                    //cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled =
                    //copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = false;
                    //pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = false;
                    //printToolStripButton.Enabled = false;
                    //undoStripButton.Enabled = undoToolStripMenuItem.Enabled = false;
                    //redoStripButton.Enabled = redoToolStripMenuItem.Enabled = false;
                    //dgvObjectExplorer.RowCount = 0;
                }

                openProjectToolStripMenuItem.Enabled = this.Project == null;
                closeProjectToolStripMenuItem.Enabled =
                    btBuildProject.Enabled =
                    btExecuteProject.Enabled =
                    projectSettingsToolStripMenuItem.Enabled =
                        this.Project != null;
                saveProjectToolStripMenuItem.Enabled = saveProjectToolStripButton.Enabled =
                    CanSaveProject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Toggles current-line highlighting mode.
        /// </summary>
        /// <remarks>
        /// Calls <see cref="ChangeCurrentLineHighLight"/> to change the highlight state and update
        /// the editor visuals accordingly.
        /// </remarks>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event arguments.</param>
        private void BtHighlightCurrentLine_Click(object sender, EventArgs e)
        {
            ChangeCurrentLineHighLight();
        }

        /// <summary>
        /// Toggles display of folding guide lines for all open editors.
        /// </summary>
        /// <remarks>
        /// Iterates open tabs and applies the state of <see cref="btShowFoldingLines"/> to each
        /// editor's <see cref="FastColoredTextBox.ShowFoldingLines"/> property, then invalidates
        /// the current editor to force a repaint.
        /// </remarks>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event arguments.</param>
        private void BtShowFoldingLines_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                (tab.Controls[0] as FastColoredTextBox).ShowFoldingLines =
                    btShowFoldingLines.Checked;
            }

            CurrentTB?.Invalidate();
        }

        /// <summary>
        /// Handles change of the selected build target platform.
        /// </summary>
        /// <remarks>
        /// Calls <see cref="ChangeSelectedPlatform"/> to apply the newly selected platform to the
        /// project / build settings.
        /// </remarks>
        /// <param name="sender">Event source (combo box).</param>
        /// <param name="e">Event arguments.</param>
        private void CbTargetPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSelectedPlatform();
        }

        /// <summary>
        /// Begins an asynchronous project build.
        /// </summary>
        /// <remarks>
        /// Async event handler that triggers <see cref="BuildProjectAsync"/> and intentionally
        /// ignores the returned result.
        /// </remarks>
        /// <param name="sender">Event source (build button).</param>
        /// <param name="e">Event arguments.</param>
        private async void BtBuildProject_ClickAsync(object sender, EventArgs e)
        {
            // tbOutput.AppendText($"Building Project{Environment.NewLine}");
            _ = await BuildProjectAsync();
        }

        /// <summary>
        /// Executes the current project asynchronously.
        /// </summary>
        /// <remarks>
        /// Async event handler that triggers <see cref="ExecuteProjectAsync"/>.
        /// </remarks>
        /// <param name="sender">Event source (execute button).</param>
        /// <param name="e">Event arguments.</param>
        private async void BtExecuteProject_ClickAsync(object sender, EventArgs e)
        {
            // tbOutput.AppendText($"Executing Project{Environment.NewLine}");
            await ExecuteProjectAsync();
        }

        /// <summary>
        /// Opens an existing project.
        /// </summary>
        /// <param name="sender">Event source (menu item).</param>
        /// <param name="e">Event arguments.</param>
        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        /// <summary>
        /// Handles node clicks in the project files tree view.
        /// </summary>
        /// <remarks>
        /// Left-clicking on a file node will focus an existing tab if open, or open a new tab for
        /// that file. Clicking non-file nodes (root / groups) is currently ignored or reserved for
        /// future behavior.
        /// </remarks>
        /// <param name="sender">Event source (tree view).</param>
        /// <param name="e">Tree node mouse click event data.</param>
        private void TvProjectFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FATabStripItem matchingItem = null;

            // Bail if not left mouse button up ...
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // Was a source/header file node selected ? ...
            if (e.Node.Tag as string != string.Empty)
            {
                // Is there already an open tab for file ?
                foreach (FATabStripItem item in tsFiles.Items)
                {
                    if (string.Compare(item.Caption, e.Node.Text, StringComparison.Ordinal) == 0)
                    {
                        // Yep, so make it the active tab
                        matchingItem = item;
                        tsFiles.SelectedItem = matchingItem;
                    }
                }

                // Nope, so open a new tab for tab
                if (matchingItem == null)
                {
                    CreateTab(e.Node.Tag as string);
                }

                // Job done ...
                return;
            }

            // If we got here the user selected one of the following:
            //
            // + Root project node
            // + 'Header Files' node
            // + 'Source Files' node
            //
            // So work out which ...
            //var title = e.Node.Text;

            //switch (title)
            //{
            //    case HEADER_FILES:
            //        break;

            //    case SOURCE_FILES:
            //        break;

            //    // Root project node ...
            //    default:
            //        break;
            //}
        }

        /// <summary>
        /// Shows a context menu for project-file nodes (right-click).
        /// </summary>
        /// <remarks>
        /// When the user right-clicks on the "Header Files" or "Source Files" group nodes a small
        /// context menu is shown with options to add or remove files. Implementation contains TODOs
        /// for completing add/remove behavior.
        /// </remarks>
        /// <param name="sender">Event source (tree view).</param>
        /// <param name="e">Mouse event data.</param>
        private void TvProjectFiles_MouseUp(Object sender, MouseEventArgs e)
        {
            // Bail if not right mouse button up ...
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            TreeNode node = tvProjectFiles.GetNodeAt(e.X, e.Y);

            // Bail if no node selected ...
            if (node == null)
            {
                return;
            }

            var nodeTitle = node.Text;

            switch (nodeTitle)
            {
                case HEADER_FILES:
                    break;

                case SOURCE_FILES:
                    break;

                default:
                    return;
            }

            // TODO 1: Handle adding new header/source files to project
            // TODO 2: Handle removing header/source files to project

            tvProjectFiles.SelectedNode = node;
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Add new file");
            item.Click += new EventHandler(Item_Click);
            item.Tag = nodeTitle;
            menu.Items.Add(item);
            item = new ToolStripMenuItem("Remove file");
            item.Click += new EventHandler(Item_Click);
            item.Tag = nodeTitle;
            menu.Items.Add(item);
            menu.Show(tvProjectFiles, e.Location);
        }

        /// <summary>
        /// Handler for context-menu item clicks created in <see cref="TvProjectFiles_MouseUp"/>.
        /// </summary>
        /// <remarks>
        /// Placeholder handler. Concrete add/remove logic should be implemented here.
        /// </remarks>
        /// <param name="sender">Event source (menu item).</param>
        /// <param name="e">Event arguments.</param>
        void Item_Click(object sender, EventArgs e)
        {
            ;
        }

        /// <summary>
        /// Closes the current file tab.
        /// </summary>
        /// <param name="sender">Event source (menu item).</param>
        /// <param name="e">Event arguments.</param>
        private void CloseFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        /// <summary>
        /// Closes the currently open project.
        /// </summary>
        /// <param name="sender">Event source (menu item).</param>
        /// <param name="e">Event arguments.</param>
        private void CloseProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        /// <summary>
        /// Saves the current project to disk.
        /// </summary>
        /// <param name="sender">Event source (toolbar button).</param>
        /// <param name="e">Event arguments.</param>
        private void SaveProjectToolStripButton_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        /// <summary>
        /// When selection in the errors grid changes, open the related file and navigate to the error line.
        /// </summary>
        /// <remarks>
        /// Filters supported file types (.c/.h), ensures an editor tab is open for the file (creates
        /// one if necessary), and navigates the editor caret to the error line number.
        /// </remarks>
        /// <param name="sender">Event source (errors DataGridView).</param>
        /// <param name="e">Event arguments.</param>
        private void ErrorsDataGridView_SelectionChanged(Object sender, EventArgs e)
        {
            // Bail if no errorList selected ...
            if (errorsDataGridView.SelectedRows.Count < 1)
            {
                return;
            }

            // Extract the error ...
            var selectedError = (Cc65Error)errorsDataGridView.SelectedRows[0].DataBoundItem;

            var fileInfo = new FileInfo(selectedError.Filename);

            switch (fileInfo.Extension.ToLower())
            {
                case ".c":
                case ".h":
                    break;

                // Bail if not a source or header file ...
                default:
                    return;
            }

            // Switch to the editor instance and navigate to the error line ...
            FATabStripItem matchingItem = null;

            // Is there already an open tab for file ?
            foreach (FATabStripItem item in tsFiles.Items)
            {
                if (
                    string.Compare(item.Caption, selectedError.Filename, StringComparison.Ordinal)
                    == 0
                )
                {
                    // Yep, so make it the active tab

                    matchingItem = item;
                    tsFiles.SelectedItem = matchingItem;
                }
            }

            // Didn't find open tab for file ...
            if (matchingItem == null)
            {
                CreateTab(Path.Combine(Project.WorkingDirectory, selectedError.Filename));
            }

            // Highlight the line in error ...
            var tb = (tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
            tb.Navigate(Math.Max(0, selectedError.LineNumber - 1));
        }

        /// <summary>
        /// Opens the project settings dialog.
        /// </summary>
        /// <param name="sender">Event source (menu item).</param>
        /// <param name="e">Event arguments.</param>
        private void ProjectSettingsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            DisplayProjectSettingsDialog();
        }
        #endregion
    }
}
