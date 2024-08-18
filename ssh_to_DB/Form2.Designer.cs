namespace ssh_to_DB
{
    partial class Form2
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
            this.dataGridView_list_host = new System.Windows.Forms.DataGridView();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_add = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_time_update = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_list_host)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_list_host
            // 
            this.dataGridView_list_host.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_list_host.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP,
            this.Type,
            this.User,
            this.Password,
            this.time});
            this.dataGridView_list_host.Location = new System.Drawing.Point(0, -1);
            this.dataGridView_list_host.Name = "dataGridView_list_host";
            this.dataGridView_list_host.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_list_host.Size = new System.Drawing.Size(596, 214);
            this.dataGridView_list_host.TabIndex = 0;
            // 
            // IP
            // 
            this.IP.FillWeight = 115F;
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            // 
            // Type
            // 
            this.Type.FillWeight = 115F;
            this.Type.HeaderText = "Type";
            this.Type.Items.AddRange(new object[] {
            "RPA",
            "ESX",
            "VPLEX"});
            this.Type.Name = "Type";
            this.Type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Type.Width = 115;
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.Width = 115;
            // 
            // Password
            // 
            this.Password.FillWeight = 115F;
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            this.Password.Width = 115;
            // 
            // time
            // 
            this.time.FillWeight = 115F;
            this.time.HeaderText = "Time_diff";
            this.time.Name = "time";
            this.time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.time.ToolTipText = "insert the time diffrent from the RPA time on format 00:00:00";
            this.time.Width = 115;
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(13, 220);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(89, 23);
            this.button_add.TabIndex = 1;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(109, 220);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(75, 23);
            this.button_delete.TabIndex = 2;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_time_update
            // 
            this.button_time_update.Location = new System.Drawing.Point(13, 259);
            this.button_time_update.Name = "button_time_update";
            this.button_time_update.Size = new System.Drawing.Size(171, 23);
            this.button_time_update.TabIndex = 3;
            this.button_time_update.Text = "update time Outomatic";
            this.button_time_update.UseVisualStyleBackColor = true;
            this.button_time_update.Click += new System.EventHandler(this.button_time_update_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(499, 220);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 4;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 294);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_time_update);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.dataGridView_list_host);
            this.Name = "Form2";
            this.Text = "splitter and RPA list";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_list_host)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_list_host;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_time_update;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
    }
}