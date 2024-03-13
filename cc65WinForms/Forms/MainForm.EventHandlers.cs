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
        /// Creates new tool strip button_click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        /// <summary>
        /// Handles the TextChangedDelayed event of the Tb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
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
        /// Handles the SelectionChangedDelayed event of the tb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void tb_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            //remember last visit time
            if (tb.Selection.IsEmpty && tb.Selection.Start.iLine < tb.LinesCount)
            {
                if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
                {
                    tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
                    lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
                }
            }

            //highlight same words
            tb.VisibleRange.ClearStyle(sameWordsStyle);
            if (!tb.Selection.IsEmpty)
            {
                return; //user selected diapason
            }
            //get fragment around caret
            Range fragment = tb.Selection.GetFragment(@"\w");
            var text = fragment.Text;
            if (text.Length == 0)
            {
                return;
            }
            //highlight same words
            Range[] ranges = tb.VisibleRange.GetRanges($"\\b{text}\\b").ToArray();

            if (ranges.Length > 1)
            {
                foreach (Range r in ranges)
                {
                    r.SetStyle(sameWordsStyle);
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the tb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void tb_KeyDown(object sender, KeyEventArgs e)
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
                //forced show (MinFragmentLength will be ignored)
                (CurrentTB.Tag as TbInfo).popupMenu.Show(true);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the tb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        void tb_MouseMove(object sender, MouseEventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            Place place = tb.PointToPlace(e.Location);

            UpdateCursorPositionLabel(place);

            //var r = new Range(tb, place, place);
            //var text = r.GetFragment("[a-zA-Z]").Text;
            //lbWordUnderMouse.Text = text;
        }

        /// <summary>
        /// Handles the Click event of the btInvisibleChars control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btInvisibleChars_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                HighlightInvisibleChars((tab.Controls[0] as FastColoredTextBox).Range);
            }

            if (CurrentTB != null)
            {
                CurrentTB.Invalidate();
            }
        }

        /// <summary>
        /// Handles the Click event of the openToolStripButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Handles the Click event of the saveToolStripButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Handles the Click event of the saveAsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        /// <summary>
        /// Handles the Click event of the quitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Tick event of the tmUpdateInterface control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmUpdateInterface_Tick(object sender, EventArgs e)
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
        /// Handles the Click event of the btHighlightCurrentLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btHighlightCurrentLine_Click(object sender, EventArgs e)
        {
            ChangeCurrentLineHighLight();
        }

        /// <summary>
        /// Handles the Click event of the btShowFoldingLines control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btShowFoldingLines_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                (tab.Controls[0] as FastColoredTextBox).ShowFoldingLines =
                    btShowFoldingLines.Checked;
            }

            if (CurrentTB != null)
            {
                CurrentTB.Invalidate();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbTargetPlatform control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbTargetPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSelectedPlatform();
        }

        /// <summary>
        /// Handles the Click event of the btBuildProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btBuildProject_Click(object sender, EventArgs e)
        {
            // tbOutput.AppendText($"Building Project{Environment.NewLine}");
            _ = await BuildProject();
        }

        /// <summary>
        /// Handles the Click event of the btExecuteProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btExecuteProject_Click(object sender, EventArgs e)
        {
            // tbOutput.AppendText($"Executing Project{Environment.NewLine}");
            await ExecuteProject();
        }

        /// <summary>
        /// Handles the Click event of the openProjectToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        /// <summary>
        /// Handles the NodeMouseClick event of the tvProjectFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        private void tvProjectFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
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

        private void tvProjectFiles_MouseUp(Object sender, MouseEventArgs e)
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
            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem("Add new file");
            item.Click += new EventHandler(item_Click);
            item.Tag = nodeTitle;
            menu.MenuItems.Add(item);
            item = new MenuItem("Remove file");
            item.Click += new EventHandler(item_Click);
            item.Tag = nodeTitle;
            menu.MenuItems.Add(item);
            menu.Show(tvProjectFiles, e.Location);
        }

        void item_Click(object sender, EventArgs e)
        {
            ;
        }

        /// <summary>
        /// Handles the Click event of the closeFileToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        /// <summary>
        /// Handles the Click event of the closeProjectToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        /// <summary>
        /// Handles the Click event of the saveProjectToolStripButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveProjectToolStripButton_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the errorsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void errorsDataGridView_SelectionChanged(Object sender, EventArgs e)
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
        /// Handles the Click event of the projectSettingsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void projectSettingsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            DisplayProjectSettingsDialog();
        }

        #endregion
    }
}
