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
    public partial class Form_panel_acces_control_emulator : Form
    {
        Npgsql.NpgsqlConnection NpgsqlConnection;
        List<DatabaseOperations.Office.SQLOffice_record> Office;
        public Form_panel_acces_control_emulator(Npgsql.NpgsqlConnection connection)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CenterToScreen();

            NpgsqlConnection = connection;

            Office = DatabaseOperations.Office.Get(connection);
            foreach (var device in Office)
            {
                if (device.device)
                {
                    dataGridView_devices.Rows.Add();
                    dataGridView_devices.Rows[dataGridView_devices.Rows.Count - 1].Cells[0].Value = device.index;
                    dataGridView_devices.Rows[dataGridView_devices.Rows.Count - 1].Cells[2].Value = device.cabinet;
                    dataGridView_devices.Rows[dataGridView_devices.Rows.Count - 1].Cells[3].Value = device.department;
                    dataGridView_devices.Rows[dataGridView_devices.Rows.Count - 1].Cells[4].Value = device.description;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selection = dataGridView_devices.SelectedRows;
            List<int> selected_ones = new List<int>();

            for (int i = 0; i < selection.Count; i++)
            {
                selected_ones.Add((int)selection[i].Cells[0].Value);
            }

            if (selected_ones.Count > 1)
                throw new Exception("Должно быть выбрано лишь одно устройство!");
            if (AutoSystem_Operations.CheckAccess(textBox_rfid.Text, selected_ones[0]))
                MessageBox.Show("Доступ разрешён", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Доступ запрещён", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
