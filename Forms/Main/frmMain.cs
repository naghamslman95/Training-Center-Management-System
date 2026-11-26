using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrainingCenter_Business;

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


        private void monthlyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsAdmin())
            {
                MessageBox.Show("Only admins can get to reports");
                return;
            }
            OpenForm(new frmMonthlyReport());
        }
       
        private void instructorReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsAdmin())
            {
                MessageBox.Show("Only admins can get to reports");
                return;
            }
            OpenForm( new frmInstructorReport());
        }
      
        private void courseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsAdmin())
            {
                MessageBox.Show("Only admins can get to reports");
                return;
            }
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
            usersToolStripMenuItem.Visible = (clsGlobal.CurrentUser.Role == clsUser.enRole.Admin);
            reportsToolStripMenuItem.Visible = (clsGlobal.CurrentUser.Role == clsUser.enRole.Admin);
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblUser.Text = clsGlobal.CurrentUser.UserName;
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
            frmCourses frm = new frmCourses();
            frm.ShowDialog();
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentsList frm = new frmStudentsList();
            frm.ShowDialog();
        }

        private void instructorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInstructors frm = new frmInstructors();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeople frm = new frmPeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsAdmin())
            {
                MessageBox.Show("Only admins can get to users list");
                return;
            }
            frmUsers frm = new frmUsers();
            frm.ShowDialog();
        }

        private void enrollmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEnrollments frm = new frmEnrollments();
            frm.ShowDialog();
        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPayments frm = new frmPayments();
            frm.ShowDialog();
        }
        
        private bool _IsAdmin()
        {
            return (clsGlobal.CurrentUser.Role == clsUser.enRole.Admin);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
