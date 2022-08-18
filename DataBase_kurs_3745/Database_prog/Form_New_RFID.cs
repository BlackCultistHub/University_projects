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
    public partial class Form_New_RFID : Form
    {
        Npgsql.NpgsqlConnection NpgsqlConnection;
        public Form_New_RFID(Npgsql.NpgsqlConnection connection)
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
                if (textBox_rfid_code.TextLength > 64)
                    throw new Exception("Длина кода должна быть 64 байта!");

                int id;
                string code = textBox_rfid_code.Text;

                var RFIDs = DatabaseOperations.RFID.Get(NpgsqlConnection);
                Math_Operations.sort_SQL(RFIDs);
                if (RFIDs.Count == 0)
                    id = 0;
                else
                    id = RFIDs[RFIDs.Count - 1].index + 1;

                DatabaseOperations.RFID.Insert(NpgsqlConnection, id, code, false);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
