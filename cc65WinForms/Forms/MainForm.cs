using cc65Wrapper;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// TODO 1: Refactor MainForm

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

        /// <summary>
        /// A list of C/C# keywords used for syntax highlight by the FCTB instance
        /// </summary>
        private readonly string[] keywords =
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

        /// <summary>
        /// A private <c>string</c> used internally as a shortcut to the current project name
        /// </summary>
        private string ProjectFile = string.Empty;

        /// <summary>
        /// Gets or sets the current project.
        /// </summary>
        /// <value>A <c>Cc65Project</c> instance</value>
        public CC65Project Project { get; set; } = null;

        /// <summary>
        /// A private instance of the <c>Cc65Emulators</c> used internally
        /// </summary>
        private Cc65Emulators emulators;
        private Style sameWordsStyle = new MarkerStyle(
            new SolidBrush(Color.FromArgb(50, Color.Gray))
        );

        /// <summary>
        /// Gets or sets the currently active <c>FastColoredTextBox</c> instance
        /// </summary>
        /// <value>A <c>FastColoredTextBox</c> instance</value>
        private FastColoredTextBox CurrentTB
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
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

        #region Private Methods

        /// <summary>
        /// Suppresses or highlights the invisible chars.
        /// </summary>
        /// <param name="range">The range.</param>
        private void HighlightInvisibleChars(Range range)
        {
            range.ClearStyle(invisibleCharsStyle);

            if (btInvisibleChars.Checked)
            {
                range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
            }
        }

        /// <summary>
        /// Saves the text associated with the specified tab.
        /// </summary>
        /// <param name="tab">The currently selected editor tab.</param>
        /// <returns><c>true</c> if successful; else <c>false</c></returns>
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
                Console.WriteLine($"Backward: {lastNavigatedDateTime}");
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
                Console.WriteLine($"Forward: {lastNavigatedDateTime}");
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new text editor tab and loads the contents of the specified file
        /// </summary>
        /// <param name="fileName">Name of the file to load into the editor tab</param>
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

        /// <summary>
        /// Changes the selected target platform for the current project
        /// </summary>
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

            UpdateTargetPlatformLabel();
        }

        /// <summary>
        /// Toggles the current line highlight across all open text editor tabs.
        /// </summary>
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

        /// <summary>
        /// Updates the cursor position label in the application status bar.
        /// </summary>
        /// <param name="place">A Place instance containing the new row/column value</param>
        private void UpdateCursorPositionLabel(Place place)
        {
            var message = $"Line {place.iLine}, Column {place.iChar}";
            CursorPositionLabel.Text = message;
        }

        #region TreeView routines

        /// <summary>
        /// Clears the project tree view
        /// </summary>
        private void ClearTreeView()
        {
            tvProjectFiles.Nodes.Clear();
        }

        /// <summary>
        /// Populates project tree view with the source/header files referenced by the current <c>Cc65Project</c> instance
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
