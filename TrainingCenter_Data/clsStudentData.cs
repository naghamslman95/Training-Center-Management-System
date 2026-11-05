using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter_Data
{
    public class clsStudentData
    {
        public static bool GetStudentInfoByStudentID(int StudentID, ref int PersonID, ref byte Status, ref int CreatedByUserID)
        {
            string query = "Select * from Students where StudentID = @StudentID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        PersonID = (int)reader["PersonID"];
                        Status = (byte)reader["Status"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool GetStudentInfoByPersonID(ref int StudentID, int PersonID, ref byte Status, ref int CreatedByUserID)
        {
            string query = "Select * from Students where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        StudentID = (int)reader["StudentID"];
                        Status = (byte)reader["Status"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static int AddNewStudent(int PersonID, byte Status, int CreatedByUserID)
        {
            string query = @"Insert into Students (PersonID, Status, CreatedByUserID)
                                values (@PersonID, @Status, @CreatedByUserID);
                                   Select Scope_Identity()";

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = Status;
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

        public static bool UpdateStudent(int StudentID, byte Status)
        {
            string query = @"UPDATE Students
                        SET Status = @Status
                           
                      WHERE StudentID = @StudentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = Status;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteStudent(int StudentID)
        {
            string query = @"Delete from Students
                                    WHERE StudentID = @StudentID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;

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

        public static bool IsStudentExistByStudentID(int StudentID)
        {
            string query = "Select 1 from Students where StudentID = @StudentID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;

                connection.Open();
                return (command.ExecuteScalar() != null);

            }
        }

        public static bool IsStudentExistByPersonID(int PersonID)
        {
            string query = "Select 1 from Students where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                connection.Open();
                return (command.ExecuteScalar() != null);
            }
        }

        public static DataTable GetAllStudents()
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Students.StudentID, Students.PersonID, 
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.LastName )As FullName,
                            Students.Status, Users.UserName as CreatedBy
                            FROM Students
                            Inner join People on People.PersonID = Students.PersonID
                            Inner Join Users on Users.UserID = Students.CreatedByUserID";

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
