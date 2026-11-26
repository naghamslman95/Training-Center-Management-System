using System;
using System.Data;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmStudentEnrollments : Form
    {
        private int _StudentID = -1;
        private clsStudent _Student;
        private DataTable DTEnrollments;
        public frmStudentEnrollments(int StudentID)
        {
            InitializeComponent();
            _StudentID = StudentID;
        }
        private void _RefreshEnrollmentsList()
        {
            DTEnrollments = clsEnrollment.GetStudentEnrollmentsByStudentID(_StudentID);
            DTEnrollments.Columns.Add("Status", typeof(string),
                "IIF(EnrollmentStatus = 1, 'Active', IIF(EnrollmentStatus = 2, 'Cancelled', 'Completed'))");
            dgvStudentEnrollments.DataSource = DTEnrollments;
            dgvStudentEnrollments.Columns["EnrollmentStatus"].Visible = false;
            dgvStudentEnrollments.Columns["Status"].DisplayIndex = dgvStudentEnrollments.Columns["EnrollmentStatus"].DisplayIndex;
            dgvStudentEnrollments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStudentEnrollments.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvStudentEnrollments.Rows.Count.ToString();
        }

        private void frmStudentEnrollments_Load(object sender, EventArgs e)
        {
            _Student = clsStudent.FindByStudentID(_StudentID);
            if (_Student == null)
            {
                MessageBox.Show("Student is not found");
                return;
            }
            _RefreshEnrollmentsList();
            lblStudentName.Text = _Student.PersonInfo.FullName;
            if (dgvStudentEnrollments.RowCount == 0)
            {
                MessageBox.Show("Student you selected does not have enrollments");
                return;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
