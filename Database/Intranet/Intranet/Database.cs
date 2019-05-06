using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Intranet
{
    class Database
    {
        SQLiteConnection connection;

        public Database()
        {
            string path = @"C:\Users\Asus\Documents\PP2\Database\Intranet.db";
            connection = new SQLiteConnection("Data Source = " + path);
        }

        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public int ExecuteSQL(string query)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            var result = command.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public DataSet GetDataSet(string sql)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            connection.Close();
            return ds;
        }

        public DataTable GetDataTable(string sql, int i)
        {
            DataSet ds = GetDataSet(sql);
            return ds.Tables[i];
        }
    }
}
