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
    public partial class frmInstructorCourses : Form
    {
        private int _InstructorID = -1;
        private clsInstructor _Instructor;
        private DataTable DTCourses;
        public frmInstructorCourses(int InstructorID)
        {
            InitializeComponent();
            _InstructorID = InstructorID;
        }

        private void _RefreshCoursesList()
        {
            DTCourses = clsCourse.GetInstructorCourses(_InstructorID);
            dgvInstructorCourses.DataSource = DTCourses;
            dgvInstructorCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvInstructorCourses.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvInstructorCourses.Rows.Count.ToString();
        }

        private void frmInstructorCourses_Load(object sender, EventArgs e)
        {
            _Instructor = clsInstructor.FindByInstructorID(_InstructorID);
            if (_Instructor == null)
            {
                MessageBox.Show("Insructor is not found");
                return;
            }
            _RefreshCoursesList();
            lblInstructorCourses.Text = _Instructor.PersonInfo.FullName;
            if (dgvInstructorCourses.RowCount == 0)
            {
                MessageBox.Show("Instructor you selected does not have courses");
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
