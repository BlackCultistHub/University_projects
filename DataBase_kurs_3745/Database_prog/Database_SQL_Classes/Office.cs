using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Office
        {
            /*
         *  OFFICE - DEVICES DATA
         */

            public class SQLOffice_record
            {
                public SQLOffice_record(int index_, bool device_, string cabinet_, string department_, string description_)
                {
                    this.index = index_;
                    this.device = device_;
                    this.cabinet = cabinet_;
                    this.department = department_;
                    this.description = description_;
                }

                public int index;
                public string cabinet;
                public string department;
                public string description;
                public bool device;
            }

            public static List<SQLOffice_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "select * from Офис;";
                    List<SQLOffice_record> results = new List<SQLOffice_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        results.Add(new SQLOffice_record(int.Parse(reader.GetValue(0).ToString()),
                                                            (reader.GetValue(1).ToString() == "True" ? true : false),
                                                            reader.GetValue(2).ToString(),
                                                            reader.GetValue(3).ToString(),
                                                            reader.GetValue(4).ToString()));
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, bool device, string cabinet, string department, string description)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "insert into Офис values("+id.ToString()+","+ device.ToString() + ",'"+cabinet+"','"+department+"','"+ description + "');";

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

                    string query = "delete from Офис where Идентификатор_связки_помещения = " + index_to_delete.ToString() + ";";

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
