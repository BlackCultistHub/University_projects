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
    public partial class Form_New_Room_Device : Form
    {
        Npgsql.NpgsqlConnection NpgsqlConnection;
        public Form_New_Room_Device(Npgsql.NpgsqlConnection connection)
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
                if (textBox_cabinet.TextLength > 10)
                    throw new Exception("Длина названия кабинета макс 10 символов!");
                if (textBox_department.TextLength > 30)
                    throw new Exception("Длина названия кабинета макс 30 символов!");
                if (textBox_description.TextLength > 30)
                    throw new Exception("Длина названия кабинета макс 30 символов!");

                int id;
                string cabinet = textBox_cabinet.Text;
                string department = textBox_department.Text;
                string description = textBox_description.Text;
                bool device = checkBox_isDevice.Checked;

                var Office = DatabaseOperations.Office.Get(NpgsqlConnection);
                Math_Operations.sort_SQL(Office);

                if (Office.Count == 0)
                    id = 0;
                else
                    id = Office[Office.Count - 1].index + 1;

                DatabaseOperations.Office.Insert(NpgsqlConnection, id, device, cabinet, department, description);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
