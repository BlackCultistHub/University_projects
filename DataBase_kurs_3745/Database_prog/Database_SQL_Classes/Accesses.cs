using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Accesses
        {

            public class SQLAccess_record
            {
                public SQLAccess_record(int index_, int office_id_, int card_number_, bool allowed_)
                {
                    this.index = index_;
                    this.office_id = office_id_;
                    this.card_number = card_number_;
                    this.allowed = allowed_;
                }

                public int index;
                public int office_id;
                public int card_number;
                public bool allowed;
            }

            public static List<SQLAccess_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "select * from Устройства_считывания;";
                    List<SQLAccess_record> results = new List<SQLAccess_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        results.Add(new SQLAccess_record(int.Parse(reader.GetValue(0).ToString()),
                                                         int.Parse(reader.GetValue(1).ToString()),
                                                         int.Parse(reader.GetValue(2).ToString()),
                                                         reader.GetValue(3).ToString() == "True"?true:false));
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, int office_id, int card_number, bool allowed)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "insert into Устройства_считывания values(" + id.ToString()+","+ office_id.ToString() + ","+ card_number.ToString()+ ","+ allowed.ToString()+ ");";

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

                    string query = "delete from Устройства_считывания where Идентификатор_устройства = " + index_to_delete.ToString() + ";";

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
