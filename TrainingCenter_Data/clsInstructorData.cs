using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter_Data
{
    public class clsInstructorData
    {
        public static bool GetInstructorInfoByInstructorID(int InstructorID, ref int PersonID, ref string Qualifications, ref int CreatedByUserID)
        {
            string query = "Select * from Instructors where InstructorID = @InstructorID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@InstructorID", SqlDbType.Int).Value = InstructorID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PersonID = (int)reader["PersonID"];
                        Qualifications = (reader["Qualifications"] != DBNull.Value) ? (string)reader["Qualifications"] : "";
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool GetInstructorInfoByPersonID(ref int InstructorID, int PersonID, ref string Qualifications, ref int CreatedByUserID)
        {
            string query = "Select * from Instructors where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        InstructorID = (int)reader["InstructorID"];
                        Qualifications = (reader["Qualifications"] != DBNull.Value) ? (string)reader["Qualifications"] : "";
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static int AddNewInstructor(int PersonID, string Qualifications, int CreatedByUserID)
        {
            string query = @"Insert into Instructors (PersonID, Qualifications, CreatedByUserID)
                                values (@PersonID, @Qualifications, @CreatedByUserID);
                                   Select Scope_Identity()";

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                 
                    SqlParameter QualificationsParam = command.Parameters.Add("@Qualifications", SqlDbType.NVarChar, 200);
                    QualificationsParam.Value = string.IsNullOrEmpty(Qualifications) ? (object)DBNull.Value : Qualifications;

                    command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

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

        public static bool UpdateInstructor(int InstructorID, string Qualifications)
        {
            string query = @"UPDATE Instructors
                        SET Qualifications = @Qualifications
                           
                      WHERE InstructorID = @InstructorID";

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@InstructorID", SqlDbType.Int).Value = InstructorID;
                    SqlParameter QualificationsParam = command.Parameters.Add("@Qualifications", SqlDbType.NVarChar, 200);
                    QualificationsParam.Value = string.IsNullOrEmpty(Qualifications) ? (object)DBNull.Value : Qualifications;

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;

                }
        }

        public static bool DeleteInstructor(int InstructorID)
        {
            string query = @"Delete from Instructors
                                    WHERE InstructorID = @InstructorID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@InstructorID", SqlDbType.Int).Value = InstructorID;

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

        public static bool IsInstructorExistByInstructorID(int InstructorID)
        {
            string query = "Select 1 from Instructors where InstructorID = @InstructorID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@InstructorID", SqlDbType.Int).Value = InstructorID;

                connection.Open();
                return (command.ExecuteScalar() != null);

            }
        }

        public static bool IsInstructorExistByPersonID(int PersonID)
        {
            string query = "Select 1 from Instructors where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                connection.Open();
                return (command.ExecuteScalar() != null);
            }
        }

        public static DataTable GetAllInstructors()
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Instructors.InstructorID, Instructors.PersonID, 
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.LastName )As FullName,
                            Instructors.Qualifications, Users.UserName as CreatedBy
                            FROM Instructors
                            Inner join People on People.PersonID = Instructors.PersonID
                            Inner Join Users on Users.UserID = Instructors.CreatedByUserID";

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
