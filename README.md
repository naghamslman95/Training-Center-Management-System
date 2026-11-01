# Training Center Management System

A Windows desktop application for managing a training center: students, instructors, courses, enrollments, and payments.

> **Status:** in development.

## Tech Stack

- C# Windows Forms (.NET Framework 4.7.2)
- SQL Server
- 3-tier architecture: Presentation (UI) / Business Logic (BLL) / Data Access (DAL), 
   each in a separate Class Library project. Dependencies flow one way: UI → BLL → DAL.

## Getting Started

### Prerequisites

- Visual Studio (with the .NET desktop development workload)
- SQL Server (Express or LocalDB is enough) and SQL Server Management Studio (SSMS)

### Setup

1. Clone the repository and open the solution in Visual Studio.
2. Open `Database/TrainingCenterDB.sql` in SSMS and execute it.
   This creates the `TrainingCenterDB` database and all its tables.
3. In the Presentation project folder, copy `App.example.config` and rename the copy to `App.config`.
4. In `App.config`, replace `YOUR_SERVER_NAME` with your SQL Server instance name
   (the same name you type in the "Server name" box when connecting in SSMS).
   Common values: `.`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`.
5. Build and run.

The connection string uses Windows Authentication, so no password is stored in the repository. `App.config` is listed in `.gitignore`, so your local settings are never committed.

## Database

The schema consists of seven tables: `People`, `Users`, `Students`, `Instructors`, `Courses`, `Enrollments`, and `Payments`.