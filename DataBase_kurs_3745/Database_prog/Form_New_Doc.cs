using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_prog
{
    public partial class Form_New_Doc : Form
    {
        Npgsql.NpgsqlConnection NpgsqlConnection;

        List<DatabaseOperations.Users.SQLUser_record> Users;
        List<DatabaseOperations.Office.SQLOffice_record> Office;
        public Form_New_Doc(Npgsql.NpgsqlConnection connection)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CenterToScreen();

            NpgsqlConnection = connection;

            comboBox_docType.SelectedIndex = 0;

            Office = DatabaseOperations.Office.Get(NpgsqlConnection);
            foreach (var room in Office)
            {
                if (room.device)
                {
                    dataGridView_office.Rows.Add();
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[0].Value = room.index;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[1].Value = room.cabinet;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[2].Value = room.department;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[3].Value = room.description;
                }
            }

            Users = DatabaseOperations.Users.Get(NpgsqlConnection);
            foreach (var user in Users)
            {
                dataGridView_users.Rows.Add();
                dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[0].Value = user.index;
                dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[1].Value = user.name;
                dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[2].Value = user.surname;
                dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[3].Value = user.role;
                dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[4].Value = user.cabinet;
                dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[5].Value = user.department;
            }
        }
        //Search office
        private void button1_Click(object sender, EventArgs e)
        {
            var searchstring = textBox_office_searchstring.Text;
            dataGridView_office.Rows.Clear();
            foreach (var room in Office)
            {
                if (room.device)
                {
                    if (((System.Text.RegularExpressions.Regex.Replace(room.cabinet, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(room.department, @"\s+", "") == searchstring) ||
                        (System.Text.RegularExpressions.Regex.Replace(room.description, @"\s+", "") == searchstring)) || (searchstring == ""))
                    {
                        dataGridView_office.Rows.Add();
                        dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[0].Value = room.index;
                        dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[1].Value = room.cabinet;
                        dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[2].Value = room.department;
                        dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[3].Value = room.description;
                    }
                }
            }
        }
        //Search users
        private void button7_Click(object sender, EventArgs e)
        {
            var searchstring = textBox_users_searchstring.Text;
            dataGridView_users.Rows.Clear();
            foreach (var user in Users)
            {
                
                if (((System.Text.RegularExpressions.Regex.Replace(user.name, @"\s+", "") == searchstring) ||
                    (System.Text.RegularExpressions.Regex.Replace(user.surname, @"\s+", "") == searchstring) ||
                    (System.Text.RegularExpressions.Regex.Replace(user.role, @"\s+", "") == searchstring) ||
                    (System.Text.RegularExpressions.Regex.Replace(user.cabinet, @"\s+", "") == searchstring) ||
                    (System.Text.RegularExpressions.Regex.Replace(user.department, @"\s+", "") == searchstring)) || (searchstring == ""))
                {
                    dataGridView_users.Rows.Add();
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[0].Value = user.index;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[1].Value = user.name;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[2].Value = user.surname;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[3].Value = user.role;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[4].Value = user.cabinet;
                    dataGridView_users.Rows[dataGridView_users.Rows.Count - 1].Cells[5].Value = user.department;
                }
            }
        }

        //ADD NEW
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //get selected user id
                var selection_users = dataGridView_users.SelectedRows;
                List<int> selected_indexes = new List<int>();

                for (int i = 0; i < selection_users.Count; i++)
                    selected_indexes.Add((int)selection_users[i].Cells[0].Value);
                int user_id;
                if (selected_indexes.Count != 1)
                    throw new Exception("Должен быть выбран только один пользователь!");
                else
                    user_id = selected_indexes[0];
                selected_indexes.Clear();
                //get selected device id
                var selection_devices = dataGridView_office.SelectedRows;

                for (int i = 0; i < selection_devices.Count; i++)
                    selected_indexes.Add((int)selection_devices[i].Cells[0].Value);
                int device_id;
                if (selected_indexes.Count != 1)
                    throw new Exception("Должно быть выбрано только одно устройство!");
                else
                    device_id = selected_indexes[0];

                var Docs = DatabaseOperations.Docs.Get(NpgsqlConnection);
                Math_Operations.sort_SQL(Docs);

                int new_doc_id;
                if (Docs.Count == 0)
                    new_doc_id = 0;
                else
                    new_doc_id = Docs[Docs.Count - 1].index + 1;

                bool access;
                if (comboBox_docType.SelectedIndex == 0)
                    access = false;
                else
                    access = true;

                DatabaseOperations.Docs.Insert(NpgsqlConnection, new_doc_id, access, user_id, device_id);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
