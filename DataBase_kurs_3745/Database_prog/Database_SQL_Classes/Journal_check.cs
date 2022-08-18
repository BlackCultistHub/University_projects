using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Journal_check
        {
            public class SQLJournal_check_record
            {
                public SQLJournal_check_record(int index_, int device_id_, int office_id_, int card_number_, bool accessed_, DateTime timeStamp_)
                {
                    this.index = index_;
                    this.device_id = device_id_;
                    this.office_id = office_id_;
                    this.card_number = card_number_;
                    this.accessed = accessed_;
                    this.timeStamp = timeStamp_;
                }

                public int index;
                public int device_id;
                public int office_id;
                public int card_number;
                public bool accessed;
                public DateTime timeStamp; 
            }

            public static List<SQLJournal_check_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "select * from Журнал_доступа;";
                    List<SQLJournal_check_record> results = new List<SQLJournal_check_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        int device_id;
                        if (reader.GetValue(1).ToString() == "")
                            device_id = -1;
                        else
                            device_id = int.Parse(reader.GetValue(1).ToString());
                        results.Add(new SQLJournal_check_record(int.Parse(reader.GetValue(0).ToString()),
                                                            device_id,
                                                            int.Parse(reader.GetValue(2).ToString()),
                                                            int.Parse(reader.GetValue(3).ToString()),
                                                            bool.Parse(reader.GetValue(5).ToString()),
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

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, int device_id, int office_id, int card_number, bool accessed, DateTime timeStamp)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "insert into Журнал_доступа values(" + id.ToString() + ",";
                    if (device_id == -1)
                        query += "NULL";
                    else
                        query += device_id.ToString();
                    query += "," + office_id.ToString() + "," + card_number.ToString() + ",'" + timeStamp.ToString() + "'," + accessed.ToString() + ");";

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

                    string query = "delete from Журнал_доступа where Номер_строки_журнала = " + index_to_delete.ToString() + ";";

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
