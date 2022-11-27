using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRUDApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=adonetdb;Trusted_Connection=True;";

            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст:");
            int age = Int32.Parse(Console.ReadLine());

            string sqlExpression = $"INSERT INTO Users (Name, Age) VALUES ('{name}', {age})";
            /*
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();
                command.CommandText = "CREATE TABLE Users (Id INT PRIMARY KEY IDENTITY, Age INT NOT NULL, Name NVARCHAR(100) NOT NULL)";
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();

                Console.WriteLine("Таблица Users создана");
            }
            */
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();


                // добавление
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"Добавлено объектов: {number}");

                // обновление ранее добавленного объекта
                Console.WriteLine("Введите новое имя:");
                name = Console.ReadLine();
                sqlExpression = $"UPDATE Users SET Name='{name}' WHERE Age={age}";
                command.CommandText = sqlExpression;
                number = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"Обновлено объектов: {number}");
            }
            Console.Read();
        }
    }
}
