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
    public partial class ucPersonInfoWithFilter : UserControl
    {
        private bool _ShowAddPerson;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAdd.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }
        public int PersonID { get { return ucPersonInfo1.PersonID; } }

        public clsPerson SelectedPersonInfo
        {
            get { return ucPersonInfo1.SelectedPersonInfo; }
        }

        public ucPersonInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _SearchNow()
        {
            if (cbFilter.SelectedIndex == 0)
            {
                if (int.TryParse(txtFilter.Text.Trim(), out int PersonID))
                    return ucPersonInfo1.LoadPersonInfo(PersonID);
                else
                    return false;
            }
            else
               return ucPersonInfo1.LoadPersonInfo(txtFilter.Text.Trim());
        }
        public bool LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text = PersonID.ToString();
           return _SearchNow();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid");
                return;
            }
            if (!_SearchNow())
                MessageBox.Show("Person is not found");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadPersonInfo(frm.PersonID);
            }
        }

        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "Please enter a value");
                return;
            }
            else
                errorProvider1.SetError(txtFilter, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Focus();
        }
        private void ucPersonInfoWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Focus();
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch.PerformClick();
            if (cbFilter.SelectedIndex == 0)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
