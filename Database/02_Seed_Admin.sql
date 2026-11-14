USE TrainingCenterDB;
GO

SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF NOT EXISTS (SELECT 1 FROM Users WHERE UserName = N'admin')
BEGIN
    DECLARE @PersonID INT;

    INSERT INTO People (FirstName, SecondName, LastName, NationalNo, Gender, DateOfBirth, Phone, Email)
    VALUES (N'Demo', N'Admin', N'User', N'DEMO-0001', 0, '1990-01-01', N'0000000000', NULL);

    SET @PersonID = SCOPE_IDENTITY();

    INSERT INTO Users (PersonID, UserName, PasswordHash, PasswordSalt, IsActive, Role)
    VALUES (@PersonID, N'admin',
            N'Besy8/7ylkRhRBaUTM0u1uOpxM0U5wYZkP6S1rAebzc=',
            N'z1YeSzhufH9MlzTIkTMEtboxvLV/xjl6TdFIV7X9RxU=',
            1, 1);
END

COMMIT TRANSACTION;
GO