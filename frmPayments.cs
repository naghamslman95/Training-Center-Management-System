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
    public partial class frmPayments : Form
    {
        private DataTable DTPayments;
        private int SelectedPaymentID()
        {
            if (dgvPayments.CurrentRow == null)
                return -1;
            else
                return (int)dgvPayments.CurrentRow.Cells["PaymentID"].Value;
        }
        public frmPayments()
        {
            InitializeComponent();
        }

        private void _RefreshPaymentsList()
        {
            DTPayments = clsPayment.GetAllPayments();
            DTPayments.Columns.Add("Status", typeof(string),
               "IIF(EnrollmentStatus = 1, 'Active', IIF(EnrollmentStatus = 2, 'Cancelled', 'Completed'))");
            dgvPayments.DataSource = DTPayments;
            dgvPayments.Columns["EnrollmentStatus"].Visible = false;
            dgvPayments.Columns["EnrollmentID"].Visible = false;
            dgvPayments.Columns["CreatedBy"].Visible = false;
            dgvPayments.Columns["Status"].DisplayIndex = dgvPayments.Columns["EnrollmentStatus"].DisplayIndex;
            dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPayments.Columns["Course"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
            cbFilter.Text = "None";
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvPayments.Rows.Count.ToString();
        }

        private void frmPayments_Load(object sender, EventArgs e)
        {
            _RefreshPaymentsList();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTPayments.DefaultView.RowFilter = "";
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
                DTPayments.DefaultView.RowFilter = "";
                _UpdateRecordCount();
                return;
            }
            string FilterColumn = cbFilter.Text;
            if (cbFilter.Text == "PaymentID")
            {
                DTPayments.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilter.Text.Trim()}";
            }
            else if (cbFilter.Text == "TotalPrice" || cbFilter.Text == "PaymentAmount")
            {
                if (decimal.TryParse(txtFilter.Text.Trim(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal price))
                {
                    DTPayments.DefaultView.RowFilter = $"[{FilterColumn}] = {price.ToString(CultureInfo.InvariantCulture)}";
                }
                else
                {
                    DTPayments.DefaultView.RowFilter = "";
                }
            }
            else
            {
                string Value = txtFilter.Text.Trim().Replace("'", "''");
                DTPayments.DefaultView.RowFilter = $"[{FilterColumn}] Like '{Value}%'";
            }

            _UpdateRecordCount();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( cbFilter.Text == "PaymentID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (cbFilter.Text == "TotalPrice" || cbFilter.Text == "PaymentAmount")
            {
                if (txtFilter.Text.Contains("."))
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                else
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSelectEnrollment frm = new frmSelectEnrollment();
            if (frm.ShowDialog() == DialogResult.OK)
                _RefreshPaymentsList();
        }

        private void addNewPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelectEnrollment frm = new frmSelectEnrollment();
            if (frm.ShowDialog() == DialogResult.OK)
                _RefreshPaymentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPaymentID() != -1)
            {
                frmAddEditPayment frm = new frmAddEditPayment(SelectedPaymentID());
                if (frm.ShowDialog() == DialogResult.OK)
                    _RefreshPaymentsList();
            }
        }
    }
}
