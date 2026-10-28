namespace Training_Center_Management_System
{
    partial class frmStudentsList
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
            this.listStudentsList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listStudentsList
            // 
            this.listStudentsList.BackColor = System.Drawing.Color.Teal;
            this.listStudentsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listStudentsList.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listStudentsList.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.listStudentsList.HideSelection = false;
            this.listStudentsList.Location = new System.Drawing.Point(0, 0);
            this.listStudentsList.Name = "listStudentsList";
            this.listStudentsList.Size = new System.Drawing.Size(825, 450);
            this.listStudentsList.TabIndex = 0;
            this.listStudentsList.UseCompatibleStateImageBehavior = false;
            this.listStudentsList.View = System.Windows.Forms.View.Details;
            // 
            // frmStudentsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 450);
            this.Controls.Add(this.listStudentsList);
            this.Name = "frmStudentsList";
            this.Text = "Students List";
            this.Load += new System.EventHandler(this.frmStudentsList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listStudentsList;
    }
}