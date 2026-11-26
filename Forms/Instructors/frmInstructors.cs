using System;
using System.Data;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmInstructors : Form
    {
        private DataTable DTInstructors;
        private int SelectedInstructorID()
        {
            if (dgvInstructors.CurrentRow == null)
                return -1;
            else
                return (int)dgvInstructors.CurrentRow.Cells["InstructorID"].Value;
        }
        public frmInstructors()
        {
            InitializeComponent();
        }
        private void _RefreshInstructorsList()
        {
            DTInstructors = clsInstructor.GetAllInstructors();
            dgvInstructors.DataSource = DTInstructors;
            dgvInstructors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvInstructors.Columns["CreatedBy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
            cbFilter.Text = "None";
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvInstructors.Rows.Count.ToString();
        }
        private void frmInstructors_Load(object sender, EventArgs e)
        {
            _RefreshInstructorsList();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTInstructors.DefaultView.RowFilter = "";
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
                DTInstructors.DefaultView.RowFilter = "";
                _UpdateRecordCount();
                return;
            }
            string FilterColumn = cbFilter.Text;
            if (cbFilter.Text == "InstructorID" || cbFilter.Text == "PersonID")
            {
                DTInstructors.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilter.Text.Trim()}";
            }

            else
            {
                string Value = txtFilter.Text.Trim().Replace("'", "''");
                DTInstructors.DefaultView.RowFilter = $"[{FilterColumn}] Like '{Value}%'";
            }

            _UpdateRecordCount();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "InstructorID" || cbFilter.Text == "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditInstructor frm = new frmAddEditInstructor();
            frm.ShowDialog();
            _RefreshInstructorsList();
        }

        private void addNewInstructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditInstructor frm = new frmAddEditInstructor();
            frm.ShowDialog();
            _RefreshInstructorsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedInstructorID() != -1)
            {
                frmAddEditInstructor frm = new frmAddEditInstructor(SelectedInstructorID());
                if (frm.ShowDialog() == DialogResult.OK)
                    _RefreshInstructorsList();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedInstructorID() != -1)
            {
                frmInstructorDetails frm = new frmInstructorDetails(SelectedInstructorID());
                frm.ShowDialog();
            }
        }

        private void dgvInstructors_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedInstructorID() != -1)
            {
                frmInstructorDetails frm = new frmInstructorDetails(SelectedInstructorID());
                frm.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InstructorID = SelectedInstructorID();
            if (InstructorID != -1)
            {
                if (clsInstructor.HasCourses(InstructorID))
                {
                    MessageBox.Show("Can not delete this instructor because there are courses linked to it");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to delete Instructor [" + InstructorID + "]?",
            "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsInstructor.DeleteInstructor(InstructorID))
                    {
                        MessageBox.Show("Instructor is deleted successfully");
                        _RefreshInstructorsList();
                    }
                    else
                        MessageBox.Show("Failed to delete instructor");
                }
            }
        }

        private void dgvInstructors_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                dgvInstructors.CurrentCell = dgvInstructors.Rows[e.RowIndex].Cells["InstructorID"];
        }
    }
}
