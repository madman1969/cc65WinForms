using cc65Wrapper;
using cc65Wrapper.Enumerations;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cc65WinForms
{
    /// <summary>
    /// Main application window for the cc65WinForms project.
    /// Provides the primary UI surface including:
    /// - Project tree view population and management
    /// - Editor tab creation and management (based on <see cref="FastColoredTextBox"/>)
    /// - Navigation through edit history (forward/back)
    /// - UI helpers such as cursor position updates and line highlighting toggles
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants

        /// <summary>
        /// Tree node text used for the project's header files group.
        /// </summary>
        private const string HEADER_FILES = "Header Files";

        /// <summary>
        /// Tree node text used for the project's source files group.
        /// </summary>
        private const string SOURCE_FILES = "Source Files";

        /// <summary>
        /// Text to display in the tree view when no project is loaded.
        /// </summary>
        private const string NO_PROJECT_LOADED = "No Project Loaded";

        #endregion

        #region Fields and properties

        /// <summary>
        /// Style used to render invisible characters (whitespace/newlines) in editors.
        /// </summary>
        readonly Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);

        /// <summary>
        /// Background color used to highlight the current line in editors.
        /// </summary>
        readonly Color currentLineColor = Color.FromArgb(100, 200, 200, 255);

        /// <summary>
        /// Background color used to indicate a changed line.
        /// </summary>
        readonly Color changedLineColor = Color.FromArgb(255, 230, 230, 255);

        /// <summary>
        /// Internal shortcut to the file path of the current project file.
        /// Empty when no project is loaded.
        /// </summary>
        private string ProjectFile = string.Empty;

        /// <summary>
        /// Gets or sets the currently loaded <see cref="CC65Project"/>.
        /// This property may be null when no project is open.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CC65Project Project { get; set; } = null;

        /// <summary>
        /// Holds the emulator configuration read from disk (emulators.json).
        /// </summary>
        readonly Cc65Emulators emulators;

        /// <summary>
        /// Style used to mark all occurrences of the currently selected word.
        /// </summary>
        readonly Style sameWordsStyle = new MarkerStyle(
            new SolidBrush(Color.FromArgb(50, Color.Gray))
        );

        /// <summary>
        /// Gets or sets the currently active editor instance.
        /// Reading returns the <see cref="FastColoredTextBox"/> hosted in the selected tab.
        /// Setting will switch the tab and focus the provided editor.
        /// </summary>
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

        /// <summary>
        /// Tracks the timestamp used for incremental navigation (back/forward).
        /// </summary>
        DateTime lastNavigatedDateTime = DateTime.Now;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// Reads emulator configuration and initializes the project tree view.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            cbTargetPlatform.SelectedIndex = 0;

            // Load emulator settings ...
            var filepath = Path.Combine(AppContext.BaseDirectory, "Test Files", "emulators.json");
            var json = File.ReadAllText(filepath);
            emulators = Cc65Emulators.FromJson(json);

            // Initialise the tree view ...
            PopulateTreeView();
        }

        #region Private Methods

        /// <summary>
        /// Suppresses or highlights the invisible chars within the provided range.
        /// </summary>
        /// <param name="range">The <see cref="FastColoredTextBoxNS.Range"/> to apply invisible char styling to.</param>
        private void HighlightInvisibleChars(FastColoredTextBoxNS.Range range)
        {
            range.ClearStyle(invisibleCharsStyle);

            if (btInvisibleChars.Checked)
            {
                range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
            }
        }

        /// <summary>
        /// Saves the text associated with the specified tab to disk.
        /// If the tab has no associated file path, a <see cref="SaveFileDialog"/> is shown.
        /// </summary>
        /// <param name="tab">The currently selected editor tab.</param>
        /// <returns><c>true</c> if the file was successfully saved; otherwise <c>false</c>.</returns>
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

        /// <summary>
        /// Navigates to the previous edit location across all open editor tabs.
        /// Uses per-line <c>LastVisit</c> timestamps to determine the previous location.
        /// </summary>
        /// <returns><c>true</c> if navigation occurred; otherwise <c>false</c>.</returns>
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

        /// <summary>
        /// Navigates to the next edit location across all open editor tabs.
        /// Finds the nearest <c>LastVisit</c> timestamp that is newer than the current navigation cursor.
        /// </summary>
        /// <returns><c>true</c> if navigation occurred; otherwise <c>false</c>.</returns>
        private bool NavigateForward()
        {
            // Track the earliest LastVisit timestamp that is still after lastNavigatedDateTime
            DateTime min = DateTime.Now;

            // Line index of the next navigation target
            int iLine = -1;

            // The text box containing that line
            FastColoredTextBox tb = null;

            // Iterate all open tabs
            for (int iTab = 0; iTab < tsFiles.Items.Count; iTab++)
            {
                // Each tab hosts a FastColoredTextBox as its first control
                var t = tsFiles.Items[iTab].Controls[0] as FastColoredTextBox;

                // Scan all lines in this text box
                for (int i = 0; i < t.LinesCount; i++)
                {
                    // Find the nearest LastVisit timestamp that is:
                    //   - newer than the last navigated point
                    //   - but still the earliest among candidates
                    if (t[i].LastVisit > lastNavigatedDateTime && t[i].LastVisit < min)
                    {
                        min = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
                }
            }

            // If a suitable line was found, navigate to it
            if (iLine >= 0)
            {
                // Switch to the tab containing the target line
                tsFiles.SelectedItem = tb.Parent as FATabStripItem;

                // Move caret to the target line
                tb.Navigate(iLine);

                // Update the navigation cursor
                lastNavigatedDateTime = tb[iLine].LastVisit;
                Console.WriteLine($"Forward: {lastNavigatedDateTime}");

                // Refresh UI focus and redraw
                tb.Focus();
                tb.Invalidate();

                return true;
            }
            else
            {
                // No forward navigation target exists
                return false;
            }
        }


        /// <summary>
        /// Creates a new text editor tab and loads the contents of the specified file.
        /// The created editor is wired up with common event handlers and styling.
        /// </summary>
        /// <param name="fileName">Full path of the file to open in the new tab; if null a new empty document is created.</param>
        private void CreateTab(string fileName)
        {
            try
            {
                var tb = new FastColoredTextBox()
                {
                    Font = new Font("Consolas", 9.75f),
                    /* tb.Font = new Font("Fira Code", 9.75f);*/
                    /* tb.ContextMenuStrip = cmMain;*/
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D,
                    /*tb.VirtualSpace = true;*/
                    LeftPadding = 17,
                    Language = Language.CSharp
                };
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
                tb.SelectionChangedDelayed += new EventHandler(Tb_SelectionChangedDelayed);
                tb.SelectionChanged += Tb_SelectionChanged;
                tb.KeyDown += new KeyEventHandler(Tb_KeyDown);
                tb.MouseMove += new MouseEventHandler(Tb_MouseMove);
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
        /// Changes the selected target platform for the current project based on the UI selection.
        /// Updates <see cref="CC65Project.TargetPlatform"/> and marks the project as modified.
        /// </summary>
        private void ChangeSelectedPlatform()
        {
            var selectedPlatform = cbTargetPlatform.SelectedItem as string;

            // Only change project target platform if a project is loaded !
            if (Project != null && !string.IsNullOrEmpty(selectedPlatform))
            {
                // Parse the selected platform string to enum
                if (Enum.TryParse<CC65ProjectTypes>(selectedPlatform.ToLower(), true, out var platform))
                {
                    Project.TargetPlatform = platform;

                    // Flag as modified ...
                    Project.IsModified = true;
                }
            }

            UpdateTargetPlatformLabel();
        }

        /// <summary>
        /// Toggles the current line highlight across all open text editor tabs.
        /// Applies or removes the configured <see cref="currentLineColor"/> depending on the toggle state.
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

            CurrentTB?.Invalidate();
        }

        /// <summary>
        /// Updates the cursor position label in the application status bar.
        /// </summary>
        /// <param name="place">A <see cref="Place"/> instance containing the new row/column value.</param>
        private void UpdateCursorPositionLabel(Place place)
        {
            var message = $"Line {place.iLine}, Column {place.iChar}";
            CursorPositionLabel.Text = message;
        }

        #region TreeView routines

        /// <summary>
        /// Clears the project tree view.
        /// </summary>
        private void ClearTreeView()
        {
            tvProjectFiles.Nodes.Clear();
        }

        /// <summary>
        /// Populates project tree view with the source/header files referenced by the current <see cref="CC65Project"/> instance.
        /// When no project is loaded a placeholder node is displayed.
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
