using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataReaderApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=adonetdb;Trusted_Connection=True;";

            string sqlExpression = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        // выводим названия столбцов
                        string columnName1 = reader.GetName(0);
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(2);

                        Console.WriteLine($"{columnName1}\t{columnName3}\t{columnName2}");
                        /*
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            object id = reader.GetValue(0);
                            object name = reader.GetValue(2);
                            object age = reader.GetValue(1);

                            Console.WriteLine($"{id} \t{name} \t{age}");
                        }*/
                        // 2 way
                        //while (await reader.ReadAsync())
                        //{
                        //    object id = reader["id"];
                        //    object name = reader["name"];
                        //    object age = reader["age"];
                        //    Console.WriteLine($"{id} \t{name} \t{age}");
                        //}
                        // 3 way
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(2);
                            int age = reader.GetInt32(1);

                            Console.WriteLine($"{id} \t{name} \t{age}");
                        }
                    }
                }
            }
            Console.Read();
        }
    }
}
