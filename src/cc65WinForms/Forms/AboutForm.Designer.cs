namespace cc65WinForms
{
    partial class AboutForm
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
            iconPictureBox = new System.Windows.Forms.PictureBox();
            titleLabel = new System.Windows.Forms.Label();
            versionLabel = new System.Windows.Forms.Label();
            copyrightLabel = new System.Windows.Forms.Label();
            logPathCaptionLabel = new System.Windows.Forms.Label();
            logLinkLabel = new System.Windows.Forms.LinkLabel();
            okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).BeginInit();
            SuspendLayout();
            //
            // iconPictureBox
            //
            iconPictureBox.Image = System.Drawing.SystemIcons.Application.ToBitmap();
            iconPictureBox.Location = new System.Drawing.Point(16, 16);
            iconPictureBox.Name = "iconPictureBox";
            iconPictureBox.Size = new System.Drawing.Size(32, 32);
            iconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            iconPictureBox.TabIndex = 0;
            iconPictureBox.TabStop = false;
            //
            // titleLabel
            //
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            titleLabel.Location = new System.Drawing.Point(60, 16);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(300, 28);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "cc65 IDE";
            //
            // versionLabel
            //
            versionLabel.AutoSize = true;
            versionLabel.Location = new System.Drawing.Point(61, 50);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new System.Drawing.Size(97, 15);
            versionLabel.TabIndex = 2;
            versionLabel.Text = "Version 1.0.0.0";
            //
            // copyrightLabel
            //
            copyrightLabel.AutoSize = true;
            copyrightLabel.Location = new System.Drawing.Point(61, 70);
            copyrightLabel.Name = "copyrightLabel";
            copyrightLabel.Size = new System.Drawing.Size(115, 15);
            copyrightLabel.TabIndex = 3;
            copyrightLabel.Text = "Copyright © 2026";
            //
            // logPathCaptionLabel
            //
            logPathCaptionLabel.AutoSize = true;
            logPathCaptionLabel.Location = new System.Drawing.Point(16, 112);
            logPathCaptionLabel.Name = "logPathCaptionLabel";
            logPathCaptionLabel.Size = new System.Drawing.Size(53, 15);
            logPathCaptionLabel.TabIndex = 4;
            logPathCaptionLabel.Text = "Log file:";
            //
            // logLinkLabel
            //
            logLinkLabel.AutoEllipsis = true;
            logLinkLabel.Location = new System.Drawing.Point(16, 132);
            logLinkLabel.Name = "logLinkLabel";
            logLinkLabel.Size = new System.Drawing.Size(344, 20);
            logLinkLabel.TabIndex = 5;
            logLinkLabel.TabStop = true;
            logLinkLabel.Text = "<Not Set>";
            logLinkLabel.LinkClicked += LogLinkLabel_LinkClicked;
            //
            // okButton
            //
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Location = new System.Drawing.Point(285, 168);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 28);
            okButton.TabIndex = 6;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            //
            // AboutForm
            //
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = okButton;
            ClientSize = new System.Drawing.Size(376, 208);
            Controls.Add(okButton);
            Controls.Add(logLinkLabel);
            Controls.Add(logPathCaptionLabel);
            Controls.Add(copyrightLabel);
            Controls.Add(versionLabel);
            Controls.Add(titleLabel);
            Controls.Add(iconPictureBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "About cc65 IDE";
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.Label logPathCaptionLabel;
        private System.Windows.Forms.LinkLabel logLinkLabel;
        private System.Windows.Forms.Button okButton;
    }
}
