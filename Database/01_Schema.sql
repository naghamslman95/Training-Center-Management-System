CREATE DATABASE TrainingCenterDB;
GO
USE TrainingCenterDB;

CREATE TABLE People(
    PersonID    INT IDENTITY(1,1) PRIMARY KEY,
    FirstName   NVARCHAR(50) NOT NULL,
    SecondName  NVARCHAR(50) NOT NULL,
    LastName    NVARCHAR(50) NOT NULL,
    NationalNo  NVARCHAR(20) NOT NULL UNIQUE,
    Gender      BIT NOT NULL,
    DateOfBirth DATE NOT NULL,
    Phone       NVARCHAR(50) NOT NULL,
    Email       NVARCHAR(100) NULL
    
);

CREATE TABLE Users(
    UserID        INT IDENTITY(1,1) PRIMARY KEY,
    PersonID      INT NOT NULL UNIQUE
        REFERENCES People(PersonID),
    UserName      NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash  NVARCHAR(256) NOT NULL,
    PasswordSalt  NVARCHAR(256) NOT NULL,
    IsActive      BIT  NOT NULL,
    Role          TINYINT NOT NULL CHECK (Role IN (1, 2))
);

CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    PersonID  INT NOT NULL UNIQUE
        REFERENCES People(PersonID),
    Status    TINYINT NOT NULL CHECK (Status IN (1, 2)),
    CreatedByUserID    INT NOT NULL 
        REFERENCES Users(UserID)
);

CREATE TABLE Instructors (
    InstructorID  INT IDENTITY(1,1) PRIMARY KEY,
    PersonID      INT NOT NULL UNIQUE
        REFERENCES People(PersonID),
    Qualifications NVARCHAR(200) NULL,
    CreatedByUserID    INT NOT NULL 
        REFERENCES Users(UserID)
);

CREATE TABLE Courses (
    CourseID    INT IDENTITY(1,1) PRIMARY KEY,
    InstructorID INT NOT NULL 
        REFERENCES Instructors (InstructorID),
    Title        NVARCHAR(50) NOT NULL,
    Duration     INT NOT NULL CHECK (Duration > 0),
    Price        DECIMAL(10,2) NOT NULL CHECK (Price > 0),
    TotalSeats   INT NOT NULL CHECK (TotalSeats > 0),
    CreatedByUserID INT NOT NULL 
        REFERENCES Users(UserID)
);

CREATE TABLE Enrollments(
    EnrollmentID     INT IDENTITY(1,1) PRIMARY KEY,
    CourseID         INT NOT NULL 
        REFERENCES Courses(CourseID),
    StudentID        INT NOT NULL 
        REFERENCES Students(StudentID),
    TotalPrice       DECIMAL(10,2) NOT NULL CHECK (TotalPrice >= 0),
    EnrollmentDate   DATETIME NOT NULL,
    EnrollmentStatus TINYINT NOT NULL CHECK (EnrollmentStatus IN (1, 2, 3)),
    CreatedByUserID    INT NOT NULL 
        REFERENCES Users(UserID)
);

CREATE TABLE Payments(
    PaymentID       INT IDENTITY(1,1) PRIMARY KEY,
    EnrollmentID    INT NOT NULL 
        REFERENCES Enrollments(EnrollmentID),
    PaymentAmount    DECIMAL(10,2) NOT NULL CHECK (PaymentAmount > 0),
    PaymentDate      DATETIME NOT NULL,
    CreatedByUserID    INT NOT NULL 
        REFERENCES Users(UserID)
);

