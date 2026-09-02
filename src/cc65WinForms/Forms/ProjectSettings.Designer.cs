namespace cc65WinForms
{
    partial class ProjectSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSettings));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.outputPathTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButtom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.setWorkingDirButton = new System.Windows.Forms.Button();
            this.workingDirLabel = new System.Windows.Forms.Label();
            this.TargetPlatformComboBox = new System.Windows.Forms.ComboBox();
            this.optimiseCodeCheckBox = new System.Windows.Forms.CheckBox();
            this.versionTextBox = new System.Windows.Forms.TextBox();
            this.targetPlatformImageList = new System.Windows.Forms.ImageList(this.components);
            this.outputFileTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.outputPathTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.projectNameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TargetPlatformComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.optimiseCodeCheckBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.versionTextBox, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 333);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // outputPathTextBox
            // 
            this.outputPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputPathTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.outputPathTextBox.Location = new System.Drawing.Point(195, 253);
            this.outputPathTextBox.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.outputPathTextBox.Name = "outputPathTextBox";
            this.outputPathTextBox.ReadOnly = true;
            this.outputPathTextBox.Size = new System.Drawing.Size(400, 22);
            this.outputPathTextBox.TabIndex = 15;
            this.outputPathTextBox.Text = "<Not Set>";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.outputFileTextBox, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(198, 128);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(438, 34);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Controls.Add(this.okButtom);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(198, 288);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(438, 37);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(360, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 30);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButtom
            // 
            this.okButtom.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButtom.Location = new System.Drawing.Point(279, 3);
            this.okButtom.Name = "okButtom";
            this.okButtom.Size = new System.Drawing.Size(75, 30);
            this.okButtom.TabIndex = 1;
            this.okButtom.Text = "OK";
            this.okButtom.UseVisualStyleBackColor = true;
            this.okButtom.Click += new System.EventHandler(this.okButtom_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(20, 2, 2, 2);
            this.label1.Size = new System.Drawing.Size(184, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "Project Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(20, 2, 2, 2);
            this.label2.Size = new System.Drawing.Size(184, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "Working Directory";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 85);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(20, 2, 2, 2);
            this.label3.Size = new System.Drawing.Size(184, 40);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target Platform";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 125);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(20, 2, 2, 2);
            this.label4.Size = new System.Drawing.Size(184, 40);
            this.label4.TabIndex = 5;
            this.label4.Text = "Output File";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 165);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(20, 2, 2, 2);
            this.label5.Size = new System.Drawing.Size(184, 40);
            this.label5.TabIndex = 6;
            this.label5.Text = "Optimise Code";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 205);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(20, 2, 2, 2);
            this.label6.Size = new System.Drawing.Size(184, 40);
            this.label6.TabIndex = 7;
            this.label6.Text = "Version";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 245);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(20, 2, 2, 2);
            this.label7.Size = new System.Drawing.Size(184, 40);
            this.label7.TabIndex = 8;
            this.label7.Text = "Output File Path";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectNameTextBox.Location = new System.Drawing.Point(198, 13);
            this.projectNameTextBox.Margin = new System.Windows.Forms.Padding(3, 8, 250, 0);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(191, 22);
            this.projectNameTextBox.TabIndex = 9;
            this.projectNameTextBox.Text = "<Not Set>";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.setWorkingDirButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.workingDirLabel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(198, 48);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(438, 34);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // setWorkingDirButton
            // 
            this.setWorkingDirButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setWorkingDirButton.Location = new System.Drawing.Point(341, 3);
            this.setWorkingDirButton.Name = "setWorkingDirButton";
            this.setWorkingDirButton.Size = new System.Drawing.Size(94, 28);
            this.setWorkingDirButton.TabIndex = 0;
            this.setWorkingDirButton.Text = "Set Path";
            this.setWorkingDirButton.UseVisualStyleBackColor = true;
            this.setWorkingDirButton.Click += new System.EventHandler(this.setWorkingDirButton_Click);
            // 
            // workingDirLabel
            // 
            this.workingDirLabel.AutoSize = true;
            this.workingDirLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workingDirLabel.Location = new System.Drawing.Point(3, 0);
            this.workingDirLabel.Name = "workingDirLabel";
            this.workingDirLabel.Size = new System.Drawing.Size(332, 34);
            this.workingDirLabel.TabIndex = 1;
            this.workingDirLabel.Text = "<Not Set>";
            this.workingDirLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TargetPlatformComboBox
            // 
            this.TargetPlatformComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.TargetPlatformComboBox.FormattingEnabled = true;
            this.TargetPlatformComboBox.Location = new System.Drawing.Point(198, 93);
            this.TargetPlatformComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.TargetPlatformComboBox.Name = "TargetPlatformComboBox";
            this.TargetPlatformComboBox.Size = new System.Drawing.Size(90, 24);
            this.TargetPlatformComboBox.TabIndex = 11;
            // 
            // optimiseCodeCheckBox
            // 
            this.optimiseCodeCheckBox.AutoSize = true;
            this.optimiseCodeCheckBox.Checked = true;
            this.optimiseCodeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.optimiseCodeCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optimiseCodeCheckBox.Location = new System.Drawing.Point(198, 168);
            this.optimiseCodeCheckBox.Name = "optimiseCodeCheckBox";
            this.optimiseCodeCheckBox.Size = new System.Drawing.Size(438, 34);
            this.optimiseCodeCheckBox.TabIndex = 13;
            this.optimiseCodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // versionTextBox
            // 
            this.versionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.versionTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.versionTextBox.Location = new System.Drawing.Point(198, 213);
            this.versionTextBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.versionTextBox.Name = "versionTextBox";
            this.versionTextBox.ReadOnly = true;
            this.versionTextBox.Size = new System.Drawing.Size(100, 22);
            this.versionTextBox.TabIndex = 14;
            this.versionTextBox.Text = "1.0.0.0";
            // 
            // targetPlatformImageList
            // 
            this.targetPlatformImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("targetPlatformImageList.ImageStream")));
            this.targetPlatformImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.targetPlatformImageList.Images.SetKeyName(0, "C128.png");
            this.targetPlatformImageList.Images.SetKeyName(1, "C16.png");
            this.targetPlatformImageList.Images.SetKeyName(2, "C64.png");
            this.targetPlatformImageList.Images.SetKeyName(3, "Logo.png");
            this.targetPlatformImageList.Images.SetKeyName(4, "Plus4.png");
            this.targetPlatformImageList.Images.SetKeyName(5, "Vic20.png");
            // 
            // outputFileTextBox
            // 
            this.outputFileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputFileTextBox.Location = new System.Drawing.Point(3, 8);
            this.outputFileTextBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.outputFileTextBox.Name = "outputFileTextBox";
            this.outputFileTextBox.Size = new System.Drawing.Size(188, 22);
            this.outputFileTextBox.TabIndex = 2;
            this.outputFileTextBox.TextChanged += new System.EventHandler(this.outputFileTextBox_TextChanged);
            // 
            // ProjectSettings
            // 
            this.AcceptButton = this.okButtom;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(644, 333);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Settings";
            this.Load += new System.EventHandler(this.ProjectSettings_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButtom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button setWorkingDirButton;
        private System.Windows.Forms.Label workingDirLabel;
        private System.Windows.Forms.ImageList targetPlatformImageList;
        private System.Windows.Forms.ComboBox TargetPlatformComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox optimiseCodeCheckBox;
        private System.Windows.Forms.TextBox versionTextBox;
        private System.Windows.Forms.TextBox outputPathTextBox;
        private System.Windows.Forms.TextBox outputFileTextBox;
    }
}