namespace Training_Center_Management_System
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.studentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coursesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enrollmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerStudentInCourseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewEnrollmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructorReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.courseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appearanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filePathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblDatetime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlTimeAndUser = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.pnlTimeAndUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DarkCyan;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.studentsToolStripMenuItem,
            this.coursesToolStripMenuItem,
            this.instructorsToolStripMenuItem,
            this.enrollmentToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(126, 450);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // studentsToolStripMenuItem
            // 
            this.studentsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.studentsToolStripMenuItem.Name = "studentsToolStripMenuItem";
            this.studentsToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.studentsToolStripMenuItem.Text = "S&tudents";
            this.studentsToolStripMenuItem.Click += new System.EventHandler(this.studentsToolStripMenuItem_Click);
            // 
            // coursesToolStripMenuItem
            // 
            this.coursesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.coursesToolStripMenuItem.Name = "coursesToolStripMenuItem";
            this.coursesToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.coursesToolStripMenuItem.Text = "&Courses";
            this.coursesToolStripMenuItem.Click += new System.EventHandler(this.coursesToolStripMenuItem_Click);
            // 
            // instructorsToolStripMenuItem
            // 
            this.instructorsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.instructorsToolStripMenuItem.Name = "instructorsToolStripMenuItem";
            this.instructorsToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.instructorsToolStripMenuItem.Text = "I&nstructors";
            this.instructorsToolStripMenuItem.Click += new System.EventHandler(this.instructorsToolStripMenuItem_Click);
            // 
            // enrollmentToolStripMenuItem
            // 
            this.enrollmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerStudentInCourseToolStripMenuItem,
            this.viewEnrollmentToolStripMenuItem});
            this.enrollmentToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.enrollmentToolStripMenuItem.Name = "enrollmentToolStripMenuItem";
            this.enrollmentToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.enrollmentToolStripMenuItem.Text = "&Enrollment";
            // 
            // registerStudentInCourseToolStripMenuItem
            // 
            this.registerStudentInCourseToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.registerStudentInCourseToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.registerStudentInCourseToolStripMenuItem.Name = "registerStudentInCourseToolStripMenuItem";
            this.registerStudentInCourseToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.registerStudentInCourseToolStripMenuItem.Text = "Re&gister Student";
            this.registerStudentInCourseToolStripMenuItem.Click += new System.EventHandler(this.registerStudentInCourseToolStripMenuItem_Click);
            // 
            // viewEnrollmentToolStripMenuItem
            // 
            this.viewEnrollmentToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.viewEnrollmentToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.viewEnrollmentToolStripMenuItem.Name = "viewEnrollmentToolStripMenuItem";
            this.viewEnrollmentToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.viewEnrollmentToolStripMenuItem.Text = "&View Enrollment";
            this.viewEnrollmentToolStripMenuItem.Click += new System.EventHandler(this.viewEnrollmentToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monthlyReportToolStripMenuItem,
            this.instructorReportToolStripMenuItem,
            this.courseToolStripMenuItem});
            this.reportsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.reportsToolStripMenuItem.Text = "&Reports";
            // 
            // monthlyReportToolStripMenuItem
            // 
            this.monthlyReportToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.monthlyReportToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.monthlyReportToolStripMenuItem.Name = "monthlyReportToolStripMenuItem";
            this.monthlyReportToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.monthlyReportToolStripMenuItem.Text = "&Monthly Report";
            this.monthlyReportToolStripMenuItem.Click += new System.EventHandler(this.monthlyReportToolStripMenuItem_Click);
            // 
            // instructorReportToolStripMenuItem
            // 
            this.instructorReportToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.instructorReportToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.instructorReportToolStripMenuItem.Name = "instructorReportToolStripMenuItem";
            this.instructorReportToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.instructorReportToolStripMenuItem.Text = "I&nstructor Report";
            this.instructorReportToolStripMenuItem.Click += new System.EventHandler(this.instructorReportToolStripMenuItem_Click);
            // 
            // courseToolStripMenuItem
            // 
            this.courseToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.courseToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.courseToolStripMenuItem.Name = "courseToolStripMenuItem";
            this.courseToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.courseToolStripMenuItem.Text = "Co&urse Statics";
            this.courseToolStripMenuItem.Click += new System.EventHandler(this.courseToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appearanceToolStripMenuItem,
            this.filePathsToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.settingsToolStripMenuItem.Text = "Sett&ings";
            // 
            // appearanceToolStripMenuItem
            // 
            this.appearanceToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.appearanceToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.appearanceToolStripMenuItem.Name = "appearanceToolStripMenuItem";
            this.appearanceToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.appearanceToolStripMenuItem.Text = "A&ppearance ";
            this.appearanceToolStripMenuItem.Click += new System.EventHandler(this.appearanceToolStripMenuItem_Click);
            // 
            // filePathsToolStripMenuItem
            // 
            this.filePathsToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.filePathsToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.filePathsToolStripMenuItem.Name = "filePathsToolStripMenuItem";
            this.filePathsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.filePathsToolStripMenuItem.Text = "&File Paths";
            this.filePathsToolStripMenuItem.Click += new System.EventHandler(this.filePathsToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.BackColor = System.Drawing.Color.DarkCyan;
            this.languageToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.languageToolStripMenuItem.Text = "L&anguage";
            this.languageToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lblDatetime
            // 
            this.lblDatetime.AutoSize = true;
            this.lblDatetime.Location = new System.Drawing.Point(25, 428);
            this.lblDatetime.Name = "lblDatetime";
            this.lblDatetime.Size = new System.Drawing.Size(0, 13);
            this.lblDatetime.TabIndex = 6;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.DarkCyan;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblDate.Location = new System.Drawing.Point(25, 11);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(43, 17);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "label2";
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.DarkCyan;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblUser.Location = new System.Drawing.Point(579, 14);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(83, 17);
            this.lblUser.TabIndex = 8;
            this.lblUser.Text = "User: Admin";
            // 
            // pnlTimeAndUser
            // 
            this.pnlTimeAndUser.BackColor = System.Drawing.Color.DarkCyan;
            this.pnlTimeAndUser.Controls.Add(this.lblUser);
            this.pnlTimeAndUser.Controls.Add(this.lblDate);
            this.pnlTimeAndUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTimeAndUser.Location = new System.Drawing.Point(126, 414);
            this.pnlTimeAndUser.Name = "pnlTimeAndUser";
            this.pnlTimeAndUser.Size = new System.Drawing.Size(674, 36);
            this.pnlTimeAndUser.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Training_Center_Management_System.Properties.Resources.logo___;
            this.pictureBox1.Location = new System.Drawing.Point(0, 376);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.BackgroundImage = global::Training_Center_Management_System.Properties.Resources.backgroundTeal1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlTimeAndUser);
            this.Controls.Add(this.lblDatetime);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Training Center";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MdiChildActivate += new System.EventHandler(this.Form1_MdiChildActivate);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlTimeAndUser.ResumeLayout(false);
            this.pnlTimeAndUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem studentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coursesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enrollmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem registerStudentInCourseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewEnrollmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlyReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instructorReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem courseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appearanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filePathsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.Label lblDatetime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel pnlTimeAndUser;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem instructorsToolStripMenuItem;
    }
}

