using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;


namespace ssh_to_DB
{
    public partial class Form1 : Form
    {
        public Label label2;
        private Button button2;
        private TextBox tBoxRpaList;
        private Button button3;
        private Button butt_RPA_LIST;
        private DataGridView dataGridView1;
        private BindingSource connectDataSetBindingSource;
        private IContainer components;
        private connectDataSet connectDataSet;
        private Button button4;
        private CheckBox checkBox1_off_line;
        private ComboBox comboBox1_Min;
        private ComboBox comboBox_max;
        private Label label3_MIN;
        private Label label6_max;
        private Label label3;
        private Button button_kate;
        private TextBox textBox_kate;
        private ComboBox comboBoxKATE;
        private Label label_kate;
        private ListBox ListSplitter;
        private Label label_splitter;
        private ListBox listBox_message;
        private Label label_type;
        private ComboBox comboBoxType;
        private Button button_filter_text;
        private CheckBox checkBox_current_Activety;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        BackgroundWorker worker;
        private CheckBox checkBoxAdd_log;
        private Button button_copy_select;
        private DataGridViewCheckBoxColumn select;
        private DataGridViewTextBoxColumn time;
        private DataGridViewTextBoxColumn comand;
        private DataGridViewTextBoxColumn Log;
        private DataGridViewTextBoxColumn RPA;
        bool abortLoop = false;

        private delegate void UpdatelistBox_messageHandler(String addMessage, Color color_message);
        private delegate void UpdateGridThreadHandler(DataTable all_row);
        delegate void UpdateGridHandler(DataTable all_row);


