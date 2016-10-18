CREATE DATABASE MinionsDB
GO

USE MinionsDB
GO

CREATE TABLE Countries
(
	CountryID INT PRIMARY KEY,
	CountryName VARCHAR(50) NOT NULL
)

CREATE TABLE Towns
(
	TownID INT PRIMARY KEY,
	TownName VARCHAR(50) NOT NULL,
	CountryID INT UNIQUE,
	CONSTRAINT FK_Towns_Countries FOREIGN KEY (CountryID)
		REFERENCES Countries (CountryID)
)


CREATE TABLE Minions
(
	MinionID INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	Age INT,
	TownID INT UNIQUE,
	CONSTRAINT FK_Minions_Towns FOREIGN KEY (TownID)
		REFERENCES Towns (TownID)
)

CREATE TABLE Villains
(
	VillainID INT PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	EvilnessFactor VARCHAR(50),
	CONSTRAINT CK_VillainsEvilnessFactor
		CHECK (EvilnessFactor IN ('good', 'bad', 'evil', 'super evil'))
)

CREATE TABLE MinionsVillainsConnections
(
	MinionID INT,
	VillainID INT,
	CONSTRAINT PK_MinionsVillainsConnections PRIMARY KEY (MinionID, VillainID),
	CONSTRAINT FK_PK_MinionsVillainsConnections_Minions FOREIGN KEY (MinionID)
		REFERENCES Minions (MinionID),
	CONSTRAINT FK_PK_MinionsVillainsConnections_Villains FOREIGN KEY (VillainID)
		REFERENCES Villains (VillainID)
)