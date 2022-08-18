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
    public partial class Form_Change_Auth : Form
    {
        Npgsql.NpgsqlConnection NpgsqlConnection;
        public Form_Change_Auth(Npgsql.NpgsqlConnection connection)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CenterToScreen();

            NpgsqlConnection = connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //try login
                var Logins = DatabaseOperations.Auth.Get(NpgsqlConnection);
                bool good = false;
                foreach (var login in Logins)
                {
                    var loginDB = System.Text.RegularExpressions.Regex.Replace(login.login, @"\s+", "");
                    var hashDB = System.Text.RegularExpressions.Regex.Replace(login.hash, @"\s+", "");
                    var hash = Math_Operations.ComputeSha256Hash(textBox_password.Text);
                    if ((textBox_login.Text == loginDB) && (hash == hashDB))
                    {
                        good = true;
                        if (textBox_newPwd.TextLength < 10)
                            throw new Exception("Пароль должен быть не короче 10 символов!");
                        if (textBox_newPwd.Text != textBox_newPwdCheck.Text)
                            throw new Exception("Пароли не совпадают!");
                        DatabaseOperations.Auth.NewPwd_byIndex(NpgsqlConnection, login.index, Math_Operations.ComputeSha256Hash(textBox_newPwd.Text));
                        MessageBox.Show("Пароль изменён!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        break;
                    }
                }
                if (!good)
                    throw new Exception("Логин или пароль введены неверно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
