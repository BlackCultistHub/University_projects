
namespace Database_prog
{
    partial class Form_Panel_user
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Panel_user));
            this.groupBox_accesses = new System.Windows.Forms.GroupBox();
            this.comboBox_access_filter = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView_accessed = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cabinet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиИзСистемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_new_request = new System.Windows.Forms.Button();
            this.groupBox_myData = new System.Windows.Forms.GroupBox();
            this.label_department = new System.Windows.Forms.Label();
            this.label_room = new System.Windows.Forms.Label();
            this.label_role = new System.Windows.Forms.Label();
            this.label_surname = new System.Windows.Forms.Label();
            this.label_user_name = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView_requests = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_accesses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_accessed)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox_myData.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_requests)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_accesses
            // 
            this.groupBox_accesses.Controls.Add(this.comboBox_access_filter);
            this.groupBox_accesses.Controls.Add(this.label11);
            this.groupBox_accesses.Controls.Add(this.dataGridView_accessed);
            this.groupBox_accesses.Location = new System.Drawing.Point(345, 31);
            this.groupBox_accesses.Name = "groupBox_accesses";
            this.groupBox_accesses.Size = new System.Drawing.Size(524, 407);
            this.groupBox_accesses.TabIndex = 0;
            this.groupBox_accesses.TabStop = false;
            this.groupBox_accesses.Text = "Мои доступы";
            // 
            // comboBox_access_filter
            // 
            this.comboBox_access_filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_access_filter.FormattingEnabled = true;
            this.comboBox_access_filter.Items.AddRange(new object[] {
            "Все",
            "Разрешённые",
            "Запрещённые"});
            this.comboBox_access_filter.Location = new System.Drawing.Point(76, 21);
            this.comboBox_access_filter.Name = "comboBox_access_filter";
            this.comboBox_access_filter.Size = new System.Drawing.Size(183, 24);
            this.comboBox_access_filter.TabIndex = 6;
            this.comboBox_access_filter.SelectedIndexChanged += new System.EventHandler(this.comboBox_access_filter_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 7;
            this.label11.Text = "Фильтр";
            // 
            // dataGridView_accessed
            // 
            this.dataGridView_accessed.AllowUserToAddRows = false;
            this.dataGridView_accessed.AllowUserToDeleteRows = false;
            this.dataGridView_accessed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_accessed.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView_accessed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_accessed.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Cabinet,
            this.Department,
            this.Description});
            this.dataGridView_accessed.Location = new System.Drawing.Point(7, 51);
            this.dataGridView_accessed.Name = "dataGridView_accessed";
            this.dataGridView_accessed.ReadOnly = true;
            this.dataGridView_accessed.RowHeadersVisible = false;
            this.dataGridView_accessed.RowHeadersWidth = 51;
            this.dataGridView_accessed.RowTemplate.Height = 24;
            this.dataGridView_accessed.Size = new System.Drawing.Size(511, 345);
            this.dataGridView_accessed.TabIndex = 0;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Разрешено";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 112;
            // 
            // Cabinet
            // 
            this.Cabinet.HeaderText = "Кабинет";
            this.Cabinet.MinimumWidth = 6;
            this.Cabinet.Name = "Cabinet";
            this.Cabinet.ReadOnly = true;
            this.Cabinet.Width = 93;
            // 
            // Department
            // 
            this.Department.HeaderText = "Отдел";
            this.Department.MinimumWidth = 6;
            this.Department.Name = "Department";
            this.Department.ReadOnly = true;
            this.Department.Width = 79;
            // 
            // Description
            // 
            this.Description.HeaderText = "Описание";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 103;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1443, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выйтиИзСистемыToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выйтиИзСистемыToolStripMenuItem
            // 
            this.выйтиИзСистемыToolStripMenuItem.Name = "выйтиИзСистемыToolStripMenuItem";
            this.выйтиИзСистемыToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.выйтиИзСистемыToolStripMenuItem.Text = "Выйти из системы";
            this.выйтиИзСистемыToolStripMenuItem.Click += new System.EventHandler(this.выйтиИзСистемыToolStripMenuItem_Click);
            // 
            // button_new_request
            // 
            this.button_new_request.Location = new System.Drawing.Point(12, 243);
            this.button_new_request.Name = "button_new_request";
            this.button_new_request.Size = new System.Drawing.Size(327, 43);
            this.button_new_request.TabIndex = 2;
            this.button_new_request.Text = "Составить заявку";
            this.button_new_request.UseVisualStyleBackColor = true;
            this.button_new_request.Click += new System.EventHandler(this.button_new_request_Click);
            // 
            // groupBox_myData
            // 
            this.groupBox_myData.Controls.Add(this.label_department);
            this.groupBox_myData.Controls.Add(this.label_room);
            this.groupBox_myData.Controls.Add(this.label_role);
            this.groupBox_myData.Controls.Add(this.label_surname);
            this.groupBox_myData.Controls.Add(this.label_user_name);
            this.groupBox_myData.Controls.Add(this.label5);
            this.groupBox_myData.Controls.Add(this.label4);
            this.groupBox_myData.Controls.Add(this.label3);
            this.groupBox_myData.Controls.Add(this.label2);
            this.groupBox_myData.Controls.Add(this.label1);
            this.groupBox_myData.Location = new System.Drawing.Point(12, 31);
            this.groupBox_myData.Name = "groupBox_myData";
            this.groupBox_myData.Size = new System.Drawing.Size(327, 206);
            this.groupBox_myData.TabIndex = 3;
            this.groupBox_myData.TabStop = false;
            this.groupBox_myData.Text = "Мои данные";
            // 
            // label_department
            // 
            this.label_department.AutoSize = true;
            this.label_department.Location = new System.Drawing.Point(94, 175);
            this.label_department.Name = "label_department";
            this.label_department.Size = new System.Drawing.Size(112, 17);
            this.label_department.TabIndex = 9;
            this.label_department.Text = "DEPARTEMENT";
            // 
            // label_room
            // 
            this.label_room.AutoSize = true;
            this.label_room.Location = new System.Drawing.Point(108, 137);
            this.label_room.Name = "label_room";
            this.label_room.Size = new System.Drawing.Size(51, 17);
            this.label_room.TabIndex = 8;
            this.label_room.Text = "ROOM";
            // 
            // label_role
            // 
            this.label_role.AutoSize = true;
            this.label_role.Location = new System.Drawing.Point(125, 102);
            this.label_role.Name = "label_role";
            this.label_role.Size = new System.Drawing.Size(46, 17);
            this.label_role.TabIndex = 7;
            this.label_role.Text = "ROLE";
            // 
            // label_surname
            // 
            this.label_surname.AutoSize = true;
            this.label_surname.Location = new System.Drawing.Point(114, 70);
            this.label_surname.Name = "label_surname";
            this.label_surname.Size = new System.Drawing.Size(76, 17);
            this.label_surname.TabIndex = 6;
            this.label_surname.Text = "SURNAME";
            // 
            // label_user_name
            // 
            this.label_user_name.AutoSize = true;
            this.label_user_name.Location = new System.Drawing.Point(79, 40);
            this.label_user_name.Name = "label_user_name";
            this.label_user_name.Size = new System.Drawing.Size(47, 17);
            this.label_user_name.TabIndex = 5;
            this.label_user_name.Text = "NAME";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Отдел ---";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Кабинет ---";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Должность ---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Фамилия ---";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя ---";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(327, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Изменить данные авторизации";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView_requests);
            this.groupBox1.Location = new System.Drawing.Point(869, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 406);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Мои заявки";
            // 
            // dataGridView_requests
            // 
            this.dataGridView_requests.AllowUserToAddRows = false;
            this.dataGridView_requests.AllowUserToDeleteRows = false;
            this.dataGridView_requests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_requests.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView_requests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_requests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView_requests.Location = new System.Drawing.Point(6, 21);
            this.dataGridView_requests.Name = "dataGridView_requests";
            this.dataGridView_requests.ReadOnly = true;
            this.dataGridView_requests.RowHeadersVisible = false;
            this.dataGridView_requests.RowHeadersWidth = 51;
            this.dataGridView_requests.RowTemplate.Height = 24;
            this.dataGridView_requests.Size = new System.Drawing.Size(562, 374);
            this.dataGridView_requests.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Дата/Время";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 117;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Статус";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 82;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Устройство - Кабинет";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 116;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Устройство - Отдел";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 116;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Устройство - Описание";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 116;
            // 
            // Form_Panel_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 446);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox_myData);
            this.Controls.Add(this.button_new_request);
            this.Controls.Add(this.groupBox_accesses);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_Panel_user";
            this.Text = "Панель сотрудника";
            this.groupBox_accesses.ResumeLayout(false);
            this.groupBox_accesses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_accessed)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox_myData.ResumeLayout(false);
            this.groupBox_myData.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_requests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_accesses;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выйтиИзСистемыToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView_accessed;
        private System.Windows.Forms.Button button_new_request;
        private System.Windows.Forms.GroupBox groupBox_myData;
        private System.Windows.Forms.Label label_user_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_surname;
        private System.Windows.Forms.Label label_department;
        private System.Windows.Forms.Label label_room;
        private System.Windows.Forms.Label label_role;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView_requests;
        private System.Windows.Forms.ComboBox comboBox_access_filter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cabinet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}