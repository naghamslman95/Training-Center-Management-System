using System;
using System.ComponentModel;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmAddEditPayment : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _PaymentID = -1;
        private clsPayment _Payment;
        private int _EnrollmentID = -1;
        private clsEnrollment _Enrollment;
        public int EnrollmentID { set { _EnrollmentID = value; } }
        public frmAddEditPayment(int PaymentID)
        {
            InitializeComponent();
            _PaymentID = PaymentID;
            _Mode = enMode.Update;
        }
        public frmAddEditPayment()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Payment";
                _Payment = new clsPayment();
                _Enrollment = clsEnrollment.FindByEnrollmentID(_EnrollmentID);
                if (!ucEnrollmentInfo1.LoadEnrollmentInfo(_EnrollmentID) || _Enrollment == null)
                {
                    MessageBox.Show("Enrollment Not Found");
                    this.Close();
                    return;
                }
            }
            if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Payment";
            }
        }

        private bool _LoadData()
        {
            _Payment = clsPayment.FindByPaymentID(_PaymentID);
            if (_Payment == null)
            {
                MessageBox.Show("Payment Not Found");
                this.Close();
                return false;
            }
            _Enrollment = clsEnrollment.FindByEnrollmentID(_Payment.EnrollmentID);
            if (!ucEnrollmentInfo1.LoadEnrollmentInfo(_Payment.EnrollmentID) || _Enrollment == null)
            {
                MessageBox.Show("Enrollment Not Found");
                this.Close();
                return false;
            }
            else
            {
                lblPaymentID.Text = _Payment.PaymentID.ToString();
                nudPaymentAmount.Value = _Payment.PaymentAmount;
                return true;
            }
        }

        private void frmAddEditPayment_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
            {
                if (!_LoadData())
                    return;
            }
        }

        private void nudPaymentAmount_Validating(object sender, CancelEventArgs e)
        {
            decimal Maximum = _Enrollment.TotalPrice - clsPayment.GetTotalPaidByEnrollmentID(_Enrollment.EnrollmentID);
            if (_Mode == enMode.Update)
                Maximum = Maximum +_Payment.PaymentAmount;
            if (nudPaymentAmount.Value > Maximum)
            {
                e.Cancel = true;
                errorProvider1.SetError(nudPaymentAmount, "Paid amount can not exceed the remaining amount");
                return;
            }
            else
                errorProvider1.SetError(nudPaymentAmount, "");
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
            _Payment.PaymentAmount = nudPaymentAmount.Value;

            if (_Mode == enMode.AddNew)
            {
                _Payment.EnrollmentID = _Enrollment.EnrollmentID;
                _Payment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            }

            if (_Payment.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error:Data  was not Saved Successfully", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
