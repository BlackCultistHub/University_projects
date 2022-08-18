using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Auth
        {

            public class SQLAuth_record
            {
                public SQLAuth_record(int index_, string login_, string hash_)
                {
                    this.index = index_;
                    this.login = login_;
                    this.hash = hash_;
                }

                public int index;
                public string login;
                public string hash;
            }

            public static List<SQLAuth_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "select * from Аутентификация;";
                    List<SQLAuth_record> results = new List<SQLAuth_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        results.Add(new SQLAuth_record(int.Parse(reader.GetValue(0).ToString()),
                                                            reader.GetValue(1).ToString(),
                                                            reader.GetValue(2).ToString()));
                        
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, string login, string hash)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");


                    string query = "insert into Аутентификация values(" + id.ToString() + ",'" + login.ToString() + "','" + hash.ToString() +"');";

                    DatabaseOperations.executeQuery(connection, query, false);

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool Delete_byIndex(Npgsql.NpgsqlConnection connection, int index_to_delete)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "delete from Аутентификация where Номер_пары_аутентификации = " + index_to_delete.ToString() + ";";

                    executeQuery(connection, query, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool NewPwd_byIndex(Npgsql.NpgsqlConnection connection, int index_to_update, string hash)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "update Аутентификация set Хэш_пароля = '"+hash+"' where Номер_пары_аутентификации = " + index_to_update.ToString() + ";";

                    executeQuery(connection, query, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
