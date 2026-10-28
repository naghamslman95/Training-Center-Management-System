namespace Training_Center_Management_System
{
    partial class frmFilePaths
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
            this.txtSaveFiles = new System.Windows.Forms.TextBox();
            this.txtBackupFiles = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSaveFiles = new System.Windows.Forms.Button();
            this.btnBackupFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSaveFiles
            // 
            this.txtSaveFiles.Location = new System.Drawing.Point(358, 88);
            this.txtSaveFiles.Name = "txtSaveFiles";
            this.txtSaveFiles.ReadOnly = true;
            this.txtSaveFiles.Size = new System.Drawing.Size(250, 20);
            this.txtSaveFiles.TabIndex = 2;
            // 
            // txtBackupFiles
            // 
            this.txtBackupFiles.Location = new System.Drawing.Point(358, 169);
            this.txtBackupFiles.Name = "txtBackupFiles";
            this.txtBackupFiles.ReadOnly = true;
            this.txtBackupFiles.Size = new System.Drawing.Size(250, 20);
            this.txtBackupFiles.TabIndex = 4;
            // 
            // btnSaveFiles
            // 
            this.btnSaveFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSaveFiles.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnSaveFiles.Location = new System.Drawing.Point(83, 83);
            this.btnSaveFiles.Name = "btnSaveFiles";
            this.btnSaveFiles.Size = new System.Drawing.Size(219, 28);
            this.btnSaveFiles.TabIndex = 1;
            this.btnSaveFiles.Text = "change saved files location";
            this.btnSaveFiles.UseVisualStyleBackColor = false;
            this.btnSaveFiles.Click += new System.EventHandler(this.btnSaveFiles_Click);
            // 
            // btnBackupFiles
            // 
            this.btnBackupFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBackupFiles.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnBackupFiles.Location = new System.Drawing.Point(83, 164);
            this.btnBackupFiles.Name = "btnBackupFiles";
            this.btnBackupFiles.Size = new System.Drawing.Size(219, 28);
            this.btnBackupFiles.TabIndex = 3;
            this.btnBackupFiles.Text = "change backedup files location";
            this.btnBackupFiles.UseVisualStyleBackColor = false;
            this.btnBackupFiles.Click += new System.EventHandler(this.btnBackupFiles_Click);
            // 
            // frmFilePaths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBackupFiles);
            this.Controls.Add(this.btnSaveFiles);
            this.Controls.Add(this.txtBackupFiles);
            this.Controls.Add(this.txtSaveFiles);
            this.Name = "frmFilePaths";
            this.Text = "File Paths";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSaveFiles;
        private System.Windows.Forms.TextBox txtBackupFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSaveFiles;
        private System.Windows.Forms.Button btnBackupFiles;
    }
}