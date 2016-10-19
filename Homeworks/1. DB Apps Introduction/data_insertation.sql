INSERT INTO [dbo].[Countries] (CountryName)
	VALUES ('Bulgaria'),
		('Germany'),
		('USA'),
		('Russia'),
		('Moon')

INSERT INTO [dbo].[Towns] (TownName, CountryID)
	VALUES ('NewYork', 3),
		('Berlin', 2),
		('Sofia', 1),
		('Moscow', 4),
		('MoonTown', 5),
		('Sliven', 1)

INSERT INTO [dbo].[Minions] (Name, Age, TownID)
	VALUES ('Bob', 13, 1),
		('Kevin', 14, 4),
		('Steward', 19, 3),
		('Boiko Borisov', 57,  5),
		('Adolf Hitler', 126, 2),
		('Bob Dilan', 1, 1)

INSERT INTO [dbo].[Villains] (Name, EvilnessFactor)
	VALUES ('Gru', 'good'),
		('Victor', 'evil'),
		('VictorJR', 'bad'),
		('Jilly', 'evil'),
		('Barak Obama', 'super evil')

INSERT INTO [dbo].[MinionsVillains] (VillainID, MinionID)
	VALUES (1, 1),
		(1, 2),
		(1, 3),
		(2, 1),
		(2, 5),
		(5, 4),
		(1, 5),
		(1, 4)