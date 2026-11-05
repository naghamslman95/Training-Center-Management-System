using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter_Data;

namespace TrainingCenter_Business
{
    public class clsStudent
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; } = enMode.AddNew;

        public enum enStatus { Active = 1, InActive = 2 };

        public int PersonID { get; set; }
        public int StudentID { get; private set; }
        public enStatus Status { get; set; }
        public int CreatedByUserID { get; set; }

        public clsPerson PersonInfo;
        public clsUser CreatedByUserInfo;

        private clsStudent(int StudentID, int PersonID, enStatus Status, int CreatedByUserID)
        {
            this.StudentID = StudentID;
            this.PersonID = PersonID;
            this.Status = Status;
            this.CreatedByUserID = CreatedByUserID;

            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            Mode = enMode.Update;

        }
        public clsStudent()
        {
            this.StudentID = -1;
            this.PersonID = -1;
            this.Status = enStatus.Active;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNewStudent()
        {
            this.StudentID = clsStudentData.AddNewStudent(this.PersonID, (byte)this.Status, this.CreatedByUserID);
            return (this.StudentID != -1);
        }

        private bool _UpdateStudent()
        {
            return clsStudentData.UpdateStudent(this.StudentID, (byte)this.Status);
        }

        public static clsStudent FindByStudentID(int StudentID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            byte Status = 0;


            if (clsStudentData.GetStudentInfoByStudentID(StudentID, ref PersonID, ref Status, ref CreatedByUserID))
                return new clsStudent(StudentID, PersonID, (enStatus)Status, CreatedByUserID);
            else
                return null;
        }
        public static clsStudent FindByPersonID(int PersonID)
        {
            int StudentID = -1, CreatedByUserID = -1;
            byte Status = 0;


            if (clsStudentData.GetStudentInfoByPersonID(ref StudentID, PersonID, ref Status, ref CreatedByUserID))
                return new clsStudent(StudentID, PersonID, (enStatus)Status, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewStudent())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateStudent();
                default:
                    return false;
            }

        }

        public static DataTable GetAllStudents()
        {
            return clsStudentData.GetAllStudents();
        }
        public static bool DeleteStudent(int ID)
        {
            return (clsStudentData.DeleteStudent(ID));
        }
        public static bool IsStudentExistByStudentID(int ID)
        {
            return clsStudentData.IsStudentExistByStudentID(ID);
        }
        public static bool IsStudentExistByPersonID(int ID)
        {
            return clsStudentData.IsStudentExistByPersonID(ID);
        }

    }
}
