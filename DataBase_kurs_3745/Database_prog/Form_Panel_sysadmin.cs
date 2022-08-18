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
    public partial class Form_Panel_sysadmin : Form
    {
        NpgsqlConnection SavedConnection;

        List<DatabaseOperations.Office.SQLOffice_record> Office = new List<DatabaseOperations.Office.SQLOffice_record>();
        public Form_Panel_sysadmin(NpgsqlConnection connection)
        {
            SavedConnection = connection;

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.CenterToScreen();

            comboBox_filter.SelectedIndex = 0;

            GetOfficeObject();
            UpdateView();
        }

        //get Office object
        private void GetOfficeObject()
        {
            Office = DatabaseOperations.Office.Get(SavedConnection);
            Math_Operations.sort_SQL(Office);
        }

        //update wrapper

        private void UpdateViewWrapper()
        {
            if (comboBox_filter.SelectedIndex == 0)
                UpdateView();
            else if (comboBox_filter.SelectedIndex == 1)
                UpdateView(true);
            else if (comboBox_filter.SelectedIndex == 2)
                UpdateView(true, true);
        }

        //update view
        private void UpdateView(bool filter = false, bool device = false)
        {
            dataGridView_office.Rows.Clear();
            if (!filter)
            {
                foreach (var record in Office)
                {
                    dataGridView_office.Rows.Add();
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[0].Value = record.index;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[1].Value = record.device ? "Да" : "Нет";
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[2].Value = record.cabinet;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[3].Value = record.department;
                    dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[4].Value = record.description;
                }
            }
            else
            {
                if (device)
                {
                    foreach (var record in Office)
                    {
                        if (record.device)
                        {
                            dataGridView_office.Rows.Add();
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[0].Value = record.index;
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[1].Value = record.device ? "Да" : "Нет";
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[2].Value = record.cabinet;
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[3].Value = record.department;
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[4].Value = record.description;
                        }
                    }
                }
                else
                {
                    foreach (var record in Office)
                    {
                        if (!record.device)
                        {
                            dataGridView_office.Rows.Add();
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[0].Value = record.index;
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[1].Value = record.device ? "Да" : "Нет";
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[2].Value = record.cabinet;
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[3].Value = record.department;
                            dataGridView_office.Rows[dataGridView_office.Rows.Count - 1].Cells[4].Value = record.description;
                        }
                    }
                }
            }
            
        }

        // REMOVE OFFICE
        private void button3_Click(object sender, EventArgs e)
        {
            var selection = dataGridView_office.SelectedRows;
            List<int> selected_indexes = new List<int>();

            for (int i = 0; i < selection.Count; i++)
            {
                selected_indexes.Add((int)selection[i].Cells[0].Value);
                dataGridView_office.Rows.RemoveAt(selection[i].Index);
            }

            foreach (var index in selected_indexes)
            {
                DatabaseOperations.Office.Delete_byIndex(SavedConnection, index);
            }

            GetOfficeObject();
            UpdateViewWrapper();
        }

        private void button4_Click(object sender, EventArgs e)
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

            var form = new Form_New_Room_Device(SavedConnection);
            form.ShowDialog();
            shadow.Dispose();
            shadow.Close();

            GetOfficeObject();
            UpdateViewWrapper();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateViewWrapper();
        }

        private void выйтиИзСистемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete("session.token");
            var auth = new Form_Auth();
            auth.Closed += (s, args) => this.Close();
            auth.Show();
            this.Hide();
        }
    }
}
