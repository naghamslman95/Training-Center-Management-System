using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingCenter_Data
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName, ref string LastName,
          ref string NationalNo, ref bool Gender, ref DateTime DateOfBirth, ref string Phone, ref string Email)
        {
            string query = "Select * from people where PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        FirstName = (string)reader["FirstName"];
                        SecondName = (string)reader["SecondName"];
                        LastName = (string)reader["LastName"];
                        NationalNo = (string)reader["NationalNo"];
                        Gender = (bool)reader["Gender"];
                        Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "";
                        Phone = (string)reader["Phone"];
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool GetPersonInfoByNationalNo(ref int PersonID, ref string FirstName, ref string SecondName, ref string LastName,
         string NationalNo, ref bool Gender, ref DateTime DateOfBirth, ref string Phone, ref string Email)
        {
            string query = "Select * from people where NationalNo = @NationalNo";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PersonID = (int)reader["PersonID"];
                        FirstName = (string)reader["FirstName"];
                        SecondName = (string)reader["SecondName"];
                        LastName = (string)reader["LastName"];
                        Gender = (bool)reader["Gender"];
                        Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "";
                        Phone = (string)reader["Phone"];
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                        return true;
                    }
                    return false;
                }
            }
        }

        public static int AddNewPerson(string FirstName, string SecondName, string LastName, string NationalNo,
            bool Gender, DateTime DateOfBirth, string Phone, string Email)
        {
            string query = @"Insert into people (FirstName, SecondName, LastName, NationalNo, Gender, DateOfBirth, Phone, Email)
                                values ( @FirstName, @SecondName, @LastName, @NationalNo, @Gender, @DateOfBirth, @Phone, @Email);
                                   Select Scope_Identity()";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
                    command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 50).Value = SecondName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
                    command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;
                    command.Parameters.Add("@Gender", SqlDbType.Bit).Value = Gender;

                    SqlParameter emailParam = command.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
                    emailParam.Value = string.IsNullOrEmpty(Email) ? (object)DBNull.Value : Email;

                    command.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = Phone;
                    command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DateOfBirth;

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

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName, string LastName,
            string NationalNo, bool Gender, DateTime DateOfBirth, string Phone, string Email)
        {
            string query = @"UPDATE People
                        SET FirstName = @FirstName,
                            SecondName = @SecondName,
                            LastName = @LastName,
                            NationalNo = @NationalNo,
                            Gender = @Gender,
                            DateOfBirth = @DateOfBirth,
                            Phone = @Phone,
                            Email = @Email

                      WHERE PersonID = @PersonID";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
                    command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 50).Value = SecondName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
                    command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;
                    command.Parameters.Add("@Gender", SqlDbType.Bit).Value = Gender;

                    SqlParameter emailParam = command.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
                    emailParam.Value = string.IsNullOrEmpty(Email) ? (object)DBNull.Value : Email;

                    command.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = Phone;
                    command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DateOfBirth;

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

        public static bool DeletePerson(int PersonID)
        {
            string query = @"DELETE FROM People
                     WHERE PersonID = @PersonID";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

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

        public static bool IsPersonExist(int PersonID)
        {
            string query = "Select 1 from People where PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                connection.Open();
                return (command.ExecuteScalar() != null);

            }
        }

        public static bool IsPersonExist(string NationalNo)
        {
            string query = "Select 1 from People where NationalNo = @NationalNo";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;

                connection.Open();
                return (command.ExecuteScalar() != null);
            }
        }

        public static DataTable GetAllPeople()
        {
            DataTable DT = new DataTable();
            string query = @"SELECT
                    PersonID, 
                    FirstName + ' ' + SecondName + ' ' + LastName AS FullName,
                    NationalNo, Gender, DateOfBirth , Phone, Email
                 FROM People
                    Order by People.FirstName";
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
    }
}
