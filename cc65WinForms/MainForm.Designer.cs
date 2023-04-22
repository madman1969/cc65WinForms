namespace cc65WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ProjectLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PlatformTargetLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CursorPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.closeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveProjectToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lbTargetPlatform = new System.Windows.Forms.ToolStripLabel();
            this.cbTargetPlatform = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btBuildProject = new System.Windows.Forms.ToolStripButton();
            this.btExecuteProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btInvisibleChars = new System.Windows.Forms.ToolStripButton();
            this.btHighlightCurrentLine = new System.Windows.Forms.ToolStripButton();
            this.btShowFoldingLines = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvProjectFiles = new System.Windows.Forms.TreeView();
            this.tsFiles = new FarsiLibrary.Win.FATabStrip();
            this.ofdMain = new System.Windows.Forms.OpenFileDialog();
            this.sfdMain = new System.Windows.Forms.SaveFileDialog();
            this.tmUpdateInterface = new System.Windows.Forms.Timer(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tsOutput = new FarsiLibrary.Win.FATabStrip();
            this.outputTSI = new FarsiLibrary.Win.FATabStripItem();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.errorsTSI = new FarsiLibrary.Win.FATabStripItem();
            this.errorsDataGridView = new System.Windows.Forms.DataGridView();
            this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cc65ErrorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsOutput)).BeginInit();
            this.tsOutput.SuspendLayout();
            this.outputTSI.SuspendLayout();
            this.errorsTSI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cc65ErrorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectLabel,
            this.PlatformTargetLabel,
            this.CursorPositionLabel});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 1034);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1303, 30);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ProjectLabel
            // 
            this.ProjectLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ProjectLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.ProjectLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ProjectLabel.Name = "ProjectLabel";
            this.ProjectLabel.Size = new System.Drawing.Size(142, 24);
            this.ProjectLabel.Text = "No Project Loaded";
            // 
            // PlatformTargetLabel
            // 
            this.PlatformTargetLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.PlatformTargetLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.PlatformTargetLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.PlatformTargetLabel.Name = "PlatformTargetLabel";
            this.PlatformTargetLabel.Size = new System.Drawing.Size(109, 24);
            this.PlatformTargetLabel.Text = "Target: NONE";
            // 
            // CursorPositionLabel
            // 
            this.CursorPositionLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CursorPositionLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.CursorPositionLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.CursorPositionLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CursorPositionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.CursorPositionLabel.Name = "CursorPositionLabel";
            this.CursorPositionLabel.Size = new System.Drawing.Size(148, 24);
            this.CursorPositionLabel.Text = "Line 00, Column 00";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1303, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator6,
            this.saveProjectToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator7,
            this.closeFileToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.newFileToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.newToolStripMenuItem.Text = "New";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Image = global::cc65WinForms.Properties.Resources._1541;
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.newProjectToolStripMenuItem.Text = "New Project";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Image = global::cc65WinForms.Properties.Resources.Disk;
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.newFileToolStripMenuItem.Text = "New File";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripButton_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectToolStripMenuItem,
            this.openFileToolStripMenuItem});
            this.openToolStripMenuItem.Image = global::cc65WinForms.Properties.Resources.openToolStripButton1;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(175, 6);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripButton_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.saveToolStripMenuItem.Text = "Save File";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(175, 6);
            // 
            // closeFileToolStripMenuItem
            // 
            this.closeFileToolStripMenuItem.Name = "closeFileToolStripMenuItem";
            this.closeFileToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.closeFileToolStripMenuItem.Text = "Close File";
            this.closeFileToolStripMenuItem.Click += new System.EventHandler(this.closeFileToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.toolStripSeparator3,
            this.saveToolStripButton,
            this.saveProjectToolStripButton,
            this.toolStripSeparator2,
            this.lbTargetPlatform,
            this.cbTargetPlatform,
            this.toolStripSeparator4,
            this.btBuildProject,
            this.btExecuteProject,
            this.toolStripSeparator5,
            this.btInvisibleChars,
            this.btHighlightCurrentLine,
            this.btShowFoldingLines});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1303, 28);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::cc65WinForms.Properties.Resources.newToolStripButton_Image;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(29, 25);
            this.newToolStripButton.Text = "newToolStripButton";
            this.newToolStripButton.ToolTipText = "New File";
            this.newToolStripButton.Click += new System.EventHandler(this.NewToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::cc65WinForms.Properties.Resources.openToolStripButton_Image;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(29, 25);
            this.openToolStripButton.Text = "toolStripButton1";
            this.openToolStripButton.ToolTipText = "Open File";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::cc65WinForms.Properties.Resources.Disk1;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(29, 25);
            this.saveToolStripButton.Text = "toolStripButton1";
            this.saveToolStripButton.ToolTipText = "Save File";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // saveProjectToolStripButton
            // 
            this.saveProjectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveProjectToolStripButton.Image = global::cc65WinForms.Properties.Resources._15411;
            this.saveProjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveProjectToolStripButton.Name = "saveProjectToolStripButton";
            this.saveProjectToolStripButton.Size = new System.Drawing.Size(29, 25);
            this.saveProjectToolStripButton.Text = "toolStripButton1";
            this.saveProjectToolStripButton.ToolTipText = "Save Project";
            this.saveProjectToolStripButton.Click += new System.EventHandler(this.saveProjectToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // lbTargetPlatform
            // 
            this.lbTargetPlatform.Name = "lbTargetPlatform";
            this.lbTargetPlatform.Size = new System.Drawing.Size(111, 25);
            this.lbTargetPlatform.Text = "Target Platform";
            // 
            // cbTargetPlatform
            // 
            this.cbTargetPlatform.Items.AddRange(new object[] {
            "C128",
            "C16",
            "C64",
            "PET",
            "Plus4",
            "VIC20"});
            this.cbTargetPlatform.Name = "cbTargetPlatform";
            this.cbTargetPlatform.Size = new System.Drawing.Size(121, 28);
            this.cbTargetPlatform.Sorted = true;
            this.cbTargetPlatform.SelectedIndexChanged += new System.EventHandler(this.cbTargetPlatform_SelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // btBuildProject
            // 
            this.btBuildProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btBuildProject.Image = global::cc65WinForms.Properties.Resources.work_process;
            this.btBuildProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btBuildProject.Name = "btBuildProject";
            this.btBuildProject.Size = new System.Drawing.Size(29, 25);
            this.btBuildProject.Text = "Build Project";
            this.btBuildProject.Click += new System.EventHandler(this.btBuildProject_Click);
            // 
            // btExecuteProject
            // 
            this.btExecuteProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btExecuteProject.Image = global::cc65WinForms.Properties.Resources.C64_icon;
            this.btExecuteProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExecuteProject.Name = "btExecuteProject";
            this.btExecuteProject.Size = new System.Drawing.Size(29, 25);
            this.btExecuteProject.Text = "Execute Project";
            this.btExecuteProject.Click += new System.EventHandler(this.btExecuteProject_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // btInvisibleChars
            // 
            this.btInvisibleChars.CheckOnClick = true;
            this.btInvisibleChars.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btInvisibleChars.Image = global::cc65WinForms.Properties.Resources.paragraph_16x16;
            this.btInvisibleChars.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btInvisibleChars.Name = "btInvisibleChars";
            this.btInvisibleChars.Size = new System.Drawing.Size(29, 25);
            this.btInvisibleChars.Text = "toolStripButton1";
            this.btInvisibleChars.ToolTipText = "Show invisible chars";
            this.btInvisibleChars.Click += new System.EventHandler(this.btInvisibleChars_Click);
            // 
            // btHighlightCurrentLine
            // 
            this.btHighlightCurrentLine.Checked = true;
            this.btHighlightCurrentLine.CheckOnClick = true;
            this.btHighlightCurrentLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btHighlightCurrentLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btHighlightCurrentLine.Image = global::cc65WinForms.Properties.Resources.edit_padding_top;
            this.btHighlightCurrentLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btHighlightCurrentLine.Name = "btHighlightCurrentLine";
            this.btHighlightCurrentLine.Size = new System.Drawing.Size(29, 25);
            this.btHighlightCurrentLine.Text = "toolStripButton1";
            this.btHighlightCurrentLine.ToolTipText = "Highlight current line";
            this.btHighlightCurrentLine.Click += new System.EventHandler(this.btHighlightCurrentLine_Click);
            // 
            // btShowFoldingLines
            // 
            this.btShowFoldingLines.Checked = true;
            this.btShowFoldingLines.CheckOnClick = true;
            this.btShowFoldingLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btShowFoldingLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btShowFoldingLines.Image = global::cc65WinForms.Properties.Resources.btShowFoldingLines_Image;
            this.btShowFoldingLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btShowFoldingLines.Name = "btShowFoldingLines";
            this.btShowFoldingLines.Size = new System.Drawing.Size(29, 25);
            this.btShowFoldingLines.Text = "toolStripButton1";
            this.btShowFoldingLines.ToolTipText = "Show folding lines";
            this.btShowFoldingLines.Click += new System.EventHandler(this.btShowFoldingLines_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvProjectFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tsFiles);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Size = new System.Drawing.Size(1301, 687);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 3;
            // 
            // tvProjectFiles
            // 
            this.tvProjectFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProjectFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvProjectFiles.Location = new System.Drawing.Point(0, 0);
            this.tvProjectFiles.Name = "tvProjectFiles";
            this.tvProjectFiles.Size = new System.Drawing.Size(248, 687);
            this.tvProjectFiles.TabIndex = 0;
            this.tvProjectFiles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProjectFiles_NodeMouseClick);
            // 
            // tsFiles
            // 
            this.tsFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsFiles.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tsFiles.Location = new System.Drawing.Point(1, 1);
            this.tsFiles.Name = "tsFiles";
            this.tsFiles.Size = new System.Drawing.Size(1047, 685);
            this.tsFiles.TabIndex = 0;
            this.tsFiles.Text = "faTabStrip1";
            // 
            // ofdMain
            // 
            this.ofdMain.DefaultExt = "c";
            this.ofdMain.Filter = "Source Files|*.c|Header Files|*.h";
            // 
            // sfdMain
            // 
            this.sfdMain.DefaultExt = "c";
            this.sfdMain.Filter = "Source Files|*.c|Header Files|*.h";
            // 
            // tmUpdateInterface
            // 
            this.tmUpdateInterface.Enabled = true;
            this.tmUpdateInterface.Interval = 400;
            this.tmUpdateInterface.Tick += new System.EventHandler(this.tmUpdateInterface_Tick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 56);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tsOutput);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.splitContainer2.Size = new System.Drawing.Size(1303, 978);
            this.splitContainer2.SplitterDistance = 689;
            this.splitContainer2.TabIndex = 4;
            // 
            // tsOutput
            // 
            this.tsOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsOutput.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tsOutput.Items.AddRange(new FarsiLibrary.Win.FATabStripItem[] {
            this.outputTSI,
            this.errorsTSI});
            this.tsOutput.Location = new System.Drawing.Point(1, 1);
            this.tsOutput.Name = "tsOutput";
            this.tsOutput.SelectedItem = this.outputTSI;
            this.tsOutput.Size = new System.Drawing.Size(1301, 283);
            this.tsOutput.TabIndex = 1;
            this.tsOutput.Text = "faTabStrip1";
            // 
            // outputTSI
            // 
            this.outputTSI.CanClose = false;
            this.outputTSI.Controls.Add(this.tbOutput);
            this.outputTSI.IsDrawn = true;
            this.outputTSI.Name = "outputTSI";
            this.outputTSI.Selected = true;
            this.outputTSI.Size = new System.Drawing.Size(1299, 262);
            this.outputTSI.TabIndex = 0;
            this.outputTSI.Title = "Output";
            // 
            // tbOutput
            // 
            this.tbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutput.Location = new System.Drawing.Point(0, 0);
            this.tbOutput.Margin = new System.Windows.Forms.Padding(5);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(1299, 262);
            this.tbOutput.TabIndex = 0;
            // 
            // errorsTSI
            // 
            this.errorsTSI.CanClose = false;
            this.errorsTSI.Controls.Add(this.errorsDataGridView);
            this.errorsTSI.IsDrawn = true;
            this.errorsTSI.Name = "errorsTSI";
            this.errorsTSI.Size = new System.Drawing.Size(1299, 263);
            this.errorsTSI.TabIndex = 1;
            this.errorsTSI.Title = "Error List";
            // 
            // errorsDataGridView
            // 
            this.errorsDataGridView.AllowUserToAddRows = false;
            this.errorsDataGridView.AllowUserToDeleteRows = false;
            this.errorsDataGridView.AllowUserToResizeRows = false;
            this.errorsDataGridView.AutoGenerateColumns = false;
            this.errorsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filenameDataGridViewTextBoxColumn,
            this.lineNumberDataGridViewTextBoxColumn,
            this.Type,
            this.errorDataGridViewTextBoxColumn});
            this.errorsDataGridView.DataSource = this.cc65ErrorBindingSource;
            this.errorsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.errorsDataGridView.MultiSelect = false;
            this.errorsDataGridView.Name = "errorsDataGridView";
            this.errorsDataGridView.ReadOnly = true;
            this.errorsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.errorsDataGridView.RowTemplate.Height = 24;
            this.errorsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.errorsDataGridView.Size = new System.Drawing.Size(1299, 263);
            this.errorsDataGridView.TabIndex = 1;
            this.errorsDataGridView.SelectionChanged += new System.EventHandler(this.errorsDataGridView_SelectionChanged);
            // 
            // filenameDataGridViewTextBoxColumn
            // 
            this.filenameDataGridViewTextBoxColumn.DataPropertyName = "Filename";
            this.filenameDataGridViewTextBoxColumn.HeaderText = "Filename";
            this.filenameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
            this.filenameDataGridViewTextBoxColumn.ReadOnly = true;
            this.filenameDataGridViewTextBoxColumn.Width = 115;
            // 
            // lineNumberDataGridViewTextBoxColumn
            // 
            this.lineNumberDataGridViewTextBoxColumn.DataPropertyName = "LineNumber";
            this.lineNumberDataGridViewTextBoxColumn.HeaderText = "LineNumber";
            this.lineNumberDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.lineNumberDataGridViewTextBoxColumn.Name = "lineNumberDataGridViewTextBoxColumn";
            this.lineNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.lineNumberDataGridViewTextBoxColumn.Width = 70;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 6;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 75;
            // 
            // errorDataGridViewTextBoxColumn
            // 
            this.errorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.errorDataGridViewTextBoxColumn.DataPropertyName = "Error";
            this.errorDataGridViewTextBoxColumn.HeaderText = "Error";
            this.errorDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.errorDataGridViewTextBoxColumn.Name = "errorDataGridViewTextBoxColumn";
            this.errorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cc65ErrorBindingSource
            // 
            this.cc65ErrorBindingSource.DataSource = typeof(cc65Wrapper.Cc65Error);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 1064);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cc65IDE";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsFiles)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsOutput)).EndInit();
            this.tsOutput.ResumeLayout(false);
            this.outputTSI.ResumeLayout(false);
            this.outputTSI.PerformLayout();
            this.errorsTSI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cc65ErrorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FarsiLibrary.Win.FATabStrip tsFiles;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton btHighlightCurrentLine;
        private System.Windows.Forms.ToolStripButton btShowFoldingLines;
        private System.Windows.Forms.ToolStripButton btInvisibleChars;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.OpenFileDialog ofdMain;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.SaveFileDialog sfdMain;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Timer tmUpdateInterface;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.ToolStripLabel lbTargetPlatform;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btBuildProject;
        private System.Windows.Forms.ToolStripButton btExecuteProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripComboBox cbTargetPlatform;
        private System.Windows.Forms.TreeView tvProjectFiles;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton saveProjectToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem closeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private FarsiLibrary.Win.FATabStrip tsOutput;
        private FarsiLibrary.Win.FATabStripItem outputTSI;
        private FarsiLibrary.Win.FATabStripItem errorsTSI;
        private System.Windows.Forms.DataGridView errorsDataGridView;
        private System.Windows.Forms.BindingSource cc65ErrorBindingSource;
        private System.Windows.Forms.ToolStripStatusLabel CursorPositionLabel;
        private System.Windows.Forms.ToolStripStatusLabel ProjectLabel;
        private System.Windows.Forms.ToolStripStatusLabel PlatformTargetLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn errorDataGridViewTextBoxColumn;
    }
}

