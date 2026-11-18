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
    public partial class frmUsers : Form
    {
        private DataTable DTUsers;

        public frmUsers()
        {
            InitializeComponent();
        }
        private void _RefreshUsersList()
        {
            DTUsers = clsUser.GetAllUsers();
            DTUsers.Columns.Add("RoleText", typeof(string), "IIF(Role = 1, 'Admin', 'Employee')");
            dgvUsers.DataSource = DTUsers;
            dgvUsers.Columns["Role"].Visible = false;
            dgvUsers.Columns["RoleText"].HeaderText = "Role";
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvUsers.Columns["RoleText"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _UpdateRecordCount();
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "IsActive")
            {
                txtFilter.Visible = false;
                cbIsActiveFilter.Visible = true;
                cbIsActiveFilter.Focus();
                cbIsActiveFilter.SelectedIndex = 0;
            }
            else
            {
                txtFilter.Visible = cbFilter.Text != "None";
                cbIsActiveFilter.Visible = false;

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
                DTUsers.DefaultView.RowFilter = "";
                _UpdateRecordCount();
                return;
            }
            string FilterColumn = cbFilter.Text;
            if (cbFilter.Text == "PersonID" || cbFilter.Text == "UserID")
            {
                DTUsers.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilter.Text.Trim()}";
            }

            else
            {
                string Value = txtFilter.Text.Trim().Replace("'", "''");
                DTUsers.DefaultView.RowFilter = $"[{FilterColumn}] Like '{Value}%'";
            }

            _UpdateRecordCount();
        }

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActiveFilter.Text;
            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }
            if (FilterValue == "All")
                DTUsers.DefaultView.RowFilter = "";
            else
                DTUsers.DefaultView.RowFilter = $"[{FilterColumn}] = {FilterValue}";
            _UpdateRecordCount();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "PersonID" || cbFilter.Text == "UserID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
