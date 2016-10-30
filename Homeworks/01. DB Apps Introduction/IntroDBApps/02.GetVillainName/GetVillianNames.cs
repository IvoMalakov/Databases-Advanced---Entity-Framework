using System;
using System.Data.SqlClient;

namespace _01.InitialSetup
{
    class GetVillianNames
    {
        static void Main()
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string selectionCommand = "SELECT v.Name, " +
                                         "COUNT (m.MinionID) AS [MinionsServed] " +
                                         "FROM Villains as v " +
                                         "INNER JOIN [dbo].[MinionsVillainsConnections] AS mv " +
                                         "ON mv.VillainID = v.VillainID " +
                                         "INNER JOIN [dbo].[Minions] AS m " +
                                         "ON m.MinionID = mv.MinionID " +
                                         "GROUP BY v.Name " +
                                         "HAVING COUNT(m.MinionID) > 3 " +
                                         "ORDER BY MinionsServed DESC";

            SqlCommand command = new SqlCommand(selectionCommand, connection);
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader[i]} ");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
