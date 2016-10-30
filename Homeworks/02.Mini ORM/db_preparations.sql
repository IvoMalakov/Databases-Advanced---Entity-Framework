CREATE DATABASE MyWebSiteDatabase
GO

USE MyWebSiteDatabase
GO

CREATE TABLE Users(
	Id INT IDENTITY PRIMARY KEY,
	Username VARCHAR(MAX),
	Pass VARCHAR(MAX),
	Age INT,
	RegistrationDate DATETIME
)

SET IDENTITY_INSERT Users ON
INSERT INTO Users(Id, Username, Pass, Age, RegistrationDate)
	VALUES(6, 'Nasko', 'Niama pass', 12, GETDATE())

SET IDENTITY_INSERT Users OFF