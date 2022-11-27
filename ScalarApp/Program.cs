using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ScalarApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=adonetdb;Trusted_Connection=True;";

            string sqlExpression = "SELECT COUNT(*) FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                object count = await command.ExecuteScalarAsync();

                command.CommandText = "SELECT MIN(Age) FROM Users";
                object minAge = await command.ExecuteScalarAsync();

                command.CommandText = "SELECT AVG(Age) FROM Users";
                object avgAge = await command.ExecuteScalarAsync();

                Console.WriteLine($"В таблице {count} объектa(ов)");
                Console.WriteLine($"Минимальный возраст: {minAge}");
                Console.WriteLine($"Средний возраст: {avgAge}");
            }
            Console.Read();
        }
    }
}
