using System;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmAddEditStudent : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _StudentID = -1;
        clsStudent _Student;
        public frmAddEditStudent()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditStudent(int StudentID)
        {
            InitializeComponent();
            _StudentID = StudentID;
            _Mode = enMode.Update;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Student";
                _Student = new clsStudent();
                chbIsActive.Checked = true;
                ucPersonInfoWithFilter1.ShowAddPerson = true;
                ucPersonInfoWithFilter1.FilterEnabled = true;
            }
            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Student";
            }
        }

        private bool _LoadData()
        {
            _Student = clsStudent.FindByStudentID(_StudentID);
            ucPersonInfoWithFilter1.FilterEnabled = false;
            if (_Student == null)
            {
                MessageBox.Show("Student Not Found");
                this.Close();
                return false;
            }
            if (ucPersonInfoWithFilter1.LoadPersonInfo(_Student.PersonID))
            {
                lblStudentID.Text = _Student.StudentID.ToString();
                if (_Student.Status == clsStudent.enStatus.Active) chbIsActive.Checked = true;
                else chbIsActive.Checked = false;
                return true;
            }
            return false;
        }

        private void frmAddEditStudent_Load(object sender, EventArgs e)
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
                if (_Mode == enMode.AddNew)
                {
                    if (clsStudent.IsStudentExistByPersonID(ucPersonInfoWithFilter1.PersonID))
                    {
                        MessageBox.Show("Selected Person already is a student, choose another one.", "Select another Person",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                _Student.PersonID = ucPersonInfoWithFilter1.PersonID;
                if (chbIsActive.Checked == true)  _Student.Status = clsStudent.enStatus.Active;
                else _Student.Status = clsStudent.enStatus.InActive;
                if (_Mode == enMode.AddNew)
                {
                    _Student.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                }
                if (_Student.Save())
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
    }
}
