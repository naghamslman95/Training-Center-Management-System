using System;
using System.Windows.Forms;

namespace Training_Center_Management_System
{
    public partial class frmStudentDetails : Form
    {
        private int _StudentID;
        public frmStudentDetails(int StudentID)
        {
            InitializeComponent();
            _StudentID = StudentID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmStudentDetails_Load(object sender, EventArgs e)
        {
            if (!ucStudentInfo1.LoadStudentInfo(_StudentID))
            {
                MessageBox.Show("Student is not found!");
            }
        }
    }
}
