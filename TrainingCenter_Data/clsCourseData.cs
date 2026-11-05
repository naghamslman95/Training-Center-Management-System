using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingCenter_Data
{
    public class clsCourseData
    {
        public static bool GetCourseInfoByCourseID(int CourseID, ref int InstructorID, ref string Title,
            ref int Duration, ref decimal Price, ref int TotalSeats, ref int CreatedByUserID)
        {
            string query = "Select * from Courses where CourseID = @CourseID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        InstructorID = (int)reader["InstructorID"];
                        Title = (string)reader["Title"];
                        Duration = (int)reader["Duration"];
                        Price = (decimal)reader["Price"];
                        TotalSeats = (int)reader["TotalSeats"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static int AddNewCourse(int InstructorID, string Title, int Duration, decimal Price,
            int TotalSeats, int CreatedByUserID)
        {
            string query = @"Insert into Courses (InstructorID, Title, Duration, Price, TotalSeats, CreatedByUserID)
                                values (@InstructorID, @Title, @Duration, @Price, @TotalSeats, @CreatedByUserID);
                                   Select Scope_Identity()";


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@InstructorID", SqlDbType.Int).Value = InstructorID;
                command.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = Title;
                command.Parameters.Add("@Duration", SqlDbType.Int).Value = Duration;
                SqlParameter priceParam = command.Parameters.Add("@Price", SqlDbType.Decimal);
                priceParam.Precision = 10;
                priceParam.Scale = 2;
                priceParam.Value = Price;
                command.Parameters.Add("@TotalSeats", SqlDbType.Int).Value = TotalSeats;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

                connection.Open();

                object result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }

        public static bool UpdateCourse(int CourseID, int InstructorID, string Title, int Duration, decimal Price,
            int TotalSeats)
        {
            string query = @"UPDATE Courses
                        SET Title = @Title,
                            InstructorID = @InstructorID,
                            Duration = @Duration,
                            Price = @Price,
                            TotalSeats = @TotalSeats                           
                      WHERE CourseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                command.Parameters.Add("@InstructorID", SqlDbType.Int).Value = InstructorID;
                command.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = Title;
                command.Parameters.Add("@Duration", SqlDbType.Int).Value = Duration;
                SqlParameter priceParam = command.Parameters.Add("@Price", SqlDbType.Decimal);
                priceParam.Precision = 10;
                priceParam.Scale = 2;
                priceParam.Value = Price;
                command.Parameters.Add("@TotalSeats", SqlDbType.Int).Value = TotalSeats;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteCourse(int CourseID)
        {
            string query = @"Delete from Courses
                                    WHERE CourseID = @CourseID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;

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

        public static bool IsCourseExistByCourseID(int CourseID)
        {
            string query = "Select 1 from Courses where CourseID = @CourseID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;

                connection.Open();
                return (command.ExecuteScalar() != null);

            }
        }

        public static DataTable GetAllCourses()
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Courses.CourseID, Courses.InstructorID, 
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.LastName )As FullName,
                            Courses.Title, Courses.Duration, Courses.Price,Courses.TotalSeats,
                            Users.UserName as CreatedBy
                            FROM Courses
                            INNER JOIN Instructors
                                     ON Instructors.InstructorID = Courses.InstructorID
                            INNER JOIN People
                                      ON People.PersonID = Instructors.PersonID
                            INNER JOIN Users
                                  ON Users.UserID = Courses.CreatedByUserID";

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
