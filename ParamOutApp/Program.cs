using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamOutApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=adonetdb;Trusted_Connection=True;";

            int age = 23;
            string name = "Kenny";
            string sqlExpression = "INSERT INTO Users (Name, Age) VALUES (@name, @age);SET @id=SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                // создаем параметр для имени
                SqlParameter nameParam = new SqlParameter("@name", name);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                // создаем параметр для возраста
                SqlParameter ageParam = new SqlParameter("@age", age);
                // добавляем параметр к команде
                command.Parameters.Add(ageParam);
                // параметр для id
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output // параметр выходной
                };
                command.Parameters.Add(idParam);

                await command.ExecuteNonQueryAsync();

                // получим значения выходного параметра
                Console.WriteLine($"Id нового объекта: {idParam.Value}");
            }
            Console.Read();
        }
    }
}
