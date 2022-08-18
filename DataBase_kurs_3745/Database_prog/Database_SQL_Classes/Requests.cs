using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Requests
        {

            public class SQLRequest_record
            {
                public SQLRequest_record(int index_, int user_id_, string request_device_, bool allow_, DateTime request_time_)
                {
                    this.index = index_;
                    this.user_id = user_id_;
                    this.allow = allow_;
                    this.request_time = request_time_;
                    //parse
                    var parsed = request_device_.Split('_');
                    this.cabinet = parsed[0];
                    this.department = parsed[1];
                    this.description = parsed[2];
                }

                public int index;
                public int user_id;
                public string cabinet;
                public string department;
                public string description;
                public bool allow;
                public DateTime request_time;
            }

            public static List<SQLRequest_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "select * from Заявки_на_изменение_доступа;";
                    List<SQLRequest_record> results = new List<SQLRequest_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        results.Add(new SQLRequest_record(int.Parse(reader.GetValue(0).ToString()),
                                                            int.Parse(reader.GetValue(1).ToString()),
                                                            reader.GetValue(2).ToString(),
                                                            (reader.GetValue(3).ToString() == "True" ? true : false),
                                                            DateTime.Parse(reader.GetValue(4).ToString())));
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, int user_id, string request_device, bool allow, DateTime request_time)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "insert into Заявки_на_изменение_доступа values(" + id.ToString()+","+ user_id.ToString() + ",'"+ request_device + "',"+ allow.ToString()+ ",'"+ request_time.ToString() + "');";

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

                    string query = "delete from Заявки_на_изменение_доступа where Номер_заявки = " + index_to_delete.ToString() + ";";

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
