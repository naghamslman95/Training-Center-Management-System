using System;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TrainingCenter_Data;

namespace TrainingCenter_Business
{
    public class clsUser
    {
        const int Iterations = 100000;
        const int Bytes = 32;
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; } = enMode.AddNew;

        public enum enRole { Admin = 1, Employee = 2};

        private string _Password;
   
        public int PersonID { get; set; }
        public int UserID { get; private set; }
        public string UserName { get; set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public bool IsActive { get; set; }
        public enRole Role { get; set; }

        public clsPerson PersonInfo;

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.PasswordHash = "";
            this.PasswordSalt = "";
            this.IsActive = false;
            this.Role = enRole.Employee;

            Mode = enMode.AddNew;
        }
        private clsUser(int UserID, int PersonID, string UserName, string PasswordHash, string PasswordSalt, bool IsActive, enRole Role)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.PasswordHash = PasswordHash;
            this.PasswordSalt = PasswordSalt;
            this.IsActive = IsActive;
            this.Role = Role;
            this.PersonInfo = (PersonID != -1) ? clsPerson.Find(PersonID) : null;

            Mode = enMode.Update;
        }

        private static byte[] SaltGeneration()
        {
            byte[] salt = new byte[Bytes];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] Hashing(string Password, byte[] salt)
        {
            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(Bytes);
            }
            return hash;
        }

        public bool SetPassword(string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                _Password = null;
                return false;
            }
            _Password = Password;
            return true;
        }
        private bool _AddNewUser()
        {
            if (string.IsNullOrEmpty(_Password)) return false;
            if (this.PersonID <= 0) return false;

            byte[] salt = SaltGeneration();
            this.PasswordSalt = Convert.ToBase64String(salt);
            this.PasswordHash = Convert.ToBase64String(Hashing(_Password, salt));

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.PasswordHash, 
                this.PasswordSalt, this.IsActive, (byte)this.Role);

            if (this.UserID != -1)
            {
                _Password = "";
                return true;
            }
            return false;
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.IsActive, (byte)this.Role);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateUser();
                default:
                    return false;
            }

        }

        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", PasswordHash = "", PasswordSalt = "";
            bool IsActive = false;
            byte Role = 0;

            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref PasswordHash, ref PasswordSalt,
                                          ref IsActive, ref Role))
                return new clsUser(UserID, PersonID, UserName, PasswordHash, PasswordSalt,
                                IsActive, (enRole)Role);
            else
                return null;
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", PasswordHash = "", PasswordSalt = "";
            bool IsActive = false;
            byte Role = 0;

            if (clsUserData.GetUserInfoByPersonID(ref UserID, PersonID, ref UserName, ref PasswordHash, ref PasswordSalt,
                                          ref IsActive, ref Role))
                return new clsUser(UserID, PersonID, UserName, PasswordHash, PasswordSalt,
                                IsActive, (enRole)Role);
            else
                return null;
        }
        public static clsUser FindByUserName(string UserName)
        {
            int UserID = -1, PersonID = -1;
            string  PasswordHash = "", PasswordSalt = "";
            bool IsActive = false;
            byte Role = 0;

            if (clsUserData.GetUserInfoByUserName(ref UserID, ref PersonID, UserName, ref PasswordHash, ref PasswordSalt,
                                          ref IsActive, ref Role))
                return new clsUser(UserID, PersonID, UserName, PasswordHash, PasswordSalt,
                                IsActive, (enRole)Role);
            else
                return null;
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool DeleteUser(int ID)
        {
            return (clsUserData.DeleteUser(ID));
        }
        public static bool IsUserExistByUserID(int ID)
        {
            return clsUserData.IsUserExistByUserID(ID);
        }
        public static bool IsUserExistByPersonID(int ID)
        {
            return clsUserData.IsUserExistByPersonID(ID);
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            return clsUserData.IsUserExistByUserName(UserName);
        }

        public static clsUser Login(string UserName, string Password)
        {
            clsUser User = FindByUserName(UserName);
            if (User!=null)
            {
                byte[] Salt = Convert.FromBase64String(User.PasswordSalt);
                byte[] Hash = Hashing(Password, Salt);
                if (Hash.SequenceEqual(Convert.FromBase64String(User.PasswordHash)))
                    return User;
                else
                    return null;
            }
            return null;
        }

        public bool ChangePassword(string Password)
        {
            if (!SetPassword(Password))
            {
                return false;
            }
            if (Mode == enMode.AddNew) return false;
            byte[] salt = SaltGeneration();
            string NewSalt = Convert.ToBase64String(salt);
            string NewHash = Convert.ToBase64String(Hashing(_Password, salt));
            if (clsUserData.ChangePassword(this.UserID, NewHash, NewSalt))
            {
                this.PasswordHash = NewHash;
                this.PasswordSalt = NewSalt;
                _Password = "";
                return true;
            }
            return false;
        }

    }
}
