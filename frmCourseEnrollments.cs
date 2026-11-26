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
    public partial class frmCourseEnrollments : Form
    {
        private int _CourseID = -1;
        private clsCourse _Course;
        private DataTable DTEnrollments;
        public frmCourseEnrollments(int CourseID)
        {
            InitializeComponent();
            _CourseID = CourseID;
        }

        private void _RefreshEnrollmentsList()
        {
            DTEnrollments = clsEnrollment.GetCourseEnrollmentsByCourseID(_CourseID);
            DTEnrollments.Columns.Add("Status", typeof(string),
                "IIF(EnrollmentStatus = 1, 'Active', IIF(EnrollmentStatus = 2, 'Cancelled', 'Completed'))");
            dgvCourseEnrollments.DataSource = DTEnrollments;
            dgvCourseEnrollments.Columns["EnrollmentStatus"].Visible = false;
            dgvCourseEnrollments.Columns["Status"].DisplayIndex = dgvCourseEnrollments.Columns["EnrollmentStatus"].DisplayIndex;
            dgvCourseEnrollments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCourseEnrollments.Columns["StudentName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvCourseEnrollments.Rows.Count.ToString();
        }

        private void frmCourseEnrollments_Load(object sender, EventArgs e)
        {
            _Course = clsCourse.FindByCourseID(_CourseID);
            if (_Course == null)
            {
                MessageBox.Show("Course is not found");
                return;
            }
            _RefreshEnrollmentsList();
            lblCourseTitle.Text = _Course.Title;
            if (dgvCourseEnrollments.RowCount == 0)
            {
                MessageBox.Show("Course you selected does not have enrollments");
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
