using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=adonetdb;Trusted_Connection=True;";
            string sql = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Создаем объект DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                // Отображаем данные
                // перебор всех таблиц
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataColumn column in dt.Columns)
                        Console.Write($"{column.ColumnName}\t");
                    Console.WriteLine();
                    // перебор всех строк таблицы
                    foreach (DataRow row in dt.Rows)
                    {
                        // получаем все ячейки строки
                        var cells = row.ItemArray;
                        foreach (object cell in cells)
                            Console.Write($"{cell}\t");
                        Console.WriteLine();
                    }
                }
            }
            Console.Read();
        }
    }
}
