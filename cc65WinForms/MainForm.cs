using cc65Wrapper;
using cc65Wrapper.Enumerations;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cc65WinForms
{
    public partial class MainForm : Form
    {
        #region Constants

        private const string HEADER_FILES = "Header Files";
        private const string SOURCE_FILES = "Source Files";
        private const string NO_PROJECT_LOADED = "No Project Loaded";

        #endregion

        #region Fields and properties

        string[] keywords =
        {
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while",
            "add",
            "alias",
            "ascending",
            "descending",
            "dynamic",
            "from",
            "get",
            "global",
            "group",
            "into",
            "join",
            "let",
            "orderby",
            "partial",
            "remove",
            "select",
            "set",
            "value",
            "var",
            "where",
            "yield"
        };
        string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
        string[] snippets =
        {
            "if(^)\n{\n;\n}",
            "if(^)\n{\n;\n}\nelse\n{\n;\n}",
            "for(^;;)\n{\n;\n}",
            "while(^)\n{\n;\n}",
            "do\n{\n^;\n}while();",
            "switch(^)\n{\ncase : break;\n}"
        };
        string[] declarationSnippets =
        {
            "public class ^\n{\n}",
            "private class ^\n{\n}",
            "internal class ^\n{\n}",
            "public struct ^\n{\n;\n}",
            "private struct ^\n{\n;\n}",
            "internal struct ^\n{\n;\n}",
            "public void ^()\n{\n;\n}",
            "private void ^()\n{\n;\n}",
            "internal void ^()\n{\n;\n}",
            "protected void ^()\n{\n;\n}",
            "public ^{ get; set; }",
            "private ^{ get; set; }",
            "internal ^{ get; set; }",
            "protected ^{ get; set; }"
        };
        Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
        Color currentLineColor = Color.FromArgb(100, 200, 200, 255);
        Color changedLineColor = Color.FromArgb(255, 230, 230, 255);

        private string ProjectFile = string.Empty;

        public Cc65Project Project { get; set; } = null;
        private Cc65Emulators emulators;
        private Style sameWordsStyle = new MarkerStyle(
            new SolidBrush(Color.FromArgb(50, Color.Gray))
        );

        FastColoredTextBox CurrentTB
        {
            get
            {
                if (tsFiles.SelectedItem == null)
                {
                    return null;
                }

                return (tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
            }
            set
            {
                tsFiles.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
        }
        DateTime lastNavigatedDateTime = DateTime.Now;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            cbTargetPlatform.SelectedIndex = 0;

            // Load emulator settings ...
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"Test Files");
            filepath = Path.Combine(filepath, "emulators.json");
            var json = File.ReadAllText(filepath);
            emulators = Cc65Emulators.FromJson(json);

            // Initialise the tree view ...
            PopulateTreeView();
        }

        #region Event Handlers

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        void Tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            var tb = (sender as FastColoredTextBox);
            //rebuild object explorer
            var text = (sender as FastColoredTextBox).Text;
            //ThreadPool.QueueUserWorkItem(
            //    (o) => ReBuildObjectExplorer(text)
            //);

            //show invisible chars
            HighlightInvisibleChars(e.ChangedRange);
        }

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
            Range[] ranges = tb.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();

            if (ranges.Length > 1)
            {
                foreach (Range r in ranges)
                {
                    r.SetStyle(sameWordsStyle);
                }
            }
        }

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

        void tb_MouseMove(object sender, MouseEventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            Place place = tb.PointToPlace(e.Location);

            UpdateCursorPositionLabel(place);

            //var r = new Range(tb, place, place);
            //var text = r.GetFragment("[a-zA-Z]").Text;
            //lbWordUnderMouse.Text = text;
        }

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

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

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
                        this.Project != null;
                saveProjectToolStripMenuItem.Enabled = saveProjectToolStripButton.Enabled =
                    CanSaveProject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btHighlightCurrentLine_Click(object sender, EventArgs e)
        {
            ChangeCurrentLineHighLight();
        }

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

        private void cbTargetPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSelectedPlatform();
        }

        private async void btBuildProject_Click(object sender, EventArgs e)
        {
            // tbOutput.AppendText($"Building Project{Environment.NewLine}");
            _ = await BuildProject();
        }

        private async void btExecuteProject_Click(object sender, EventArgs e)
        {
            // tbOutput.AppendText($"Executing Project{Environment.NewLine}");
            await ExecuteProject();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void tvProjectFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FATabStripItem matchingItem = null;

            if (e.Node.Tag as string != string.Empty)
            {
                // Is there already an open tab for file ?
                foreach (FATabStripItem item in tsFiles.Items)
                {
                    if (item.Caption == e.Node.Text)
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

                return;
            }

            var title = e.Node.Text;

            switch (title)
            {
                case HEADER_FILES:
                    break;

                case SOURCE_FILES:
                    break;
            }
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        private void saveProjectToolStripButton_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

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
                if (item.Caption == selectedError.Filename)
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

        #endregion

        #region Private Methods

        private void HighlightInvisibleChars(Range range)
        {
            range.ClearStyle(invisibleCharsStyle);
            if (btInvisibleChars.Checked)
            {
                range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
            }
        }

        private bool Save(FATabStripItem tab)
        {
            var tb = (tab.Controls[0] as FastColoredTextBox);
            if (tab.Tag == null)
            {
                if (sfdMain.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                tab.Title = Path.GetFileName(sfdMain.FileName);
                tab.Tag = sfdMain.FileName;
            }

            try
            {
                File.WriteAllText(tab.Tag as string, tb.Text);
                tb.IsChanged = false;
            }
            catch (Exception ex)
            {
                if (
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error
                    ) == DialogResult.Retry
                )
                {
                    return Save(tab);
                }
                else
                {
                    return false;
                }
            }

            tb.Invalidate();

            return true;
        }

        private bool NavigateBackward()
        {
            var max = new DateTime();
            var iLine = -1;
            FastColoredTextBox tb = null;
            for (var iTab = 0; iTab < tsFiles.Items.Count; iTab++)
            {
                var t = (tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (var i = 0; i < t.LinesCount; i++)
                {
                    if (t[i].LastVisit < lastNavigatedDateTime && t[i].LastVisit > max)
                    {
                        max = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
                }
            }
            if (iLine >= 0)
            {
                tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                Console.WriteLine("Backward: " + lastNavigatedDateTime);
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool NavigateForward()
        {
            DateTime min = DateTime.Now;
            var iLine = -1;
            FastColoredTextBox tb = null;
            for (var iTab = 0; iTab < tsFiles.Items.Count; iTab++)
            {
                var t = (tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (var i = 0; i < t.LinesCount; i++)
                {
                    if (t[i].LastVisit > lastNavigatedDateTime && t[i].LastVisit < min)
                    {
                        min = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
                }
            }
            if (iLine >= 0)
            {
                tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                Console.WriteLine("Forward: " + lastNavigatedDateTime);
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateTab(string fileName)
        {
            try
            {
                var tb = new FastColoredTextBox();
                tb.Font = new Font("Consolas", 9.75f);
                // tb.Font = new Font("Fira Code", 9.75f);
                // tb.ContextMenuStrip = cmMain;
                tb.Dock = DockStyle.Fill;
                tb.BorderStyle = BorderStyle.Fixed3D;
                //tb.VirtualSpace = true;
                tb.LeftPadding = 17;
                tb.Language = Language.CSharp;
                tb.AddStyle(sameWordsStyle); //same words style
                var tab = new FATabStripItem(
                    fileName != null ? Path.GetFileName(fileName) : "[new]",
                    tb
                )
                {
                    Tag = fileName
                };
                if (fileName != null)
                {
                    tb.OpenFile(fileName);
                }

                tb.Tag = new TbInfo();
                tsFiles.AddTab(tab);
                tsFiles.SelectedItem = tab;
                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;
                tb.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(
                    Tb_TextChangedDelayed
                );
                tb.SelectionChangedDelayed += new EventHandler(tb_SelectionChangedDelayed);
                tb.KeyDown += new KeyEventHandler(tb_KeyDown);
                tb.MouseMove += new MouseEventHandler(tb_MouseMove);
                tb.ChangedLineColor = changedLineColor;
                if (btHighlightCurrentLine.Checked)
                {
                    tb.CurrentLineColor = currentLineColor;
                }

                tb.ShowFoldingLines = btShowFoldingLines.Checked;
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
                //create autocomplete popup menu
                //AutocompleteMenu popupMenu = new AutocompleteMenu(tb);
                //popupMenu.Items.ImageList = ilAutocomplete;
                //popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                //BuildAutocompleteMenu(popupMenu);
                //(tb.Tag as TbInfo).popupMenu = popupMenu;
            }
            catch (Exception ex)
            {
                if (
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error
                    ) == DialogResult.Retry
                )
                {
                    CreateTab(fileName);
                }
            }
        }

        private void OpenFile()
        {
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                CreateTab(ofdMain.FileName);
            }
        }

        private void SaveFile()
        {
            if (tsFiles.SelectedItem != null)
            {
                Save(tsFiles.SelectedItem);
            }
        }

        private void SaveOpenFiles()
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                Save(tab);
            }
        }

        private void SaveFileAs()
        {
            if (tsFiles.SelectedItem != null)
            {
                var oldFile = tsFiles.SelectedItem.Tag as string;
                tsFiles.SelectedItem.Tag = null;
                if (!Save(tsFiles.SelectedItem))
                {
                    if (oldFile != null)
                    {
                        tsFiles.SelectedItem.Tag = oldFile;
                        tsFiles.SelectedItem.Title = Path.GetFileName(oldFile);
                    }
                }
            }
        }

        private void CloseFile()
        {
            SaveFile();

            tsFiles.RemoveTab(tsFiles.SelectedItem);
        }

        private void CloseAllFiles()
        {
            while (tsFiles.Items.Count > 0)
            {
                tsFiles.RemoveTab(tsFiles.Items[0]);
            }
        }

        private void ChangeSelectedPlatform()
        {
            var selectedPlatform = cbTargetPlatform.SelectedItem as string;

            // Only change project target platform if a project is loaded !
            if (Project != null)
            {
                Project.TargetPlatform = selectedPlatform.ToLower();

                // Flag as modified ...
                Project.IsModified = true;
            }
        }

        private void ChangeCurrentLineHighLight()
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                if (btHighlightCurrentLine.Checked)
                {
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = currentLineColor;
                }
                else
                {
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = Color.Transparent;
                }
            }
            if (CurrentTB != null)
            {
                CurrentTB.Invalidate();
            }
        }

        private void UpdateCursorPositionLabel(Place place)
        {
            var message = $"Line {place.iLine}, Column {place.iChar}";
            CursorPositionLabel.Text = message;
        }

        #region Project routines

        /// <summary>
        /// Opens the project.
        /// </summary>
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
                Project = Cc65Project.FromJson(json);

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
            // Bail if no project loaded or unamed ...
            if (Project == null || string.IsNullOrEmpty(Project.ProjectName))
            {
                return;
            }

            // Convert project to JSON ...
            var asJSON = Project.AsJson();

            // Do we have a project filepath ? ...
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
        /// Builds the project.
        /// </summary>
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
                // List<string> errorList = Cc65Build.ErrorsAsStringList(result);
                errorList = Cc65Build.ErrorsAsErrorList(result);

                PopulateErrorsDataGridView(errorList);

                tbOutput.AppendText(
                    $"Build failed, found {errorList.Count} errors{Environment.NewLine}"
                );

                //foreach (var error in errorList)
                //{
                //    tbOutput.AppendText($"{error}{Environment.NewLine}");
                //}
            }
            else
            {
                builtOK = true;
                tbOutput.AppendText($"Build successful{Environment.NewLine}");
            }

            PopulateErrorsDataGridView(errorList);

            // tbOutput.ScrollToEnd();

            return builtOK;
        }

        /// <summary>
        /// Populates the error list with build errorList
        /// </summary>
        /// <param name="errorList"></param>
        private void PopulateErrorsDataGridView(List<Cc65Error> errorList)
        {
            errorsDataGridView.DataSource = errorList;
        }

        /// <summary>
        /// Launches the project in WinVICE.
        /// </summary>
        private async Task ExecuteProject()
        {
            var builtOK = await BuildProject();

            if (builtOK)
            {
                tbOutput.AppendText(
                    $"Launching {Project.ProjectName} in emulator ...{Environment.NewLine}"
                );

                CliWrap.Models.ExecutionResult result = await Cc65Emulators.LaunchEmulator(
                    Project,
                    emulators
                );
            }
        }

        #endregion

        #region TreeView routines

        /// <summary>
        /// Clears the TreeView.
        /// </summary>
        private void ClearTreeView()
        {
            tvProjectFiles.Nodes.Clear();
        }

        /// <summary>
        /// Populate the tree with the project files
        /// </summary>
        private void PopulateTreeView()
        {
            ClearTreeView();

            // Show empty tree view if no project loaded ...
            if (Project == null)
            {
                var emptyNode = new TreeNode
                {
                    Name = NO_PROJECT_LOADED,
                    Text = NO_PROJECT_LOADED,
                    Tag = string.Empty
                };
                tvProjectFiles.Nodes.Add(emptyNode);

                return;
            }

            // Add root node ...
            var rootNode = new TreeNode
            {
                Name = $"{Project.ProjectName}",
                Text = $"{Project.ProjectName}",
                Tag = string.Empty
            };
            tvProjectFiles.Nodes.Add(rootNode);

            // Add 'Header Files' node ...
            var hdrFiles = new TreeNode
            {
                Name = HEADER_FILES,
                Text = HEADER_FILES,
                Tag = string.Empty,
                // IsExpanded = true
            };
            rootNode.Nodes.Add(hdrFiles);

            // Add 'Source Files' node ...
            var srcFiles = new TreeNode
            {
                Name = SOURCE_FILES,
                Text = "Source Files",
                Tag = string.Empty,
                // IsExpanded = true
            };
            rootNode.Nodes.Add(srcFiles);

            // Add the header files ...
            foreach (var hdrfile in Project.HeaderFiles)
            {
                var node = new TreeNode
                {
                    Name = hdrfile,
                    Text = hdrfile,
                    Tag = Path.Combine(Project.WorkingDirectory, hdrfile)
                };

                hdrFiles.Nodes.Add(node);
            }

            // Add the source files ...
            foreach (var srcfile in Project.InputFiles)
            {
                var node = new TreeNode
                {
                    Name = srcfile,
                    Text = srcfile,
                    Tag = Path.Combine(Project.WorkingDirectory, srcfile)
                };

                srcFiles.Nodes.Add(node);
            }

            tvProjectFiles.ExpandAll();
        }

        #endregion

        #endregion
    }
}
