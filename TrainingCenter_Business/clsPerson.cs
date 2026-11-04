using System;
using System.Data;
using TrainingCenter_Data;

namespace TrainingCenter_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; }  = enMode.AddNew;

        public int PersonID { get; private set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " "  + LastName; }
        }
        public string NationalNo { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        private clsPerson(int PersonID, string FirstName, string SecondName, string LastName, string NationalNo,
        bool Gender, DateTime DateOfBirth, string Phone, string Email)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.Gender = Gender;
            this.Email = Email;
            this.Phone = Phone;
            this.DateOfBirth = DateOfBirth;

            Mode = enMode.Update;
        }
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.Gender = false;
            this.Email = "";
            this.Phone = "";
            this.DateOfBirth = DateTime.Today;

            Mode = enMode.AddNew;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson( this.FirstName,this.SecondName, this.LastName, this.NationalNo,
                        this.Gender,this.DateOfBirth, this.Phone , this.Email);
            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.LastName, this.NationalNo,
                        this.Gender, this.DateOfBirth, this.Phone, this.Email);
        }

        public static clsPerson Find(int ID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", LastName = "", Email = "", Phone = "";
            bool Gender = false;
            DateTime DateOfBirth = DateTime.Today;

            if (clsPersonData.GetPersonInfoByID(ID,  ref FirstName, ref SecondName, ref LastName,ref NationalNo,
           ref Gender, ref DateOfBirth,  ref Phone, ref Email ))
                return new clsPerson(ID,  FirstName, SecondName, LastName, NationalNo, Gender,DateOfBirth,
                    Phone, Email);
            else
                return null;
        }
        public static clsPerson Find(string NationalNo)
        {
            string  FirstName = "", SecondName = "", LastName = "", Email = "", Phone = "";
            bool Gender = false;
            DateTime DateOfBirth = DateTime.Today;
            int PersonID = -1;

            if (clsPersonData.GetPersonInfoByNationalNo(ref PersonID, ref FirstName, ref SecondName, ref LastName,  NationalNo,
           ref Gender, ref DateOfBirth, ref Phone, ref Email))
                return new clsPerson(PersonID, FirstName, SecondName, LastName, NationalNo, Gender, DateOfBirth,
                    Phone, Email);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdatePerson();
                default:
                    return false;
            }

        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        public static bool DeletePerson(int ID)
        {
            return (clsPersonData.DeletePerson(ID));
        }
        public static bool IsPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
    }
}
