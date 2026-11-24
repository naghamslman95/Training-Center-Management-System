using System;
using System.ComponentModel;
using System.Data;
using System.Security.Claims;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmAddEditEnrollment : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _EnrollmentID = -1;
        clsEnrollment _Enrollment;
        clsStudent _Student;
        clsCourse _Course;
        public frmAddEditEnrollment()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditEnrollment(int EnrollmentID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _EnrollmentID = EnrollmentID;
        }
        private bool _FillCoursesComboBox()
        {
            DataTable DT = clsCourse.GetAllCourses();
            if (DT.Rows.Count == 0)
                return false;
            else
            {
                cbCourses.DisplayMember = "Title";
                cbCourses.ValueMember = "CourseID";
                cbCourses.DataSource = DT;
                return true;
            }
        }

        private void _ResetDefaultValues()
        {
            if (!_FillCoursesComboBox())
            {
                MessageBox.Show("Add a course first");
                btnSave.Enabled = false;
            }
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Enrollment";
                _Enrollment = new clsEnrollment();
                btnSave.Enabled = false;
                ucPersonInfoWithFilter1.FilterEnabled = true;
                lblEnrollmentDate.Text = DateTime.Now.ToShortDateString();
            }
            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Enrollment";
            }
        }

        private bool _LoadData()
        {
            _Enrollment = clsEnrollment.FindByEnrollmentID(_EnrollmentID);
            if (_Enrollment == null)
            {
                MessageBox.Show("Enrollment Not Found");
                this.Close();
                return false;
            }
            _Student = _Enrollment.StudentInfo;
            if (_Student == null)
            {
                MessageBox.Show("Student Not Found");
                this.Close();
                return false;               
            }
            if (!ucPersonInfoWithFilter1.LoadPersonInfo(_Student.PersonID))
            {
                MessageBox.Show("Person Not Found");
                this.Close();
                return false;
            }
            else
            {
                lblEnrollmentID.Text = _Enrollment.EnrollmentID.ToString();
                cbCourses.SelectedValue = _Enrollment.CourseID;
                lblEnrollmentStatus.Text = _Enrollment.EnrollmentStatus.ToString();
                lblEnrollmentDate.Text = _Enrollment.EnrollmentDate.ToShortDateString();
                nudTotalPrice.Value = _Enrollment.TotalPrice;
                return true;
            }
        }

        private void frmAddEditEnrollment_Load(object sender, EventArgs e)
        {
            ucPersonInfoWithFilter1.DataBack += PersonSelected;
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
            {
                if (!_LoadData())
                    return;
                else
                {
                    nudPaidAmount.Enabled = false;
                    ucPersonInfoWithFilter1.FilterEnabled = false;
                }
            }
        }
        private void PersonSelected(object sender, int PersonID)
        {
            if (PersonID == -1)
            {
                _Student = null;
                lblAddStudent.Visible = false;
                btnSave.Enabled = false;
                return;
            }
            _Student = clsStudent.FindByPersonID(PersonID);
            if (_Student != null)
            {
                lblAddStudent.Visible = false;
                btnSave.Enabled = true;
            }
            else
            {
                lblAddStudent.Visible = true;
                btnSave.Enabled = false;
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

        private void nudPaidAmount_Validating(object sender, CancelEventArgs e)
        {
            if (nudPaidAmount.Value > nudTotalPrice.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(nudPaidAmount, "Paid amount can not exceed the total price");
                return;
            }
            else
                errorProvider1.SetError(nudPaidAmount, "");
        }
        private bool _CheckEnrollmentAllowed()
        {
            _Course = clsCourse.FindByCourseID(Convert.ToInt32(cbCourses.SelectedValue));
            if (_Course == null)
            {
                MessageBox.Show("Course not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_Mode == enMode.Update && _Enrollment.CourseID == _Course.CourseID)
                return true;  

            if (_Course.TotalSeats <= clsEnrollment.ActiveEnrollmentsPerCourse(_Course.CourseID))
            {
                MessageBox.Show("No seats available for this course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (clsEnrollment.DoesStudentHaveActiveEnrollment(_Student.StudentID, _Course.CourseID))
            {
                MessageBox.Show("This student already has an active enrollment for this course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid");
                return;
            }
            if (!this._CheckEnrollmentAllowed())  return;

            _Enrollment.CourseID = Convert.ToInt32(cbCourses.SelectedValue);
            _Enrollment.StudentID = _Student.StudentID;
            _Enrollment.TotalPrice = nudTotalPrice.Value;
            if (_Mode == enMode.AddNew)
            {
                _Enrollment.EnrollmentStatus = clsEnrollment.enEnrollmentStatus.Active;
                _Enrollment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            }

            if (!_Enrollment.Save())
            {
                MessageBox.Show("Error:Data  was not Saved Successfully", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nudPaidAmount.Value > 0)
            {
                clsPayment Payment = new clsPayment();
                Payment.EnrollmentID = _Enrollment.EnrollmentID;
                Payment.PaymentAmount = nudPaidAmount.Value;
                if (_Mode == enMode.AddNew)
                {
                    Payment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                }
                if (Payment.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Enrollment saved but payment failed", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.OK;
                }

            }
            else
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                _Course = clsCourse.FindByCourseID(Convert.ToInt32(cbCourses.SelectedValue));
                if (_Course != null)
                    nudTotalPrice.Value = _Course.Price;
            }
        }

        private void lblAddStudent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditStudent frm = new frmAddEditStudent();
            frm.PersonID = ucPersonInfoWithFilter1.PersonID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PersonSelected(this, ucPersonInfoWithFilter1.PersonID);
            }
        }
    }
}
