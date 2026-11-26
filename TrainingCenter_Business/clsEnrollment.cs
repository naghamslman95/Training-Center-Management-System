using System;
using System.Data;
using TrainingCenter_Data;

namespace TrainingCenter_Business
{
    public class clsEnrollment
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; } = enMode.AddNew;
        public enum enEnrollmentStatus { Active = 1, Cancelled = 2, Completed = 3 };

        public int EnrollmentID { get; private set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public enEnrollmentStatus EnrollmentStatus { get; set; }
        public int CreatedByUserID { get; set; }

        public clsCourse CourseInfo;
        public clsStudent StudentInfo;
        public clsUser CreatedByUserInfo;

        private int _originalCourseID;
        private int _originalStudentID;
        private enEnrollmentStatus _originalStatus;
        private decimal _originalTotalPrice;

        private clsEnrollment(int EnrollmentID, int CourseID, int StudentID, decimal TotalPrice,
            DateTime EnrollmentDate, enEnrollmentStatus EnrollmentStatus, int CreatedByUserID)
        {
            this.EnrollmentID = EnrollmentID;
            this.StudentID = StudentID;
            this.CourseID = CourseID;
            this.TotalPrice = TotalPrice;
            this.EnrollmentDate = EnrollmentDate;
            this.EnrollmentStatus = EnrollmentStatus;
            this.CreatedByUserID = CreatedByUserID;

            this.CourseInfo = clsCourse.FindByCourseID(CourseID);
            this.StudentInfo = clsStudent.FindByStudentID(StudentID);
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            _originalCourseID = CourseID;
            _originalStudentID = StudentID;
            _originalStatus = EnrollmentStatus;
            _originalTotalPrice = TotalPrice;

            Mode = enMode.Update;

        }
        public clsEnrollment()
        {
            this.EnrollmentID = -1;
            this.StudentID = -1;
            this.CourseID = -1;
            this.TotalPrice = -1;
            this.EnrollmentDate = DateTime.Now;
            this.EnrollmentStatus = enEnrollmentStatus.Active;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNewEnrollment()
        {
            if (this.StudentID <= 0) return false;
            if (this.CourseID <= 0) return false;
            if (this.TotalPrice < 0) return false;
            if (DoesStudentHaveActiveEnrollment(this.StudentID, this.CourseID)) return false;

            this.CourseInfo = clsCourse.FindByCourseID(this.CourseID);
            if (this.CourseInfo == null) return false;

            if (CourseInfo.TotalSeats <= ActiveEnrollmentsPerCourse(this.CourseID)) return false;

            this.EnrollmentID = clsEnrollmentData.AddNewEnrollment(this.CourseID, this.StudentID, this.TotalPrice,
                this.EnrollmentDate, (byte)this.EnrollmentStatus, this.CreatedByUserID);

            if (this.EnrollmentID != -1)
            {
                _originalStatus = this.EnrollmentStatus;
                _originalCourseID = this.CourseID;
                _originalStudentID = this.StudentID;
                _originalTotalPrice = this.TotalPrice; 
                return true;
            }
            return false;
        }

        private bool _UpdateEnrollment()
        {
            if (_originalStatus == enEnrollmentStatus.Cancelled || _originalStatus == enEnrollmentStatus.Completed) return false;
            if (this.StudentID <= 0) return false;
            if (this.CourseID <= 0) return false;
            if (this.TotalPrice < 0) return false;
            if (this.CourseID != _originalCourseID || this.StudentID != _originalStudentID ||  
                        this.TotalPrice != _originalTotalPrice)
            {
                if (clsPayment.GetTotalPaidByEnrollmentID(this.EnrollmentID) > 0) return false;
            }
            if (this.CourseID != _originalCourseID || this.StudentID != _originalStudentID)
            {
                if (DoesStudentHaveActiveEnrollment(this.StudentID, this.CourseID)) return false;
            }
            if (this.CourseID != _originalCourseID)
            {
                this.CourseInfo = clsCourse.FindByCourseID(this.CourseID);
                if (this.CourseInfo == null) return false;

                if (CourseInfo.TotalSeats <= ActiveEnrollmentsPerCourse(this.CourseID)) return false;
            }

            if (clsEnrollmentData.UpdateEnrollment(this.EnrollmentID, this.CourseID, this.StudentID,
                this.TotalPrice, (byte)this.EnrollmentStatus))
            {
                _originalStatus = this.EnrollmentStatus;
                _originalCourseID = this.CourseID;
                _originalStudentID = this.StudentID;
                _originalTotalPrice = this.TotalPrice;
                return true;
            }
            return false;
        }
        public bool Cancel()
        {
            if (Mode == enMode.AddNew) return false;
            if (_originalStatus == enEnrollmentStatus.Cancelled || _originalStatus == enEnrollmentStatus.Completed) return false;

            if (clsEnrollmentData.UpdateEnrollmentStatus(this.EnrollmentID, (byte)enEnrollmentStatus.Cancelled))
            {
                this.EnrollmentStatus = enEnrollmentStatus.Cancelled;
                _originalStatus = enEnrollmentStatus.Cancelled;
                return true;
            }
            return false;
        }
        public bool SetComplete()
        {
            if (Mode == enMode.AddNew) return false;
            if (_originalStatus == enEnrollmentStatus.Cancelled || _originalStatus == enEnrollmentStatus.Completed) return false;

            if (clsEnrollmentData.UpdateEnrollmentStatus(this.EnrollmentID, (byte)enEnrollmentStatus.Completed))
            {
                this.EnrollmentStatus = enEnrollmentStatus.Completed;
                _originalStatus = enEnrollmentStatus.Completed;
                return true;
            }
            return false;
        }
        public static clsEnrollment FindByEnrollmentID(int EnrollmentID)
        {
            int CourseID = -1, StudentID = -1, CreatedByUserID = -1;
            byte EnrollmentStatus = 0;
            decimal TotalPrice = -1;
            DateTime EnrollmentDate = DateTime.Now;

            if (clsEnrollmentData.GetEnrollmentInfoByEnrollmentID(EnrollmentID, ref CourseID, ref StudentID,
            ref TotalPrice, ref EnrollmentDate, ref EnrollmentStatus, ref CreatedByUserID))
                return new clsEnrollment(EnrollmentID, CourseID, StudentID, TotalPrice, EnrollmentDate,
                        (enEnrollmentStatus)EnrollmentStatus, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewEnrollment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateEnrollment();
                default:
                    return false;
            }

        }
        public static bool DeleteEnrollment(int ID)
        {
            return (clsEnrollmentData.DeleteEnrollment(ID));
        }
        public static DataTable GetAllEnrollments()
        {
            return clsEnrollmentData.GetAllEnrollments();
        }
        public static DataTable GetStudentEnrollmentsByStudentID(int StudentID)
        {
            return clsEnrollmentData.GetStudentEnrollmentsByStudentID(StudentID);
        }
        public static DataTable GetCourseEnrollmentsByCourseID(int CourseID)
        {
            return clsEnrollmentData.GetCourseEnrollmentsByCourseID(CourseID);
        }
        public static int GetStudentActiveEnrollmentForCourse(int StudentID, int CourseID)
        {
            return clsEnrollmentData.GetStudentActiveEnrollmentForCourse(StudentID, CourseID);
        }
        public static bool DoesStudentHaveActiveEnrollment(int StudentID, int CourseID)
        {
            return clsEnrollmentData.DoesStudentHaveActiveEnrollment(StudentID, CourseID);
        }
        public static bool DoesCourseHaveEnrollments(int CourseID)
        {
            return clsEnrollmentData.DoesCourseHaveEnrollments(CourseID);
        }
        public static int ActiveEnrollmentsPerCourse(int CourseID)
        {
            return clsEnrollmentData.ActiveEnrollmentsPerCourse(CourseID);
        }
        public static int GetEnrollmentsPerMonth(DateTime Start, DateTime NextMonthStart)
        {
            DataTable DT = clsEnrollmentData.GetEnrollmentsCountByStatus(Start, NextMonthStart);
            int TotalCount = 0;
            foreach (DataRow row in DT.Rows)
                TotalCount += Convert.ToInt32(row["Total"]);
            return TotalCount;
        }
        public static int GetEnrollmentStatusCountPerMonth(DateTime Start, DateTime NextMonthStart, enEnrollmentStatus Status)
        {
            DataTable DT = clsEnrollmentData.GetEnrollmentsCountByStatus(Start, NextMonthStart);
            int TotalCount = 0;
            foreach (DataRow row in DT.Rows)
            {
                if (Convert.ToInt32(row["EnrollmentStatus"]) == (int)Status)
                       TotalCount += Convert.ToInt32(row["Total"]);
            }
            return TotalCount;
        }
        public static decimal GetBilledPerMonth(DateTime Start, DateTime NextMonthStart)
        {
            return clsEnrollmentData.GetBilledPerMonth(Start, NextMonthStart);
        }
        public static decimal GetOutstandingPerMonth(DateTime Start, DateTime NextMonthStart)
        {
            decimal Billed = clsEnrollment.GetBilledPerMonth(Start, NextMonthStart);
            decimal Paid = clsPayment.GetPaidForMonthEnrollments(Start, NextMonthStart);
            return Billed - Paid;
        }
    }
}
