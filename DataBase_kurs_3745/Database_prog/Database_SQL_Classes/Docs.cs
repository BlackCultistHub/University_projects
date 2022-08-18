using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class DatabaseOperations
    {
        public static class Docs
        {
            public class SQLDocument_record
            {
                public SQLDocument_record(int index_, 
                    bool access_, string user_name_, string user_surname_, string device_cab_, string device_depart_, string device_descr_,
                    int user_id_, int office_id_)
                {
                    this.index = index_;
                    this.access = access_;
                    this.name = user_name_;
                    this.surname = user_surname_;
                    this.cabinet = device_cab_;
                    this.department = device_depart_;
                    this.description = device_descr_;
                    this.user_id = user_id_;
                    this.office_id = office_id_;
                }

                public int index;
                public bool access;
                public string name;
                public string surname;
                public string cabinet;
                public string department;
                public string description;
                public int user_id;
                public int office_id;
            }

            public static List<SQLDocument_record> Get(Npgsql.NpgsqlConnection connection)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    var Office = DatabaseOperations.Office.Get(connection);

                    var Users = DatabaseOperations.Users.Get(connection);

                    string query = "select * from Приказы;";
                    List<SQLDocument_record> results = new List<SQLDocument_record>();
                    var reader = executeQuery(connection, query);

                    if (reader == null)
                        throw new Exception("Got null SQL response!");

                    while (reader.Read())
                    {
                        //find corresponding device
                        DatabaseOperations.Office.SQLOffice_record device_data = null;
                        foreach (var record in Office)
                        {
                            if (record.device)
                            {
                                if (int.Parse(reader.GetValue(2).ToString()) == record.index) //found
                                {
                                    device_data = record;
                                    break;
                                } 
                            }
                        }
                        if (device_data == null)
                            throw new Exception("Device not found!");

                        //find user
                        DatabaseOperations.Users.SQLUser_record user_data = null;
                        foreach (var record in Users)
                        {
                            if (int.Parse(reader.GetValue(1).ToString()) == record.index)
                            {
                                user_data = record;
                                break;
                            }
                        }
                        if (user_data == null)
                            throw new Exception("User not found!");


                        results.Add(new SQLDocument_record(int.Parse(reader.GetValue(0).ToString()),
                                                            (reader.GetValue(3).ToString() == "True" ? true : false),
                                                            user_data.name,
                                                            user_data.surname,
                                                            device_data.cabinet,
                                                            device_data.department,
                                                            device_data.description,
                                                            int.Parse(reader.GetValue(1).ToString()),
                                                            int.Parse(reader.GetValue(2).ToString())));
                    }
                    reader.Close();
                    return results;
                }
                catch
                {
                    return null;
                }
            }

            public static bool Insert(Npgsql.NpgsqlConnection connection, int id, bool access, int user_id, int device_id)
            {
                try
                {
                    if (connection == null)
                        throw new Exception("Connection is null");

                    string query = "insert into Приказы values(" + id.ToString() + "," + user_id.ToString() + "," + device_id.ToString() + "," + access.ToString() + ");";

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

                    string query = "delete from Приказы where Номер_приказа = " + index_to_delete.ToString() + ";";

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
