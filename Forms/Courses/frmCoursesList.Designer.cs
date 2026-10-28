namespace Training_Center_Management_System
{
    partial class frmCoursesList
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
            this.listCourses = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listCourses
            // 
            this.listCourses.BackColor = System.Drawing.Color.Teal;
            this.listCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCourses.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listCourses.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.listCourses.HideSelection = false;
            this.listCourses.Location = new System.Drawing.Point(0, 0);
            this.listCourses.Name = "listCourses";
            this.listCourses.Size = new System.Drawing.Size(800, 450);
            this.listCourses.TabIndex = 1;
            this.listCourses.UseCompatibleStateImageBehavior = false;
            this.listCourses.View = System.Windows.Forms.View.Details;
            this.listCourses.SelectedIndexChanged += new System.EventHandler(this.listCoursesList_SelectedIndexChanged);
            // 
            // frmCoursesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listCourses);
            this.Name = "frmCoursesList";
            this.Text = "Courses List";
            this.Load += new System.EventHandler(this.frmCoursesList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listCourses;
    }
}