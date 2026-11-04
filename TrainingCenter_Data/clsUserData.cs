using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingCenter_Data
{
    public class clsUserData
    {
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName,
           ref string PasswordHash, ref string PasswordSalt, ref bool IsActive, ref byte Role)
        {
            string query = "Select * from users where UserID = @UserID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PersonID = (int)reader["PersonID"];
                        UserName = (string)reader["UserName"];
                        PasswordHash = (string)reader["PasswordHash"];
                        PasswordSalt = (string)reader["PasswordSalt"];
                        IsActive = (bool)reader["IsActive"];
                        Role = (byte)reader["Role"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool GetUserInfoByPersonID(ref int UserID, int PersonID, ref string UserName,
          ref string PasswordHash, ref string PasswordSalt, ref bool IsActive, ref byte Role)
        {
            string query = "Select * from users where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserID = (int)reader["UserID"];
                        UserName = (string)reader["UserName"];
                        PasswordHash = (string)reader["PasswordHash"];
                        PasswordSalt = (string)reader["PasswordSalt"];
                        IsActive = (bool)reader["IsActive"];
                        Role = (byte)reader["Role"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool GetUserInfoByUserName(ref int UserID, ref int PersonID, string UserName,
          ref string PasswordHash, ref string PasswordSalt, ref bool IsActive, ref byte Role)
        {
            string query = "Select * from users where UserName = @UserName";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@UserName", SqlDbType.NVarChar,50).Value = UserName;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserID = (int)reader["UserID"];
                        PersonID = (int)reader["PersonID"];
                        PasswordHash = (string)reader["PasswordHash"];
                        PasswordSalt = (string)reader["PasswordSalt"];
                        IsActive = (bool)reader["IsActive"];
                        Role = (byte)reader["Role"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static int AddNewUser(int PersonID, string UserName, string PasswordHash,
                        string PasswordSalt, bool IsActive, byte Role)
        {            
            string query = @"Insert into users (PersonID, UserName, PasswordHash,PasswordSalt, IsActive, Role)
                                values (@PersonID, @UserName, @PasswordHash, @PasswordSalt, @IsActive, @Role);
                                   Select Scope_Identity()";

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = UserName;
                    command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = PasswordHash;
                    command.Parameters.Add("@PasswordSalt", SqlDbType.NVarChar, 256).Value = PasswordSalt;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    command.Parameters.Add("@Role", SqlDbType.TinyInt).Value = Role;

                    connection.Open();

                    object result = command.ExecuteScalar();

                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    return -1;

                throw;
            }
        }

        public static bool UpdateUser(int UserID, string UserName, bool IsActive, byte Role)
        {
            string query = @"UPDATE Users
                        SET UserName = @UserName,
                            IsActive = @IsActive,
                            Role = @Role
                           
                      WHERE UserID = @UserID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = UserName;                   
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    command.Parameters.Add("@Role", SqlDbType.TinyInt).Value = Role;


                    connection.Open();

                    return command.ExecuteNonQuery() > 0;

                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    return false;

                throw;
            }
        }

        public static bool DeleteUser(int UserID)
        {
            string query = @"Delete from Users
                                    WHERE UserID = @UserID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    return false;

                throw;
            }
        }

        public static bool IsUserExistByUserID(int UserID)
        {
            string query = "Select 1 from Users where UserID = @UserID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                connection.Open();
                return (command.ExecuteScalar() != null);

            }
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            string query = "Select 1 from Users where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                connection.Open();
                return (command.ExecuteScalar() != null);
            }
        }

        public static bool IsUserExistByUserName(string UserName)
        {
            string query = "Select 1 from Users where UserName = @UserName";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@UserName", SqlDbType.NVarChar,50).Value = UserName;

                connection.Open();
                return (command.ExecuteScalar() != null);

            }
        }

        public static DataTable GetAllUsers()
        {
            DataTable DT = new DataTable();
         
            string query = @"SELECT
                            Users.UserID, Users.PersonID, 
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.LastName )As FullName,
                            Users.UserName, Users.IsActive, Users.Role
                            FROM Users
                            Inner join People on People.PersonID = Users.PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DT.Load(reader);
                    return DT;
                }
            }
        }

        public static bool ChangePassword(int UserID, string PasswordHash, string PasswordSalt)
        {
            string query = @"UPDATE Users
                        SET PasswordHash = @PasswordHash,
                            PasswordSalt = @PasswordSalt                           
                      WHERE UserID = @UserID";

             using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = PasswordHash;
                    command.Parameters.Add("@PasswordSalt", SqlDbType.NVarChar, 256).Value = PasswordSalt;

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;

                }
        }
    }
}
