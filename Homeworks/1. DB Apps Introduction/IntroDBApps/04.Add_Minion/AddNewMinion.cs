using System;
using System.Data.SqlClient;

namespace AddMinionNamespace
{
    class AddNewMinion
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);

            string minionInput = Console.ReadLine();
            string villainInput = Console.ReadLine();

            string[] minionData = minionInput.Split(':')[1].Trim().Split(' ');
            string minionName = minionData[0];
            int minionAge = int.Parse(minionData[1]);
            string minionTown = minionData[2];

            string villainName = villainInput.Split(':')[1].Trim();

            using (connection)
            {
                connection.Open();

                bool townExists = CheckIfTownExists(minionTown, connection);

                if (!townExists)
                {
                    AddTown(minionTown, connection);
                    Console.WriteLine("Town {0} was added to the database.", minionTown);
                }

                bool vilainExists = CheckVillainExists(villainName, connection);

                if (!vilainExists)
                {
                    AddVillain(villainName, connection);
                    Console.WriteLine("Villain {0} Jimmy was added to the database.", villainName);
                }

                int townId = GetTownIdByTownName(minionTown, connection);
                AddMinion(minionName, townId, minionAge ,connection);

                int minionId = GetMinionIdByName(minionName, connection);
                int villainId = GetVillainIdByName(villainName, connection);

                AddMinionToVillain(minionId, villainId, connection);
                Console.WriteLine("Successfully added {0} to be minion of {1}.", minionName, villainName);
            }

            connection.Close();
        }

        private static int GetTownIdByTownName(string minionTown, SqlConnection connection)
        {
            int townId = 0;

            string commandString = "SELECT t.TownID " +
                                   "FROM Towns AS t " +
                                   "WHERE t.TownName = @townName";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@townName", minionTown);

            townId = (int)command.ExecuteScalar();

            return townId;
        }

        private static void AddMinionToVillain(int minionId, int villainId, SqlConnection connection)
        {
            string commandString = "INSERT INTO MinionsVillains (MinionID, VillainID) " +
                                   "VALUES (@minionId, @villainId)";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@minionId", minionId);
            command.Parameters.AddWithValue("@villainId", villainId);

            command.ExecuteNonQuery();
        }

        private static int GetVillainIdByName(string villainName, SqlConnection connection)
        {
            int villainId = 0;

            string commandString = "SELECT v.VillainID " +
                                   "FROM Villains AS v " +
                                   "WHERE v.Name = @villainName";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@villainName", villainName);

            villainId = (int)command.ExecuteScalar();

            return villainId;
        }

        private static int GetMinionIdByName(string minionName, SqlConnection connection)
        {
            int minionId = 0;

            string commandString = "SELECT m.MinionID " +
                                   "FROM Minions AS m " +
                                   "WHERE m.Name = @minionName";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@minionName", minionName);

            minionId = (int)command.ExecuteScalar();

            return minionId;
        }

        private static void AddMinion(string minionName, int townId, int age, SqlConnection connection)
        {
            string commandString = "INSERT INTO Minions (Name, Age, TownID) " +
                                   "VALUES (@minionName, @minionAge, @townId)";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@minionName", minionName);
            command.Parameters.AddWithValue("@minionAge", age);
            command.Parameters.AddWithValue("@townId", townId);

            command.ExecuteNonQuery();
        }

        private static void AddVillain(string villainName, SqlConnection connection)
        {
            string commandString = "INSERT INTO Villains(Name, EvilnessFactor) " +
                                   "VALUES (@villainName, 'evil')";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@villainName", villainName);

            command.ExecuteNonQuery();
        }

        private static bool CheckVillainExists(string villainName, SqlConnection connection)
        {
            string commandString = "SELECT COUNT(*) AS [VillainCounter] " +
                                   "FROM Villains AS v " +
                                   "WHERE v.Name = @villainName";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@villainName", villainName);

            int executeResult = (int)command.ExecuteScalar();

            if (executeResult == 0)
            {
                return false;
            }

            return true;
        }

        private static void AddTown(string minionTown, SqlConnection connection)
        {
            string commandString = "INSERT INTO Towns(TownName) " +
                                   "VALUES (@townName)";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@townName", minionTown);

            command.ExecuteNonQuery();
        }

        private static bool CheckIfTownExists(string minionTown, SqlConnection connection)
        {
            string commandString = "SELECT COUNT(*) AS [TownCounter] " + 
                                   "FROM Towns AS t " + 
                                   "WHERE t.TownName = @townName";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@townName", minionTown);

            int executeResult = (int)command.ExecuteScalar();

            if (executeResult == 0)
            {
                return false;
            }

            return true;
        }
    }
}