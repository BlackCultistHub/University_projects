
namespace Database_prog
{
    partial class Form_New_Room_Device
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_New_Room_Device));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_isDevice = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_cabinet = new System.Windows.Forms.TextBox();
            this.textBox_department = new System.Windows.Forms.TextBox();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_description);
            this.groupBox1.Controls.Add(this.textBox_department);
            this.groupBox1.Controls.Add(this.textBox_cabinet);
            this.groupBox1.Controls.Add(this.checkBox_isDevice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 246);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Устройство или кабинет";
            // 
            // checkBox_isDevice
            // 
            this.checkBox_isDevice.AutoSize = true;
            this.checkBox_isDevice.Location = new System.Drawing.Point(10, 177);
            this.checkBox_isDevice.Name = "checkBox_isDevice";
            this.checkBox_isDevice.Size = new System.Drawing.Size(189, 21);
            this.checkBox_isDevice.TabIndex = 5;
            this.checkBox_isDevice.Text = "Устройство считывания";
            this.checkBox_isDevice.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Описание";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Отдел";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Кабинет";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_cabinet
            // 
            this.textBox_cabinet.Location = new System.Drawing.Point(77, 33);
            this.textBox_cabinet.Name = "textBox_cabinet";
            this.textBox_cabinet.Size = new System.Drawing.Size(164, 22);
            this.textBox_cabinet.TabIndex = 6;
            // 
            // textBox_department
            // 
            this.textBox_department.Location = new System.Drawing.Point(63, 71);
            this.textBox_department.Name = "textBox_department";
            this.textBox_department.Size = new System.Drawing.Size(178, 22);
            this.textBox_department.TabIndex = 7;
            // 
            // textBox_description
            // 
            this.textBox_description.Location = new System.Drawing.Point(87, 110);
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(154, 22);
            this.textBox_description.TabIndex = 8;
            // 
            // Form_New_Room_Device
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 273);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_New_Room_Device";
            this.Text = "Добавление";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox_isDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.TextBox textBox_department;
        private System.Windows.Forms.TextBox textBox_cabinet;
    }
}