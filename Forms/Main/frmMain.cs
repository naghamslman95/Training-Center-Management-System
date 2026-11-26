using System;
using System.Drawing;
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
        private void monthlyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsAdmin())
            {
                MessageBox.Show("Only admins can access reports");
                return;
            }
            frmMonthlyReport frm = new frmMonthlyReport();
            frm.ShowDialog();
        }
       
        private void instructorReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsAdmin())
            {
                MessageBox.Show("Only admins can access reports");
                return;
            }
            frmInstructorReport frm = new frmInstructorReport();
            frm.ShowDialog();
        }
      
        private void courseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsAdmin())
            {
                MessageBox.Show("Only admins can access reports");
                return;
            }
            frmCourseStatics frm = new frmCourseStatics();
            frm.ShowDialog();
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
