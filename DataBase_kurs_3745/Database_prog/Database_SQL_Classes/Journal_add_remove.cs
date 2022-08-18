using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Journal_add_remove
        {
            public class SQLJournal_add_remove_record
            {
                public SQLJournal_add_remove_record(int index_, int request_id_, int doc_id_, string status_, bool add_)
                {
                    this.index = index_;
                    this.request_id = request_id_;
                    this.doc_id = doc_id_;
                    this.status = status_;
                    this.add = add_;
                }

                public int index;
                public int request_id;
                public int doc_id;
                public string status;
                public bool add;
            }

            public static List<SQLJournal_add_remove_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "select * from Журнал_заявок;";
                    List<SQLJournal_add_remove_record> results = new List<SQLJournal_add_remove_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        int readerInd = -1;
                        int docInd = -1;
                        if (reader.GetValue(1).ToString() != "")
                            readerInd = int.Parse(reader.GetValue(1).ToString());
                        else if (reader.GetValue(2).ToString() != "")
                            docInd = int.Parse(reader.GetValue(2).ToString());

                        results.Add(new SQLJournal_add_remove_record(int.Parse(reader.GetValue(0).ToString()),
                                                            readerInd,
                                                            docInd,
                                                            reader.GetValue(3).ToString(),
                                                            bool.Parse(reader.GetValue(4).ToString())));
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, int request_id, int doc_id, string status, bool add)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");
                    string query = "insert into Журнал_заявок values(" + id.ToString() + ",";
                    if (request_id == -1)
                        query += "NULL";
                    else
                        query += request_id.ToString();
                    query += ",";
                    if (doc_id == -1)
                        query += "NULL";
                    else
                        query += doc_id.ToString();
                    query += ",'" + status + "'," + add.ToString() + ");";

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

                    string query = "delete from Журнал_заявок where Номер_строки = " + index_to_delete.ToString() + ";";

                    executeQuery(connection, query, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool Approve_byIndex(Npgsql.NpgsqlConnection connection, int index_to_free)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "update Журнал_заявок set Статус = 'Разрешено' where Номер_заявки = " + index_to_free.ToString() + ";";

                    executeQuery(connection, query, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool Deny_byIndex(Npgsql.NpgsqlConnection connection, int index_to_free)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "update Журнал_заявок set Статус = 'Отклонено' where Номер_заявки = " + index_to_free.ToString() + ";";

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
