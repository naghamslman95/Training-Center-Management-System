using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter_Data
{
    public class clsEnrollmentData
    {
        public static bool GetEnrollmentInfoByEnrollmentID(int EnrollmentID, ref int CourseID, ref int StudentID, 
            ref decimal TotalPrice, ref DateTime EnrollmentDate, ref byte EnrollmentStatus, ref int CreatedByUserID)
        {
            string query = "Select * from Enrollments where EnrollmentID = @EnrollmentID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@EnrollmentID", SqlDbType.Int).Value = EnrollmentID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CourseID = (int)reader["CourseID"];
                        StudentID = (int)reader["StudentID"];
                        TotalPrice = (decimal)reader["TotalPrice"];
                        EnrollmentDate = (DateTime)reader["EnrollmentDate"];
                        EnrollmentStatus = (Byte)reader["EnrollmentStatus"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static int AddNewEnrollment(int CourseID, int StudentID, decimal TotalPrice, DateTime EnrollmentDate,
             byte EnrollmentStatus, int CreatedByUserID)
        {
            string query = @"Insert into Enrollments (CourseID, StudentID, TotalPrice, EnrollmentDate, EnrollmentStatus, CreatedByUserID)
                                values (@CourseID, @StudentID, @TotalPrice, @EnrollmentDate, @EnrollmentStatus, @CreatedByUserID);
                                   Select Scope_Identity()";


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                SqlParameter priceParam = command.Parameters.Add("@TotalPrice", SqlDbType.Decimal);
                priceParam.Precision = 10;
                priceParam.Scale = 2;
                priceParam.Value = TotalPrice;
                command.Parameters.Add("@EnrollmentDate", SqlDbType.DateTime).Value = EnrollmentDate;
                command.Parameters.Add("@EnrollmentStatus", SqlDbType.TinyInt).Value = EnrollmentStatus;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

                connection.Open();

                object result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }

        public static bool UpdateEnrollment(int EnrollmentID, int CourseID, int StudentID, decimal TotalPrice,
             byte EnrollmentStatus)
        {
            string query = @"UPDATE Enrollments
                        SET CourseID = @CourseID,
                            StudentID = @StudentID,
                            TotalPrice = @TotalPrice,
                            EnrollmentStatus = @EnrollmentStatus

                      WHERE EnrollmentID = @EnrollmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@EnrollmentID", SqlDbType.Int).Value = EnrollmentID;
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                SqlParameter priceParam = command.Parameters.Add("@TotalPrice", SqlDbType.Decimal);
                priceParam.Precision = 10;
                priceParam.Scale = 2;
                priceParam.Value = TotalPrice;
                command.Parameters.Add("@EnrollmentStatus", SqlDbType.TinyInt).Value = EnrollmentStatus;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool UpdateEnrollmentStatus(int EnrollmentID, byte EnrollmentStatus)
        {
            string query = @"UPDATE Enrollments
                        SET EnrollmentStatus = @EnrollmentStatus
                      WHERE EnrollmentID = @EnrollmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@EnrollmentID", SqlDbType.Int).Value = EnrollmentID;
                command.Parameters.Add("@EnrollmentStatus", SqlDbType.TinyInt).Value = EnrollmentStatus;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteEnrollment(int EnrollmentID)
        {
            string query = @"Delete from Enrollments
                                    WHERE EnrollmentID = @EnrollmentID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@EnrollmentID", SqlDbType.Int).Value = EnrollmentID;

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

        public static DataTable GetAllEnrollments()
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Enrollments.EnrollmentID, Courses.Title, 
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.LastName )As StudentName,
                            Enrollments.TotalPrice, Enrollments.EnrollmentDate, Enrollments.EnrollmentStatus,
                            Users.UserName as CreatedBy
                            FROM Enrollments
                            INNER JOIN Courses
                                     ON Enrollments.CourseID = Courses.CourseID
                            INNER JOIN Students
                                      ON Enrollments.StudentID = Students.StudentID
                            INNER JOIN People
                                      ON People.PersonID = Students.PersonID
                            INNER JOIN Users
                                  ON Users.UserID = Enrollments.CreatedByUserID";

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

        public static DataTable GetStudentEnrollmentsByStudentID(int StudentID)
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Enrollments.EnrollmentID, Courses.Title, 
                            Enrollments.TotalPrice, Enrollments.EnrollmentDate, Enrollments.EnrollmentStatus,
                            Users.UserName as CreatedBy
                            FROM Enrollments
                            INNER JOIN Courses
                                     ON Enrollments.CourseID = Courses.CourseID
                            INNER JOIN Users
                                  ON Users.UserID = Enrollments.CreatedByUserID
                            Where Enrollments.StudentID = @StudentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DT.Load(reader);
                    return DT;
                }
            }
        }
        public static DataTable GetCourseEnrollmentsByCourseID(int CourseID)
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Enrollments.EnrollmentID, 
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.LastName )As StudentName,
                            Enrollments.TotalPrice, Enrollments.EnrollmentDate, Enrollments.EnrollmentStatus,
                            Users.UserName as CreatedBy
                            FROM Enrollments
                            INNER JOIN Students
                                     ON Students.StudentID = Enrollments.StudentID
                            INNER JOIN People
                                     ON People.PersonID = Students.PersonID
                            INNER JOIN Users
                                  ON Users.UserID = Enrollments.CreatedByUserID
                            Where Enrollments.CourseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DT.Load(reader);
                    return DT;
                }
            }
        }

        public static int GetStudentActiveEnrollmentForCourse(int StudentID, int CourseID)
        {
            string query = @"Select EnrollmentID from Enrollments 
                             WHERE StudentID = @StudentID
                             And CourseID = @CourseID
                             And EnrollmentStatus = 1";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                         return (int)reader["EnrollmentID"];
                }
                return -1;
            }
        }

        public static bool DoesStudentHaveActiveEnrollment(int StudentID, int CourseID)
        {
            return (GetStudentActiveEnrollmentForCourse(StudentID, CourseID) != -1);
        }

        public static bool DoesCourseHaveEnrollments(int CourseID)
        {
            string query = @"Select 1 from Enrollments Where CourseID = @CourseID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;

                connection.Open();
                return (command.ExecuteScalar() != null);

            }
        }
        public static int ActiveEnrollmentsPerCourse(int CourseID)
        {
            string query = @"Select Count (Enrollments.EnrollmentID) from Enrollments 
                             WHERE CourseID = @CourseID
                             And EnrollmentStatus = 1 ";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                connection.Open();

                object result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public static DataTable GetEnrollmentsCountByStatus(DateTime Start, DateTime NextMonthStart)
        {
            DataTable DT = new DataTable();
            string query = @"Select Count(*) as Total, EnrollmentStatus from Enrollments 
                             WHERE Enrollments.EnrollmentDate >= @Start
                                AND Enrollments.EnrollmentDate < @NextMonthStart
                             Group by EnrollmentStatus";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Start", SqlDbType.DateTime).Value = Start;
                command.Parameters.Add("@NextMonthStart", SqlDbType.DateTime).Value = NextMonthStart;
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
