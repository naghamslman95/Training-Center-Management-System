using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter_Data;

namespace TrainingCenter_Business
{
    public class clsCourse
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; } = enMode.AddNew;

        public int CourseID { get; private set; }
        public int InstructorID { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int TotalSeats { get; set; }
        public int CreatedByUserID { get; set; }

        public clsInstructor InstructorInfo;
        public clsUser CreatedByUserInfo;

        private clsCourse(int CourseID, int InstructorID, string Title, int Duration, decimal Price, 
            int TotalSeats, int CreatedByUserID)
        {
            this.InstructorID = InstructorID;
            this.CourseID = CourseID;
            this.Title = Title;
            this.Duration = Duration;   
            this.Price = Price;
            this.TotalSeats = TotalSeats;
            this.CreatedByUserID = CreatedByUserID;

            this.InstructorInfo = clsInstructor.FindByInstructorID(InstructorID);
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            Mode = enMode.Update;

        }
        public clsCourse()
        {
            this.InstructorID = -1;
            this.CourseID = -1;
            this.Title = "";
            this.Duration = -1;
            this.Price = -1;
            this.TotalSeats = -1;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        private bool _IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title) && Title.Length <= 50
                && InstructorID > 0
                && Duration > 0
                && Price > 0
                && TotalSeats > 0;
        }
        private bool _AddNewCourse()
        {
            if (!_IsValid())
                return false;
            this.CourseID = clsCourseData.AddNewCourse(this.InstructorID, this.Title, this.Duration,
                this.Price, this.TotalSeats, this.CreatedByUserID);
            return (this.CourseID != -1);
        }

        private bool _UpdateCourse()
        {
            if (!_IsValid())
                return false;
            return clsCourseData.UpdateCourse(this.CourseID, this.InstructorID, this.Title, this.Duration,
                         this.Price, this.TotalSeats);
        }

        public static clsCourse FindByCourseID(int CourseID)
        {
            int InstructorID = -1, Duration = -1, TotalSeats = -1, CreatedByUserID = -1;
            string Title = "";
            decimal Price = -1;

            if (clsCourseData.GetCourseInfoByCourseID(CourseID, ref InstructorID, ref Title, ref Duration,
                ref Price, ref TotalSeats, ref CreatedByUserID))
                return new clsCourse(CourseID, InstructorID, Title, Duration, Price, TotalSeats, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCourse())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateCourse();
                default:
                    return false;
            }

        }

        public static DataTable GetAllCourses()
        {
            return clsCourseData.GetAllCourses();
        }
        public static bool DeleteCourse(int ID)
        {
            return (clsCourseData.DeleteCourse(ID));
        }
        public static bool IsCourseExistByCourseID(int ID)
        {
            return clsCourseData.IsCourseExistByCourseID(ID);
        }
    }
}
