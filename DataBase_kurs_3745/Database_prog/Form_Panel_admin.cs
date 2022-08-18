using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Npgsql;

namespace Database_prog
{
    public partial class Form_Panel_admin : Form
    {
        NpgsqlConnection SavedConnection;

        List<DatabaseOperations.RFID.SQLRFID_record> RFID_list = new List<DatabaseOperations.RFID.SQLRFID_record>();
        List<DatabaseOperations.Users.SQLUser_record> Users_list = new List<DatabaseOperations.Users.SQLUser_record>();
        List<DatabaseOperations.Docs.SQLDocument_record> Docs_list = new List<DatabaseOperations.Docs.SQLDocument_record>();
        List<DatabaseOperations.Accesses.SQLAccess_record> Accesses_list = new List<DatabaseOperations.Accesses.SQLAccess_record>();
        List<DatabaseOperations.Journal_check.SQLJournal_check_record> Journal_access_list = new List<DatabaseOperations.Journal_check.SQLJournal_check_record>();
        List<DatabaseOperations.Journal_add_remove.SQLJournal_add_remove_record> Journal_request_list = new List<DatabaseOperations.Journal_add_remove.SQLJournal_add_remove_record>();
        //non-view
        List<DatabaseOperations.Office.SQLOffice_record> Office_list = new List<DatabaseOperations.Office.SQLOffice_record>();
        List<DatabaseOperations.Requests.SQLRequest_record> Requests_list = new List<DatabaseOperations.Requests.SQLRequest_record>();
        public Form_Panel_admin(NpgsqlConnection connection)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.CenterToScreen();

            SavedConnection = connection;

            //filters
            comboBox_rfid_filter.SelectedIndex = 0;
            comboBox_docs_filter.SelectedIndex = 0;
            comboBox_accessDevices_filter.SelectedIndex = 0;
            comboBox_access_filter.SelectedIndex = 0;
            comboBox_request_filter.SelectedIndex = 0;

            Office_list = DatabaseOperations.Office.Get(SavedConnection);
            Requests_list = DatabaseOperations.Requests.Get(SavedConnection);

            GetRFIDObject();
            UpdateViewWrapper();

            GetUsersObject();
            UpdateUsersView();

            GetDocsObject();
            UpdateDocsViewWrapper();

            GetAccessObject();
            UpdateAccessViewWrapper();

            GetJournalAccessObject();
            UpdateJournalAccessViewWrapper();

