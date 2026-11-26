using System;
using System.Windows.Forms;
using TrainingCenter_Business;

namespace Training_Center_Management_System
{
    public partial class frmPaymentDetails : Form
    {
        private int _PaymentID = -1;
        private clsPayment _Payment;
        public frmPaymentDetails(int paymentID)
        {
            InitializeComponent();
            _PaymentID = paymentID;
        }

        private void frmPaymentDetails_Load(object sender, EventArgs e)
        {
            _Payment = clsPayment.FindByPaymentID(_PaymentID);
            if (_Payment == null)
            {
                MessageBox.Show("Payment is not found!");
                ResetPaymentInfo();
                return;
            }
            if (!ucEnrollmentInfo1.LoadEnrollmentInfo(_Payment.EnrollmentID))
            {
                MessageBox.Show("Enrollment is not found!");
                ResetPaymentInfo();
                return;
            }
            FillPaymentInfo(_Payment);
        }

        private void FillPaymentInfo(clsPayment Payment)
        {
            lblPaymentID.Text = Payment.PaymentID.ToString();
            lblPaymentAmount.Text = Payment.PaymentAmount.ToString();
            lblPaymentDate.Text = Payment.PaymentDate.ToShortDateString();
            lblCreatedBy.Text = Payment.CreatedByUserInfo.UserName;
        }
        private void ResetPaymentInfo()
        {
            _PaymentID = -1;
            _Payment = null;
            lblPaymentID.Text = "???";
            lblPaymentAmount.Text = "???";
            lblPaymentDate.Text = "???";
            lblCreatedBy.Text = "???";
        }
    }
}
