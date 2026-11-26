using System;
using System.Configuration;
using System.Data;
using TrainingCenter_Data;

namespace TrainingCenter_Business
{
    public class clsPayment
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; } = enMode.AddNew;

        public int PaymentID { get; private set; }
        public int EnrollmentID { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CreatedByUserID { get; set; }

        public clsEnrollment EnrollmentInfo;
        public clsUser CreatedByUserInfo;

        private decimal _originalPaymentAmount;
        private int _originalEnrollmentID;

        private clsPayment(int PaymentID, int EnrollmentID, decimal PaymentAmount,DateTime PaymentDate, int CreatedByUserID)
        {
            this.EnrollmentID = EnrollmentID;
            this.PaymentID = PaymentID;
            this.PaymentAmount = PaymentAmount;
            this.PaymentDate = PaymentDate;
            this.CreatedByUserID = CreatedByUserID;

            this.EnrollmentInfo = clsEnrollment.FindByEnrollmentID(EnrollmentID);
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            _originalPaymentAmount = PaymentAmount;
            _originalEnrollmentID = EnrollmentID;


        Mode = enMode.Update;

        }
        public clsPayment()
        {
            this.EnrollmentID = -1;
            this.PaymentID = -1;
            this.PaymentAmount = -1;
            this.PaymentDate = DateTime.Now;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNewPayment()
        {
            if (this.EnrollmentID <= 0) return false;
            if (this.PaymentAmount <= 0) return false;

            this.EnrollmentInfo = clsEnrollment.FindByEnrollmentID(this.EnrollmentID);
            if (this.EnrollmentInfo == null) return false;
            if (this.EnrollmentInfo.EnrollmentStatus == clsEnrollment.enEnrollmentStatus.Cancelled) return false;
            if ((GetTotalPaidByEnrollmentID(this.EnrollmentID)+this.PaymentAmount )> EnrollmentInfo.TotalPrice) return false;
            this.PaymentID = clsPaymentData.AddNewPayment(this.EnrollmentID, this.PaymentAmount, 
                this.PaymentDate, this.CreatedByUserID);

            if(this.PaymentID != -1)
            {
                _originalPaymentAmount = this.PaymentAmount;
                _originalEnrollmentID = this.EnrollmentID;
                return true;
            }
            return false;
        }

        private bool _UpdatePayment()
        {
            if (this.PaymentAmount <= 0) return false;
            if (this.EnrollmentID != _originalEnrollmentID) return false;
                this.EnrollmentInfo = clsEnrollment.FindByEnrollmentID(this.EnrollmentID);
            if (this.EnrollmentInfo == null) return false;
            if (this.EnrollmentInfo.EnrollmentStatus == clsEnrollment.enEnrollmentStatus.Cancelled) return false;

            if ((GetTotalPaidByEnrollmentID(this.EnrollmentID) - _originalPaymentAmount + this.PaymentAmount) 
                        > EnrollmentInfo.TotalPrice) return false;

            if ( clsPaymentData.UpdatePayment(this.PaymentID, this.PaymentAmount))
            {
                _originalPaymentAmount = this.PaymentAmount;
                return true;
            }
            return false;
        }

        public static clsPayment FindByPaymentID(int PaymentID)
        {
            int EnrollmentID = -1, CreatedByUserID = -1;
            decimal PaymentAmount = -1;
            DateTime PaymentDate = DateTime.Now;

            if (clsPaymentData.GetPaymentInfoByPaymentID(PaymentID, ref EnrollmentID, ref PaymentAmount,
            ref PaymentDate,  ref CreatedByUserID))
                return new clsPayment(PaymentID, EnrollmentID, PaymentAmount, PaymentDate, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPayment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdatePayment();
                default:
                    return false;
            }

        }
        public static bool DeletePayment(int ID)
        {
            return (clsPaymentData.DeletePayment(ID));
        }

        public static DataTable GetAllPayments()
        {
            return clsPaymentData.GetAllPayments();
        }
        public static DataTable GetStudentPayments(int StudentID)
        {
            return clsPaymentData.GetStudentPayments(StudentID);
        }
        public static decimal GetStudentPaymentsForCourse(int StudentID, int CourseID)
        {
            return clsPaymentData.GetStudentPaymentsForCourse(StudentID, CourseID);
        }
        public static decimal GetTotalPaidByEnrollmentID(int EnrollmentID)
        {
            return clsPaymentData.GetTotalPaidByEnrollmentID(EnrollmentID);
        }
        public static decimal GetCollectedRevenuePerMonth(DateTime Start, DateTime NextMonthStart)
        {
            return clsPaymentData.GetCollectedRevenuePerMonth(Start, NextMonthStart);
        }
        public static decimal GetPaidForMonthEnrollments(DateTime Start, DateTime NextMonthStart)
        {
            return clsPaymentData.GetPaidForMonthEnrollments(Start, NextMonthStart);
        }
    }
}
