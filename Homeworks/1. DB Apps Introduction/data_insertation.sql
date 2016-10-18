INSERT INTO [dbo].[Countries] (CountryID, CountryName)
	VALUES (1, 'Bulgaria'),
		(2, 'Germany'),
		(3, 'USA'),
		(4, 'Russia'),
		(5, 'Moon')

INSERT INTO [dbo].[Towns] (TownID, TownName, CountryID)
	VALUES (1, 'New York', 3),
		(2, 'Berlin', 2),
		(3, 'Sofia', 1),
		(4, 'Moscow', 4),
		(5, 'MoonTown', 5)

INSERT INTO [dbo].[Minions] (MinionID, Name, Age, TownID)
	VALUES (1, 'Bob', 13, 1),
		(2, 'Kevin', 14, 4),
		(3, 'Steward', 19, 3),
		(4, 'Boiko Borisov', 57,  5),
		(5, 'Adolf Hitler', 126, 2)

INSERT INTO [dbo].[Villains] (VillainID, Name, EvilnessFactor)
	VALUES (1, 'Gru', 'good'),
		(2, 'Victor', 'evil'),
		(3, 'Victor JR', 'bad'),
		(4, 'Jilly', 'evil'),
		(5, 'Barak Obama', 'super evil')

INSERT INTO [dbo].[MinionsVillainsConnections] (VillainID, MinionID)
	VALUES (1, 1),
		(1, 2),
		(1, 3),
		(2, 1),
		(2, 5),
		(5, 4),
		(1, 5),
		(1, 4)