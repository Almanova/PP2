using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DBConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Database database = new Database();
            string query = "INSERT INTO Students (Name, Surname) VALUES (@Name, @Surname)";
            database.OpenConnection();
            SQLiteCommand command = new SQLiteCommand(query, database.connection);
            command.Parameters.AddWithValue("@Name", "Madina");
            command.Parameters.AddWithValue("@Surname", "Almanova");
            command.ExecuteNonQuery();
            database.CloseConnection();
            Console.ReadKey();*/

            Database database = new Database();
            string query = "SELECT * FROM Students WHERE StudentID > 1";
            database.OpenConnection();
            SQLiteCommand command = new SQLiteCommand(query, database.connection);
            SQLiteDataReader result = command.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine("StudentID: {0}, Name: {1}, Surname: {2}", result["StudentID"], result["Name"], result["Surname"]);
                }
            }

            database.CloseConnection();
            Console.ReadKey();
        }
    }
}
