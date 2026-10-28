namespace Training_Center_Management_System
{
    partial class frmAppearance
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnBackground = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnFontAndSize = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.SuspendLayout();
            // 
            // btnBackground
            // 
            this.btnBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBackground.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackground.Location = new System.Drawing.Point(255, 63);
            this.btnBackground.Name = "btnBackground";
            this.btnBackground.Size = new System.Drawing.Size(244, 38);
            this.btnBackground.TabIndex = 1;
            this.btnBackground.Text = "change Interface background color";
            this.btnBackground.UseVisualStyleBackColor = false;
            this.btnBackground.Click += new System.EventHandler(this.btnBackground_Click);
            // 
            // btnColor
            // 
            this.btnColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnColor.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColor.Location = new System.Drawing.Point(255, 153);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(244, 38);
            this.btnColor.TabIndex = 2;
            this.btnColor.Text = "change text and heading color";
            this.btnColor.UseVisualStyleBackColor = false;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnFontAndSize
            // 
            this.btnFontAndSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnFontAndSize.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontAndSize.Location = new System.Drawing.Point(255, 243);
            this.btnFontAndSize.Name = "btnFontAndSize";
            this.btnFontAndSize.Size = new System.Drawing.Size(244, 38);
            this.btnFontAndSize.TabIndex = 3;
            this.btnFontAndSize.Text = "change font type and size";
            this.btnFontAndSize.UseVisualStyleBackColor = false;
            this.btnFontAndSize.Click += new System.EventHandler(this.btnFontAndSize_Click);
            // 
            // frmAppearance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFontAndSize);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnBackground);
            this.Name = "frmAppearance";
            this.Text = "Appearance";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnBackground;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnFontAndSize;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}