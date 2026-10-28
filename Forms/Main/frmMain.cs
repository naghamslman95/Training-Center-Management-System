using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Training_Center_Management_System
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = Color.Teal; 
                }
            }
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.BackgroundImageLayout = ImageLayout.Stretch;
            Application.EnableVisualStyles();

        }


        private void OpenForm (Form formToShow)
        {
            var ExistingForm = this.MdiChildren.FirstOrDefault(f => f.GetType()== formToShow.GetType());
            if (ExistingForm != null)
                ExistingForm.Focus();
            else
            {
                formToShow.MdiParent = this;
                formToShow.Show();
            }
        }
 
        private void registerStudentInCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegisterStudent frm = new frmRegisterStudent();
            frm.ShowDialog();
        }

        private void viewEnrollmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmViewEnrollments());
        }

        private void monthlyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmMonthlyReport());
        }
       
        private void instructorReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm( new frmInstructorReport());
        }
      
        private void courseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm( new frmCourseStatics());
        }

        private void appearanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAppearance frm = new frmAppearance();
            frm.ShowDialog();
        }

        private void filePathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFilePaths frm = new frmFilePaths();
            frm.ShowDialog();
        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLanguages frm = new frmLanguages();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            lblDate.Text = DateTime.Now.ToString();
           

        }

       
        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild!= null)
            {
                pictureBox1.Visible = false;
                pnlTimeAndUser.Visible = false;
            }
            else
            {
                pictureBox1.Visible = true;
                pnlTimeAndUser.Visible = true;
            }
        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmCoursesList());
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmStudentsList());
        }

        private void instructorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmInstructorsList());
        }
    }
}
