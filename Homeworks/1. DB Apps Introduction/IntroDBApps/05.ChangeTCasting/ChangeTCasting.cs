using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05.ChangeTCasting
{
    class ChangeTCasting
    {
        static void Main()
        {
            IList<string> townNames = new List<string>();

            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);

            string country = Console.ReadLine().ToLower();

            connection.Open();

            using(connection)
            {
                int numberOfTowns = CountTownsForCountry(country, connection);

                if (numberOfTowns == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }

                else
                {
                    Console.WriteLine("{0} town names were affected.", numberOfTowns);

                    townNames = ChangeTownNames(country, connection);

                    Console.Write("[");
                    Console.Write(string.Join(", ", townNames));
                    Console.WriteLine("]");
                }
            }

            connection.Close();
        }

        private static IList<string> ChangeTownNames(string country, SqlConnection connection)
        {
            IList<string> townNames = new List<string>();

            string commandString = "SELECT UPPER(t.TownName) AS TownNames " +
                                   "FROM Towns AS t " +
                                   "INNER JOIN Countries AS c " +
                                   "ON c.CountryID = t.CountryID " +
                                   "WHERE c.CountryName = @countryName";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@countryName", country);

            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        townNames.Add(reader[i].ToString());
                    }
                }
            }

            return townNames;

        }

        private static int CountTownsForCountry(string country, SqlConnection connection)
        {
            int numberOfTowns = 0;

            string commandString = "SELECT COUNT(*) AS [NumberOfTowns] " +
                                   "FROM Countries AS c " +
                                   "LEFT OUTER JOIN Towns AS t " +
                                   "ON t.CountryID = c.CountryID " +
                                   "WHERE c.CountryName = @countryName";

            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@countryName", country);

            numberOfTowns = (int)command.ExecuteScalar();

            return numberOfTowns;
        }
    }
}
