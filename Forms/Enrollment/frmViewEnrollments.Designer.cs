namespace Training_Center_Management_System
{
    partial class frmViewEnrollments
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
            this.listCoursesList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listCoursesList
            // 
            this.listCoursesList.BackColor = System.Drawing.Color.Teal;
            this.listCoursesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCoursesList.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listCoursesList.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.listCoursesList.FullRowSelect = true;
            this.listCoursesList.HideSelection = false;
            this.listCoursesList.Location = new System.Drawing.Point(0, 0);
            this.listCoursesList.Name = "listCoursesList";
            this.listCoursesList.Size = new System.Drawing.Size(800, 450);
            this.listCoursesList.TabIndex = 0;
            this.listCoursesList.UseCompatibleStateImageBehavior = false;
            this.listCoursesList.View = System.Windows.Forms.View.Details;
            this.listCoursesList.ItemActivate += new System.EventHandler(this.listCoursesList_ItemActivate);
            // 
            // frmViewEnrollments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listCoursesList);
            this.Name = "frmViewEnrollments";
            this.Text = "View Enrollments";
            this.Load += new System.EventHandler(this.frmViewEnrollments_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listCoursesList;
    }
}