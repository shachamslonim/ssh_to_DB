namespace ssh_to_DB
{
    partial class AddGrep
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.More_info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.log = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Catgory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Log1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Catgory1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_add = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.label_filter = new System.Windows.Forms.Label();
            this.label_pool = new System.Windows.Forms.Label();
            this.label_will_run = new System.Windows.Forms.Label();
            this.button_close = new System.Windows.Forms.Button();
            this.checkListBoxFilter = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.Command,
            this.More_info,
            this.log,
            this.Catgory,
            this.id});
            this.dataGridView1.Location = new System.Drawing.Point(-4, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1105, 148);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // check
            // 
            this.check.FillWeight = 20F;
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Width = 20;
            // 
            // Command
            // 
            this.Command.FillWeight = 500F;
            this.Command.HeaderText = "Command";
            this.Command.Name = "Command";
            this.Command.Width = 500;
            // 
            // More_info
            // 
            this.More_info.FillWeight = 280F;
            this.More_info.HeaderText = "More information";
            this.More_info.Name = "More_info";
            this.More_info.Width = 280;
            // 
            // log
            // 
            this.log.HeaderText = "Log";
            this.log.Name = "log";
            // 
            // Catgory
            // 
            this.Catgory.HeaderText = "Catgory";
            this.Catgory.Name = "Catgory";
            // 
            // id
            // 
            this.id.FillWeight = 60F;
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Width = 60;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Log1,
            this.Catgory1,
            this.id_add});
            this.dataGridView2.Location = new System.Drawing.Point(-5, 268);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(943, 148);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FillWeight = 20F;
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 20;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 500F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Command";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 500;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 280F;
            this.dataGridViewTextBoxColumn2.HeaderText = "More information";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 280;
            // 
            // Log1
            // 
            this.Log1.HeaderText = "Log";
            this.Log1.Name = "Log1";
            this.Log1.Visible = false;
            // 
            // Catgory1
            // 
            this.Catgory1.HeaderText = "Catgory";
            this.Catgory1.Name = "Catgory1";
            this.Catgory1.Visible = false;
            // 
            // id_add
            // 
            this.id_add.HeaderText = "id";
            this.id_add.Name = "id_add";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 221);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(93, 221);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // label_filter
            // 
            this.label_filter.AutoSize = true;
            this.label_filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_filter.Location = new System.Drawing.Point(13, 13);
            this.label_filter.Name = "label_filter";
            this.label_filter.Size = new System.Drawing.Size(137, 16);
            this.label_filter.TabIndex = 4;
            this.label_filter.Text = "Filter  by catagory:";
            // 
            // label_pool
            // 
            this.label_pool.AutoSize = true;
            this.label_pool.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pool.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_pool.Location = new System.Drawing.Point(440, 13);
            this.label_pool.Name = "label_pool";
            this.label_pool.Size = new System.Drawing.Size(169, 24);
            this.label_pool.TabIndex = 5;
            this.label_pool.Text = "The pool of Grep";
            // 
            // label_will_run
            // 
            this.label_will_run.AutoSize = true;
            this.label_will_run.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_will_run.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_will_run.Location = new System.Drawing.Point(449, 221);
            this.label_will_run.Name = "label_will_run";
            this.label_will_run.Size = new System.Drawing.Size(160, 24);
            this.label_will_run.TabIndex = 6;
            this.label_will_run.Text = "What will be run";
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(1026, 428);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 7;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // checkListBoxFilter
            // 
            this.checkListBoxFilter.FormattingEnabled = true;
            this.checkListBoxFilter.Items.AddRange(new object[] {
            "control",
            "Pipe",
            "Regulated",
            "High-load",
            "Volume",
            "memory",
            "CPU",
            "journal ",
            "Ganaral",
            "replication",
            "network"});
            this.checkListBoxFilter.Location = new System.Drawing.Point(156, 13);
            this.checkListBoxFilter.Name = "checkListBoxFilter";
            this.checkListBoxFilter.ScrollAlwaysVisible = true;
            this.checkListBoxFilter.Size = new System.Drawing.Size(120, 19);
            this.checkListBoxFilter.TabIndex = 8;
            this.checkListBoxFilter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkListBoxFilterItemcheck);
            this.checkListBoxFilter.MouseEnter += new System.EventHandler(this.filter_open_option);
            this.checkListBoxFilter.MouseLeave += new System.EventHandler(this.checkListBoxFilterLeave);
            this.checkListBoxFilter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.filter_open_option);
            // 
            // AddGrep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 463);
            this.Controls.Add(this.checkListBoxFilter);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.label_will_run);
            this.Controls.Add(this.label_pool);
            this.Controls.Add(this.label_filter);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AddGrep";
            this.Text = "AddGrep";
            this.Load += new System.EventHandler(this.AddGrep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
        private System.Windows.Forms.DataGridViewTextBoxColumn More_info;
        private System.Windows.Forms.DataGridViewTextBoxColumn log;
        private System.Windows.Forms.DataGridViewTextBoxColumn Catgory;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Log1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Catgory1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_add;
        private System.Windows.Forms.Label label_filter;
        private System.Windows.Forms.Label label_pool;
        private System.Windows.Forms.Label label_will_run;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.CheckedListBox checkListBoxFilter;
    }
}