USE TrainingCenterDB;
GO

-- =====================================================
-- Demo data (all fictional). Run after 01_Schema.sql and 02_Seed_Admin.sql
-- DEMO-0002          : employee
-- DEMO-0003 .. 0007  : students
-- DEMO-0008 .. 0011  : instructors
-- =====================================================

-- Part 1: People
SET XACT_ABORT ON;
BEGIN TRANSACTION;

INSERT INTO People (FirstName, SecondName, LastName, NationalNo, Gender, DateOfBirth, Phone, Email)
SELECT v.FirstName, v.SecondName, v.LastName, v.NationalNo, v.Gender, v.DateOfBirth, v.Phone, v.Email
FROM (VALUES
    -- Employee
    (N'Layla',   N'Omar',    N'Haddad',  N'DEMO-0002', 1, '1993-05-19', N'0000000002', N'layla.haddad@example.com'),
    -- Students
    (N'Ahmad',   N'Khaled',  N'Nasser',  N'DEMO-0003', 0, '1998-04-12', N'0000000003', N'ahmad.nasser@example.com'),
    (N'Maya',    N'Samir',   N'Khoury',  N'DEMO-0004', 1, '2001-09-23', N'0000000004', N'maya.khoury@example.com'),
    (N'John',    N'Michael', N'Carter',  N'DEMO-0005', 0, '1995-11-05', N'0000000005', NULL),
    (N'Emma',    N'Rose',    N'Walker',  N'DEMO-0006', 1, '2003-02-17', N'0000000006', N'emma.walker@example.com'),
    (N'Yousef',  N'Hassan',  N'Ali',     N'DEMO-0007', 0, '2000-07-30', N'0000000007', N'yousef.ali@example.com'),
    -- Instructors
    (N'Omar',    N'Faisal',  N'Mansour', N'DEMO-0008', 0, '1982-06-14', N'0000000008', N'omar.mansour@example.com'),
    (N'Nadia',   N'Karim',   N'Saleh',   N'DEMO-0009', 1, '1985-01-28', N'0000000009', N'nadia.saleh@example.com'),
    (N'David',   N'James',   N'Miller',  N'DEMO-0010', 0, '1978-10-09', N'0000000010', NULL),
    (N'Rana',    N'Tariq',   N'Abbas',   N'DEMO-0011', 1, '1988-03-21', N'0000000011', N'rana.abbas@example.com')
) AS v (FirstName, SecondName, LastName, NationalNo, Gender, DateOfBirth, Phone, Email)
WHERE NOT EXISTS (SELECT 1 FROM People p WHERE p.NationalNo = v.NationalNo);

COMMIT TRANSACTION;
GO

-- Part 2: Users (employee linked to DEMO-0002)
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DECLARE @EmployeePersonID INT =
    (SELECT PersonID FROM People WHERE NationalNo = N'DEMO-0002');

IF @EmployeePersonID IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM Users WHERE UserName = N'employee')
BEGIN
    INSERT INTO Users (PersonID, UserName, PasswordHash, PasswordSalt, IsActive, Role)
    VALUES (@EmployeePersonID, N'employee',
            N'xlvtuDSUGmb5ygxh5BCOZ80ND6vYtqafztFcJ3LpnUE=',
            N'Yi7mVAhdzjB9PYlx21+1EiB+2MtKKjroupBO9jVPeyQ=',
            1, 2);
END

COMMIT TRANSACTION;
GO

-- Part 3: Students, Instructors, Courses
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DECLARE @AdminID INT = (SELECT UserID FROM Users WHERE UserName = N'admin');

-- Students (DEMO-0003 .. DEMO-0007)
INSERT INTO Students (PersonID, Status, CreatedByUserID)
SELECT p.PersonID, 1, @AdminID
FROM People p
WHERE p.NationalNo IN (N'DEMO-0003', N'DEMO-0004', N'DEMO-0005', N'DEMO-0006', N'DEMO-0007')
  AND NOT EXISTS (SELECT 1 FROM Students s WHERE s.PersonID = p.PersonID);

-- Instructors (DEMO-0008 .. DEMO-0011)
INSERT INTO Instructors (PersonID, Qualifications, CreatedByUserID)
SELECT p.PersonID, v.Qualifications, @AdminID
FROM (VALUES
    (N'DEMO-0008', N'MSc in Computer Science'),
    (N'DEMO-0009', N'BSc in Information Systems, Oracle Certified'),
    (N'DEMO-0010', N'Certified Microsoft Office Specialist'),
    (N'DEMO-0011', N'BSc in Education')
) AS v (NationalNo, Qualifications)
JOIN People p ON p.NationalNo = v.NationalNo
WHERE NOT EXISTS (SELECT 1 FROM Instructors i WHERE i.PersonID = p.PersonID);

