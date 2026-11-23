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
    public partial class frmAddEditCourse : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _CourseID = -1;
        clsCourse _Course;
        public frmAddEditCourse()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditCourse(int CourseID)
        {
            InitializeComponent();
            _CourseID = CourseID;
            _Mode = enMode.Update;
        }

        private void _ResetDefaultValues()
        {
            if (!_FillInstructorsComboBox())
            {
                MessageBox.Show("Add an instructor first");
                btnSave.Enabled = false;
            }
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Course";
                _Course = new clsCourse();
            }
            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Course";
            }
        }

        private bool _LoadData()
        {
            _Course = clsCourse.FindByCourseID(_CourseID);
            if (_Course == null)
            {
                MessageBox.Show("Course Not Found");
                this.Close();
                return false;
            }
            lblCourseID.Text = _Course.CourseID.ToString();
            txtTitle.Text = _Course.Title;
            cbInstructors.SelectedValue = _Course.InstructorID;
            nudDuration.Value = _Course.Duration;
            nudPrice.Value = _Course.Price;
            nudTotalSeats.Value = _Course.TotalSeats;
            return true;
        }

        private void frmAddEditCourse_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
            {
                if (!_LoadData())
                    return;
            }
        }
        private bool _FillInstructorsComboBox()
        {
            DataTable DT = clsInstructor.GetAllInstructors();
            if (DT.Rows.Count == 0)
                return false;
            else
            {
                cbInstructors.DataSource = DT;
                cbInstructors.DisplayMember = "FullName";
                cbInstructors.ValueMember = "InstructorID";
                return true;
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
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid");
                return;
            }
            _Course.Title = txtTitle.Text.Trim();
            _Course.InstructorID = Convert.ToInt32(cbInstructors.SelectedValue);
            _Course.Duration = (int)nudDuration.Value;
            _Course.Price = nudPrice.Value;
            _Course.TotalSeats = (int)nudTotalSeats.Value;
            if (_Mode == enMode.AddNew)
            {
                _Course.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            }
            if (_Course.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error:Data  was not Saved Successfully", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This Field Is Required");
                return;
            }
            else
                errorProvider1.SetError(txtTitle, "");

            if (txtTitle.Text.Trim().Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Text is too long. Max length is 50.");
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");
            }
        }
    }
}
