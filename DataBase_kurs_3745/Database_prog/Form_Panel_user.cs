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
    public partial class Form_Panel_user : Form
    {
        NpgsqlConnection SavedConnection;

        List<DatabaseOperations.Requests.SQLRequest_record> Requests_list = new List<DatabaseOperations.Requests.SQLRequest_record>();
        List<DatabaseOperations.Accesses.SQLAccess_record> Access_list = new List<DatabaseOperations.Accesses.SQLAccess_record>();

        DatabaseOperations.Users.SQLUser_record thisUser = null;
        public Form_Panel_user(NpgsqlConnection connection, string login)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.CenterToScreen();

            SavedConnection = connection;

            comboBox_access_filter.SelectedIndex = 0;

            //get auths
            var Auths = DatabaseOperations.Auth.Get(SavedConnection);

            //get Users
            var Users = DatabaseOperations.Users.Get(SavedConnection);

            foreach (var pair in Auths)
            {
                if (System.Text.RegularExpressions.Regex.Replace(pair.login, @"\s+", "") == login)
                {
                    foreach (var user in Users)
                    {
                        if (pair.index == user.auth_id)
                        {
                            thisUser = user;
                            label_user_name.Text = user.name;
                            label_surname.Text = user.surname;
                            label_role.Text = user.role;
                            label_room.Text = user.cabinet;
                            label_department.Text = user.department;
                        }
                    }
                }
            }

            GetRequests();
            UpdateRequestsView();
            GetAccesses();
            UpdateAccessesViewWrapper();
        }

        public void GetRequests()
        {
            Requests_list = DatabaseOperations.Requests.Get(SavedConnection);
        }

        public void UpdateRequestsView()
        {
            //View Requests
            dataGridView_requests.Rows.Clear();
            var Journal_requests = DatabaseOperations.Journal_add_remove.Get(SavedConnection);
            foreach (var request in Requests_list)
            {
                if (request.user_id == thisUser.index)
                {
                    string status = "";
                    foreach (var line in Journal_requests)
                    {
                        if (line.request_id == request.index)
                        {
                            status = line.status;
                            break;
                        }
                    }
                    dataGridView_requests.Rows.Add();
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[0].Value = request.request_time;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[1].Value = status;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[2].Value = request.cabinet;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[3].Value = request.department;
                    dataGridView_requests.Rows[dataGridView_requests.Rows.Count - 1].Cells[4].Value = request.description;
                }
            }
        }

        public void GetAccesses()
        {
            Access_list = DatabaseOperations.Accesses.Get(SavedConnection);
            Math_Operations.sort_SQL(Access_list);
        }

        public void UpdateAccessesViewWrapper()
        {
            if (comboBox_access_filter.SelectedIndex == 0)
                UpdateAccessesView();
            else if (comboBox_access_filter.SelectedIndex == 1)
                UpdateAccessesView(true, true);
            else
                UpdateAccessesView(true);
        }

        public void UpdateAccessesView(bool filter = false, bool accessed = false)
        {
            dataGridView_accessed.Rows.Clear();
            foreach (var access in Access_list)
            {
                if ((filter && (accessed && access.allowed)) ||
                        (filter && (!accessed && !access.allowed)) ||
                        (!filter))
                {
                    //find device info from office
                    var Office = DatabaseOperations.Office.Get(SavedConnection);
                    DatabaseOperations.Office.SQLOffice_record targetOffice = null;
                    foreach (var room in Office)
                    {
                        if (room.index == access.office_id)
                        {
                            targetOffice = room;
                            break;
                        }
                    }
                    if (access.card_number == thisUser.card_number)
                    {
                        dataGridView_accessed.Rows.Add();
                        dataGridView_accessed.Rows[dataGridView_accessed.Rows.Count - 1].Cells[0].Value = access.allowed ? "Да" : "Нет";
                        dataGridView_accessed.Rows[dataGridView_accessed.Rows.Count - 1].Cells[1].Value = targetOffice.cabinet;
                        dataGridView_accessed.Rows[dataGridView_accessed.Rows.Count - 1].Cells[2].Value = targetOffice.department;
                        dataGridView_accessed.Rows[dataGridView_accessed.Rows.Count - 1].Cells[3].Value = targetOffice.description;
                    }
                }
            }
        }

        private void выйтиИзСистемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete("session.token");
            var auth = new Form_Auth();
            auth.Closed += (s, args) => this.Close();
            auth.Show();
            this.Hide();
        }

        private void button_new_request_Click(object sender, EventArgs e)
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

            var form = new Form_New_User_Request(SavedConnection, thisUser.index);
            form.ShowDialog();
            shadow.Dispose();
            shadow.Close();

            GetAccesses();
            UpdateAccessesViewWrapper();

            GetRequests();
            UpdateRequestsView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hash_before = "";
            var Logins = DatabaseOperations.Auth.Get(SavedConnection);
            foreach (var login in Logins)
            {
                if (login.index == thisUser.auth_id)
                    hash_before = login.hash;
            }

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

            var form = new Form_Change_Auth(SavedConnection);
            form.ShowDialog();
            shadow.Dispose();
            shadow.Close();

            string hash_after = "";
            Logins = DatabaseOperations.Auth.Get(SavedConnection);
            foreach (var login in Logins)
            {
                if (login.index == thisUser.auth_id)
                    hash_after = login.hash;
            }
            if (hash_before != hash_after)
            {
                MessageBox.Show("Необходимо выполнить повторный вход!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.IO.File.Delete("session.token");
                var auth = new Form_Auth();
                auth.Closed += (s, args) => this.Close();
                auth.Show();
                this.Hide();
            }
        }

        private void comboBox_access_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccessesViewWrapper();
        }
    }
}
