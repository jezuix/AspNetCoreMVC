using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Util
{
    public class DAL
    {
        private static string server = "localhost";
        private static string database = "financeiro";
        private static string user = "root";
        private static string password = "rrb1617R";
        private string connectionString = $"Server={server};DataBase={database};Uid={user};Pwd={password}";
        private MySqlConnection connection;

        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        //Executa Selects
        public DataTable RetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dataTable);

            return dataTable;
        }

        //Executa Inserts, Updates, Deletes
        public void ExecutarComandosSQL(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
