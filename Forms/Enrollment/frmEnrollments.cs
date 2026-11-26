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
    public partial class frmEnrollments : Form
    {
        private DataTable DTEnrollments;
        private int SelectedEnrollmentID()
        {
            if (dgvEnrollments.CurrentRow == null)
                return -1;
            else
                return (int)dgvEnrollments.CurrentRow.Cells["EnrollmentID"].Value;
        }
        public frmEnrollments()
        {
            InitializeComponent();
        }

        private void _RefreshEnrollmentsList()
        {
            DTEnrollments = clsEnrollment.GetAllEnrollments();
            DTEnrollments.Columns.Add("Status", typeof(string),
                "IIF(EnrollmentStatus = 1, 'Active', IIF(EnrollmentStatus = 2, 'Cancelled', 'Completed'))");
            dgvEnrollments.DataSource = DTEnrollments;
            dgvEnrollments.Columns["EnrollmentStatus"].Visible = false;
            dgvEnrollments.Columns["Status"].DisplayIndex = dgvEnrollments.Columns["EnrollmentStatus"].DisplayIndex;
            dgvEnrollments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEnrollments.Columns["CreatedBy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
            cbFilter.Text = "None";
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvEnrollments.Rows.Count.ToString();
        }

        private void frmEnrollments_Load(object sender, EventArgs e)
        {
            _RefreshEnrollmentsList();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTEnrollments.DefaultView.RowFilter = "";
            _UpdateRecordCount();
            if (cbFilter.Text == "EnrollmentStatus")
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
                DTEnrollments.DefaultView.RowFilter = "";
                _UpdateRecordCount();
                return;
            }
            string FilterColumn = cbFilter.Text;
            if (cbFilter.Text == "EnrollmentID")
            {
                DTEnrollments.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilter.Text.Trim()}";
            }
            else if (cbFilter.Text == "TotalPrice")
            {
                if (decimal.TryParse(txtFilter.Text.Trim(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal price))
                {
                    DTEnrollments.DefaultView.RowFilter = $"[{FilterColumn}] = {price.ToString(CultureInfo.InvariantCulture)}";
                }
                else
                {
                    DTEnrollments.DefaultView.RowFilter = "";
                }
            }
            else
            {
                string Value = txtFilter.Text.Trim().Replace("'", "''");
                DTEnrollments.DefaultView.RowFilter = $"[{FilterColumn}] Like '{Value}%'";
            }

            _UpdateRecordCount();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "EnrollmentID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (cbFilter.Text == "TotalPrice")
            {
                if (txtFilter.Text.Contains("."))
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                else
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
            }
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "EnrollmentStatus";
            string FilterValue = cbStatusFilter.Text;
            switch (FilterValue)
            {
                case "All":
                    break;
                case "Active":
                    FilterValue = "1";
                    break;
                case "Cancelled":
                    FilterValue = "2";
                    break;
                case "Completed":
                    FilterValue = "3";
                    break;
            }
            if (FilterValue == "All")
                DTEnrollments.DefaultView.RowFilter = "";
            else
                DTEnrollments.DefaultView.RowFilter = $"[{FilterColumn}] = {FilterValue}";
            _UpdateRecordCount();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditEnrollment frm = new frmAddEditEnrollment();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _RefreshEnrollmentsList();
            }
        }
        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditEnrollment frm = new frmAddEditEnrollment();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _RefreshEnrollmentsList();
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedEnrollmentID() != -1)
            {
                frmAddEditEnrollment frm = new frmAddEditEnrollment(SelectedEnrollmentID());
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _RefreshEnrollmentsList();
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EnrollmentID = SelectedEnrollmentID();
            if (EnrollmentID != -1)
            {
                if (clsPayment.GetTotalPaidByEnrollmentID(EnrollmentID) > 0)
                {
                    MessageBox.Show("Can not delete this enrollment because there are payments linked to it");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to delete enrollment [" + EnrollmentID + "]?",
            "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsEnrollment.DeleteEnrollment(EnrollmentID))
                    {
                        MessageBox.Show("Enrollment is deleted successfully");
                        _RefreshEnrollmentsList();
                    }
                    else
                        MessageBox.Show("Failed to delete enrollment");
                }
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EnrollmentID = SelectedEnrollmentID();
            if (EnrollmentID == -1) return;
            clsEnrollment Enrollment = clsEnrollment.FindByEnrollmentID(EnrollmentID);
            if (Enrollment == null) return;

            if (MessageBox.Show("Are you sure you want to cancel enrollment [" + EnrollmentID + "]?",
                "Confirm Cancel", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (Enrollment.Cancel())
                {
                    MessageBox.Show("Enrollment is cancelled successfully");
                    _RefreshEnrollmentsList();
                }
                else
                    MessageBox.Show("Error while cancelling enrollment");
            }
        }

        private void setCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EnrollmentID = SelectedEnrollmentID();
            if (EnrollmentID == -1) return;
            clsEnrollment Enrollment = clsEnrollment.FindByEnrollmentID(EnrollmentID);
            if (Enrollment == null) return;

            if (MessageBox.Show("Are you sure you want to set enrollment [" + EnrollmentID + "] complete?",
                "Confirm complete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (Enrollment.SetComplete())
                {
                    MessageBox.Show("Enrollment is set complete successfully");
                    _RefreshEnrollmentsList();
                }
                else
                    MessageBox.Show("Error while setting enrollment complete");
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedEnrollmentID() != -1)
            {
                frmEnrollmentDetails frm = new frmEnrollmentDetails(SelectedEnrollmentID());
                frm.ShowDialog();
            }
        }

        private void dgvEnrollments_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedEnrollmentID() != -1)
            {
                frmEnrollmentDetails frm = new frmEnrollmentDetails(SelectedEnrollmentID());
                frm.ShowDialog();
            }
        }

        private void dgvEnrollments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                dgvEnrollments.CurrentCell = dgvEnrollments.Rows[e.RowIndex].Cells["EnrollmentID"];
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvEnrollments.CurrentRow == null)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                {
                    if (item.Name != "addNewToolStripMenuItem")
                        item.Enabled = false;
                }
                return;
            }
            else
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                {
                    if (item.Name != "addNewToolStripMenuItem")
                        item.Enabled = true;
                }
            }
                editToolStripMenuItem.Enabled =
                    (Convert.ToInt32(dgvEnrollments.CurrentRow.Cells["EnrollmentStatus"].Value) == 1 && clsPayment.GetTotalPaidByEnrollmentID(SelectedEnrollmentID()) == 0);
            deleteToolStripMenuItem.Enabled = (Convert.ToInt32(dgvEnrollments.CurrentRow.Cells["EnrollmentStatus"].Value) == 1);
            cancelToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled;
            setCompleteToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled;
            addPaymentToolStripMenuItem.Enabled = (Convert.ToInt32(dgvEnrollments.CurrentRow.Cells["EnrollmentStatus"].Value) != 2);
        }

        private void addPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedEnrollmentID() == -1) return;
            frmAddEditPayment frm = new frmAddEditPayment();
            frm.EnrollmentID = SelectedEnrollmentID();
            if (frm.ShowDialog() == DialogResult.OK) 
                _RefreshEnrollmentsList();
        }
    }
}
