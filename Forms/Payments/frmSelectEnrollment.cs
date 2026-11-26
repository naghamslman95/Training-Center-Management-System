using System;
using System.Data;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmSelectEnrollment : Form
    {
        private clsStudent _Student;
        private DataTable DTEnrollments;
        private int SelectedEnrollmentID()
        {
            if (dgvStudentEnrollments.CurrentRow == null)
                return -1;
            else
                return (int)dgvStudentEnrollments.CurrentRow.Cells["EnrollmentID"].Value;
        }
        public frmSelectEnrollment()
        {
            InitializeComponent();
            ucPersonInfoWithFilter1.DataBack += PersonSelected;
            ucPersonInfoWithFilter1.ShowAddPerson = false;
        }
        private void _RefreshEnrollmentsList()
        {
            DTEnrollments = clsEnrollment.GetStudentEnrollmentsByStudentID(_Student.StudentID);
            DTEnrollments.Columns.Add("Status", typeof(string),
                "IIF(EnrollmentStatus = 1, 'Active', IIF(EnrollmentStatus = 2, 'Cancelled', 'Completed'))");
            dgvStudentEnrollments.DataSource = DTEnrollments;
            dgvStudentEnrollments.Columns["EnrollmentStatus"].Visible = false;
            dgvStudentEnrollments.Columns["Status"].DisplayIndex = dgvStudentEnrollments.Columns["EnrollmentStatus"].DisplayIndex;
            dgvStudentEnrollments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStudentEnrollments.Columns["CreatedBy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvStudentEnrollments.Rows.Count.ToString();
        }

        private void PersonSelected(object sender, int PersonID)
        {
            dgvStudentEnrollments.DataSource = null;
            if (PersonID == -1)
            {
                _Student = null;
                btnSelect.Enabled = false;
                return;
            }
            _Student = clsStudent.FindByPersonID(PersonID);
            if (_Student == null)
            {
                MessageBox.Show("Person you selected is not a student");
                btnSelect.Enabled = false;
                return;
            }
            _RefreshEnrollmentsList();
            if (dgvStudentEnrollments.RowCount == 0)
            {
                MessageBox.Show("Student you selected does not have enrollments");
                btnSelect.Enabled = false;
                return;
            }
            else
                btnSelect.Enabled = true;
        }
        private void _SelectEnrollment()
        {
            if (SelectedEnrollmentID() == -1) return;
            if (Convert.ToInt32(dgvStudentEnrollments.CurrentRow.Cells["EnrollmentStatus"].Value) == 2)
            {
                MessageBox.Show("Enrollment is cancelled. Choose another one");
                return;
            }
            frmAddEditPayment frm = new frmAddEditPayment();
            frm.EnrollmentID = SelectedEnrollmentID();
            if (frm.ShowDialog() == DialogResult.OK) this.DialogResult = DialogResult.OK;
        }
        private void dgvStudentEnrollments_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            _SelectEnrollment();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            _SelectEnrollment();
        }

        private void dgvStudentEnrollments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                dgvStudentEnrollments.CurrentCell = dgvStudentEnrollments.Rows[e.RowIndex].Cells["EnrollmentID"];
        }
    }
}