-- Courses (DEMO-0011 has no course on purpose; Python has only 3 seats)
INSERT INTO Courses (InstructorID, Title, Duration, Price, TotalSeats, CreatedByUserID)
SELECT i.InstructorID, v.Title, v.Duration, v.Price, v.TotalSeats, @AdminID
FROM (VALUES
    (N'DEMO-0008', N'Introduction to Programming with C#', 40, 200.00, 15),
    (N'DEMO-0008', N'Introduction to Python',              30, 150.00,  3),
    (N'DEMO-0009', N'Database Management with SQL',        35, 180.00, 15),
    (N'DEMO-0010', N'Computer Skills for Beginners',       20, 100.00, 20)
) AS v (InstructorNationalNo, Title, Duration, Price, TotalSeats)
JOIN People p ON p.NationalNo = v.InstructorNationalNo
JOIN Instructors i ON i.PersonID = p.PersonID
WHERE NOT EXISTS (SELECT 1 FROM Courses c WHERE c.Title = v.Title);

COMMIT TRANSACTION;
GO

-- Part 4: Enrollments (status: 1 Active, 2 Cancelled, 3 Completed)
-- Python is full: 3 Active enrollments for 3 seats (Cancelled does not take a seat)
-- DEMO-0007 has no enrollments on purpose
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DECLARE @EmployeeID INT = (SELECT UserID FROM Users WHERE UserName = N'employee');

INSERT INTO Enrollments (CourseID, StudentID, TotalPrice, EnrollmentDate, EnrollmentStatus, CreatedByUserID)
SELECT c.CourseID, s.StudentID, v.TotalPrice, v.EnrollmentDate, v.Status, @EmployeeID
FROM (VALUES
    (N'DEMO-0003', N'Introduction to Python',              150.00, '2026-09-01T10:00:00', 1),
    (N'DEMO-0004', N'Introduction to Python',              150.00, '2026-09-02T11:30:00', 1),
    (N'DEMO-0005', N'Introduction to Python',              120.00, '2026-09-03T09:15:00', 1),  -- discounted
    (N'DEMO-0006', N'Introduction to Python',              150.00, '2026-09-04T14:00:00', 2),  -- cancelled
    (N'DEMO-0003', N'Introduction to Programming with C#', 200.00, '2026-06-10T10:00:00', 3),  -- completed
    (N'DEMO-0004', N'Database Management with SQL',        180.00, '2026-10-05T12:00:00', 1),
    (N'DEMO-0006', N'Computer Skills for Beginners',         0.00, '2026-10-12T13:45:00', 1)   -- free
) AS v (StudentNationalNo, CourseTitle, TotalPrice, EnrollmentDate, Status)
JOIN People p ON p.NationalNo = v.StudentNationalNo
JOIN Students s ON s.PersonID = p.PersonID
JOIN Courses c ON c.Title = v.CourseTitle
WHERE NOT EXISTS (SELECT 1 FROM Enrollments e
                  WHERE e.CourseID = c.CourseID AND e.StudentID = s.StudentID);

COMMIT TRANSACTION;
GO

-- Part 5: Payments (sum per enrollment never exceeds TotalPrice)
-- DEMO-0003 Python: paid in full (100 + 50), DEMO-0003 C#: paid in full
-- DEMO-0004 Python: partial, DEMO-0004 SQL: partial
-- DEMO-0005 Python: no payments
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DECLARE @EmployeeID INT = (SELECT UserID FROM Users WHERE UserName = N'employee');

INSERT INTO Payments (EnrollmentID, PaymentAmount, PaymentDate, CreatedByUserID)
SELECT e.EnrollmentID, v.Amount, v.PaymentDate, @EmployeeID
FROM (VALUES
    (N'DEMO-0003', N'Introduction to Python',              100.00, '2026-09-01T10:10:00'),
    (N'DEMO-0003', N'Introduction to Python',               50.00, '2026-09-15T10:00:00'),
    (N'DEMO-0004', N'Introduction to Python',               50.00, '2026-09-02T11:40:00'),
    (N'DEMO-0003', N'Introduction to Programming with C#', 200.00, '2026-06-10T10:05:00'),
    (N'DEMO-0004', N'Database Management with SQL',         90.00, '2026-10-05T12:10:00')
) AS v (StudentNationalNo, CourseTitle, Amount, PaymentDate)
JOIN People p ON p.NationalNo = v.StudentNationalNo
JOIN Students s ON s.PersonID = p.PersonID
JOIN Courses c ON c.Title = v.CourseTitle
JOIN Enrollments e ON e.StudentID = s.StudentID AND e.CourseID = c.CourseID
WHERE NOT EXISTS (SELECT 1 FROM Payments pay
                  WHERE pay.EnrollmentID = e.EnrollmentID
                    AND pay.PaymentAmount = v.Amount
                    AND pay.PaymentDate = v.PaymentDate);

COMMIT TRANSACTION;
GO