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
    public partial class Form_New_User_Request : Form
    {
        Npgsql.NpgsqlConnection SavedConnection;

        int user_id;
        public Form_New_User_Request(Npgsql.NpgsqlConnection connection, int user_id_)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CenterToScreen();

            SavedConnection = connection;
            this.user_id = user_id_;

            var Office = DatabaseOperations.Office.Get(connection);
            foreach(var device in Office)
            {
                if (device.device)
                {
                    dataGridView_devices.Rows.Add();
                    dataGridView_devices.Rows[dataGridView_devices.Rows.Count - 1].Cells[0].Value = device.cabinet;
                    dataGridView_devices.Rows[dataGridView_devices.Rows.Count - 1].Cells[1].Value = device.department;
                    dataGridView_devices.Rows[dataGridView_devices.Rows.Count - 1].Cells[2].Value = device.description;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string request_device = "";

                var selection = dataGridView_devices.SelectedRows;
                List<string> selected_ones = new List<string>();

                for (int i = 0; i < selection.Count; i++)
                {
                    selected_ones.Add((string)selection[i].Cells[0].Value);
                    request_device = (string)selection[i].Cells[0].Value + "_" + (string)selection[i].Cells[1].Value + "_" + (string)selection[i].Cells[2].Value;
                }

                if (selected_ones.Count > 1)
                    throw new Exception("Должно быть выбрано лишь одно устройство!");

                var Requests = DatabaseOperations.Requests.Get(SavedConnection);
                Math_Operations.sort_SQL(Requests);

                int next_id;
                if (Requests.Count == 0)
                    next_id = 0;
                else
                    next_id = Requests[Requests.Count - 1].index + 1;

                DatabaseOperations.Requests.Insert(SavedConnection, next_id, user_id, request_device, true, DateTime.Now);

                AutoSystem_Operations.UpdateAccesses(true, user_id);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
