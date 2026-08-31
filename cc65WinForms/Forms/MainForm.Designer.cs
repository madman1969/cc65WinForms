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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            ProjectLabel = new System.Windows.Forms.ToolStripStatusLabel();
            PlatformTargetLabel = new System.Windows.Forms.ToolStripStatusLabel();
            CursorPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            closeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            projectSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            newToolStripButton = new System.Windows.Forms.ToolStripButton();
            openToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            saveProjectToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            lbTargetPlatform = new System.Windows.Forms.ToolStripLabel();
            cbTargetPlatform = new System.Windows.Forms.ToolStripComboBox();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            btBuildProject = new System.Windows.Forms.ToolStripButton();
            btExecuteProject = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            btInvisibleChars = new System.Windows.Forms.ToolStripButton();
            btHighlightCurrentLine = new System.Windows.Forms.ToolStripButton();
            btShowFoldingLines = new System.Windows.Forms.ToolStripButton();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            tvProjectFiles = new System.Windows.Forms.TreeView();
            tsFiles = new FarsiLibrary.Win.FATabStrip();
            ofdMain = new System.Windows.Forms.OpenFileDialog();
            sfdMain = new System.Windows.Forms.SaveFileDialog();
            tmUpdateInterface = new System.Windows.Forms.Timer(components);
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            tsOutput = new FarsiLibrary.Win.FATabStrip();
            outputTSI = new FarsiLibrary.Win.FATabStripItem();
            tbOutput = new System.Windows.Forms.TextBox();
            errorsTSI = new FarsiLibrary.Win.FATabStripItem();
            errorsDataGridView = new System.Windows.Forms.DataGridView();
            filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lineNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            errorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            cc65ErrorBindingSource = new System.Windows.Forms.BindingSource(components);
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tsFiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tsOutput).BeginInit();
            tsOutput.SuspendLayout();
            outputTSI.SuspendLayout();
            errorsTSI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cc65ErrorBindingSource).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ProjectLabel, PlatformTargetLabel, CursorPositionLabel });
            statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStrip1.Location = new System.Drawing.Point(0, 974);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            statusStrip1.Size = new System.Drawing.Size(1140, 24);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // ProjectLabel
            // 
            ProjectLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            ProjectLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            ProjectLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            ProjectLabel.Name = "ProjectLabel";
            ProjectLabel.Size = new System.Drawing.Size(113, 19);
            ProjectLabel.Text = "No Project Loaded";
            // 
            // PlatformTargetLabel
            // 
            PlatformTargetLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            PlatformTargetLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            PlatformTargetLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            PlatformTargetLabel.Name = "PlatformTargetLabel";
            PlatformTargetLabel.Size = new System.Drawing.Size(86, 19);
            PlatformTargetLabel.Text = "Target: NONE";
            // 
            // CursorPositionLabel
            // 
            CursorPositionLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            CursorPositionLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            CursorPositionLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            CursorPositionLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            CursorPositionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            CursorPositionLabel.Name = "CursorPositionLabel";
            CursorPositionLabel.Size = new System.Drawing.Size(116, 19);
            CursorPositionLabel.Text = "Line 00, Column 00";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1140, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator6, saveProjectToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator7, closeFileToolStripMenuItem, closeProjectToolStripMenuItem, toolStripSeparator1, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newProjectToolStripMenuItem, newFileToolStripMenuItem });
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            newToolStripMenuItem.Text = "New";
            // 
            // newProjectToolStripMenuItem
            // 
            newProjectToolStripMenuItem.Image = Properties.Resources._1541;
            newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            newProjectToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            newProjectToolStripMenuItem.Text = "New Project";
            // 
            // newFileToolStripMenuItem
            // 
            newFileToolStripMenuItem.Image = Properties.Resources.Disk;
            newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            newFileToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            newFileToolStripMenuItem.Text = "New File";
            newFileToolStripMenuItem.Click += NewToolStripButton_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openProjectToolStripMenuItem, openFileToolStripMenuItem });
            openToolStripMenuItem.Image = Properties.Resources.openToolStripButton1;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // openProjectToolStripMenuItem
            // 
            openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            openProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            openProjectToolStripMenuItem.Text = "Open Project";
            openProjectToolStripMenuItem.Click += OpenProjectToolStripMenuItem_Click;
            // 
            // openFileToolStripMenuItem
            // 
            openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            openFileToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            openFileToolStripMenuItem.Text = "Open File";
            openFileToolStripMenuItem.Click += OpenToolStripButton_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new System.Drawing.Size(140, 6);
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            saveProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            saveProjectToolStripMenuItem.Text = "Save Project";
            saveProjectToolStripMenuItem.Click += SaveProjectToolStripButton_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            saveToolStripMenuItem.Text = "Save File";
            saveToolStripMenuItem.Click += SaveToolStripButton_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(140, 6);
            // 
            // closeFileToolStripMenuItem
            // 
            closeFileToolStripMenuItem.Name = "closeFileToolStripMenuItem";
            closeFileToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            closeFileToolStripMenuItem.Text = "Close File";
            closeFileToolStripMenuItem.Click += CloseFileToolStripMenuItem_Click;
            // 
            // closeProjectToolStripMenuItem
            // 
            closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            closeProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            closeProjectToolStripMenuItem.Text = "Close Project";
            closeProjectToolStripMenuItem.Click += CloseProjectToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += QuitToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { projectSettingsToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // projectSettingsToolStripMenuItem
            // 
            projectSettingsToolStripMenuItem.Name = "projectSettingsToolStripMenuItem";
            projectSettingsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            projectSettingsToolStripMenuItem.Text = "Project Settings";
            projectSettingsToolStripMenuItem.Click += ProjectSettingsToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, openToolStripButton, toolStripSeparator3, saveToolStripButton, saveProjectToolStripButton, toolStripSeparator2, lbTargetPlatform, cbTargetPlatform, toolStripSeparator4, btBuildProject, btExecuteProject, toolStripSeparator5, btInvisibleChars, btHighlightCurrentLine, btShowFoldingLines });
            toolStrip1.Location = new System.Drawing.Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(1140, 27);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = Properties.Resources.newToolStripButton_Image;
            newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Size = new System.Drawing.Size(24, 24);
            newToolStripButton.Text = "newToolStripButton";
            newToolStripButton.ToolTipText = "New File";
            newToolStripButton.Click += NewToolStripButton_Click;
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            openToolStripButton.Image = Properties.Resources.openToolStripButton_Image;
            openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new System.Drawing.Size(24, 24);
            openToolStripButton.Text = "toolStripButton1";
            openToolStripButton.ToolTipText = "Open File";
            openToolStripButton.Click += OpenToolStripButton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = Properties.Resources.Disk1;
            saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new System.Drawing.Size(24, 24);
            saveToolStripButton.Text = "toolStripButton1";
            saveToolStripButton.ToolTipText = "Save File";
            saveToolStripButton.Click += SaveToolStripButton_Click;
            // 
            // saveProjectToolStripButton
            // 
            saveProjectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            saveProjectToolStripButton.Image = Properties.Resources._15411;
            saveProjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            saveProjectToolStripButton.Name = "saveProjectToolStripButton";
            saveProjectToolStripButton.Size = new System.Drawing.Size(24, 24);
            saveProjectToolStripButton.Text = "toolStripButton1";
            saveProjectToolStripButton.ToolTipText = "Save Project";
            saveProjectToolStripButton.Click += SaveProjectToolStripButton_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // lbTargetPlatform
            // 
            lbTargetPlatform.Name = "lbTargetPlatform";
            lbTargetPlatform.Size = new System.Drawing.Size(89, 24);
            lbTargetPlatform.Text = "Target Platform";
            // 
            // cbTargetPlatform
            // 
            cbTargetPlatform.Items.AddRange(new object[] { "C128", "C16", "C64", "PET", "Plus4", "VIC20" });
            cbTargetPlatform.Name = "cbTargetPlatform";
            cbTargetPlatform.Size = new System.Drawing.Size(107, 27);
            cbTargetPlatform.Sorted = true;
            cbTargetPlatform.SelectedIndexChanged += CbTargetPlatform_SelectedIndexChanged;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // btBuildProject
            // 
            btBuildProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btBuildProject.Image = Properties.Resources.work_process;
            btBuildProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            btBuildProject.Name = "btBuildProject";
            btBuildProject.Size = new System.Drawing.Size(24, 24);
            btBuildProject.Text = "Build Project";
            btBuildProject.Click += BtBuildProject_ClickAsync;
            // 
            // btExecuteProject
            // 
            btExecuteProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btExecuteProject.Image = Properties.Resources.C64_icon;
            btExecuteProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            btExecuteProject.Name = "btExecuteProject";
            btExecuteProject.Size = new System.Drawing.Size(24, 24);
            btExecuteProject.Text = "Execute Project";
            btExecuteProject.Click += BtExecuteProject_ClickAsync;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // btInvisibleChars
            // 
            btInvisibleChars.CheckOnClick = true;
            btInvisibleChars.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btInvisibleChars.Image = Properties.Resources.paragraph_16x16;
            btInvisibleChars.ImageTransparentColor = System.Drawing.Color.Magenta;
            btInvisibleChars.Name = "btInvisibleChars";
            btInvisibleChars.Size = new System.Drawing.Size(24, 24);
            btInvisibleChars.Text = "toolStripButton1";
            btInvisibleChars.ToolTipText = "Show invisible chars";
            btInvisibleChars.Click += BtInvisibleChars_Click;
            // 
            // btHighlightCurrentLine
            // 
            btHighlightCurrentLine.Checked = true;
            btHighlightCurrentLine.CheckOnClick = true;
            btHighlightCurrentLine.CheckState = System.Windows.Forms.CheckState.Checked;
            btHighlightCurrentLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btHighlightCurrentLine.Image = Properties.Resources.edit_padding_top;
            btHighlightCurrentLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            btHighlightCurrentLine.Name = "btHighlightCurrentLine";
            btHighlightCurrentLine.Size = new System.Drawing.Size(24, 24);
            btHighlightCurrentLine.Text = "toolStripButton1";
            btHighlightCurrentLine.ToolTipText = "Highlight current line";
            btHighlightCurrentLine.Click += BtHighlightCurrentLine_Click;
            // 
            // btShowFoldingLines
            // 
            btShowFoldingLines.Checked = true;
            btShowFoldingLines.CheckOnClick = true;
            btShowFoldingLines.CheckState = System.Windows.Forms.CheckState.Checked;
            btShowFoldingLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btShowFoldingLines.Image = Properties.Resources.btShowFoldingLines_Image;
            btShowFoldingLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            btShowFoldingLines.Name = "btShowFoldingLines";
            btShowFoldingLines.Size = new System.Drawing.Size(24, 24);
            btShowFoldingLines.Text = "toolStripButton1";
            btShowFoldingLines.ToolTipText = "Show folding lines";
            btShowFoldingLines.Click += BtShowFoldingLines_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(1, 1);
            splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tvProjectFiles);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tsFiles);
            splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1);
            splitContainer1.Size = new System.Drawing.Size(1138, 647);
            splitContainer1.SplitterDistance = 200;
            splitContainer1.SplitterWidth = 3;
            splitContainer1.TabIndex = 3;
            // 
            // tvProjectFiles
            // 
            tvProjectFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            tvProjectFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tvProjectFiles.Location = new System.Drawing.Point(0, 0);
            tvProjectFiles.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            tvProjectFiles.Name = "tvProjectFiles";
            tvProjectFiles.Size = new System.Drawing.Size(198, 645);
            tvProjectFiles.TabIndex = 0;
            tvProjectFiles.NodeMouseClick += TvProjectFiles_NodeMouseClick;
            tvProjectFiles.MouseUp += TvProjectFiles_MouseUp;
            // 
            // tsFiles
            // 
            tsFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            tsFiles.Font = new System.Drawing.Font("Tahoma", 8.25F);
            tsFiles.Location = new System.Drawing.Point(1, 1);
            tsFiles.Margin = new System.Windows.Forms.Padding(2);
            tsFiles.Name = "tsFiles";
            tsFiles.Padding = new System.Windows.Forms.Padding(1, 20, 1, 1);
            tsFiles.Size = new System.Drawing.Size(931, 643);
            tsFiles.TabIndex = 0;
            tsFiles.Text = "faTabStrip1";
            // 
            // ofdMain
            // 
            ofdMain.DefaultExt = "c";
            ofdMain.Filter = "Source Files|*.c|Header Files|*.h";
            // 
            // sfdMain
            // 
            sfdMain.DefaultExt = "c";
            sfdMain.Filter = "Source Files|*.c|Header Files|*.h";
            // 
            // tmUpdateInterface
            // 
            tmUpdateInterface.Enabled = true;
            tmUpdateInterface.Interval = 400;
            tmUpdateInterface.Tick += TmUpdateInterface_Tick;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(0, 51);
            splitContainer2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer1);
            splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tsOutput);
            splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(1);
            splitContainer2.Size = new System.Drawing.Size(1140, 923);
            splitContainer2.SplitterDistance = 649;
            splitContainer2.TabIndex = 4;
            // 
            // tsOutput
            // 
            tsOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            tsOutput.Font = new System.Drawing.Font("Tahoma", 8.25F);
            tsOutput.Items.AddRange(new FarsiLibrary.Win.FATabStripItem[] { outputTSI, errorsTSI });
            tsOutput.Location = new System.Drawing.Point(1, 1);
            tsOutput.Margin = new System.Windows.Forms.Padding(2);
            tsOutput.Name = "tsOutput";
            tsOutput.Padding = new System.Windows.Forms.Padding(1, 20, 1, 1);
            tsOutput.SelectedItem = outputTSI;
            tsOutput.Size = new System.Drawing.Size(1138, 268);
            tsOutput.TabIndex = 1;
            tsOutput.Text = "faTabStrip1";
            // 
            // outputTSI
            // 
            outputTSI.CanClose = false;
            outputTSI.Controls.Add(tbOutput);
            outputTSI.Dock = System.Windows.Forms.DockStyle.Fill;
            outputTSI.IsDrawn = true;
            outputTSI.Location = new System.Drawing.Point(1, 20);
            outputTSI.Margin = new System.Windows.Forms.Padding(2);
            outputTSI.Name = "outputTSI";
            outputTSI.Selected = true;
            outputTSI.Size = new System.Drawing.Size(1136, 247);
            outputTSI.TabIndex = 0;
            outputTSI.Title = "Output";
            // 
            // tbOutput
            // 
            tbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            tbOutput.Location = new System.Drawing.Point(0, 0);
            tbOutput.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            tbOutput.Multiline = true;
            tbOutput.Name = "tbOutput";
            tbOutput.ReadOnly = true;
            tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tbOutput.Size = new System.Drawing.Size(1136, 247);
            tbOutput.TabIndex = 0;
            // 
            // errorsTSI
            // 
            errorsTSI.CanClose = false;
            errorsTSI.Controls.Add(errorsDataGridView);
            errorsTSI.Dock = System.Windows.Forms.DockStyle.Fill;
            errorsTSI.IsDrawn = true;
            errorsTSI.Location = new System.Drawing.Point(0, 0);
            errorsTSI.Margin = new System.Windows.Forms.Padding(2);
            errorsTSI.Name = "errorsTSI";
            errorsTSI.Size = new System.Drawing.Size(1136, 247);
            errorsTSI.TabIndex = 1;
            errorsTSI.Title = "Error List";
            // 
            // errorsDataGridView
            // 
            errorsDataGridView.AllowUserToAddRows = false;
            errorsDataGridView.AllowUserToDeleteRows = false;
            errorsDataGridView.AllowUserToResizeRows = false;
            errorsDataGridView.AutoGenerateColumns = false;
            errorsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            errorsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { filenameDataGridViewTextBoxColumn, lineNumberDataGridViewTextBoxColumn, Type, errorDataGridViewTextBoxColumn });
            errorsDataGridView.DataSource = cc65ErrorBindingSource;
            errorsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            errorsDataGridView.Location = new System.Drawing.Point(0, 0);
            errorsDataGridView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            errorsDataGridView.MultiSelect = false;
            errorsDataGridView.Name = "errorsDataGridView";
            errorsDataGridView.ReadOnly = true;
            errorsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            errorsDataGridView.RowTemplate.Height = 24;
            errorsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            errorsDataGridView.Size = new System.Drawing.Size(1136, 247);
            errorsDataGridView.TabIndex = 1;
            errorsDataGridView.SelectionChanged += ErrorsDataGridView_SelectionChanged;
            // 
            // filenameDataGridViewTextBoxColumn
            // 
            filenameDataGridViewTextBoxColumn.DataPropertyName = "Filename";
            filenameDataGridViewTextBoxColumn.HeaderText = "Filename";
            filenameDataGridViewTextBoxColumn.MinimumWidth = 6;
            filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
            filenameDataGridViewTextBoxColumn.ReadOnly = true;
            filenameDataGridViewTextBoxColumn.Width = 115;
            // 
            // lineNumberDataGridViewTextBoxColumn
            // 
            lineNumberDataGridViewTextBoxColumn.DataPropertyName = "LineNumber";
            lineNumberDataGridViewTextBoxColumn.HeaderText = "LineNumber";
            lineNumberDataGridViewTextBoxColumn.MinimumWidth = 6;
            lineNumberDataGridViewTextBoxColumn.Name = "lineNumberDataGridViewTextBoxColumn";
            lineNumberDataGridViewTextBoxColumn.ReadOnly = true;
            lineNumberDataGridViewTextBoxColumn.Width = 70;
            // 
            // Type
            // 
            Type.DataPropertyName = "Type";
            Type.HeaderText = "Type";
            Type.MinimumWidth = 6;
            Type.Name = "Type";
            Type.ReadOnly = true;
            Type.Width = 75;
            // 
            // errorDataGridViewTextBoxColumn
            // 
            errorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            errorDataGridViewTextBoxColumn.DataPropertyName = "Error";
            errorDataGridViewTextBoxColumn.HeaderText = "Error";
            errorDataGridViewTextBoxColumn.MinimumWidth = 6;
            errorDataGridViewTextBoxColumn.Name = "errorDataGridViewTextBoxColumn";
            errorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cc65ErrorBindingSource
            // 
            cc65ErrorBindingSource.DataSource = typeof(cc65Wrapper.Cc65Error);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1140, 998);
            Controls.Add(splitContainer2);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "cc65IDE";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tsFiles).EndInit();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tsOutput).EndInit();
            tsOutput.ResumeLayout(false);
            outputTSI.ResumeLayout(false);
            outputTSI.PerformLayout();
            errorsTSI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)cc65ErrorBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectSettingsToolStripMenuItem;
    }
}

