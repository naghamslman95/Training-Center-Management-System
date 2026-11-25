namespace Training_Center_Management_System
{
    partial class frmPaymentDetails
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbPaymentInfo = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblPaymentDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPaymentAmount = new System.Windows.Forms.Label();
            this.lblPaymentID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucEnrollmentInfo1 = new Training_Center_Management_System.ucEnrollmentInfo();
            this.gbPaymentInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.CausesValidation = false;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(277, 408);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 44;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(186, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(198, 32);
            this.lblTitle.TabIndex = 43;
            this.lblTitle.Text = "Payment Details";
            // 
            // gbPaymentInfo
            // 
            this.gbPaymentInfo.Controls.Add(this.lblCreatedBy);
            this.gbPaymentInfo.Controls.Add(this.lblPaymentDate);
            this.gbPaymentInfo.Controls.Add(this.label5);
            this.gbPaymentInfo.Controls.Add(this.lblPaymentAmount);
            this.gbPaymentInfo.Controls.Add(this.lblPaymentID);
            this.gbPaymentInfo.Controls.Add(this.label3);
            this.gbPaymentInfo.Controls.Add(this.label2);
            this.gbPaymentInfo.Controls.Add(this.label1);
            this.gbPaymentInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPaymentInfo.Location = new System.Drawing.Point(14, 319);
            this.gbPaymentInfo.Margin = new System.Windows.Forms.Padding(5);
            this.gbPaymentInfo.Name = "gbPaymentInfo";
            this.gbPaymentInfo.Padding = new System.Windows.Forms.Padding(5);
            this.gbPaymentInfo.Size = new System.Drawing.Size(620, 66);
            this.gbPaymentInfo.TabIndex = 46;
            this.gbPaymentInfo.TabStop = false;
            this.gbPaymentInfo.Text = "Payment Information";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCreatedBy.Location = new System.Drawing.Point(556, 38);
            this.lblCreatedBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(32, 15);
            this.lblCreatedBy.TabIndex = 15;
            this.lblCreatedBy.Text = "?????";
            // 
            // lblPaymentDate
            // 
            this.lblPaymentDate.AutoSize = true;
            this.lblPaymentDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentDate.Location = new System.Drawing.Point(393, 38);
            this.lblPaymentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaymentDate.Name = "lblPaymentDate";
            this.lblPaymentDate.Size = new System.Drawing.Size(32, 15);
            this.lblPaymentDate.TabIndex = 14;
            this.lblPaymentDate.Text = "?????";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(299, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Payment Date:";
            // 
            // lblPaymentAmount
            // 
            this.lblPaymentAmount.AutoSize = true;
            this.lblPaymentAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentAmount.Location = new System.Drawing.Point(245, 38);
            this.lblPaymentAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new System.Drawing.Size(32, 15);
            this.lblPaymentAmount.TabIndex = 11;
            this.lblPaymentAmount.Text = "?????";
            // 
            // lblPaymentID
            // 
            this.lblPaymentID.AutoSize = true;
            this.lblPaymentID.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentID.Location = new System.Drawing.Point(92, 38);
            this.lblPaymentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaymentID.Name = "lblPaymentID";
            this.lblPaymentID.Size = new System.Drawing.Size(32, 15);
            this.lblPaymentID.TabIndex = 8;
            this.lblPaymentID.Text = "?????";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(481, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Created By:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(138, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Payment Amount:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Payment ID:";
            // 
            // ucEnrollmentInfo1
            // 
            this.ucEnrollmentInfo1.Location = new System.Drawing.Point(12, 53);
            this.ucEnrollmentInfo1.Name = "ucEnrollmentInfo1";
            this.ucEnrollmentInfo1.Size = new System.Drawing.Size(625, 270);
            this.ucEnrollmentInfo1.TabIndex = 45;
            // 
            // frmPaymentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 450);
            this.Controls.Add(this.gbPaymentInfo);
            this.Controls.Add(this.ucEnrollmentInfo1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmPaymentDetails";
            this.Text = "Payment Details";
            this.Load += new System.EventHandler(this.frmPaymentDetails_Load);
            this.gbPaymentInfo.ResumeLayout(false);
            this.gbPaymentInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private ucEnrollmentInfo ucEnrollmentInfo1;
        private System.Windows.Forms.GroupBox gbPaymentInfo;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblPaymentDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPaymentAmount;
        private System.Windows.Forms.Label lblPaymentID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}