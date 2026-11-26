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
    public partial class frmStudentsList : Form
    {
        private DataTable DTStudents;
        private int SelectedStudentID()
        {
            if (dgvStudents.CurrentRow == null)
                return -1;
            else
                return (int)dgvStudents.CurrentRow.Cells[0].Value;
        }
        public frmStudentsList()
        {
            InitializeComponent();
        }
        private void _RefreshStudentsList()
        {
            DTStudents = clsStudent.GetAllStudents();
            DTStudents.Columns.Add("StatusText", typeof(string), "IIF(Status = 1, 'Active', 'InActive')");
            dgvStudents.DataSource = DTStudents;
            dgvStudents.Columns["Status"].Visible = false;
            dgvStudents.Columns["StatusText"].HeaderText = "Status";
            dgvStudents.Columns["StatusText"].DisplayIndex = dgvStudents.Columns["Status"].DisplayIndex;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStudents.Columns["CreatedBy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
            cbFilter.Text = "None";
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvStudents.Rows.Count.ToString();
        }
        private void frmStudentsList_Load(object sender, EventArgs e)
        {
            _RefreshStudentsList();
            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Status")
            {
                txtFilter.Visible = false;
                cbStatusFilter.Visible = true;
                cbStatusFilter.Focus();
                cbStatusFilter.SelectedIndex = 0;
            }
            else
            {
                txtFilter.Visible = cbFilter.Text != "None";
                cbStatusFilter.Visible = false;

                if (txtFilter.Visible)
                {
                    txtFilter.Text = "";
                    txtFilter.Focus();
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "None" || txtFilter.Text == "")
            {
                DTStudents.DefaultView.RowFilter = "";
                _UpdateRecordCount();
                return;
            }
            string FilterColumn = cbFilter.Text;
            if (cbFilter.Text == "StudentID" || cbFilter.Text == "PersonID")
            {
                DTStudents.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilter.Text.Trim()}";
            }

            else
            {
                string Value = txtFilter.Text.Trim().Replace("'", "''");
                DTStudents.DefaultView.RowFilter = $"[{FilterColumn}] Like '{Value}%'";
            }

            _UpdateRecordCount();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "StudentID" || cbFilter.Text == "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Status";
            string FilterValue = cbStatusFilter.Text;
            switch (FilterValue)
            {
                case "All":
                    break;
                case "Active":
                    FilterValue = "1";
                    break;
                case "InActive":
                    FilterValue = "2";
                    break;
            }
            if (FilterValue == "All")
                DTStudents.DefaultView.RowFilter = "";
            else
                DTStudents.DefaultView.RowFilter = $"[{FilterColumn}] = {FilterValue}";
            _UpdateRecordCount();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditStudent frm = new frmAddEditStudent();
            if (frm.ShowDialog() == DialogResult.OK)
                _RefreshStudentsList();
        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditStudent frm = new frmAddEditStudent();
            if (frm.ShowDialog() == DialogResult.OK)
                _RefreshStudentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedStudentID() != -1)
            {
                frmAddEditStudent frm = new frmAddEditStudent(SelectedStudentID());
                if (frm.ShowDialog() == DialogResult.OK)
                    _RefreshStudentsList();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedStudentID() != -1)
            {
                frmStudentDetails frm = new frmStudentDetails(SelectedStudentID());
                frm.ShowDialog();
            }
        }

        private void dgvStudents_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedStudentID() != -1)
            {
                frmStudentDetails frm = new frmStudentDetails(SelectedStudentID());
                frm.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int StudentID = SelectedStudentID();
            if (StudentID != -1)
            {
                if (clsStudent.HasEnrollments(StudentID))
                {
                    MessageBox.Show("Can not delete this student because there are enrollments linked to it");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to delete Student [" + StudentID + "]?",
            "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsStudent.DeleteStudent(StudentID))
                    {
                        MessageBox.Show("Student is deleted successfully");
                        _RefreshStudentsList();
                    }
                    else
                        MessageBox.Show("Failed to delete student");
                }
            }
        }

        private void dgvStudents_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                dgvStudents.CurrentCell = dgvStudents.Rows[e.RowIndex].Cells["StudentID"];
        }

        private void ShowEnrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedStudentID() != -1)
            {
                frmStudentEnrollments frm = new frmStudentEnrollments(SelectedStudentID());
                frm.ShowDialog();
            }
        }
    }
}