        public Form1()
        {
            InitializeComponent();
            host_list("rpa_list2.txt");
      
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tBoxRpaList = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.butt_RPA_LIST = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Log = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RPA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.connectDataSet = new ssh_to_DB.connectDataSet();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox1_off_line = new System.Windows.Forms.CheckBox();
            this.comboBox1_Min = new System.Windows.Forms.ComboBox();
            this.comboBox_max = new System.Windows.Forms.ComboBox();
            this.label3_MIN = new System.Windows.Forms.Label();
            this.label6_max = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_kate = new System.Windows.Forms.Button();
            this.textBox_kate = new System.Windows.Forms.TextBox();
            this.comboBoxKATE = new System.Windows.Forms.ComboBox();
            this.label_kate = new System.Windows.Forms.Label();
            this.ListSplitter = new System.Windows.Forms.ListBox();
            this.label_splitter = new System.Windows.Forms.Label();
            this.listBox_message = new System.Windows.Forms.ListBox();
            this.label_type = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.button_filter_text = new System.Windows.Forms.Button();
            this.checkBox_current_Activety = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxAdd_log = new System.Windows.Forms.CheckBox();
            this.button_copy_select = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP RPA";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(48, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "Collect Log";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tBoxRpaList
            // 
            this.tBoxRpaList.Location = new System.Drawing.Point(48, 32);
            this.tBoxRpaList.Multiline = true;
            this.tBoxRpaList.Name = "tBoxRpaList";
            this.tBoxRpaList.Size = new System.Drawing.Size(188, 75);
            this.tBoxRpaList.TabIndex = 5;
            this.tBoxRpaList.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(48, 164);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 36);
            this.button3.TabIndex = 11;
            this.button3.Text = "show table";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // butt_RPA_LIST
            // 
            this.butt_RPA_LIST.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.butt_RPA_LIST.Location = new System.Drawing.Point(48, 4);
            this.butt_RPA_LIST.Name = "butt_RPA_LIST";
            this.butt_RPA_LIST.Size = new System.Drawing.Size(119, 23);
            this.butt_RPA_LIST.TabIndex = 12;
            this.butt_RPA_LIST.Text = "RPA list";
            this.butt_RPA_LIST.UseVisualStyleBackColor = true;
            this.butt_RPA_LIST.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.time,
            this.comand,
            this.Log,
            this.RPA});
            this.dataGridView1.DataSource = this.connectDataSetBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 207);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 22;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1057, 398);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // select
            // 
            this.select.HeaderText = "select";
            this.select.Name = "select";
            this.select.Width = 20;
            // 
            // time
            // 
            this.time.HeaderText = "date";
            this.time.MaxInputLength = 12767;
            this.time.Name = "time";
            this.time.Width = 125;
            // 
            // comand
            // 
            this.comand.HeaderText = "command";
            this.comand.MaxInputLength = 102767;
            this.comand.Name = "comand";
            this.comand.Width = 695;
            // 
            // Log
            // 
            this.Log.FillWeight = 80F;
            this.Log.HeaderText = "LOG";
            this.Log.Name = "Log";
            this.Log.Width = 105;
            // 
            // RPA
            // 
            this.RPA.HeaderText = "RPA";
            this.RPA.Name = "RPA";
            this.RPA.Width = 90;
            // 
            // connectDataSetBindingSource
            // 
            this.connectDataSetBindingSource.DataSource = this.connectDataSet;
            this.connectDataSetBindingSource.Position = 0;
            // 
            // connectDataSet
            // 
            this.connectDataSet.DataSetName = "connectDataSet";
            this.connectDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(508, 175);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 22);
            this.button4.TabIndex = 14;
            this.button4.Text = "fileter date";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // checkBox1_off_line
            // 
            this.checkBox1_off_line.AutoSize = true;
            this.checkBox1_off_line.Location = new System.Drawing.Point(173, 170);
            this.checkBox1_off_line.Name = "checkBox1_off_line";
            this.checkBox1_off_line.Size = new System.Drawing.Size(89, 17);
            this.checkBox1_off_line.TabIndex = 15;
            this.checkBox1_off_line.Text = "work_off_line";
            this.checkBox1_off_line.UseVisualStyleBackColor = true;
            this.checkBox1_off_line.CheckedChanged += new System.EventHandler(this.checkBox1_off_line_CheckedChanged);
            // 
            // comboBox1_Min
            // 
            this.comboBox1_Min.FormattingEnabled = true;
            this.comboBox1_Min.Location = new System.Drawing.Point(661, 180);
            this.comboBox1_Min.Name = "comboBox1_Min";
            this.comboBox1_Min.Size = new System.Drawing.Size(181, 21);
            this.comboBox1_Min.TabIndex = 16;
            // 
            // comboBox_max
            // 
            this.comboBox_max.FormattingEnabled = true;
            this.comboBox_max.Location = new System.Drawing.Point(874, 180);
            this.comboBox_max.Name = "comboBox_max";
            this.comboBox_max.Size = new System.Drawing.Size(195, 21);
            this.comboBox_max.TabIndex = 17;
            // 
            // label3_MIN
            // 
            this.label3_MIN.AutoSize = true;
            this.label3_MIN.Location = new System.Drawing.Point(628, 183);
            this.label3_MIN.Name = "label3_MIN";
            this.label3_MIN.Size = new System.Drawing.Size(27, 13);
            this.label3_MIN.TabIndex = 18;
            this.label3_MIN.Text = "Min:";
            // 
            // label6_max
            // 
            this.label6_max.AutoSize = true;
            this.label6_max.Location = new System.Drawing.Point(848, 183);
            this.label6_max.Name = "label6_max";
            this.label6_max.Size = new System.Drawing.Size(30, 13);
            this.label6_max.TabIndex = 21;
            this.label6_max.Text = "Max:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(514, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "message";
            // 
            // button_kate
            // 
            this.button_kate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_kate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_kate.Location = new System.Drawing.Point(3, 110);
            this.button_kate.Name = "button_kate";
            this.button_kate.Size = new System.Drawing.Size(45, 23);
            this.button_kate.TabIndex = 23;
            this.button_kate.Text = "Kate file";
            this.button_kate.UseVisualStyleBackColor = true;
            this.button_kate.Click += new System.EventHandler(this.button_kate_Click);
            // 
            // textBox_kate
            // 
            this.textBox_kate.Location = new System.Drawing.Point(48, 110);
            this.textBox_kate.Name = "textBox_kate";
            this.textBox_kate.Size = new System.Drawing.Size(188, 20);
            this.textBox_kate.TabIndex = 24;
            // 
            // comboBoxKATE
            // 
            this.comboBoxKATE.FormattingEnabled = true;
            this.comboBoxKATE.Location = new System.Drawing.Point(661, 153);
            this.comboBoxKATE.Name = "comboBoxKATE";
            this.comboBoxKATE.Size = new System.Drawing.Size(408, 21);
            this.comboBoxKATE.TabIndex = 25;
            this.comboBoxKATE.Text = "Please select test";
            this.comboBoxKATE.SelectedIndexChanged += new System.EventHandler(this.comboBoxKATE_SelectedIndexChanged);
            // 
            // label_kate
            // 
            this.label_kate.AutoSize = true;
            this.label_kate.Location = new System.Drawing.Point(609, 156);
            this.label_kate.Name = "label_kate";
            this.label_kate.Size = new System.Drawing.Size(55, 13);
            this.label_kate.TabIndex = 26;
            this.label_kate.Text = " Kate test:";
            this.label_kate.Click += new System.EventHandler(this.label_kate_Click);
            // 
            // ListSplitter
            // 
            this.ListSplitter.FormattingEnabled = true;
            this.ListSplitter.Location = new System.Drawing.Point(288, 33);
            this.ListSplitter.Name = "ListSplitter";
            this.ListSplitter.Size = new System.Drawing.Size(122, 69);
            this.ListSplitter.TabIndex = 27;
            // 
            // label_splitter
            // 
            this.label_splitter.AutoSize = true;
            this.label_splitter.Location = new System.Drawing.Point(242, 32);
            this.label_splitter.Name = "label_splitter";
            this.label_splitter.Size = new System.Drawing.Size(42, 13);
            this.label_splitter.TabIndex = 28;
            this.label_splitter.Text = "Splitter:";
            // 
            // listBox_message
            // 
            this.listBox_message.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox_message.FormattingEnabled = true;
            this.listBox_message.Location = new System.Drawing.Point(508, 20);
            this.listBox_message.Name = "listBox_message";
            this.listBox_message.Size = new System.Drawing.Size(561, 121);
            this.listBox_message.TabIndex = 29;
            this.listBox_message.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_message_DrawItem);
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(237, 110);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(55, 13);
            this.label_type.TabIndex = 30;
            this.label_type.Text = "Log Type:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Short_Time",
            "Long_Time",
            "collect_1_0",
            "User_Log2",
            "User_Log3"});
            this.comboBoxType.Location = new System.Drawing.Point(288, 109);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxType.TabIndex = 31;
            this.comboBoxType.Text = "Short_Time";
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // button_filter_text
            // 
            this.button_filter_text.Location = new System.Drawing.Point(508, 153);
            this.button_filter_text.Name = "button_filter_text";
            this.button_filter_text.Size = new System.Drawing.Size(103, 21);
            this.button_filter_text.TabIndex = 32;
            this.button_filter_text.Text = "filter text";
            this.button_filter_text.UseVisualStyleBackColor = true;
            this.button_filter_text.Visible = false;
            this.button_filter_text.Click += new System.EventHandler(this.button_filter_text_Click);
            // 
            // checkBox_current_Activety
            // 
            this.checkBox_current_Activety.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_current_Activety.AutoSize = true;
            this.checkBox_current_Activety.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox_current_Activety.Image = ((System.Drawing.Image)(resources.GetObject("checkBox_current_Activety.Image")));
            this.checkBox_current_Activety.Location = new System.Drawing.Point(206, 134);
            this.checkBox_current_Activety.Name = "checkBox_current_Activety";
            this.checkBox_current_Activety.Size = new System.Drawing.Size(30, 30);
            this.checkBox_current_Activety.TabIndex = 33;
            this.toolTip2.SetToolTip(this.checkBox_current_Activety, ":-)");
            this.checkBox_current_Activety.UseVisualStyleBackColor = false;
            this.checkBox_current_Activety.CheckedChanged += new System.EventHandler(this.checkBox_current_Activety_CheckedChanged_1);
            this.checkBox_current_Activety.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkBox_current_Activety_MouseUp);
            // 
            // toolTip2
            // 
            this.toolTip2.ToolTipTitle = "add more log to the current log ";
            // 
            // checkBoxAdd_log
            // 
            this.checkBoxAdd_log.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAdd_log.AutoSize = true;
            this.checkBoxAdd_log.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxAdd_log.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxAdd_log.Image")));
            this.checkBoxAdd_log.Location = new System.Drawing.Point(173, 134);
            this.checkBoxAdd_log.Name = "checkBoxAdd_log";
            this.checkBoxAdd_log.Size = new System.Drawing.Size(30, 30);
            this.checkBoxAdd_log.TabIndex = 34;
            this.toolTip2.SetToolTip(this.checkBoxAdd_log, ":-)");
            this.checkBoxAdd_log.UseVisualStyleBackColor = false;
            this.checkBoxAdd_log.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button_copy_select
            // 
            this.button_copy_select.Location = new System.Drawing.Point(967, 611);
            this.button_copy_select.Name = "button_copy_select";
            this.button_copy_select.Size = new System.Drawing.Size(102, 23);
            this.button_copy_select.TabIndex = 35;
            this.button_copy_select.Text = "Copy to  Clipboard";
            this.button_copy_select.UseVisualStyleBackColor = true;
            this.button_copy_select.Click += new System.EventHandler(this.button_copy_select_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1084, 742);
            this.Controls.Add(this.button_copy_select);
            this.Controls.Add(this.checkBoxAdd_log);
            this.Controls.Add(this.checkBox_current_Activety);
            this.Controls.Add(this.button_filter_text);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.listBox_message);
            this.Controls.Add(this.label_splitter);
            this.Controls.Add(this.ListSplitter);
            this.Controls.Add(this.label_kate);
            this.Controls.Add(this.comboBoxKATE);
            this.Controls.Add(this.textBox_kate);
            this.Controls.Add(this.button_kate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6_max);
            this.Controls.Add(this.label3_MIN);
            this.Controls.Add(this.comboBox_max);
            this.Controls.Add(this.comboBox1_Min);
            this.Controls.Add(this.checkBox1_off_line);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.butt_RPA_LIST);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tBoxRpaList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Analysis log To DB";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private delegate void UpdateTextBoxHandler(String addMessage);
        private delegate void UpDateDataGridViewHandler(DataTable all_row);
       


        private void label2_Click(object sender, EventArgs e)
        {
            string a;
            a = "ddddd";
            change_lib(a);
        }
        public void change_lib(string a)
        {

            label2.Text = a;


        }


        public void run_InitializeComponent()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (this.button2.Text == "Collect Log")
            {
                listBox_message.Items.Add(new MyListBoxItem(Color.Black, "Step 2 the collect log will take 5 minute "));

                //     FapiClient.runMe();
                DataBaseFun.drop_table("Activity");
                DataBaseFun.drop_table("Kate");
                DataBaseFun.create_table("Activity");
                DataBaseFun.create_table("Kate");
                //   bool WorkOffLine = false;
                if (File.Exists(Ofd.FileName))
                {

                    SshExeTest.filter_file(Ofd.FileName, "Kate", "kate_tasks.log");

                }
                if (checkBox1_off_line.Checked == false)
                {
                    //     this is the difoult 

                    host_list_for_execute(false);

                    // for test with text file  SshExeTest.executeBatchFile("log_fapi_RPVE_for_program.bat");



                }

                /* The function that work with scp of Fapi log 
          
                  SshExeTest.createUserConnect("RPA",WorkOffLine);*/
                listBox_message.Items.Clear();
                listBox_message.Items.Add(new MyListBoxItem(Color.Black, "Step 3 the collect log finish "));

                //    
            }
            else
            {
                listBox_message.Items.Add(new MyListBoxItem(Color.Black, "Step 2 the collect log will take 5 minute "));
                if (File.Exists(Ofd.FileName))
                {
                    listBox_message.Items.Add(new MyListBoxItem(Color.Gold, "collect kate log "));
                    SshExeTest.filter_file(Ofd.FileName, "Kate", "kate_tasks.log");

                }

                add_to_to_exist();
                listBox_message.Items.Clear();
                listBox_message.Items.Add(new MyListBoxItem(Color.Black, "Step 3 the collect log finish "));

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                show_table();
            }
            catch (Exception ex)
            {


                SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";

            }


        }

        private void button4_Click(object sender, EventArgs e)
        {

            //  SshExeTest.drop_table("Start");
            if (butt_RPA_LIST.Text == "RPA list")
            {
                Form2 secondForm;
                secondForm = new Form2();
                secondForm.Show();
                // System.Diagnostics.Process.Start("notepad.exe", "rpa_list2.txt");
                butt_RPA_LIST.Text = "REFRESH LIST";
                butt_RPA_LIST.BackColor = SystemColors.HotTrack;
            }
            else
            {
                butt_RPA_LIST.BackColor = SystemColors.Info;
                host_list("rpa_list2.txt");
                butt_RPA_LIST.Text = "RPA list";
            }

        }

        //        public void All_show_host(string NameLogFile)
        //       {
        //          host_list("rpa_list2.txt");
        //  }



        public void host_list(string NameLogFile)
        {
            ListSplitter.Items.Clear();
            const string listhost = "rpa_list2.txt";
            String TextLine = " ";
            // 1
            // Declare new List.
            List<string> HostInformation = new List<string>();


            // 2
            // Use using StreamReader for disposing.
            using (StreamReader r = new StreamReader(listhost))
            {
                // 3
                // Use while != null pattern for loop
                string line = " ";
                long Countline = 0;
                while ((line = r.ReadLine()) != null)
                {

                    //count the farst word for not count 
                    //
                    // Split string on spaces.
                    // ... This will separate all the words.
                    //

                    int CountWord = 0;
                    string[] words = line.Split(';');
                    String IP = "";
                    String type = "";
                    String userName = "";
                    String password = "";
                    string plus_minus = "-";
                    string timeDiff = " ";
                    foreach (string word in words)
                    {

                        if (CountWord == 0)
                        {
                            //not show this line 
                            if (word.IndexOf("#") != -1)
                            {
                                Countline = Countline - 1;
                                break;
                                //Console.WriteLine(word);
                            }
                        }


                        switch (CountWord)
                        {
                            case 0:
                                IP = word;

                                break;
                            case 1:
                                type = word;
                                if (type == "RPA")
                                {
                                    TextLine = TextLine + IP + Environment.NewLine;
                                    tBoxRpaList.Text = TextLine;
                                }
                                if (type == "ESX")
                                {
                                    ListSplitter.Items.Add(IP + " " + type);
                                }
                                if (type == "vplex")
                                {
                                    ListSplitter.Items.Add(IP + " " + type);
                                }
                                break;
                            case 2:
                                userName = word;
                                break;
                            case 3:
                                password = word;
                                break;
                            case 4:
                                plus_minus = word;
                                break;
                            case 5:
                                timeDiff = word;
                                break;
                            case 6:
                                //not
                                break;

                            default:
                                MessageBox.Show("Invalid selection. Please select 1, 2, or 3.");

                                break;
                        }




                        //     Lines[Countline] = IP;


                        CountWord = CountWord + 1;
                    }

                    // 4
                    // Insert logic here.
                    // ...
                    // "line" is a line in the file. Add it to our List.
                    //HostInformation.Add(line);

                    Countline = Countline + 1;
                }

            }

            //    return HostInformation;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (comboBox1_Min.SelectedItem == null)
                comboBox1_Min.SelectedItem = comboBox1_Min.Items[0];
            if (comboBox_max.SelectedItem == null)
                comboBox_max.SelectedItem = comboBox_max.Items[0];

            CultureInfo culture = new CultureInfo("en-US");
            //   DataTable all_row = DataBaseFun.select_DB(Convert.ToDateTime(comboBox1_Min.SelectedItem, culture), Convert.ToDateTime(comboBox_max.Text));
            DataTable all_row = DataBaseFun.select_DB(Convert.ToDateTime(comboBox1_Min.SelectedItem, culture), Convert.ToDateTime(comboBox_max.SelectedItem, culture));
            for (int i = 0; i < all_row.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(false ,all_row.Rows[i].ItemArray[0], all_row.Rows[i].ItemArray[1], all_row.Rows[i].ItemArray[2], all_row.Rows[i].ItemArray[3]);
            }

        }
        private void show_table(string query = "SELECT * FROM Activity order by date")
        {
            try
            {

                button4.Visible = true;
                button_filter_text.Visible = true;

                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb;Persist Security Info=False";
                //           connect.Open();
                //        OleDbCommand command = new OleDbCommand();
                //       command.Connection = connect;
                /////////   string query = "SELECT * FROM Activity order by date";
                //    command.CommandText = sql;
                //   OleDbDataAdapter da = new OleDbDataAdapter(command);
                //   DataTable table = new DataTable();
                //   da.Fill(table);
                //   dataGridView1.DataSource = table;
                //     dataGridView1.DataBind();
                //  OleDbDataReader reader = command.ExecuteReader();


                //create an OleDbDataAdapter to execute the query
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);

                //create a command builder
                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

                //create a DataTable to hold the query results
                DataTable dTable = new DataTable();


                DataSet ds = new DataSet();

                //fill the DataTable
                dAdapter.Fill(dTable);

                ///    dAdapter.Fill(dataTable);
                DateTime minnn = DataBaseFun.selectGroupFun("MIN");
                DateTime maxx = DataBaseFun.selectGroupFun("MAX");
                string minnnn = minnn.ToString("MM/dd/yyyy HH:mm:tt");
                string maxxx = maxx.ToString("MM/dd/yyyy HH:mm:tt");
                int counterReverse = 0;
                comboBox1_Min.Items.Add(minnn);
                comboBox_max.Items.Add(maxx);
                maxx.AddMinutes(60);

                comboBox1_Min.Text = minnnn;
                comboBox_max.Text = maxxx;
                // for show the Tables
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(false ,dTable.Rows[i].ItemArray[0], dTable.Rows[i].ItemArray[1], dTable.Rows[i].ItemArray[2], dTable.Rows[i].ItemArray[3]);

                }

                string queryKate = "SELECT * FROM Kate order by date";
                //    command.CommandText = sql;
                //   OleDbDataAdapter da = new OleDbDataAdapter(command);
                //   DataTable table = new DataTable();
                //   da.Fill(table);
                //   dataGridView1.DataSource = table;
                //     dataGridView1.DataBind();
                //  OleDbDataReader reader = command.ExecuteReader();


                //create an OleDbDataAdapter to execute the query
                OleDbDataAdapter dAdapterKate = new OleDbDataAdapter(queryKate, connString);

                //create a command builder
                OleDbCommandBuilder cBuilderKate = new OleDbCommandBuilder(dAdapterKate);

                //create a DataTable to hold the query results
                DataTable dTableKate = new DataTable();


                DataSet dsKate = new DataSet();

                //fill the DataTable
                dAdapterKate.Fill(dTableKate);
                comboBoxKATE.Items.Clear();
                for (int i = 0; i < dTableKate.Rows.Count; i++)
                {
                    comboBoxKATE.Items.Add(Convert.ToString(dTableKate.Rows[i].ItemArray[0]) + " " + Convert.ToString(dTableKate.Rows[i].ItemArray[1]));
                    comboBox1_Min.Items.Add(dTableKate.Rows[i].ItemArray[0]);
                    comboBox_max.Items.Add(dTableKate.Rows[i].ItemArray[0]);

                }





                // if kate exist the order will be deffrent
                if (!File.Exists(Ofd.FileName))
                {




                    //add items for combolist all 60 minutes
                    for (DateTime date = minnn; date < maxx; date = date.AddMinutes(60))
                    {
                        comboBox1_Min.Items.Add(date);
                        counterReverse++;
                    }
                    for (int i = counterReverse; i >= 0; i--)
                    {
                        comboBox_max.Items.Add(comboBox1_Min.Items[i]);
                    }
                }




            }
            catch (Exception ex)
            {


                SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                //  return "Failed_create_DB_Connection";

            }

        }

        OpenFileDialog Ofd = new OpenFileDialog();
        private void button_kate_Click(object sender, EventArgs e)
        {
            Ofd.Filter = "KATE|*kate_tasks.log*";
            if (Ofd.ShowDialog() == DialogResult.OK)
            {
                textBox_kate.Text = Ofd.FileName;
            }

        }

        private void tBox_result_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxKATE_SelectedIndexChanged(object sender, EventArgs e)
        {

            // string[] selectTest = comboBoxKATE.Text.Split('#');
            foreach (var DateCompare in comboBox1_Min.Items)
            {
                //Convert.ToString(DateCompare).IndexOf(selectTest[0])
                //  if (Convert.ToString(DateCompare) == selectTest[0])

                if (comboBoxKATE.Text.IndexOf(Convert.ToString(DateCompare)) != -1)
                {
                    comboBox1_Min.Text = Convert.ToString(DateCompare);
                    comboBox1_Min.SelectedItem = DateCompare;

                }

            }
            dataGridView1.Rows.Clear();
            if (comboBox_max.SelectedItem == null)
                comboBox_max.SelectedItem = comboBox_max.Items[0];

            CultureInfo culture = new CultureInfo("en-US");
            //   DataTable all_row = DataBaseFun.select_DB(Convert.ToDateTime(comboBox1_Min.SelectedItem, culture), Convert.ToDateTime(comboBox_max.Text));
            DataTable all_row = DataBaseFun.select_DB(Convert.ToDateTime(comboBox1_Min.SelectedItem, culture), Convert.ToDateTime(comboBox_max.SelectedItem, culture));
            for (int i = 0; i < all_row.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(false ,all_row.Rows[i].ItemArray[0], all_row.Rows[i].ItemArray[1], all_row.Rows[i].ItemArray[2], all_row.Rows[i].ItemArray[3]);
            }
            dataGridView1.Rows[0].Cells[0].Selected  = true;
            
        }

        private void label_kate_Click(object sender, EventArgs e)
        {

        }




        private void host_list_for_execute(bool current)
        {
            //  xzv
            bool FapiLogRun = false;
            const string listhost = "rpa_list2.txt";
            // String TextLine = " ";
            // 1
            // Declare new List.
            List<string> HostInformation = new List<string>();


            // 2
            // Use using StreamReader for disposing.
            using (StreamReader r = new StreamReader(listhost))
            {
                // 3
                // Use while != null pattern for loop
                string line = " ";
                long Countline = 0;
                int less_time = -45;
                while ((line = r.ReadLine()) != null)
                {

                    //count the farst word for not count 
                    //
                    // Split string on spaces.
                    // ... This will separate all the words.
                    //

                    int CountWord = 0;
                    string[] words = line.Split(';');
                    String IP = "";
                    String type = "";
                    String userName = "";
                    String password = "";
                    string Plus_min = "-";
                    string time_string = "00:00:00";

                    foreach (string word in words)
                    {

                        if (CountWord == 0)
                        {
                            //not show this line 
                            if (word.IndexOf("#") != -1)
                            {
                                Countline = Countline - 1;
                                break;
                                //Console.WriteLine(word);
                            }
                        }


                        switch (CountWord)
                        {
                            case 0:
                                IP = word;

                                break;
                            case 1:
                                type = word;

                                break;
                            case 2:
                                userName = word;
                                break;
                            case 3:
                                password = word;
                                break;
                            case 4:
                                Plus_min = word;
                                break;
                            case 5:
                                time_string = word;
                                break;
                            case 6:
                                //not need
                                break;
                            default:
                                SshExeTest.WriteLogFile("1", "Read _log _ssh", "you have more when 5 attrebute on host_list function");

                                break;
                        }




                        //     Lines[Countline] = IP;


                        CountWord = CountWord + 1;
                    }


                    if (type == "RPA")
                    {
                      //  listBox_message.Items.Add(new MyListBoxItem(Color.Purple, "now collect log from RPA IP:  " + IP));
                        message_color("now collect log from RPA IP:  " + IP, Color.Purple);
                        // THE CURREMT IS THE TABLE
                        //Thread demoThread = new Thread(() => ThreadProcUnsafe(IP, userName, password, "current", current));

                        //demoThread.Start();
                        //demoThread.Join();
                        string filter_log = "";
                        if (current == true)
                            filter_log = "current";
                        else
                            filter_log = comboBoxType.Text;

                        Read_log_ssh.read_RPA_logs(IP, userName, password, 22, filter_log , current);
                        if (FapiLogRun == false)
                        {
                            message_color("Collect EVENT log  " , Color.Purple);
                            // collect the logs 
                            FapiClient.Collect_logs(IP);
                            FapiLogRun = true;


                        }
                /*        DateTime max = DataBaseFun.selectGroupFun("MAX");
                        if (max != DateTime.MinValue)
                        {


                            DateTime minimun = max.AddMinutes(-45);

                            DataTable all_row = new DataTable();
                            all_row =  DataBaseFun.select_DB(minimun, max);
                           /// update_dataGridView1_current(all_row);
                            UpdateGridHandler ug = update_dataGridView1_current;
                            ug.BeginInvoke(all_row, cb, null);
                        }*/


                    }
                    if (type == "ESX")
                    {
                        //execute the grep for ESx splitter 
                     
                        message_color("now collect spliter type ESX IP:" + IP ,Color.Purple);
                        //listBox_message.Items.Add(new MyListBoxItem(Color.Purple, "now collect spliter type ESX IP:" + IP));

                        Read_log_ssh.read_RPA_SPLITER(type, IP, userName, password, 22);
                        
                        if (time_string != "00:00:00")
                        {
 //check if have diffrent time between RPA to ESX
                            List<int> ESX_list =DataBaseFun.select_list_int_GroupFun("SELECT TOP 1 Hours, Minutes, Seconds FROM Time_diff WHERE Type='ESX'; ", 3);
                            List<int> RPA_list = DataBaseFun.select_list_int_GroupFun("SELECT TOP 1 Hours, Minutes, Seconds FROM Time_diff WHERE Type='RPA'; ",3);
                            for (int i = 0; i < 3; i++)
                            {
                                ESX_list[i] = ESX_list[i] - RPA_list[i];
                            }
                            if (ESX_list[0] == 0 && ESX_list[1] == 0 && ESX_list[2] <= 3) 
                            {
                            DataBaseFun.updateTime(ESX_list[0], ESX_list[1], ESX_list[2], "Activity", "ESX");
                            }



                        }
                    }
                    


                    if (current == true)
                    {
                    DateTime max = DataBaseFun.selectGroupFun("MAX");
                    if (max != DateTime.MinValue)
                    {


                        DateTime minimun = max.AddMinutes(less_time);

                        DataTable all_row = new DataTable();
                        all_row = DataBaseFun.select_DB(minimun, max);
                        /// update_dataGridView1_current(all_row);
                        UpdateGridHandler ug = update_dataGridView1_current;
                        ug.BeginInvoke(all_row, cb, null);
                    }
                }

                    Countline = Countline + 1;


                }
                if (current == false)
                {
                    less_time = -300;
                    button_filter_text.Visible = true;
                    button4.Visible = true;
                                        DateTime max = DataBaseFun.selectGroupFun("MAX");
                    if (max != DateTime.MinValue)
                    {


                        DateTime minimun = max.AddMinutes(less_time);

                        DataTable all_row = new DataTable();
                        all_row = DataBaseFun.select_DB(minimun, max);
                        /// update_dataGridView1_current(all_row);
                        UpdateGridHandler ug = update_dataGridView1_current;
                        ug.BeginInvoke(all_row, cb, null);
                    }

                }

                
               
            }

            //    return HostInformation;
        }
        private void ThreadProcUnsafe(string IP, string userName, string password, string comboBoxType, bool current)
        {
           Read_log_ssh.read_RPA_logs(IP, userName, password, 22, comboBoxType, current);
       }


        void message_color(string message, Color color_message)
        {
            if (listBox_message.InvokeRequired)
                listBox_message.Invoke(new UpdatelistBox_messageHandler(UpdateTextBox), message, color_message);
            else
                UpdateTextBox(message, color_message);
        }
        private void UpdateTextBox(String message ,Color color_message)
        {
            listBox_message.Items.Add(new MyListBoxItem(Color.Purple, message));
        }

     
        
        
        
        void update_dataGridView1_current(DataTable all_row)
        {
            ////////DateTime max = DateTime.Now;

            if (dataGridView1.InvokeRequired)
            {
                //////     dataGridView1.Invoke(new UpdateGridThreadHandler(UpDateDataGridView), all_row);
              //  dataGridView1.Rows.Clear();
                UpdateGridThreadHandler handler = UpdateGrid11;
               // dataGridView1.Rows.Clear();
            //    System.Threading.Thread.Sleep(1000);



                dataGridView1.BeginInvoke(handler, all_row);
                
            }
            else
            {

                //////  UpDateDataGridView(all_row);
           //     dataGridView1.Rows.Clear();
                dataGridView1.DataSource = all_row;
            }
                      
              

        }
        private void cb(IAsyncResult res)
        {

        }
        private void UpdateGrid11(DataTable all_row)
        {
           


            BindingSource source = new BindingSource();

            // I add it :

            DataColumn dcolColumn = new DataColumn("select", typeof(Boolean));
            dcolColumn.DefaultValue = false;
            all_row.Columns.Add(dcolColumn);

            source.DataSource = all_row;
            dataGridView1.DataSource = source;
            // DataPropertyName need to be like the table on the database
            
            
            dataGridView1.Columns[1].DataPropertyName = "date";
            dataGridView1.Columns[2].DataPropertyName = "command";
            dataGridView1.Columns[3].DataPropertyName = "LOG";
            dataGridView1.Columns[4].DataPropertyName = "RPA";
            dataGridView1.Columns[0].DataPropertyName = "select";

         ///////////////////   dataGridView1.ReadOnly = false;
            dataGridView1.Columns[0].ReadOnly = false;
            

            dataGridView1.Refresh();
            
        }

        private void UpDateDataGridView(DataTable all_row)
        {

           dataGridView1.Rows.Clear();
          // DateTime max = new DateTime(1970, 1, 1, 0, 0, 0, 0);
          ///////////////////////  DateTime max = DateTime.Now;
          //  DateTime max = DataBaseFun.selectGroupFun("MAX");
           // if (max==DateTime.MinValue)
           // {
          //////////////////////////      DateTime minimun = max.AddMinutes(MinutestoAdd);

            //    CultureInfo culture = new CultureInfo("en-US");
                //   DataTable all_row = DataBaseFun.select_DB(Convert.ToDateTime(comboBox1_Min.SelectedItem, culture), Convert.ToDateTime(comboBox_max.Text));
            //string ggg = all_row.Rows[0].ItemArray[3].ToString;
           

          //  dataGridView1.Rows.Add(all_row.Rows[0].ItemArray[0], all_row.Rows[0].ItemArray[1], all_row.Rows[0].ItemArray[2], all_row.Rows[0].ItemArray[3]);

          //  }

            for (int i = 0; i < all_row.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(false ,all_row.Rows[i].ItemArray[0], all_row.Rows[i].ItemArray[1], all_row.Rows[i].ItemArray[2], all_row.Rows[i].ItemArray[3]);
            }


            
        }


        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button_filter_text_Click(object sender, EventArgs e)
        {

            //  SshExeTest.drop_table("Start");
            if (button_filter_text.Text == "filter text")
            {
                Form_filter secondForm;
                secondForm = new Form_filter();
                secondForm.Show();
                // System.Diagnostics.Process.Start("notepad.exe", "rpa_list2.txt");
                button_filter_text.Text = "run Query";
                button_filter_text.BackColor = SystemColors.HotTrack;
            }
            else
            {
                button_filter_text.BackColor = SystemColors.Info;
                // host_list("rpa_list2.txt");
                button_filter_text.Text = "filter text";

                try
                {

                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    foreach (string goInto in DataBaseFun.select_list_GroupFun("SELECT Query FROM Filter_Query where name = 'last_Query'"))
                    {
                        if (goInto != "")
                        {
                            show_table(goInto);
                            listBox_message.Items.Add(new MyListBoxItem(Color.Green, goInto));
                            break;
                        }
                    }

                }
                catch (Exception ex)
                {


                    SshExeTest.WriteLogFile("1", "button3_Click()", "Error: selet . \n{0}" + ex.Message);
                    //  return "Failed_create_DB_Connection";

                }

            }
        }

        private void listBox_message_DrawItem(object sender, DrawItemEventArgs e)
        {
            MyListBoxItem item = listBox_message.Items[e.Index] as MyListBoxItem; // Get the current item and cast it to MyListBoxItem
            if (item != null)
            {
                e.Graphics.DrawString( // Draw the appropriate text in the ListBox
                    item.Message, // The message linked to the item
                    listBox_message.Font, // Take the font from the listbox
                    new SolidBrush(item.ItemColor), // Set the color 
                    0, // X pixel coordinate
                    e.Index * listBox_message.ItemHeight // Y pixel coordinate.  Multiply the index by the ItemHeight defined in the listbox.
                );
            }
            else
            {
                // The item isn't a MyListBoxItem, do something about it
            }
        }

        private void checkBox_current_Activety_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_current_Activety_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void checkBox_current_Activety_CheckedChanged_1(object sender, EventArgs e)
        {
            //chnage the icon and work with option to ceack on the  button
            if (this.checkBox_current_Activety.Checked)
            {
            this.checkBox_current_Activety.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\favicon2.ico");
            abortLoop = false;
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();  
         
                
            }
            else
            {
                this.checkBox_current_Activety.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\Grean2.ico");
                abortLoop = true;
                worker = null;   
            }

        } //no host list 


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {  
             for (int i = 0; i < 1000000; i++)  
        {
            if (abortLoop)
            {
                abortLoop = false;
                break;
            }
                host_list_for_execute(true);

            }

            // This exception will be handled by the Task
            // and will not cause the program to crash
           
        }
        public class MyListBoxItem
        {
            public MyListBoxItem(Color c, string m)
            {
                ItemColor = c;
                Message = m;
            }
            public Color ItemColor { get; set; }
            public string Message { get; set; }
        }

        private void checkBox1_off_line_CheckedChanged(object sender, EventArgs e)
        {
            FapiClient.Collect_logs("10.76.10.127");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
if ( (bool)dataGridView1.SelectedRows[0].Cells[0].Value == false )
{
    dataGridView1.SelectedRows[0].Cells[0].Value = true;
}
else
{
    dataGridView1.SelectedRows[0].Cells[0].Value = false;

}
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((bool)dataGridView1.SelectedRows[0].Cells[0].Value == false)
            {
                dataGridView1.SelectedRows[0].Cells[0].Value = true;
            }
            else
            {
                dataGridView1.SelectedRows[0].Cells[0].Value = false;

            }
        }

        private void button_copy_select_Click(object sender, EventArgs e)
        {
            string line_select = "";
            if (dataGridView1.Rows.Count != 0)
            {
                
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {

                    if ((bool)item.Cells[0].Value == true)
                    {
                        for (int i = 1; i < 5; i++)
                        {
                            line_select = line_select + "," + item.Cells[i].Value.ToString();
                           
                        }
                        line_select = line_select + "\n";
                    }


                }
            }

        //    MessageBox.Show(line_select, "copy to Clipboard and close ", MessageBoxButtons.OK);
       
            Clipboard.SetText(line_select);

        }


        private void add_to_to_exist()
        {
            const string listhost = "rpa_list2.txt";
            // String TextLine = " ";
            // 1
            // Declare new List.
            List<string> HostInformation = new List<string>();


            // 2
            // Use using StreamReader for disposing.
            using (StreamReader r = new StreamReader(listhost))
            {
                string line = " ";
                long Countline = 0;
               // int less_time = -45;
                while ((line = r.ReadLine()) != null)
                {

                    //count the farst word for not count 
                    //
                    // Split string on spaces.
                    // ... This will separate all the words.
                    //

                    int CountWord = 0;
                    string[] words = line.Split(';');
                    String IP = "";
                    String type = "";
                    String userName = "";
                    String password = "";


                    foreach (string word in words)
                    {

                        if (CountWord == 0)
                        {
                            //not show this line 
                            if (word.IndexOf("#") != -1)
                            {
                                Countline = Countline - 1;
                                break;
                                //Console.WriteLine(word);
                            }
                        }


                        switch (CountWord)
                        {
                            case 0:
                                IP = word;

                                break;
                            case 1:
                                type = word;

                                break;
                            case 2:
                                userName = word;
                                break;
                            case 3:
                                password = word;
                                break;
                            case 4:
                                //not need
                                break;
                            default:
                                SshExeTest.WriteLogFile("1", "Read _log _ssh", "read the 3 valus");

                                break;
                        }


                        CountWord = CountWord + 1;
                    }

                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                //chnage the icon and work with option to ceack on the  button


            if (this.checkBoxAdd_log.Checked)
            {
                button2.Text = "Add Exist collect logs";
                label_type.Text = "Run the query you select on wizard";
                label_type.Size = new System.Drawing.Size(110, 13);
                comboBoxType.Visible = false;
                textBox_kate.Text = "";
                AddGrep addForm;
                addForm = new AddGrep();
                addForm.Show();
                
            }
            else
            {
                button2.Text = "Collect logs";
                label_type.Text = "Log type:";
                label_type.Size = new System.Drawing.Size(55, 13);
                comboBoxType.Visible = true;
            }
        }
    }
}