            GetJournalRequestsObject();
            UpdateJournalRequestsViewWrapper();
        }
        private void выйтиИзСистемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete("session.token");
            var auth = new Form_Auth();
            auth.Closed += (s, args) => this.Close();
            auth.Show();
            this.Hide();
        }

        /*
         *  RFID FUNCS
         */
        private void GetRFIDObject()
        {
            RFID_list = DatabaseOperations.RFID.Get(SavedConnection);
            Math_Operations.sort_SQL(RFID_list);
        }
        private void UpdateViewWrapper()
        {
            if (comboBox_rfid_filter.SelectedIndex == 0)
                UpdateView();
            else if (comboBox_rfid_filter.SelectedIndex == 1)
                UpdateView(true);
            else if (comboBox_rfid_filter.SelectedIndex == 2)
                UpdateView(true, true);
        }

        //update view
        private void UpdateView(bool filter = false, bool given = false)
        {
            dataGridView_rfid.Rows.Clear();
            if (filter)
            {
                if (given)
                {
                    foreach (var card in RFID_list)
                    {
                        if (card.given)
                        {
                            dataGridView_rfid.Rows.Add();
                            dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[0].Value = card.index;
                            dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[1].Value = card.given ? "Да" : "Нет";
                            dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[2].Value = card.RFID_code;
                        }
                    }
                }
                else
                {
                    foreach (var card in RFID_list)
                    {
                        if (!card.given)
                        {
                            dataGridView_rfid.Rows.Add();
                            dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[0].Value = card.index;
                            dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[1].Value = card.given ? "Да" : "Нет";
                            dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[2].Value = card.RFID_code;
                        }
                    }
                }
            }
            else
            {
                foreach(var card in RFID_list)
                {
                    dataGridView_rfid.Rows.Add();
                    dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[0].Value = card.index;
                    dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[1].Value = card.given ? "Да" : "Нет";
                    dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[2].Value = card.RFID_code;
                }
            }
        }

        private void comboBox_rfid_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateViewWrapper();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form shadow = new Form();
            shadow.MinimizeBox = false;
            shadow.MaximizeBox = false;
            shadow.ControlBox = false;

            shadow.Text = "";
            shadow.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            shadow.Size = this.Size;
            shadow.BackColor = Color.Black;
            shadow.Opacity = 0.3;
            shadow.Show();
            shadow.Location = this.Location;
            shadow.Enabled = false;

            var form = new Form_New_RFID(SavedConnection);
            form.ShowDialog();
            shadow.Dispose();
            shadow.Close();

            GetRFIDObject();
            UpdateViewWrapper();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var selection = dataGridView_rfid.SelectedRows;
            List<int> selected_indexes = new List<int>();

            for (int i = 0; i < selection.Count; i++)
                selected_indexes.Add((int)selection[i].Cells[0].Value);

            foreach (var index in selected_indexes)
                DatabaseOperations.RFID.Claim_byIndex(SavedConnection, index);

            GetRFIDObject();
            UpdateViewWrapper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var selection = dataGridView_rfid.SelectedRows;
                List<int> selected_indexes = new List<int>();

                for (int i = 0; i < selection.Count; i++)
                {
                    if ((string)selection[i].Cells[1].Value == "Да")
                        throw new Exception("Одна или более выделенных карт присвоены!");
                    selected_indexes.Add((int)selection[i].Cells[0].Value);
                    dataGridView_rfid.Rows.RemoveAt(selection[i].Index);
                }

                foreach (var index in selected_indexes)
                    DatabaseOperations.RFID.Delete_byIndex(SavedConnection, index);

                GetRFIDObject();
                UpdateViewWrapper();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         *  USER FUNCS
         */

        private void GetUsersObject()
        {
            Users_list = DatabaseOperations.Users.Get(SavedConnection);
            Math_Operations.sort_SQL(Users_list);
        }

        //update view
        private void UpdateUsersView(bool usesearch = false, string searchstring = "")
        {
            dataGridView_users.Rows.Clear();
            if ((usesearch && searchstring == "") || !usesearch)
            {
                foreach (var user in Users_list)
                {
                    dataGridView_users.Rows.Add();
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[0].Value = user.index;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[1].Value = user.name;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[2].Value = user.surname;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[3].Value = user.role;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[4].Value = user.cabinet;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[5].Value = user.department;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[6].Value = user.card_number;
                }
            }
            else //search with non- empty string
            {
                foreach (var user in Users_list)
                {
                    if ((System.Text.RegularExpressions.Regex.Replace(user.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(user.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(user.role, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(user.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(user.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(user.card_number.ToString(), @"\s+", "") == searchstring))
                    {
                        dataGridView_users.Rows.Add();
                        dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[0].Value = user.index;
                        dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[1].Value = user.name;
                        dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[2].Value = user.surname;
                        dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[3].Value = user.role;
                        dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[4].Value = user.cabinet;
                        dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[5].Value = user.department;
                        dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[6].Value = user.card_number;
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateUsersView(true, textBox_user_searchstring.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form shadow = new Form();
            shadow.MinimizeBox = false;
            shadow.MaximizeBox = false;
            shadow.ControlBox = false;

            shadow.Text = "";
            shadow.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            shadow.Size = this.Size;
            shadow.BackColor = Color.Black;
            shadow.Opacity = 0.3;
            shadow.Show();
            shadow.Location = this.Location;
            shadow.Enabled = false;

            var form = new Form_New_User(SavedConnection);
            form.ShowDialog();
            shadow.Dispose();
            shadow.Close();

            GetUsersObject();
            UpdateUsersView();

            GetRFIDObject();
            UpdateViewWrapper();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var selection = dataGridView_users.SelectedRows;

            List<int> selected_indexes = new List<int>();

            for (int i = 0; i < selection.Count; i++)
            {
                selected_indexes.Add((int)selection[i].Cells[0].Value);
                dataGridView_users.Rows.RemoveAt(selection[i].Index);
            }

            var Users = DatabaseOperations.Users.Get(SavedConnection);
            foreach (var user in Users)
            {
                foreach (int index in selected_indexes)
                {
                    if (user.index == index)
                    {
                        //free card
                        DatabaseOperations.RFID.Free_byIndex(SavedConnection, user.card_number);
                        //remove auth
                        DatabaseOperations.Auth.Delete_byIndex(SavedConnection, user.auth_id);
                    }
                }
            }

            foreach (var index in selected_indexes)
                DatabaseOperations.Users.Delete_byIndex(SavedConnection, index);

            GetRFIDObject();
            UpdateViewWrapper();

            GetUsersObject();
            UpdateUsersView();
        }

        /*
         * DOCS FUNCTIONS
         */

        public void GetDocsObject()
        {
            Docs_list = DatabaseOperations.Docs.Get(SavedConnection);
            Math_Operations.sort_SQL(Docs_list);
        }

        public void UpdateDocsViewWrapper(bool search = false, string searchstring = "")
        {
            if (search)
            {
                if (comboBox_docs_filter.SelectedIndex == 0)
                    UpdateDocsView(false, false, true, searchstring);
                else if (comboBox_docs_filter.SelectedIndex == 1)
                    UpdateDocsView(true, true, true, searchstring);
                else if (comboBox_docs_filter.SelectedIndex == 2)
                    UpdateDocsView(true, false, true, searchstring);
            }
            else
            {
                if (comboBox_docs_filter.SelectedIndex == 0)
                    UpdateDocsView();
                else if (comboBox_docs_filter.SelectedIndex == 1)
                    UpdateDocsView(true, true);
                else if (comboBox_docs_filter.SelectedIndex == 2)
                    UpdateDocsView(true);
            }
        }

        public void UpdateDocsView(bool filter = false, bool access = false, bool search = false, string searchstring = "")
        {
            dataGridView_docs.Rows.Clear();
            if (filter && search)
            {
                foreach (var doc in Docs_list)
                {
                    if  (((System.Text.RegularExpressions.Regex.Replace(doc.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.description, @"\s+", "") == searchstring) ||
                        (searchstring == "")) && 
                        ((access && doc.access) || (!access && !doc.access)))
                    {
                        dataGridView_docs.Rows.Add();
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[0].Value = doc.index;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[1].Value = doc.name;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[2].Value = doc.surname;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[3].Value = doc.access == true ? "Да" : "Нет";
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[4].Value = doc.cabinet;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[5].Value = doc.department;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[6].Value = doc.description;
                    }
                }
            }
            else if (filter && !search)
            {
                foreach (var doc in Docs_list)
                {
                    if ((access && doc.access) || (!access && !doc.access))
                    {
                        dataGridView_docs.Rows.Add();
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[0].Value = doc.index;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[1].Value = doc.name;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[2].Value = doc.surname;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[3].Value = doc.access == true ? "Да" : "Нет";
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[4].Value = doc.cabinet;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[5].Value = doc.department;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[6].Value = doc.description;
                    }
                }
            }
            else if (!filter && search)
            {
                foreach (var doc in Docs_list)
                {
                    if ((System.Text.RegularExpressions.Regex.Replace(doc.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(doc.description, @"\s+", "") == searchstring) ||
                        (searchstring == ""))
                    {
                        dataGridView_docs.Rows.Add();
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[0].Value = doc.index;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[1].Value = doc.name;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[2].Value = doc.surname;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[3].Value = doc.access == true ? "Да" : "Нет";
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[4].Value = doc.cabinet;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[5].Value = doc.department;
                        dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[6].Value = doc.description;
                    }
                }
            }
            else
            {
                foreach (var doc in Docs_list)
                {
                    dataGridView_docs.Rows.Add();
                    dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[0].Value = doc.index;
                    dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[1].Value = doc.name;
                    dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[2].Value = doc.surname;
                    dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[3].Value = doc.access==true?"Да":"Нет";
                    dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[4].Value = doc.cabinet;
                    dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[5].Value = doc.department;
                    dataGridView_docs.Rows[dataGridView_docs.Rows.Count - 1].Cells[6].Value = doc.description;
                }
            }
        }
        //ADD NEW
        private void button_add_doc_Click(object sender, EventArgs e)
        {
            Form shadow = new Form();
            shadow.MinimizeBox = false;
            shadow.MaximizeBox = false;
            shadow.ControlBox = false;

            shadow.Text = "";
            shadow.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            shadow.Size = this.Size;
            shadow.BackColor = Color.Black;
            shadow.Opacity = 0.3;
            shadow.Show();
            shadow.Location = this.Location;
            shadow.Enabled = false;

            var form = new Form_New_Doc(SavedConnection);
            form.ShowDialog();
            shadow.Dispose();
            shadow.Close();

            GetDocsObject();
            UpdateDocsViewWrapper();
        }
        //DELETE SELECTED
        private void button_delete_selected_doc_Click(object sender, EventArgs e)
        {
            var selection = dataGridView_docs.SelectedRows;
            List<int> selected_indexes = new List<int>();

            for (int i = 0; i < selection.Count; i++)
            {
                selected_indexes.Add((int)selection[i].Cells[0].Value);
                dataGridView_docs.Rows.RemoveAt(selection[i].Index);
            }

            foreach (var index in selected_indexes)
            {
                DatabaseOperations.Docs.Delete_byIndex(SavedConnection, index);
            }

            GetDocsObject();
            UpdateDocsViewWrapper();
        }

        // DOCS SEARCH
        private void button10_Click(object sender, EventArgs e)
        {
            UpdateDocsViewWrapper(true, textBox_docs_search.Text);
        }

        private void comboBox_docs_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDocsViewWrapper();
        }

        /*
         * ACCESS
         */

        public void GetAccessObject()
        {
            Accesses_list = DatabaseOperations.Accesses.Get(SavedConnection);
            Math_Operations.sort_SQL(Accesses_list);
        }

        public void UpdateAccessViewWrapper(bool search = false, string searchstring = "")
        {
            if (search)
            {
                if (comboBox_accessDevices_filter.SelectedIndex == 0)
                    UpdateAccessView(false, false, true, searchstring);
                else if (comboBox_accessDevices_filter.SelectedIndex == 1)
                    UpdateAccessView(true, true, true, searchstring);
                else if (comboBox_accessDevices_filter.SelectedIndex == 2)
                    UpdateAccessView(true, false, true, searchstring);
            }
            else
            {
                if (comboBox_accessDevices_filter.SelectedIndex == 0)
                    UpdateAccessView();
                else if (comboBox_accessDevices_filter.SelectedIndex == 1)
                    UpdateAccessView(true, true);
                else if (comboBox_accessDevices_filter.SelectedIndex == 2)
                    UpdateAccessView(true);
            }
        }

        public void UpdateAccessView(bool filter = false, bool access = false, bool search = false, string searchstring = "")
        {
            dataGridView_accessDevices.Rows.Clear();
            if (filter && search)
            {
                foreach (var accessDevice in Accesses_list)
                {
                    //find user
                    DatabaseOperations.Users.SQLUser_record targetUser = null;
                    foreach (var user in Users_list)
                        if (user.card_number == accessDevice.card_number)
                        {
                            targetUser = user;
                            break;
                        }
                    //find device from office
                    DatabaseOperations.Office.SQLOffice_record targetOffice = null;
                    foreach (var office in Office_list)
                        if (office.index == accessDevice.office_id)
                        {
                            targetOffice = office;
                            break;
                        }
                    if (targetUser == null || targetOffice == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");
                    if ((System.Text.RegularExpressions.Regex.Replace(targetUser.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetUser.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetOffice.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetOffice.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetOffice.description, @"\s+", "") == searchstring) ||
                        (searchstring == "") &&
                        ((access && accessDevice.allowed) || (!access && !accessDevice.allowed)))
                    {
                        dataGridView_accessDevices.Rows.Add();
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[0].Value = accessDevice.allowed == true ? "Да" : "Нет";
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[1].Value = targetUser.name;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[2].Value = targetUser.surname;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[3].Value = targetOffice.cabinet;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[4].Value = targetOffice.department;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[5].Value = targetOffice.description;
                    }
                }
            }
            else if (filter && !search)
            {
                foreach (var accessDevice in Accesses_list)
                {
                    if ((access && accessDevice.allowed) || (!access && !accessDevice.allowed))
                    {
                        //find user
                        DatabaseOperations.Users.SQLUser_record targetUser = null;
                        foreach (var user in Users_list)
                            if (user.card_number == accessDevice.card_number)
                            {
                                targetUser = user;
                                break;
                            }
                        //find device from office
                        DatabaseOperations.Office.SQLOffice_record targetOffice = null;
                        foreach (var office in Office_list)
                            if (office.index == accessDevice.office_id)
                            {
                                targetOffice = office;
                                break;
                            }
                        if (targetUser == null || targetOffice == null)
                            throw new Exception("Данные устарели! Требуется перезапуск!");
                        dataGridView_accessDevices.Rows.Add();
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[0].Value = accessDevice.allowed == true ? "Да" : "Нет";
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[1].Value = targetUser.name;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[2].Value = targetUser.surname;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[3].Value = targetOffice.cabinet;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[4].Value = targetOffice.department;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[5].Value = targetOffice.description;
                    }
                }
            }
            else if (!filter && search)
            {
                foreach (var accessDevice in Accesses_list)
                {
                    //find user
                    DatabaseOperations.Users.SQLUser_record targetUser = null;
                    foreach (var user in Users_list)
                        if (user.card_number == accessDevice.card_number)
                        {
                            targetUser = user;
                            break;
                        }
                    //find device from office
                    DatabaseOperations.Office.SQLOffice_record targetOffice = null;
                    foreach (var office in Office_list)
                        if (office.index == accessDevice.office_id)
                        {
                            targetOffice = office;
                            break;
                        }
                    if (targetUser == null || targetOffice == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");
                    if ((System.Text.RegularExpressions.Regex.Replace(targetUser.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetUser.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetOffice.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetOffice.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetOffice.description, @"\s+", "") == searchstring) ||
                        (searchstring == ""))
                    {
                        dataGridView_accessDevices.Rows.Add();
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[0].Value = accessDevice.allowed == true ? "Да" : "Нет";
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[1].Value = targetUser.name;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[2].Value = targetUser.surname;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[3].Value = targetOffice.cabinet;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[4].Value = targetOffice.department;
                        dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[5].Value = targetOffice.description;
                    }
                }
            }
            else
            {
                foreach (var accessDevice in Accesses_list)
                {
                    //find user
                    DatabaseOperations.Users.SQLUser_record targetUser = null;
                    foreach (var user in Users_list)
                        if (user.card_number == accessDevice.card_number)
                        {
                            targetUser = user;
                            break;
                        }
                    //find device from office
                    DatabaseOperations.Office.SQLOffice_record targetOffice = null;
                    foreach (var office in Office_list)
                        if (office.index == accessDevice.office_id)
                        {
                            targetOffice = office;
                            break;
                        }
                    if (targetUser == null || targetOffice == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");
                    dataGridView_accessDevices.Rows.Add();
                    dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[0].Value = accessDevice.allowed == true ? "Да" : "Нет";
                    dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[1].Value = targetUser.name;
                    dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[2].Value = targetUser.surname;
                    dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[3].Value = targetOffice.cabinet;
                    dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[4].Value = targetOffice.department;
                    dataGridView_accessDevices.Rows[dataGridView_accessDevices.Rows.Count - 1].Cells[5].Value = targetOffice.description;
                }
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            AutoSystem_Operations.UpdateAccesses();
            GetAccessObject();
            UpdateAccessViewWrapper();

            GetJournalRequestsObject();
            UpdateJournalRequestsViewWrapper();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            UpdateAccessViewWrapper(true, textBox_accessDevices_searchstring.Text);
        }
        private void comboBox_accessDevices_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccessViewWrapper();
        }

        /*
         * JOURNAL ACCESS
         */

        public void GetJournalAccessObject()
        {
            Journal_access_list = DatabaseOperations.Journal_check.Get(SavedConnection);
            Math_Operations.sort_SQL(Journal_access_list);
        }

        public void UpdateJournalAccessViewWrapper(bool search = false, string searchstring = "")
        {
            if (search)
            {
                if (comboBox_access_filter.SelectedIndex == 0)
                    UpdateJournalAccessView(false, false, true, searchstring);
                else if (comboBox_access_filter.SelectedIndex == 1)
                    UpdateJournalAccessView(true, true, true, searchstring);
                else if (comboBox_access_filter.SelectedIndex == 2)
                    UpdateJournalAccessView(true, false, true, searchstring);
            }
            else
            {
                if (comboBox_access_filter.SelectedIndex == 0)
                    UpdateJournalAccessView();
                else if (comboBox_access_filter.SelectedIndex == 1)
                    UpdateJournalAccessView(true, true);
                else if (comboBox_access_filter.SelectedIndex == 2)
                    UpdateJournalAccessView(true);
            }
        }

        public void UpdateJournalAccessView(bool filter = false, bool access = false, bool search = false, string searchstring = "")
        {
            dataGridView_accessed_journal.Rows.Clear();
            if (filter && search)
            {
                foreach (var line in Journal_access_list)
                {
                    DatabaseOperations.Office.SQLOffice_record targetDeviceOffice = null; //(!)
                    DatabaseOperations.RFID.SQLRFID_record targetCard = null;
                    DatabaseOperations.Users.SQLUser_record targetUser = null; //(!)

                    //find device -> device from office
                    foreach (var office in Office_list)
                    {
                        if (office.index == line.office_id)
                        {
                            targetDeviceOffice = office;
                            break;
                        }
                    }
                    //find card
                    foreach (var card in RFID_list)
                    {
                        if (line.card_number == card.index)
                        {
                            targetCard = card;
                            break;
                        }
                    }
                    //find card -> user
                    foreach (var user in Users_list)
                        if (targetCard.index == user.card_number)
                        {
                            targetUser = user;
                            break;
                        }
                    if (targetUser == null || targetDeviceOffice == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");
                    if ((System.Text.RegularExpressions.Regex.Replace(targetUser.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetUser.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetDeviceOffice.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetDeviceOffice.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetDeviceOffice.description, @"\s+", "") == searchstring) ||
                        (searchstring == "") &&
                        ((access && line.accessed) || (!access && !line.accessed)))
                    {
                        dataGridView_accessed_journal.Rows.Add();
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[0].Value = line.index;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[1].Value = line.timeStamp;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[2].Value = line.accessed ? "Предоставлено" : "Отказано";
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[3].Value = targetUser.name;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[5].Value = targetDeviceOffice.cabinet;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[6].Value = targetDeviceOffice.department;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[7].Value = targetDeviceOffice.description;
                    }
                }
            }
            else if (filter && !search)
            {
                foreach (var line in Journal_access_list)
                {
                    if ((access && line.accessed) || (!access && !line.accessed))
                    {
                        DatabaseOperations.Office.SQLOffice_record targetDeviceOffice = null; //(!)
                        DatabaseOperations.RFID.SQLRFID_record targetCard = null;
                        DatabaseOperations.Users.SQLUser_record targetUser = null; //(!)

                        //find device -> device from office
                        foreach (var office in Office_list)
                        {
                            if (office.index == line.office_id)
                            {
                                targetDeviceOffice = office;
                                break;
                            }
                        }
                        //find card
                        foreach (var card in RFID_list)
                        {
                            if (line.card_number == card.index)
                            {
                                targetCard = card;
                                break;
                            }
                        }
                        //find card -> user
                        foreach (var user in Users_list)
                            if (targetCard.index == user.card_number)
                            {
                                targetUser = user;
                                break;
                            }
                        if (targetUser == null || targetDeviceOffice == null)
                            throw new Exception("Данные устарели! Требуется перезапуск!");
                        dataGridView_accessed_journal.Rows.Add();
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[0].Value = line.index;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[1].Value = line.timeStamp;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[2].Value = line.accessed ? "Предоставлено" : "Отказано";
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[3].Value = targetUser.name;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[5].Value = targetDeviceOffice.cabinet;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[6].Value = targetDeviceOffice.department;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[7].Value = targetDeviceOffice.description;
                    }
                }
            }
            else if (!filter && search)
            {
                foreach (var line in Journal_access_list)
                {
                    DatabaseOperations.Office.SQLOffice_record targetDeviceOffice = null; //(!)
                    DatabaseOperations.RFID.SQLRFID_record targetCard = null;
                    DatabaseOperations.Users.SQLUser_record targetUser = null; //(!)

                    //find device -> device from office
                    foreach (var office in Office_list)
                    {
                        if (office.index == line.office_id)
                        {
                            targetDeviceOffice = office;
                            break;
                        }
                    }
                    //find card
                    foreach (var card in RFID_list)
                    {
                        if (line.card_number == card.index)
                        {
                            targetCard = card;
                            break;
                        }
                    }
                    //find card -> user
                    foreach (var user in Users_list)
                        if (targetCard.index == user.card_number)
                        {
                            targetUser = user;
                            break;
                        }
                    if (targetUser == null || targetDeviceOffice == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");
                    if ((System.Text.RegularExpressions.Regex.Replace(targetUser.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetUser.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetDeviceOffice.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetDeviceOffice.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetDeviceOffice.description, @"\s+", "") == searchstring) ||
                        (searchstring == ""))
                    {
                        dataGridView_accessed_journal.Rows.Add();
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[0].Value = line.index;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[1].Value = line.timeStamp;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[2].Value = line.accessed ? "Предоставлено" : "Отказано";
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[3].Value = targetUser.name;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[5].Value = targetDeviceOffice.cabinet;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[6].Value = targetDeviceOffice.department;
                        dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[7].Value = targetDeviceOffice.description;
                    }
                }
            }
            else
            {
                foreach (var line in Journal_access_list)
                {
                    DatabaseOperations.Office.SQLOffice_record targetDeviceOffice = null; //(!)
                    DatabaseOperations.RFID.SQLRFID_record targetCard = null;  
                    DatabaseOperations.Users.SQLUser_record targetUser = null; //(!)

                    //find device -> device from office
                    foreach (var office in Office_list)
                    {
                        if (office.index == line.office_id)
                        {
                            targetDeviceOffice = office;
                            break;
                        }
                    }
                    //find card
                    foreach (var card in RFID_list)
                    {
                        if (line.card_number == card.index)
                        {
                            targetCard = card;
                            break;
                        }
                    }
                    //find card -> user
                    foreach (var user in Users_list)
                        if (targetCard.index == user.card_number)
                        {
                            targetUser = user;
                            break;
                        }
                    if (targetUser == null || targetDeviceOffice == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");

                    dataGridView_accessed_journal.Rows.Add();
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[0].Value = line.index;
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[1].Value = line.timeStamp;
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[2].Value = line.accessed ? "Предоставлено" : "Отказано";
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[3].Value = targetUser.name;
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[5].Value = targetDeviceOffice.cabinet;
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[6].Value = targetDeviceOffice.department;
                    dataGridView_accessed_journal.Rows[dataGridView_accessed_journal.Rows.Count - 1].Cells[7].Value = targetDeviceOffice.description;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateJournalAccessViewWrapper(true, textBox_journal_access_searchstring.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var selection = dataGridView_accessed_journal.SelectedRows;
            List<int> selected_indexes = new List<int>();

            for (int i = 0; i < selection.Count; i++)
            {
                selected_indexes.Add((int)selection[i].Cells[0].Value);
                dataGridView_accessed_journal.Rows.RemoveAt(selection[i].Index);
            }

            foreach (var index in selected_indexes)
            {
                DatabaseOperations.Journal_check.Delete_byIndex(SavedConnection, index);
            }

            GetJournalAccessObject();
            UpdateJournalAccessViewWrapper();
        }

        private void comboBox_access_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateJournalAccessViewWrapper();
        }

        /*
         * JOURNAL REQUESTS
         */

        public void GetJournalRequestsObject()
        {
            Journal_request_list = DatabaseOperations.Journal_add_remove.Get(SavedConnection);
            Math_Operations.sort_SQL(Journal_request_list);
        }

        public void UpdateJournalRequestsViewWrapper(bool search = false, string searchstring = "")
        {
            if (search)
            {
                if (comboBox_request_filter.SelectedIndex == 0)
                    UpdateJournalRequestsView(false, false, true, searchstring);
                else if (comboBox_request_filter.SelectedIndex == 1)
                    UpdateJournalRequestsView(true, true, true, searchstring);
                else if (comboBox_request_filter.SelectedIndex == 2)
                    UpdateJournalRequestsView(true, false, true, searchstring);
            }
            else
            {
                if (comboBox_request_filter.SelectedIndex == 0)
                    UpdateJournalRequestsView();
                else if (comboBox_request_filter.SelectedIndex == 1)
                    UpdateJournalRequestsView(true, true);
                else if (comboBox_request_filter.SelectedIndex == 2)
                    UpdateJournalRequestsView(true);
            }
        }

        public void UpdateJournalRequestsView(bool filter = false, bool access = false, bool search = false, string searchstring = "")
        {
            dataGridView_requests.Rows.Clear();
            if (filter && search)
            {
                foreach (var line in Journal_request_list)
                {
                    //find request
                    DatabaseOperations.Requests.SQLRequest_record targetRequest = null;
                    foreach (var request in Requests_list)
                        if (line.request_id == request.index)
                        {
                            targetRequest = request;
                            break;
                        }
                    //find request -> user
                    DatabaseOperations.Users.SQLUser_record targetUser = null;
                    if (!(targetRequest == null))
                    {
                        foreach (var user in Users_list)
                            if (targetRequest.user_id == user.index)
                            {
                                targetUser = user;
                                break;
                            }
                    }
                    if (targetUser == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");
                    if ((System.Text.RegularExpressions.Regex.Replace(targetUser.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetUser.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetRequest.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetRequest.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetRequest.description, @"\s+", "") == searchstring) ||
                        (searchstring == "") &&
                        ((access && line.add) || (!access && !line.add)))
                    {
                        if (targetRequest == null)
                        {
                            foreach (var doc in Docs_list)
                            {
                                if (doc.index == line.doc_id)
                                {
                                    targetRequest = new DatabaseOperations.Requests.SQLRequest_record(-1, -1, doc.cabinet + "_" + doc.department + "_" + doc.description, true, DateTime.Now);
                                    targetUser = new DatabaseOperations.Users.SQLUser_record(-1, doc.name, doc.surname, "", "", "", -1);
                                }
                            }
                        }
                        dataGridView_requests.Rows.Add();
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[0].Value = line.index;
                        if (targetRequest == null) //non-user request
                            dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = "-";
                        else
                            dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = targetRequest.request_time;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[2].Value = line.status;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[3].Value = targetUser.name;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[5].Value = line.add ? "Добавление" : "Удаление";
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[6].Value = targetRequest.cabinet;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[7].Value = targetRequest.department;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[8].Value = targetRequest.description;
                    }
                }
            }
            else if (filter && !search)
            {
                foreach (var line in Journal_request_list)
                {
                    if ((access && line.add) || (!access && !line.add))
                    {
                        //find request
                        DatabaseOperations.Requests.SQLRequest_record targetRequest = null;
                        foreach (var request in Requests_list)
                            if (line.request_id == request.index)
                            {
                                targetRequest = request;
                                break;
                            }
                        //find request -> user
                        DatabaseOperations.Users.SQLUser_record targetUser = null;
                        if (!(targetRequest == null))
                        {
                            foreach (var user in Users_list)
                                if (targetRequest.user_id == user.index)
                                {
                                    targetUser = user;
                                    break;
                                }
                        }
                        if (targetUser == null)
                            throw new Exception("Данные устарели! Требуется перезапуск!");
                        if (targetRequest == null)
                        {
                            foreach (var doc in Docs_list)
                            {
                                if (doc.index == line.doc_id)
                                {
                                    targetRequest = new DatabaseOperations.Requests.SQLRequest_record(-1, -1, doc.cabinet + "_" + doc.department + "_" + doc.description, true, DateTime.Now);
                                    targetUser = new DatabaseOperations.Users.SQLUser_record(-1, doc.name, doc.surname, "", "", "", -1);
                                }
                            }
                        }
                        dataGridView_requests.Rows.Add();
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[0].Value = line.index;
                        if (targetRequest == null) //non-user request
                            dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = "-";
                        else
                            dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = targetRequest.request_time;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[2].Value = line.status;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[3].Value = targetUser.name;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[5].Value = line.add ? "Добавление" : "Удаление";
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[6].Value = targetRequest.cabinet;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[7].Value = targetRequest.department;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[8].Value = targetRequest.description;
                    }
                }
            }
            else if (!filter && search)
            {
                foreach (var line in Journal_request_list)
                {
                    //find request
                    DatabaseOperations.Requests.SQLRequest_record targetRequest = null;
                    foreach (var request in Requests_list)
                        if (line.request_id == request.index)
                        {
                            targetRequest = request;
                            break;
                        }
                    //find request -> user
                    DatabaseOperations.Users.SQLUser_record targetUser = null;
                    if (!(targetRequest == null))
                    {
                        foreach (var user in Users_list)
                            if (targetRequest.user_id == user.index)
                            {
                                targetUser = user;
                                break;
                            }
                    }
                    if (targetUser == null)
                        throw new Exception("Данные устарели! Требуется перезапуск!");
                    if ((System.Text.RegularExpressions.Regex.Replace(targetUser.name, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetUser.surname, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetRequest.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetRequest.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(targetRequest.description, @"\s+", "") == searchstring) ||
                        (searchstring == ""))
                    {
                        if (targetRequest == null)
                        {
                            foreach (var doc in Docs_list)
                            {
                                if (doc.index == line.doc_id)
                                {
                                    targetRequest = new DatabaseOperations.Requests.SQLRequest_record(-1, -1, doc.cabinet + "_" + doc.department + "_" + doc.description, true, DateTime.Now);
                                    targetUser = new DatabaseOperations.Users.SQLUser_record(-1, doc.name, doc.surname, "", "", "", -1);
                                }
                            }
                        }
                        dataGridView_requests.Rows.Add();
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[0].Value = line.index;
                        if (targetRequest == null) //non-user request
                            dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = "-";
                        else
                            dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = targetRequest.request_time;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[2].Value = line.status;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[3].Value = targetUser.name;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[5].Value = line.add ? "Добавление" : "Удаление";
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[6].Value = targetRequest.cabinet;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[7].Value = targetRequest.department;
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[8].Value = targetRequest.description;
                    }
                }
            }
            else
            {
                foreach (var line in Journal_request_list)
                {
                    //find request
                    DatabaseOperations.Requests.SQLRequest_record targetRequest = null;
                    foreach (var request in Requests_list)
                        if (line.request_id == request.index)
                        {
                            targetRequest = request;
                            break;
                        }
                    //find request -> user
                    DatabaseOperations.Users.SQLUser_record targetUser = null;
                    if (!(targetRequest == null))
                    {
                        foreach (var user in Users_list)
                            if (targetRequest.user_id == user.index)
                            {
                                targetUser = user;
                                break;
                            }
                    }
                    if (targetRequest == null)
                    {
                        foreach (var doc in Docs_list)
                        {
                            if (doc.index == line.doc_id)
                            {
                                targetRequest = new DatabaseOperations.Requests.SQLRequest_record(-1, -1, doc.cabinet+"_"+doc.department+"_"+doc.description, true, DateTime.Now);
                                targetUser = new DatabaseOperations.Users.SQLUser_record(-1, doc.name, doc.surname, "", "", "", -1);
                            }
                        }
                    }
                    dataGridView_requests.Rows.Add();
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[0].Value = line.index;
                    if (targetRequest == null) //non-user request
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = "-";
                    else
                        dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = targetRequest.request_time;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[2].Value = line.status;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[3].Value = targetUser.name;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[4].Value = targetUser.surname;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[5].Value = line.add ? "Добавление" : "Удаление";
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[6].Value = targetRequest.cabinet;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[7].Value = targetRequest.department;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[8].Value = targetRequest.description;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UpdateJournalRequestsViewWrapper(true, textBox_requests_searchstring.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var selection = dataGridView_requests.SelectedRows;
            List<int> selected_indexes = new List<int>();

            for (int i = 0; i < selection.Count; i++)
            {
                selected_indexes.Add((int)selection[i].Cells[0].Value);
                dataGridView_requests.Rows.RemoveAt(selection[i].Index);
            }

            foreach (var index in selected_indexes)
            {
                DatabaseOperations.Journal_add_remove.Delete_byIndex(SavedConnection, index);
            }

            GetJournalRequestsObject();
            UpdateJournalRequestsViewWrapper();
        }

        private void comboBox_request_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateJournalRequestsViewWrapper();
        }
    }
}
