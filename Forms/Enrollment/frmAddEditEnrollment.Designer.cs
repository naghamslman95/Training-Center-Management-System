namespace Training_Center_Management_System
{
    partial class frmAddEditEnrollment
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
            this.nudTotalPrice = new System.Windows.Forms.NumericUpDown();
            this.cbCourses = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEnrollmentID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudPaidAmount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEnrollmentDate = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAddStudent = new System.Windows.Forms.LinkLabel();
            this.lblEnrollmentStatus = new System.Windows.Forms.Label();
            this.ucPersonInfoWithFilter1 = new Training_Center_Management_System.ucPersonInfoWithFilter();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaidAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // nudTotalPrice
            // 
            this.nudTotalPrice.DecimalPlaces = 2;
            this.nudTotalPrice.Location = new System.Drawing.Point(486, 61);
            this.nudTotalPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTotalPrice.Name = "nudTotalPrice";
            this.nudTotalPrice.Size = new System.Drawing.Size(120, 20);
            this.nudTotalPrice.TabIndex = 23;
            // 
            // cbCourses
            // 
            this.cbCourses.FormattingEnabled = true;
            this.cbCourses.Location = new System.Drawing.Point(127, 60);
            this.cbCourses.Name = "cbCourses";
            this.cbCourses.Size = new System.Drawing.Size(200, 21);
            this.cbCourses.TabIndex = 20;
            this.cbCourses.SelectedIndexChanged += new System.EventHandler(this.cbCourses_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(344, 153);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.CausesValidation = false;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(162, 153);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(355, 17);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 17);
            this.label12.TabIndex = 19;
            this.label12.Text = "Enrollment Status:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(355, 62);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 17);
            this.label11.TabIndex = 17;
            this.label11.Text = "Total Price:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 108);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Enrollment Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Course:";
            // 
            // lblEnrollmentID
            // 
            this.lblEnrollmentID.AutoSize = true;
            this.lblEnrollmentID.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollmentID.Location = new System.Drawing.Point(124, 16);
            this.lblEnrollmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnrollmentID.Name = "lblEnrollmentID";
            this.lblEnrollmentID.Size = new System.Drawing.Size(32, 17);
            this.lblEnrollmentID.TabIndex = 43;
            this.lblEnrollmentID.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 42;
            this.label1.Text = "Enrollment ID:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblEnrollmentStatus);
            this.panel1.Controls.Add(this.nudPaidAmount);
            this.panel1.Controls.Add(this.lblEnrollmentID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblEnrollmentDate);
            this.panel1.Controls.Add(this.nudTotalPrice);
            this.panel1.Controls.Add(this.cbCourses);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(15, 390);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 197);
            this.panel1.TabIndex = 44;
            // 
            // nudPaidAmount
            // 
            this.nudPaidAmount.DecimalPlaces = 2;
            this.nudPaidAmount.Location = new System.Drawing.Point(486, 105);
            this.nudPaidAmount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPaidAmount.Name = "nudPaidAmount";
            this.nudPaidAmount.Size = new System.Drawing.Size(120, 20);
            this.nudPaidAmount.TabIndex = 27;
            this.nudPaidAmount.Validating += new System.ComponentModel.CancelEventHandler(this.nudPaidAmount_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(355, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Paid Amount:";
            // 
            // lblEnrollmentDate
            // 
            this.lblEnrollmentDate.AutoSize = true;
            this.lblEnrollmentDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollmentDate.Location = new System.Drawing.Point(138, 108);
            this.lblEnrollmentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnrollmentDate.Name = "lblEnrollmentDate";
            this.lblEnrollmentDate.Size = new System.Drawing.Size(26, 17);
            this.lblEnrollmentDate.TabIndex = 24;
            this.lblEnrollmentDate.Text = "???";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(175, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 32);
            this.lblTitle.TabIndex = 41;
            this.lblTitle.Text = "Add New Enrollment";
            // 
            // lblAddStudent
            // 
            this.lblAddStudent.AutoSize = true;
            this.lblAddStudent.Location = new System.Drawing.Point(489, 327);
            this.lblAddStudent.Name = "lblAddStudent";
            this.lblAddStudent.Size = new System.Drawing.Size(91, 13);
            this.lblAddStudent.TabIndex = 46;
            this.lblAddStudent.TabStop = true;
            this.lblAddStudent.Text = "Add New Student";
            this.lblAddStudent.Visible = false;
            // 
            // lblEnrollmentStatus
            // 
            this.lblEnrollmentStatus.AutoSize = true;
            this.lblEnrollmentStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollmentStatus.Location = new System.Drawing.Point(483, 16);
            this.lblEnrollmentStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnrollmentStatus.Name = "lblEnrollmentStatus";
            this.lblEnrollmentStatus.Size = new System.Drawing.Size(45, 17);
            this.lblEnrollmentStatus.TabIndex = 44;
            this.lblEnrollmentStatus.Text = "Active";
            // 
            // ucPersonInfoWithFilter1
            // 
            this.ucPersonInfoWithFilter1.FilterEnabled = false;
            this.ucPersonInfoWithFilter1.Location = new System.Drawing.Point(15, 48);
            this.ucPersonInfoWithFilter1.Name = "ucPersonInfoWithFilter1";
            this.ucPersonInfoWithFilter1.ShowAddPerson = false;
            this.ucPersonInfoWithFilter1.Size = new System.Drawing.Size(625, 331);
            this.ucPersonInfoWithFilter1.TabIndex = 45;
            // 
            // frmAddEditEnrollment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(649, 599);
            this.Controls.Add(this.lblAddStudent);
            this.Controls.Add(this.ucPersonInfoWithFilter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmAddEditEnrollment";
            this.Text = "Add/Edit Enrollment";
            this.Load += new System.EventHandler(this.frmAddEditEnrollment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaidAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudTotalPrice;
        private System.Windows.Forms.ComboBox cbCourses;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEnrollmentID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.NumericUpDown nudPaidAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEnrollmentDate;
        private ucPersonInfoWithFilter ucPersonInfoWithFilter1;
        private System.Windows.Forms.LinkLabel lblAddStudent;
        private System.Windows.Forms.Label lblEnrollmentStatus;
    }
}