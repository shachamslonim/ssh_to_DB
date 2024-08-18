using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ssh_to_DB.Properties;

namespace ssh_to_DB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

    
                host_list_dailog("rpa_list2.txt");
 //              List<int> time_list = DataBaseFun.select_list_int_GroupFun("SELECT TOP 1 Hours, Minutes, Seconds FROM Time_diff WHERE Type='RPA'; ", 3);
 //              string time_diff = time_list[0].ToString + ":" + time_list[1].ToString + ":" + time_list[2].ToString;
            //dataGridView_list_host.Rows.Remove()
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            dataGridView_list_host.Rows.Add("", "RPA", "", "", "00:00:00");
        }
        private void host_list_dailog(String NameLogFile)
        {
          
            const string listhost = "rpa_list2.txt";
            //    String TextLine = " ";
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
                bool insertRow = true;
                while ((line = r.ReadLine()) != null)
                {
                    insertRow = true;
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
                    string timeDiff = " ";
                    foreach (string word in words)
                    {

                        if (CountWord == 0)
                        {
                            //not show this line 
                            if (word.IndexOf("#") != -1)
                            {
                                Countline = Countline - 1;
                                insertRow = false;
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
                                timeDiff = word;
                                break;
                            case 5:
                                //bot
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
                    if (insertRow == true)
                    {
                        dataGridView_list_host.Rows.Add(IP, type, userName, password, timeDiff);
                    }

                    Countline = Countline + 1;
                }

            }

            //    return HostInformation;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {

            if (this.dataGridView_list_host.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView_list_host.SelectedRows)
                    if (!row.IsNewRow)
                        dataGridView_list_host.Rows.Remove(row);
            }
                        else
            {
                MessageBox.Show("Please select all the row no slot");
            }
      
        }

        private void button_save_Click(object sender, EventArgs e)
        {

            string path = Application.StartupPath ;
         //   File.Delete(path);
            // This text is added only once to the file. 
            if (!File.Exists(path))
            {
                int countColumnUse =0;
                bool saveOrNot = true;
                string lines = "#IP;SiteID;BoxID;RootPassword;timeDeff;" + Environment.NewLine;
                for (int row = 0; row < (dataGridView_list_host.RowCount-1); row++)
                {
                    // Create a file to write to. 
                    
                    for (int col = 0; col < dataGridView_list_host.ColumnCount ; col++)
                    {
                        if (dataGridView_list_host.Rows[row].Cells[col].Value != null  )
                        {
                            lines += dataGridView_list_host.Rows[row].Cells[col].Value.ToString() + (string.IsNullOrEmpty(lines) ? " " : ";");
                           if (dataGridView_list_host.Rows[row].Cells[col].Value.ToString()!="")
                            countColumnUse++;
                        }
                    }
                    if (countColumnUse != 5)
                        saveOrNot = false;
                    lines += Environment.NewLine;
                    countColumnUse = 0;
                }
                if (saveOrNot == true)
                {
                    File.Delete(path + "\\rpa_list2.txt");
                    File.WriteAllText(path + "\\rpa_list2.txt", lines);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("All row need to be with full Value or if you have extra line please delete the empty line");
                    //  host_list("rpa_list2.txt");
                    //All_show_host("rpa_list2.txt");
                   
                }
               // Form1.host_list("rpa_list2.txt");
            }

        }

        private void button_time_update_Click(object sender, EventArgs e)
        {
            string formatConvert = "HH:mm:ss";
            DateTime MyDateTime;
            Regex rgx = new Regex(@"\d{2}:\d{2}:\d{2}");
            DataBaseFun.DELETE_table("Time_diff");
            TimeSpan t;
            DateTime temp_time;
            string output_time;
            for (int row = 0; row < (dataGridView_list_host.RowCount-1); row++)
                {
                    // Create a file to write to.
              
                   int counter=0;
                string[] SshParametr= new string[4];
                    for (int col = 0; col < dataGridView_list_host.ColumnCount ; col++)
                    {
                       
                        if (dataGridView_list_host.Rows[row].Cells[col].Value != null  )
                        {
                            SshParametr[counter]=dataGridView_list_host.Rows[row].Cells[col].Value.ToString();
                            
                         //   lines += dataGridView_list_host.Rows[row].Cells[col].Value.ToString() + (string.IsNullOrEmpty(lines) ? " " : ";");
                           if (SshParametr[counter]=="" ||counter ==3 )
                           {
                               if (counter == 3)
                               {
                                   output_time = SshExeTest.ssh_command("date", SshParametr[1], SshParametr[0], SshParametr[2], SshParametr[3], 22);
                                   Match mat = rgx.Match(output_time);
                                   string StringDate = mat.ToString();
                                   MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
                                   temp_time = DateTime.Now;
                                   t = MyDateTime - temp_time;
                                   dataGridView_list_host.Rows[row].Cells[4].Value = t.ToString();
                                   int Hours = t.Hours;
                                   int min = t.Minutes;
                                   int sec = t.Seconds;

                                   DataBaseFun.insert_table("Time_diff", SshParametr[0], SshParametr[1], Hours, min, sec);
                               }
                               break;
                           }
                        
                               }
                           
                           counter++;
                        }



                }


            DataBaseFun.select_list_int_GroupFun("SELECT TOP 1 Hours, Minutes, Seconds FROM Time_diff WHERE Type='RPA'; ", 3); 
        }

    }
}
