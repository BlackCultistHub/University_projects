using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Users
        {

            public class SQLUser_record
            {
                public SQLUser_record(int index_, string name_, string surname_, string role_, string cabinet_, string department_, int card_number_)
                {
                    this.index = index_;
                    this.name = name_;
                    this.surname = surname_;
                    this.role = role_;
                    this.cabinet = cabinet_;
                    this.department = department_;
                    this.card_number = card_number_;
                }
                public SQLUser_record(int index_, string name_, string surname_, string role_, int card_number_, int office_id_, int auth_id_)
                {
                    this.index = index_;
                    this.name = name_;
                    this.surname = surname_;
                    this.role = role_;
                    this.card_number = card_number_;
                    this.office_id = office_id_;
                    this.auth_id = auth_id_;
                }

                public int index;
                public string name;
                public string surname;
                public string role;
                public string cabinet;
                public string department;
                public int card_number;
                public int office_id;
                public int auth_id;
            }

            public static List<SQLUser_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    var office = Office.Get(connection);

                    string query = "select * from Пользователи;";
                    List<SQLUser_record> results = new List<SQLUser_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        results.Add(new SQLUser_record(int.Parse(reader.GetValue(0).ToString()),
                                                            reader.GetValue(4).ToString(),
                                                            reader.GetValue(5).ToString(),
                                                            reader.GetValue(6).ToString(),
                                                            int.Parse(reader.GetValue(3).ToString()),
                                                            int.Parse(reader.GetValue(1).ToString()),
                                                            int.Parse(reader.GetValue(2).ToString())));
                        int office_key = int.Parse(reader.GetValue(1).ToString());
                        foreach (var record in office)
                        {
                            if (record.index == office_key)
                            {
                                results[results.Count - 1].cabinet = record.cabinet;
                                results[results.Count - 1].department = record.department;
                                break;
                            }
                        }
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, string name, string surname, string role, int office_id, int card_number, int auth_id)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "insert into Пользователи values("+id.ToString()+","+ office_id + ","+auth_id+","+card_number+",'"+ name.ToString() + "','" +surname.ToString() + "','"+role.ToString()+ "');";

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

                    string query = "delete from Пользователи where Идентификатор_пользователя = " + index_to_delete.ToString() + ";";

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
