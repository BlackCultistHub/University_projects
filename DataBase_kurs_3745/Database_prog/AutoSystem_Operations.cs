using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static class AutoSystem_Operations
    {
        private class AccessFromDB
        {
            public AccessFromDB(int index_, int office_device_id_, int card_id_, bool allow_, int doc_id_)
            {
                this.index = index_;
                this.office_device_id = office_device_id_;
                this.card_id = card_id_;
                this.allow = allow_;
                this.doc_id = doc_id_;
            }

            public AccessFromDB(int index_, int office_device_id_, int card_id_, bool allow_, int doc_id_, int request_id_)
            {
                this.index = index_;
                this.office_device_id = office_device_id_;
                this.card_id = card_id_;
                this.allow = allow_;
                this.doc_id = doc_id_;
                this.request_id = request_id_;
            }
            public int index;
            public int office_device_id;
            public int card_id;
            public bool allow;
            public int doc_id;

            public int request_id = -1;
        }

        public static void UpdateAccesses(bool fromUser = false, int user_id = 0)
        {
            try
            {
                //open connection as AutoSystem
                string conString = "Host=127.0.0.1;Port=5432;Username=Автоматизированная_система;Password=AutoSystem;Database=Контроль_доступа;";
                var IS_conn = DatabaseOperations.connect(conString);

                //update accessess
                //1. get docs
                var Docs = DatabaseOperations.Docs.Get(IS_conn);
                var Office = DatabaseOperations.Office.Get(IS_conn);
                //2. get accesses
                var Accesses = DatabaseOperations.Accesses.Get(IS_conn);
                Math_Operations.sort_SQL(Accesses);
                //3. get users
                var Users = DatabaseOperations.Users.Get(IS_conn);
                //4. (users + docs) info -> accesses

                var Requests = DatabaseOperations.Requests.Get(IS_conn);
                Math_Operations.sort_SQL(Requests);

                List<AccessFromDB> AccessesToCheck = new List<AccessFromDB>();
                foreach (var doc in Docs)
                {
                    int card_id = -1;
                    int request_id = -1;
                    if (fromUser)
                    {
                        foreach (var user in Users)
                        {
                            if ((user.index == doc.user_id) && (user.index == user_id))
                            {
                                card_id = user.card_number;
                                break;
                            }
                        }
                        for (int i = Requests.Count - 1; i >= 0; i--)
                        {
                            if (Requests[i].user_id == user_id)
                            {
                                request_id = Requests[i].index;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var user in Users)
                        {
                            if (user.index == doc.user_id)
                            {
                                card_id = user.card_number;
                                break;
                            }
                        }
                    }
                    AccessesToCheck.Add(new AccessFromDB(0, doc.office_id, card_id, doc.access, doc.index, request_id));
                }
                //check add
                int new_access_id;
                if (Accesses.Count == 0)
                    new_access_id = 0;
                else
                    new_access_id = Accesses[Accesses.Count - 1].index + 1;
                foreach (var access in AccessesToCheck)
                {
                    if ((fromUser && (access.card_id != -1)) || (!fromUser))
                    {
                        if (Accesses.Count == 0)
                        {
                            //add if not
                            if (DatabaseOperations.Accesses.Insert(IS_conn, new_access_id, access.office_device_id, access.card_id, access.allow))
                            {
                                new_access_id++;
                                //JOURNAL ADD ACCESS
                                //get id
                                int j_id;
                                var Journal = DatabaseOperations.Journal_add_remove.Get(IS_conn);
                                Math_Operations.sort_SQL(Journal);
                                if (Journal.Count == 0)
                                    j_id = 0;
                                else
                                    j_id = Journal[Journal.Count - 1].index + 1;
                                DatabaseOperations.Journal_add_remove.Insert(IS_conn, j_id, access.request_id, access.doc_id, fromUser ? "Добавлено" : "Автоматически", true);
                            }
                        }
                        else
                        {
                            bool add_this_access = true;
                            foreach (var accessDB in Accesses)
                            {
                                
                                //4.1 check if already exists
                                if ((accessDB.office_id == access.office_device_id) &&
                                    (accessDB.card_number == access.card_id) &&
                                    (accessDB.allowed == access.allow))
                                {
                                    add_this_access = false;
                                }
                            }
                            if (add_this_access)
                            {
                                //add if not
                                if (DatabaseOperations.Accesses.Insert(IS_conn, new_access_id, access.office_device_id, access.card_id, access.allow))
                                {
                                    new_access_id++;
                                    //JOURNAL ADD ACCESS
                                    //get id
                                    int j_id;
                                    var Journal = DatabaseOperations.Journal_add_remove.Get(IS_conn);
                                    Math_Operations.sort_SQL(Journal);
                                    if (Journal.Count == 0)
                                        j_id = 0;
                                    else
                                        j_id = Journal[Journal.Count - 1].index + 1;
                                    DatabaseOperations.Journal_add_remove.Insert(IS_conn, j_id, access.request_id, access.doc_id, fromUser ? "Добавлено" : "Автоматически", true);
                                }
                                break;
                            }
                            else
                            {
                                if (fromUser)
                                {
                                    //JOURNAL ADD ACCESS
                                    //get id
                                    int j_id;
                                    var Journal = DatabaseOperations.Journal_add_remove.Get(IS_conn);
                                    Math_Operations.sort_SQL(Journal);
                                    if (Journal.Count == 0)
                                        j_id = 0;
                                    else
                                        j_id = Journal[Journal.Count - 1].index + 1;
                                    DatabaseOperations.Journal_add_remove.Insert(IS_conn, j_id, access.request_id, access.doc_id, "Отклонено", true);
                                    
                                }
                            }
                        }
                    }
                }
                //CHECK FOR DELETE
                if (!fromUser)
                {
                    foreach (var accessDB in Accesses)
                    {
                        bool remove = true;
                        foreach (var access in AccessesToCheck)
                        {
                            if (((accessDB.office_id == access.office_device_id) &&
                                (accessDB.card_number == access.card_id) &&
                                (accessDB.allowed == access.allow)))
                            {
                                remove = false;
                                break;
                            }
                        }
                        if (remove)
                        {
                            DatabaseOperations.Accesses.Delete_byIndex(IS_conn, accessDB.index);
                            //JOURNAL ADD REMOVE!!!!!!!!!!!
                            //get id
                            int j_id;
                            var Journal = DatabaseOperations.Journal_add_remove.Get(IS_conn);
                            Math_Operations.sort_SQL(Journal);
                            if (Journal.Count == 0)
                                j_id = 0;
                            else
                                j_id = Journal[Journal.Count - 1].index + 1;
                            DatabaseOperations.Journal_add_remove.Insert(IS_conn, j_id, -1, -1, "Автоматически", true);
                        }
                    }
                }
                IS_conn.Close();
            }
            catch
            {

            }
        }

        public static bool CheckAccess(string RFID_code, int device_id) //device id from Устройства_считывания
        {
            //open connection as AutoSystem
            string conString = "Host=127.0.0.1;Port=5432;Username=Автоматизированная_система;Password=AutoSystem;Database=Контроль_доступа;";
            var IS_conn = DatabaseOperations.connect(conString);

            //checking by table Устройства_считывания
            var Accesses = DatabaseOperations.Accesses.Get(IS_conn);
            Math_Operations.sort_SQL(Accesses);

            var Cards = DatabaseOperations.RFID.Get(IS_conn);
            Math_Operations.sort_SQL(Cards);

            DatabaseOperations.Accesses.SQLAccess_record targetAccess = null;

            bool good = false;
            foreach(var access in Accesses)
            {
                if (access.office_id == device_id)
                    targetAccess = access;
                if (targetAccess != null)
                {
                    //find corresponding card
                    string RFID = "";
                    foreach (var card in Cards)
                    {
                        if (card.index == access.card_number)
                        {
                            RFID = System.Text.RegularExpressions.Regex.Replace(card.RFID_code, @"\s+", "");
                            break;
                        }
                    }

                    if ((access.office_id == device_id) && (RFID_code == RFID))
                    {
                        good = true;
                        break;
                    }
                }
            }

            int j_id;
            var Journal = DatabaseOperations.Journal_check.Get(IS_conn);
            Math_Operations.sort_SQL(Journal);
            if (Journal.Count == 0)
                j_id = 0;
            else
                j_id = Journal[Journal.Count - 1].index + 1;

            int request_card_number = 0;
            foreach (var card in Cards)
            {
                if (System.Text.RegularExpressions.Regex.Replace(card.RFID_code, @"\s+", "") == RFID_code)
                {
                    request_card_number = card.index;
                    break;
                }
            }

            if (good)
            {
                DatabaseOperations.Journal_check.Insert(IS_conn, j_id, targetAccess.index, targetAccess.office_id, request_card_number, true, DateTime.Now);
                IS_conn.Close();
                return true;
            }
            else
            {
                DatabaseOperations.Journal_check.Insert(IS_conn, j_id, -1, targetAccess.office_id, request_card_number, false, DateTime.Now);
                IS_conn.Close();
                return false;
            }
        }
    }
}
