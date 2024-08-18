namespace ssh_to_DB
{
    partial class Form_filter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_filter));
            this.text_sql = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.log = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RPA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add_Or = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.button_add = new System.Windows.Forms.Button();
            this.button_Apply = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.comboBox_query_list = new System.Windows.Forms.ComboBox();
            this.button_close = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // text_sql
            // 
            this.text_sql.Location = new System.Drawing.Point(12, 33);
            this.text_sql.Name = "text_sql";
            this.text_sql.Size = new System.Drawing.Size(687, 20);
            this.text_sql.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "select sql query";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.log,
            this.Command,
            this.RPA,
            this.add_Or});
            this.dataGridView1.Location = new System.Drawing.Point(12, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(461, 170);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            // 
            // log
            // 
            this.log.HeaderText = "log";
            this.log.Items.AddRange(new object[] {
            "rreasons.log",
            "ESX",
            "storage.txt",
            "control",
            "management",
            "site_connector.txt",
            "replication",
            "Fapi",
            "mirror.txt",
            "servererror.txt"});
            this.log.Name = "log";
            // 
            // Command
            // 
            this.Command.HeaderText = "comment";
            this.Command.Name = "Command";
            // 
            // RPA
            // 
            this.RPA.HeaderText = "RPA";
            this.RPA.Name = "RPA";
            // 
            // add_Or
            // 
            this.add_Or.HeaderText = "Add_Or";
            this.add_Or.Items.AddRange(new object[] {
            "OR",
            "ADD"});
            this.add_Or.Name = "add_Or";
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(12, 233);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(75, 23);
            this.button_add.TabIndex = 3;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_Apply
            // 
            this.button_Apply.Location = new System.Drawing.Point(93, 233);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(75, 23);
            this.button_Apply.TabIndex = 4;
            this.button_Apply.Text = "Show query";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(174, 233);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // comboBox_query_list
            // 
            this.comboBox_query_list.FormattingEnabled = true;
            this.comboBox_query_list.Location = new System.Drawing.Point(255, 233);
            this.comboBox_query_list.Name = "comboBox_query_list";
            this.comboBox_query_list.Size = new System.Drawing.Size(226, 21);
            this.comboBox_query_list.TabIndex = 6;
            this.comboBox_query_list.Text = "The name for save / or choose exist query";
            this.comboBox_query_list.SelectedIndexChanged += new System.EventHandler(this.comboBox_query_list_SelectedIndexChanged);
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(621, 233);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(104, 23);
            this.button_close.TabIndex = 7;
            this.button_close.Text = "close/query apply";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(479, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(273, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "When query exist, can close the wizard, by close button ";
            // 
            // Form_filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.comboBox_query_list);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_sql);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_filter";
            this.Text = "filter :";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_sql;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.ComboBox comboBox_query_list;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.DataGridViewComboBoxColumn log;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
        private System.Windows.Forms.DataGridViewTextBoxColumn RPA;
        private System.Windows.Forms.DataGridViewComboBoxColumn add_Or;
        private System.Windows.Forms.Label label2;
    }
}