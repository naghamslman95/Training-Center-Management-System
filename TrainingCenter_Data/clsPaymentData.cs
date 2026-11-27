using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter_Data
{
    public class clsPaymentData
    {
        public static bool GetPaymentInfoByPaymentID(int PaymentID, ref int EnrollmentID, ref decimal PaymentAmount, 
            ref DateTime PaymentDate, ref int CreatedByUserID)
        {
            string query = "Select * from Payments where PaymentID = @PaymentID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PaymentID", SqlDbType.Int).Value = PaymentID;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        EnrollmentID = (int)reader["EnrollmentID"];
                        PaymentAmount = (decimal)reader["PaymentAmount"];
                        PaymentDate = (DateTime)reader["PaymentDate"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        return true;
                    }
                    return false;
                }
            }
        }

        public static int AddNewPayment(int EnrollmentID, decimal PaymentAmount, DateTime PaymentDate, int CreatedByUserID)
        {
            string query = @"Insert into Payments (EnrollmentID, PaymentAmount, PaymentDate, CreatedByUserID)
                                values (@EnrollmentID, @PaymentAmount, @PaymentDate, @CreatedByUserID);
                                   Select Scope_Identity()";


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@EnrollmentID", SqlDbType.Int).Value = EnrollmentID;
                SqlParameter priceParam = command.Parameters.Add("@PaymentAmount", SqlDbType.Decimal);
                priceParam.Precision = 10;
                priceParam.Scale = 2;
                priceParam.Value = PaymentAmount;
                command.Parameters.Add("@PaymentDate", SqlDbType.DateTime).Value = PaymentDate;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

                connection.Open();

                object result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }

        public static bool UpdatePayment(int PaymentID, decimal PaymentAmount)
        {
            string query = @"UPDATE Payments
                        SET PaymentAmount = @PaymentAmount
                      WHERE PaymentID = @PaymentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PaymentID", SqlDbType.Int).Value = PaymentID;
                SqlParameter priceParam = command.Parameters.Add("@PaymentAmount", SqlDbType.Decimal);
                priceParam.Precision = 10;
                priceParam.Scale = 2;
                priceParam.Value = PaymentAmount;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeletePayment(int PaymentID)
        {
            string query = @"Delete from Payments
                                    WHERE PaymentID = @PaymentID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PaymentID", SqlDbType.Int).Value = PaymentID;

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
        public static DataTable GetAllPayments()
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Payments.PaymentID, Payments.EnrollmentID, Courses.Title as Course, 
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.LastName )As StudentName,
                            Enrollments.TotalPrice, Payments.PaymentAmount, Payments.PaymentDate, Enrollments.EnrollmentStatus,
                            Users.UserName as CreatedBy
                            FROM Payments
                            INNER JOIN Enrollments
                                     ON Enrollments.EnrollmentID = Payments.EnrollmentID
                            INNER JOIN Courses
                                     ON Enrollments.CourseID = Courses.CourseID
                            INNER JOIN Students
                                      ON Enrollments.StudentID = Students.StudentID
                            INNER JOIN People
                                      ON People.PersonID = Students.PersonID
                            INNER JOIN Users
                                  ON Users.UserID = Payments.CreatedByUserID
                            ORDER BY Payments.PaymentDate Desc";

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

        public static DataTable GetStudentPayments(int StudentID)
        {
            DataTable DT = new DataTable();

            string query = @"SELECT
                            Payments.PaymentID, Payments.EnrollmentID, Courses.Title,
                            Payments.PaymentAmount, Payments.PaymentDate,
                            Users.UserName as CreatedBy
                            FROM Payments
                            INNER JOIN Enrollments
                                     ON Enrollments.EnrollmentID = Payments.EnrollmentID
                            INNER JOIN Courses
                                     ON Enrollments.CourseID = Courses.CourseID
                            INNER JOIN Users
                                  ON Users.UserID = Payments.CreatedByUserID
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

        public static decimal GetStudentPaymentsForCourse(int StudentID, int CourseID)
        {
            string query = @"Select SUM(Payments.PaymentAmount) from Payments 
                             Inner Join Enrollments 
                                      On Enrollments.EnrollmentID = Payments.EnrollmentID
                             WHERE StudentID = @StudentID
                             And CourseID = @CourseID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                connection.Open();

                object result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;
                return Convert.ToDecimal(result);
            }
        }
        public static decimal GetTotalPaidByEnrollmentID(int EnrollmentID)
        {
            string query = @"Select SUM(Payments.PaymentAmount) from Payments 
                             WHERE EnrollmentID = @EnrollmentID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@EnrollmentID", SqlDbType.Int).Value = EnrollmentID;
                connection.Open();

                object result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;
                return Convert.ToDecimal(result);
            }
        }
        public static decimal GetCollectedRevenuePerMonth(DateTime Start, DateTime NextMonthStart)
        {
            string query = @"Select ISNULL(SUM(Payments.PaymentAmount), 0) from Payments 
                              WHERE Payments.PaymentDate >= @Start
                                AND Payments.PaymentDate < @NextMonthStart ";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Start", SqlDbType.DateTime).Value = Start;
                command.Parameters.Add("@NextMonthStart", SqlDbType.DateTime).Value = NextMonthStart;
                connection.Open();

                object result = command.ExecuteScalar();
                return Convert.ToDecimal(result);
            }
        }
        public static decimal GetPaidForMonthEnrollments(DateTime Start, DateTime NextMonthStart)
        {
            string query = @"Select ISNULL(SUM(Payments.PaymentAmount), 0) from Payments 
                              Inner Join Enrollments 
                                      On Enrollments.EnrollmentID = Payments.EnrollmentID
                              WHERE Enrollments.EnrollmentDate >= @Start
                                AND Enrollments.EnrollmentDate < @NextMonthStart 
                                AND EnrollmentStatus <> 2";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Start", SqlDbType.DateTime).Value = Start;
                command.Parameters.Add("@NextMonthStart", SqlDbType.DateTime).Value = NextMonthStart;
                connection.Open();

                object result = command.ExecuteScalar();
                return Convert.ToDecimal(result);
            }
        }
        public static decimal GetTotalPaidByCourseID(int CourseID)
        {
            string query = @"Select ISNULL(SUM(Payments.PaymentAmount), 0) from Payments
                             Inner Join Enrollments 
                                      On Enrollments.EnrollmentID = Payments.EnrollmentID
                             WHERE CourseID = @CourseID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CourseID", SqlDbType.Int).Value = CourseID;
                connection.Open();

                object result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;
                return Convert.ToDecimal(result);
            }
        }
    }
}
