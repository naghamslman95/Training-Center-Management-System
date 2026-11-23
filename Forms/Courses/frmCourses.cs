using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmCourses : Form
    {
        private DataTable DTCourses;
        private int SelectedCourseID()
        {
            if (dgvCourses.CurrentRow == null)
                return -1;
            else
                return (int)dgvCourses.CurrentRow.Cells["CourseID"].Value;
        }
        public frmCourses()
        {
            InitializeComponent();
        }
        private void _RefreshCoursesList()
        {
            DTCourses = clsCourse.GetAllCourses();
            dgvCourses.DataSource = DTCourses;
            dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCourses.Columns["CreatedBy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
            cbFilter.Text = "None";
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvCourses.Rows.Count.ToString();
        }
        private void frmCourses_Load(object sender, EventArgs e)
        {
            _RefreshCoursesList();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTCourses.DefaultView.RowFilter = "";
            _UpdateRecordCount();

            txtFilter.Visible = cbFilter.Text != "None";
            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "None" || txtFilter.Text == "")
            {
                DTCourses.DefaultView.RowFilter = "";
                _UpdateRecordCount();
                return;
            }
            string FilterColumn = cbFilter.Text;
            if (cbFilter.Text == "CourseID" || cbFilter.Text == "InstructorID" || cbFilter.Text == "Duration"
                || cbFilter.Text == "TotalSeats")
            {
                DTCourses.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilter.Text.Trim()}";
            }
            else if (cbFilter.Text == "Price")
            {
                if (decimal.TryParse(txtFilter.Text.Trim(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal price))
                {
                    DTCourses.DefaultView.RowFilter = $"[{FilterColumn}] = {price.ToString(CultureInfo.InvariantCulture)}";
                }
                else
                {
                    DTCourses.DefaultView.RowFilter = "";
                }
            }
            else
            {
                string Value = txtFilter.Text.Trim().Replace("'", "''");
                DTCourses.DefaultView.RowFilter = $"[{FilterColumn}] Like '{Value}%'";
            }

            _UpdateRecordCount();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "CourseID" || cbFilter.Text == "InstructorID" || cbFilter.Text == "Duration"
                || cbFilter.Text == "TotalSeats")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (cbFilter.Text == "Price")
            {
                if (txtFilter.Text.Contains("."))
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                else
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditCourse frm = new frmAddEditCourse();
            frm.ShowDialog();
            _RefreshCoursesList();
        }

        private void addNewInstructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditCourse frm = new frmAddEditCourse();
            frm.ShowDialog();
            _RefreshCoursesList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCourseID() != -1)
            {
                frmAddEditCourse frm = new frmAddEditCourse(SelectedCourseID());
                if (frm.ShowDialog() == DialogResult.OK)
                    _RefreshCoursesList();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedCourseID() != -1)
            {
                frmCourseDetails frm = new frmCourseDetails(SelectedCourseID());
                frm.ShowDialog();
            }
        }

        private void dgvCourses_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedCourseID() != -1)
            {
                frmCourseDetails frm = new frmCourseDetails(SelectedCourseID());
                frm.ShowDialog();
            }
        }
    }
}
