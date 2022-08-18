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
    public partial class Form_New_User : Form
    {
        Npgsql.NpgsqlConnection NpgsqlConnection;

        List<DatabaseOperations.RFID.SQLRFID_record> RFIDs;
        List<DatabaseOperations.Office.SQLOffice_record> Office;
        public Form_New_User(Npgsql.NpgsqlConnection connection)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CenterToScreen();

            NpgsqlConnection = connection;

            RFIDs = DatabaseOperations.RFID.Get(NpgsqlConnection);
            Office = DatabaseOperations.Office.Get(NpgsqlConnection);

            foreach(var card in RFIDs)
            {
                if (!card.given)
                {
                    dataGridView_rfid.Rows.Add();
                    dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[0].Value = card.index;
                    dataGridView_rfid.Rows[dataGridView_rfid.Rows.Count - 1].Cells[1].Value = card.RFID_code;
                }
            }

            foreach(var room in Office)
            {
                if (!room.device)
                {
                    dataGridView_office.Rows.Add();
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[0].Value = room.index;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[1].Value = room.cabinet;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[2].Value = room.department;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[3].Value = room.description;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var searchstring = textBox_office_searchstring.Text;
            dataGridView_office.Rows.Clear();
            foreach (var room in Office)
            {
                if (!room.device)
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox_name.TextLength != 0 && textBox_surname.TextLength != 0)
            {
                string login = textBox_surname.Text + "-" + textBox_name.Text[0];
                textBox_login.Text = Translit(login);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text = GeneratePassword(16);
        }
        public static string Translit(string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "", "E", "Yu", "Ya" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "", "e", "yu", "ya" };
            string[] rus_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }
        public string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            //const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890[]{}<>?/.,$:;"; //75
            StringBuilder res = new StringBuilder();

            System.Security.Cryptography.RNGCryptoServiceProvider rnd = new System.Security.Cryptography.RNGCryptoServiceProvider();

            while (0 < length--)
            {
                int random_numb = 100;
                
                while (random_numb > valid.Length - 1)
                {
                    byte[] buffer = new byte[256];
                    rnd.GetBytes(buffer);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        random_numb = buffer[i];
                        if (random_numb < valid.Length)
                            break;
                    }
                }

                res.Append(valid[random_numb]);
            }
            rnd.Dispose();
            return res.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_name.TextLength == 0)
                    throw new Exception("Имя должно быть заполнено!");
                else if (textBox_name.TextLength > 20)
                    throw new Exception("Имя не должно превышать 20 символов!");
                if (textBox_surname.TextLength == 0)
                    throw new Exception("Фамилия должна быть заполнена!");
                else if (textBox_surname.TextLength > 20)
                    throw new Exception("Фамилия не должна превышать 20 символов!");
                if (textBox_job.TextLength == 0)
                    throw new Exception("Должность должна быть заполнена!");
                else if (textBox_job.TextLength > 40)
                    throw new Exception("Должность не должна превышать 40 символов!");
                if (textBox_login.TextLength == 0)
                    throw new Exception("Логин должен быть заполнен!");
                else if (textBox_login.TextLength > 20)
                    throw new Exception("Логин не должен превышать 20 символов!");
                if (textBox_pwd.TextLength == 0)
                    throw new Exception("Пароль должен быть заполнен!");
                else if (textBox_pwd.TextLength < 10)
                    throw new Exception("Пароль должен быть не короче 10 символов!");

                //get selected card id
                var selection_cards = dataGridView_rfid.SelectedRows;
                List<int> selected_indexes = new List<int>();

                for (int i = 0; i < selection_cards.Count; i++)
                    selected_indexes.Add((int)selection_cards[i].Cells[0].Value);
                int users_card_id;
                if (selected_indexes.Count != 1)
                    throw new Exception("Должна быть выбрана одна карта для пользователя!");
                else
                    users_card_id = selected_indexes[0];
                selected_indexes.Clear();
                //get selected office id
                var selection_offices = dataGridView_office.SelectedRows;

                for (int i = 0; i < selection_offices.Count; i++)
                    selected_indexes.Add((int)selection_offices[i].Cells[0].Value);
                int users_office_id;
                if (selected_indexes.Count != 1)
                    throw new Exception("Должен быть выбран только один кабинет для пользователя!");
                else
                    users_office_id = selected_indexes[0];
                //new auth add
                //get auths for id
                var Auths = DatabaseOperations.Auth.Get(NpgsqlConnection);
                Math_Operations.sort_SQL(Auths);
                int new_auth_id = Auths[Auths.Count - 1].index + 1;
                //insert new auth
                string new_hash = Math_Operations.ComputeSha256Hash(textBox_pwd.Text);
                DatabaseOperations.Auth.Insert(NpgsqlConnection, new_auth_id, textBox_login.Text, new_hash);
                //new user add
                //get users for id
                var Users = DatabaseOperations.Users.Get(NpgsqlConnection);
                Math_Operations.sort_SQL(Users);
                int new_user_id;
                if (Users.Count == 0)
                    new_user_id = 0;
                else
                    new_user_id = Users[Users.Count - 1].index + 1;
                //insert new
                DatabaseOperations.Users.Insert(NpgsqlConnection, 
                    new_user_id, textBox_name.Text, textBox_surname.Text, textBox_job.Text,
                    users_office_id, users_card_id, new_auth_id);

                DatabaseOperations.RFID.Claim_byIndex(NpgsqlConnection, users_card_id);

                AutoSystem_Operations.UpdateAccesses();

                string message = "Информация пользователя:" + Environment.NewLine +
                                 "Логин: " + textBox_login.Text + Environment.NewLine +
                                 "Пароль: " + textBox_pwd.Text;
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
