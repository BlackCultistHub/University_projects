using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_prog
{
    static partial class Math_Operations
    {
        /*
         *  OFFICE
         */
        public static List<DatabaseOperations.Office.SQLOffice_record> sort_SQL(List<DatabaseOperations.Office.SQLOffice_record> target)
        {
            DatabaseOperations.Office.SQLOffice_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  RFID
         */
        public static List<DatabaseOperations.RFID.SQLRFID_record> sort_SQL(List<DatabaseOperations.RFID.SQLRFID_record> target)
        {
            DatabaseOperations.RFID.SQLRFID_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  USERS
         */
        public static List<DatabaseOperations.Users.SQLUser_record> sort_SQL(List<DatabaseOperations.Users.SQLUser_record> target)
        {
            DatabaseOperations.Users.SQLUser_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  USERS
         */
        public static List<DatabaseOperations.Auth.SQLAuth_record> sort_SQL(List<DatabaseOperations.Auth.SQLAuth_record> target)
        {
            DatabaseOperations.Auth.SQLAuth_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  DOCS
         */
        public static List<DatabaseOperations.Docs.SQLDocument_record> sort_SQL(List<DatabaseOperations.Docs.SQLDocument_record> target)
        {
            DatabaseOperations.Docs.SQLDocument_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  ACCESS
         */
        public static List<DatabaseOperations.Accesses.SQLAccess_record> sort_SQL(List<DatabaseOperations.Accesses.SQLAccess_record> target)
        {
            DatabaseOperations.Accesses.SQLAccess_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  REQUESTS
         */
        public static List<DatabaseOperations.Requests.SQLRequest_record> sort_SQL(List<DatabaseOperations.Requests.SQLRequest_record> target)
        {
            DatabaseOperations.Requests.SQLRequest_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  Journal access
         */
        public static List<DatabaseOperations.Journal_check.SQLJournal_check_record> sort_SQL(List<DatabaseOperations.Journal_check.SQLJournal_check_record> target)
        {
            DatabaseOperations.Journal_check.SQLJournal_check_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }

        /*
         *  Journal request
         */
        public static List<DatabaseOperations.Journal_add_remove.SQLJournal_add_remove_record> sort_SQL(List<DatabaseOperations.Journal_add_remove.SQLJournal_add_remove_record> target)
        {
            DatabaseOperations.Journal_add_remove.SQLJournal_add_remove_record temp;
            for (int j = 0; j <= target.Count - 2; j++)
            {
                for (int i = 0; i <= target.Count - 2; i++)
                {
                    if (target[i].index > target[i + 1].index)
                    {
                        temp = target[i + 1];
                        target[i + 1] = target[i];
                        target[i] = temp;
                    }
                }
            }
            return target;
        }
    }
}
