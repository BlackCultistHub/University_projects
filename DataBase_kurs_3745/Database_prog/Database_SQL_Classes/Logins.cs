using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        /*
         *  AUTH DATA
         */
        public class SQLLogin_record
        {
            public SQLLogin_record(string login_, string hash_) { this.login = login_; this.hash = hash_; }

            public string login;
            public string hash;
        }
        public static List<SQLLogin_record> getAuthData(Npgsql.NpgsqlConnection connection)
        {
            try
            {
                List<SQLLogin_record> results = new List<SQLLogin_record>();
                if (connection == null)
                    throw new Exception("Connection is null");
                var cmd = new Npgsql.NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "select Логин, Хэш_пароля from Аутентификация;";
                cmd.ExecuteNonQuery();

                Npgsql.NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new SQLLogin_record(reader.GetValue(0).ToString(), reader.GetValue(1).ToString()));
                }
                reader.Close();
                cmd.Cancel();
                return results;
            }
            catch
            {
                return null;
            }
        }
    }
}
