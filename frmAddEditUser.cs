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
    public partial class frmAddEditUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _UserID = -1;
        clsUser _User;
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }
        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                _User = new clsUser();

                tbLoginInfo.Enabled = false;
                ucPersonInfoWithFilter1.ShowAddPerson = true;
                ucPersonInfoWithFilter1.FilterEnabled = true;
            }
            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update User";
                tbLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cbRole.Text = "Employee";

        }

        private bool _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ucPersonInfoWithFilter1.FilterEnabled = false;
            if (_User == null)
            {
                MessageBox.Show("User Not Found");
                this.Close();
                return false;
            }
            if (ucPersonInfoWithFilter1.LoadPersonInfo(_User.PersonID))
            {
                lblUserID.Text = _User.UserID.ToString();
                txtUserName.Text = _User.UserName;
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                chbIsActive.Checked = _User.IsActive;
                if (_User.Role == clsUser.enRole.Admin)
                    cbRole.Text = "Admin";
                else cbRole.Text = "Employee";
                return true;
            }
            return false;
        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
            {
                if (!_LoadData())
                    return;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                if (_User.UserID == clsGlobal.CurrentUser.UserID)
                {
                    cbRole.Enabled = false;
                    chbIsActive.Enabled = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var Result = MessageBox.Show("Are you sure you want to close? any unsaved information will be lost.",
                           "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                tbLoginInfo.Enabled = true;
                tabControl1.SelectedTab = tbLoginInfo;
                return;
            }
            if (ucPersonInfoWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistByPersonID(ucPersonInfoWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already is a user, choose another one.", "Select another Person",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    btnSave.Enabled = true;
                    tbLoginInfo.Enabled = true;
                    tabControl1.SelectedTab = tbLoginInfo;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid");
                return;
            }

            _User.PersonID = ucPersonInfoWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            if (_Mode == enMode.AddNew)
            {
                if (!_User.SetPassword(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Invalid password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (cbRole.Text == "Admin") _User.Role = clsUser.enRole.Admin;
            else _User.Role = clsUser.enRole.Employee;
            _User.IsActive = chbIsActive.Checked;

            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error:Data  was not Saved Successfully", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                TextBox Temp = ((TextBox)sender);
                if (string.IsNullOrEmpty(Temp.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(Temp, "This Field Is Requied");
                }
                else
                    errorProvider1.SetError(Temp, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtConfirmPassword, "This Field Is Requied");
                    return;
                }
                else
                    errorProvider1.SetError(txtConfirmPassword, null);

                if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtConfirmPassword, "Please confirm password");
                    return;
                }
                else
                    errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This Field Is Requied");
                return;
            }
            else
                errorProvider1.SetError(txtUserName, null);

            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExistByUserName(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }

            }
            else
            {
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExistByUserName(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                }
            }
        }
    }
}
