using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Npgsql;
using IniParser;
using IniParser.Model;

namespace Database_prog
{
    public partial class Form_Auth : Form
    {
        NpgsqlConnection NpgsqlConnection;

        public string connectionString = "";
        public string login = "";
        public string pwd = "";
        public string pwd_hash = "";

        //AUTH
        bool ALLOW_USER = false;
        bool ALLOW_SYSAMIN = false;
        bool ALLOW_ADMIN = false;

        public Form_Auth()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.CenterToScreen();

            timer1.Start();

            //CHECK TOKEN
            if (File.Exists("session.token"))
            {
                var token_parser = new FileIniDataParser();
                IniData token_data = token_parser.ReadFile("session.token");
                login = token_data["Session"]["Login"];
                pwd = token_data["Session"]["Pwd"];

                string role_pwd = "";
                string role_name = "";
                if (login == "Сотрудник_СБ")
                {
                    role_name = "Сотрудник_СБ";
                    role_pwd = "SecDep";
                }
                else if (login == "Системный_администратор")
                {
                    role_name = "Системный_администратор";
                    role_pwd = "SysAdmin";
                }
                else
                {
                    role_name = "Сотрудник_компании";
                    role_pwd = "qwerty";
                }

                var user_pwd = pwd;
                var hash = Math_Operations.ComputeSha256Hash(user_pwd);

                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("config.ini");
                var host = data["Connection"]["Host"];
                var port = data["Connection"]["Port"];
                var default_db = data["Connection"]["Database"];

                connectionString = "Host=" + host + ";Port=" + port + ";Username=" + role_name + ";Password=" + role_pwd + ";Database=" + default_db + ";";

                var Connection = DatabaseOperations.connect(connectionString);
                if (Connection == null)
                    throw new Exception("Невозможно подключиться к базе данных!");
                var Logins = DatabaseOperations.getAuthData(Connection);
                bool goodLogin = false;
                foreach (var Login in Logins)
                {
                    Login.login = System.Text.RegularExpressions.Regex.Replace(Login.login, @"\s+", "");
                    Login.hash = System.Text.RegularExpressions.Regex.Replace(Login.hash, @"\s+", "");
                    if (Login.login == login && Login.hash == hash)
                    {
                        if (role_name == "Сотрудник_компании")
                        {
                            NpgsqlConnection = Connection;
                            goodLogin = true;
                            ALLOW_USER = true;
                            break;
                        }
                        else if (role_name == "Сотрудник_СБ")
                        {
                            NpgsqlConnection = Connection;
                            goodLogin = true;
                            ALLOW_ADMIN = true;
                            break;
                        }
                        else if (role_name == "Системный_администратор")
                        {
                            NpgsqlConnection = Connection;
                            goodLogin = true;
                            ALLOW_SYSAMIN = true;
                            break;
                        }
                    }
                }
                if (!goodLogin)
                {
                    File.Delete("session.token");
                    throw new Exception("Токен повреждён или данные авторизации изменены!");
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ALLOW_USER)
            {
                UserPanel();
            }
            else if (ALLOW_ADMIN)
            {
                AdminPanel();
            }
            else if (ALLOW_SYSAMIN)
            {
                SysAdminPanel();
            }
        }

        private void SysAdminPanel()
        {
            timer1.Stop();
            this.Hide();
            var panel = new Form_Panel_sysadmin(NpgsqlConnection);
            panel.Closed += (s, args) => this.Close();
            panel.Show();
        }

        private void AdminPanel()
        {
            timer1.Stop();
            this.Hide();
            var panel = new Form_Panel_admin(NpgsqlConnection);
            panel.Closed += (s, args) => this.Close();
            panel.Show();
        }

