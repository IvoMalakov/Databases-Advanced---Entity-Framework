using System;
using System.Data.SqlClient;

namespace _01.InitialSetup
{
    class Connection
    {
        static void Main()
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using (connection)
            {
              
            }
        }
    }
}
