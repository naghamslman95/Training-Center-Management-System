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


    }
}
