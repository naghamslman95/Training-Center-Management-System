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
2. Create the database. In SSMS, open and execute these scripts from the `Database/` folder, **in this order**:

   | Script | Purpose | Required? |
   |---|---|---|
   | `01_Schema.sql` | Creates the `TrainingCenterDB` database and all its tables | Yes |
   | `02_Seed_Admin.sql` | Creates the first admin account (without it, nobody can log in) | Yes |
   | `03_Seed_Demo.sql` | Adds fictional demo data so you can try every operation | Optional |

3. In the Presentation project folder, copy `App.example.config` and rename the copy to `App.config`.
4. In `App.config`, replace `YOUR_SERVER_NAME` with your SQL Server instance name
   (the same name you type in the "Server name" box when connecting in SSMS).
   Common values: `.`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`.
5. Build and run.

The connection string uses Windows Authentication, so no password is stored in the repository. `App.config` is listed in `.gitignore`, so your local settings are never committed.

## Demo Accounts

These accounts are created by the seed scripts, for trying the application (the login screen is still in development):

| Username | Password | Role | Created by |
|---|---|---|---|
| `admin` | `Demo@12345` | Admin | `02_Seed_Admin.sql` |
| `employee` | `Demo@12345` | Employee | `03_Seed_Demo.sql` |

> **Demo credentials only.** Change or remove these accounts before using the application with real data.
> All people, courses, enrollments, and payments in `03_Seed_Demo.sql` are fictional.

## Database

The schema consists of seven tables: `People`, `Users`, `Students`, `Instructors`, `Courses`, `Enrollments`, and `Payments`.

`02_Seed_Admin.sql` and `03_Seed_Demo.sql` are safe to run more than once: they skip rows that already exist.

`01_Schema.sql` creates the database itself, so running it a second time will fail. To start over, drop the database first and run the scripts again in order:

```sql
USE master;
GO
IF DB_ID(N'TrainingCenterDB') IS NOT NULL
BEGIN
    ALTER DATABASE TrainingCenterDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TrainingCenterDB;
END
GO
```