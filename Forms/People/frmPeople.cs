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
    public partial class frmPeople : Form
    {
        private DataTable DTPeople;
        private int SelectedPersonID()
        {
            if (dgvPeople.CurrentRow == null)
                return -1;
            else
                return (int)dgvPeople.CurrentRow.Cells[0].Value;
        }
        public frmPeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            DTPeople = clsPerson.GetAllPeople();
            DTPeople.Columns.Add("GenderText", typeof(string), "IIF(Gender, 'Female', 'Male')");
            dgvPeople.DataSource = DTPeople;
            dgvPeople.Columns["Gender"].Visible = false;
            dgvPeople.Columns["GenderText"].HeaderText = "Gender";
            dgvPeople.Columns["GenderText"].DisplayIndex = dgvPeople.Columns["Gender"].DisplayIndex;
            dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPeople.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _UpdateRecordCount();
            cbFilter.Text = "None";
           
        }
        private void _UpdateRecordCount()
        {
            lblRecords.Text = dgvPeople.Rows.Count.ToString();
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            cbFilter.SelectedIndex = 0;
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTPeople.DefaultView.RowFilter = "";
            _UpdateRecordCount();

            if (cbFilter.Text == "Gender")
            {
                txtFilter.Visible = false;
                cbGenderFilter.Visible = true;
                cbGenderFilter.Focus();
                cbGenderFilter.SelectedIndex = 0;
            }
            else
            {
                txtFilter.Visible = cbFilter.Text != "None";
                cbGenderFilter.Visible = false;
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
                DTPeople.DefaultView.RowFilter = "";
                _UpdateRecordCount();
                return;
            }
            string FilterColumn = cbFilter.Text;
            if (cbFilter.Text == "PersonID")
                DTPeople.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilter.Text.Trim()}";
            else
            {
                string Value = txtFilter.Text.Trim().Replace("'", "''");
                DTPeople.DefaultView.RowFilter = $"[{FilterColumn}] Like '{Value}%'";
            }
            _UpdateRecordCount();
        }
        private void cbGenderFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Gender";
            string FilterValue = cbGenderFilter.Text;
            switch (FilterValue)
            {
                case "All":
                    break;
                case "Male":
                    FilterValue = "false";
                    break;
                case "Female":
                    FilterValue = "true";
                    break;
            }
            if (FilterValue == "All")
                DTPeople.DefaultView.RowFilter = "";
            else
                DTPeople.DefaultView.RowFilter = $"[{FilterColumn}] = {FilterValue}";
            _UpdateRecordCount();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            if (frm.ShowDialog() == DialogResult.OK)
                _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPersonID() != -1)
            {
                frmAddEditPerson frm = new frmAddEditPerson(SelectedPersonID());
                if (frm.ShowDialog() == DialogResult.OK)
                    _RefreshPeopleList();
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPersonID() != -1)
            {
                frmPersonDetails frm = new frmPersonDetails(SelectedPersonID());
                frm.ShowDialog();
            }
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedPersonID() != -1)
            {
                frmPersonDetails frm = new frmPersonDetails(SelectedPersonID());
                frm.ShowDialog();
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            if (frm.ShowDialog() == DialogResult.OK)
                _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPersonID() != -1)
            {
                if (MessageBox.Show("Are you sure you want to delete person [" + SelectedPersonID() + "]?",
                "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsPerson.DeletePerson(SelectedPersonID()))
                    {
                        MessageBox.Show("Person is deleted successfully");
                        _RefreshPeopleList();
                    }
                    else
                        MessageBox.Show("Person is not deleted because there is data linked to it");

                }
            }
        }

        private void dgvPeople_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                dgvPeople.CurrentCell = dgvPeople.Rows[e.RowIndex].Cells["PersonID"];
        }
    }
}
