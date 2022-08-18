using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static NpgsqlConnection connect(string connectionString)
        {
            try
            {
                var con = new NpgsqlConnection(connectionString);
                con.Open();
                return con;
            }
            catch
            {
                return null;
            }
        }

        public static void close_connection(NpgsqlConnection connection)
        {
            if (connection != null)
                connection.Close();
        }

        public static NpgsqlDataReader executeQuery(NpgsqlConnection connection, string query, bool getReader = true)
        {
            var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            if (getReader)
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                cmd.Cancel();
                return reader;
            }
            else
            {
                cmd.Cancel();
                return null;
            }
        }
    }
}