        private void UserPanel()
        {
            timer1.Stop();
            this.Hide();
            var panel = new Form_Panel_user(NpgsqlConnection, login);
            panel.Closed += (s, args) => this.Close();
            panel.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("config.ini"))
                {
                    var parser = new FileIniDataParser();
                    IniData data = new IniData();
                    data.Sections.Add(new SectionData("Connection"));
                    data.Sections["Connection"].AddKey("Host");
                    data.Sections["Connection"]["Host"] = "127.0.0.1";
                    data.Sections["Connection"].AddKey("Port");
                    data.Sections["Connection"]["Port"] = "5432";
                    data.Sections["Connection"].AddKey("Database");
                    data.Sections["Connection"]["Database"] = "Контроль_доступа";
                    parser.WriteFile("config.ini", data);
                    //connectionString = "Host=127.0.0.1;Port=5432;Username=" + settings[2] + ";Password=" + settings[3] + ";Database=Контроль_доступа";
                }
                else
                {
                    var parser = new FileIniDataParser();
                    IniData data = parser.ReadFile("config.ini");
                    var host = data["Connection"]["Host"];
                    var port = data["Connection"]["Port"];
                    var default_db = data["Connection"]["Database"];

                    var user_login = textBox_login.Text;

                    string role_pwd = "";
                    string role_name = "";
                    if (user_login == "Сотрудник_СБ")
                    {
                        role_name = "Сотрудник_СБ";
                        role_pwd = "SecDep";
                    }
                    else if (user_login == "Системный_администратор")
                    {
                        role_name = "Системный_администратор";
                        role_pwd = "SysAdmin";
                    }
                    else if (user_login == "Автоматизированная_система")
                    {
                        role_name = "Автоматизированная_система";
                        role_pwd = "AutoSystem";
                    }
                    else
                    {
                        role_name = "Сотрудник_компании";
                        role_pwd = "qwerty";
                    }

                    var user_pwd = textBox_password.Text;
                    var hash = Math_Operations.ComputeSha256Hash(user_pwd);

                    connectionString = "Host=" + host + ";Port=" + port + ";Username=" + role_name + ";Password=" + role_pwd + ";Database=" + default_db + ";";

                    var Connection = DatabaseOperations.connect(connectionString);
                    if (Connection == null)
                        throw new Exception("Невозможно подключиться к базе данных!");
                    var Logins = DatabaseOperations.getAuthData(Connection);
                    bool goodLogin = false;
                    foreach (var Login in Logins)
                    {
                        Login.login = System.Text.RegularExpressions.Regex.Replace(Login.login, @"\s+", "");
                        Login.hash = System.Text.RegularExpressions.Regex.Replace(Login.hash, @"\s+", "");
                        if (Login.login == user_login && Login.hash == hash)
                        {
                            if (role_name == "Сотрудник_компании")
                            {
                                //MAKE TOKEN
                                var token_parser = new FileIniDataParser();
                                IniData token_data = new IniData();
                                token_data.Sections.Add(new SectionData("Session"));
                                token_data.Sections["Session"].AddKey("Login");
                                token_data.Sections["Session"]["Login"] = user_login;
                                token_data.Sections["Session"].AddKey("Pwd");
                                token_data.Sections["Session"]["Pwd"] = user_pwd;
                                token_parser.WriteFile("session.token", token_data);

                                this.Hide();
                                var panel = new Form_Panel_user(Connection, user_login);
                                panel.Closed += (s, args) => this.Close();
                                panel.Show();
                                goodLogin = true;
                                NpgsqlConnection = Connection;
                                break;
                            }
                            else if (role_name == "Сотрудник_СБ")
                            {
                                //MAKE TOKEN
                                var token_parser = new FileIniDataParser();
                                IniData token_data = new IniData();
                                token_data.Sections.Add(new SectionData("Session"));
                                token_data.Sections["Session"].AddKey("Login");
                                token_data.Sections["Session"]["Login"] = user_login;
                                token_data.Sections["Session"].AddKey("Pwd");
                                token_data.Sections["Session"]["Pwd"] = user_pwd;
                                token_parser.WriteFile("session.token", token_data);

                                this.Hide();
                                var panel = new Form_Panel_admin(Connection);
                                panel.Closed += (s, args) => this.Close();
                                panel.Show();
                                goodLogin = true;
                                NpgsqlConnection = Connection;
                                break;
                            }
                            else if (role_name == "Системный_администратор")
                            {
                                //MAKE TOKEN
                                var token_parser = new FileIniDataParser();
                                IniData token_data = new IniData();
                                token_data.Sections.Add(new SectionData("Session"));
                                token_data.Sections["Session"].AddKey("Login");
                                token_data.Sections["Session"]["Login"] = user_login;
                                token_data.Sections["Session"].AddKey("Pwd");
                                token_data.Sections["Session"]["Pwd"] = user_pwd;
                                token_parser.WriteFile("session.token", token_data);

                                this.Hide();
                                var panel = new Form_Panel_sysadmin(Connection);
                                panel.Closed += (s, args) => this.Close();
                                panel.Show();
                                goodLogin = true;
                                NpgsqlConnection = Connection;
                                break;
                            }
                            else if (role_name == "Автоматизированная_система")
                            {
                                this.Hide();
                                var panel = new Form_panel_acces_control_emulator(Connection);
                                panel.Closed += (s, args) => this.Close();
                                panel.Show();
                                goodLogin = true;
                                NpgsqlConnection = Connection;
                                break;
                            }
                        }
                    }
                    if (!goodLogin)
                        throw new Exception("Неверно введён логин или пароль!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_Auth_FormClosing(object sender, FormClosingEventArgs e)
        {
            DatabaseOperations.close_connection(NpgsqlConnection);
        }
    }
}
