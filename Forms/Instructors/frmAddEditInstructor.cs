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
    public partial class frmAddEditInstructor : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _InstructorID = -1;
        clsInstructor _Instructor;
        public frmAddEditInstructor()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditInstructor(int InstructorID)
        {
            InitializeComponent();
            _InstructorID = InstructorID;
            _Mode = enMode.Update;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Instructor";
                _Instructor = new clsInstructor();
                ucPersonInfoWithFilter1.ShowAddPerson = true;
                ucPersonInfoWithFilter1.FilterEnabled = true;
            }
            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Instructor";
            }
        }

        private bool _LoadData()
        {
            _Instructor = clsInstructor.FindByInstructorID(_InstructorID);
            ucPersonInfoWithFilter1.FilterEnabled = false;
            if (_Instructor == null)
            {
                MessageBox.Show("Instructor Not Found");
                this.Close();
                return false;
            }
            if (ucPersonInfoWithFilter1.LoadPersonInfo(_Instructor.PersonID))
            {
                lblInstructorID.Text = _Instructor.InstructorID.ToString();
                txtQualifications.Text = _Instructor.Qualifications;
                return true;
            }
            return false;
        }

        private void frmAddEditInstructor_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
            {
                if (!_LoadData())
                    return;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ucPersonInfoWithFilter1.PersonID != -1)
            {
                if (!this.ValidateChildren())
                {
                    MessageBox.Show("Some fields are not valid");
                    return;
                }
                if (_Mode == enMode.AddNew)
                {
                    if (clsInstructor.IsInstructorExistByPersonID(ucPersonInfoWithFilter1.PersonID))
                    {
                        MessageBox.Show("Selected Person already is an instructor, choose another one.", "Select another Person",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                _Instructor.PersonID = ucPersonInfoWithFilter1.PersonID;
                _Instructor.Qualifications = txtQualifications.Text.Trim();               
                if (_Mode == enMode.AddNew)
                {
                    _Instructor.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                }
                if (_Instructor.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Error:Data  was not Saved Successfully", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQualifications_Validating(object sender, CancelEventArgs e)
        {
            if (txtQualifications.Text.Trim().Length>200)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtQualifications, "Text is too long. Max length is 200.");
            }
            else
            {
                errorProvider1.SetError(txtQualifications, "");
            }
        }

    }
}
