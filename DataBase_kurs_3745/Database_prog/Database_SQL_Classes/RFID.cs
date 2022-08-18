using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class RFID
        {

            public class SQLRFID_record
            {
                public SQLRFID_record(int index_, string RFID_code_, bool given_)
                {
                    this.index = index_;
                    this.RFID_code = RFID_code_;
                    this.given = given_;
                }

                public int index;
                public string RFID_code;
                public bool given;
            }

            public static List<SQLRFID_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "select * from Бесконтактные_карты;";
                    List<SQLRFID_record> results = new List<SQLRFID_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        results.Add(new SQLRFID_record(int.Parse(reader.GetValue(0).ToString()),
                                                            reader.GetValue(1).ToString(),
                                                            (reader.GetValue(2).ToString() == "True" ? true : false)));
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, string RFID_code, bool given)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "insert into Бесконтактные_карты values("+id.ToString()+",'"+ RFID_code.ToString() + "',"+given.ToString()+");";

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

                    string query = "delete from Бесконтактные_карты where Номер_карты = " + index_to_delete.ToString() + ";";

                    executeQuery(connection, query, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool Claim_byIndex(Npgsql.NpgsqlConnection connection, int index_to_free)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "update Бесконтактные_карты set Выдана = true where Номер_карты = " + index_to_free.ToString() + ";";

                    executeQuery(connection, query, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool Free_byIndex(Npgsql.NpgsqlConnection connection, int index_to_free)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "update Бесконтактные_карты set Выдана = false where Номер_карты = " + index_to_free.ToString() + ";";

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
