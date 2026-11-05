using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter_Data;

namespace TrainingCenter_Business
{
    public class clsInstructor
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; } = enMode.AddNew;

        public int PersonID { get; set; }
        public int InstructorID { get; private set; }
        public string Qualifications { get; set; }
        public int CreatedByUserID { get; set; }

        public clsPerson PersonInfo;
        public clsUser CreatedByUserInfo;

        private clsInstructor(int InstructorID, int PersonID, string Qualifications, int CreatedByUserID)
        {
            this.InstructorID = InstructorID;
            this.PersonID = PersonID;
            this.Qualifications = Qualifications;
            this.CreatedByUserID = CreatedByUserID;

            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            Mode = enMode.Update;

        }
        public clsInstructor()
        {
            this.InstructorID = -1;
            this.PersonID = -1;
            this.Qualifications = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNewInstructor()
        {
            this.InstructorID = clsInstructorData.AddNewInstructor(this.PersonID, this.Qualifications, this.CreatedByUserID);
            return (this.InstructorID != -1);
        }

        private bool _UpdateInstructor()
        {
            return clsInstructorData.UpdateInstructor(this.InstructorID, this.Qualifications);
        }

        public static clsInstructor FindByInstructorID(int InstructorID)
        {
            int PersonID = -1, CreatedByUserID = -1;
           string Qualifications = "";

            if (clsInstructorData.GetInstructorInfoByInstructorID(InstructorID, ref PersonID, ref Qualifications, ref CreatedByUserID))
                return new clsInstructor(InstructorID, PersonID, Qualifications, CreatedByUserID);
            else
                return null;
        }
        public static clsInstructor FindByPersonID(int PersonID)
        {
            int InstructorID = -1, CreatedByUserID = -1;
            string Qualifications = "";

            if (clsInstructorData.GetInstructorInfoByPersonID(ref InstructorID, PersonID, ref Qualifications, ref CreatedByUserID))
                return new clsInstructor(InstructorID, PersonID, Qualifications, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInstructor())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateInstructor();
                default:
                    return false;
            }

        }

        public static DataTable GetAllInstructors()
        {
            return clsInstructorData.GetAllInstructors();
        }
        public static bool DeleteInstructor(int ID)
        {
            return (clsInstructorData.DeleteInstructor(ID));
        }
        public static bool IsInstructorExistByInstructorID(int ID)
        {
            return clsInstructorData.IsInstructorExistByInstructorID(ID);
        }
        public static bool IsInstructorExistByPersonID(int ID)
        {
            return clsInstructorData.IsInstructorExistByPersonID(ID);
        }
    }
}
