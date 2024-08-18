using System;
using System.Collections;
using Tamir.SharpSsh;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Data.OleDb;
using System.Data;
using ssh_to_DB;
using System.Collections.Generic;
using System.Text;
using System.IO;





namespace ssh_to_DB
{
    /// <summary>
    /// Summary description for SshExeTest.
    /// </summary>
    public class SshExeTest 
    {
        public static void  RunExample()
        {
            try
            {
                SshConnectionInfo input =  Util.GetInput("10.76.7.136","admin","password");
                SshExec exec = new SshExec(input.Host, input.User);
                if (input.Pass != null) exec.Password = input.Pass;
                if (input.IdentityFile != null) exec.AddIdentityFile(input.IdentityFile);
                Form1 frm1 = new Form1();
                frm1.change_lib("Connecting...");
    
              //  frm1.run_InitializeComponent();
                exec.Connect();
            //    ssh_to_DB.Form1.label2.Text = "OK";
                MessageBox.Show("OK");
              // while (true)
               // {
                    Console.Write("Enter a command to execute ['Enter' to cancel]: ");
                    string command = Console.ReadLine();
                   // if (command == null) break;
                    string output = exec.RunCommand("switchshow");
                    int idx;
                    idx = output.IndexOf("0   0");
                    idx = idx - 2;
                    output = output.Substring(idx);
                    string out1 = GetInput(ref output, "10.76.7.136");
                    Console.WriteLine(output);
              // }
                Console.Write("Disconnecting...");
                exec.Close();
                Console.WriteLine("OK");
            }
            catch (Exception e)
            {
                WriteLogFile("1", "button1_Click()", "Error: Failed to create a ssh connection. \n{0}" + e.Message);
            
            }

        }
        //ssh_to_DB.Form1.change_lib("Connecting...");
        public static string GetInput(ref string switch_show, string switch_id)
        {
            
            string input = switch_show;
            string[,] item = new string[32,10];
            int i = 0;
         
           
            string[] lines = input.Split('\n');
             foreach (string line in lines)
               {
                   
            //      item  = {"  "," "," "," "," "," "," "," "," "};
                       //[] = {" "," "," "," "," "," "," "," "," "};
                   char[] delimiters = new char[] { ' '};
                   string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                   for (int j = 0; j <= (parts.Length - 1); j++)
                   {
                       item[i,j]=parts[j];
                   }
                  i++;
                 }

                 string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;data source=C:\\Users\\slonis\\My Documents\\Visual Studio 2005\\Projects\\ssh_to_DB\\ssh_to_DB\\connect.mdb";
                
                 DataSet myDataSet = new DataSet();
                 OleDbConnection myAccessConn = null;
                 try
                 {
                     myAccessConn = new OleDbConnection(connectionString);
                 }
                 catch (Exception ex)
                 {
                     WriteLogFile("1", "button1_Click()", "Error: Failed to create a database connection. \n{0}"+ ex.Message);
                     return "ggg";
                 }


                 string sql = "SELECT * FROM switch";

                 try
                 {

                     OleDbCommand myAccessCommand = new OleDbCommand(sql, myAccessConn);
                     OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                     myAccessConn.Open();
                     myDataAdapter.Fill(myDataSet, "switch");
                    
                 }
                 catch (Exception ex)
                 {
                    WriteLogFile("1", "button1_Click()","Error: Failed to retrieve the required data from the DB.\n{0}" + ex.Message);
                     return "fff";
                 }
                 finally
                 {
                     myAccessConn.Close();
                 }
                 DataColumnCollection drc = myDataSet.Tables["switch"].Columns;
                 DataRowCollection dra = myDataSet.Tables["switch"].Rows;


                 foreach (DataRow dr in dra)
                 {
          
                     WriteLogFile("1", "button1_Click()", "switch " + dr[0] + " is " + dr[1] + " is "+ dr[2]  );

                 }


            //----------------------------------------------------
                 string insertStatement = "INSERT INTO switch "
                                         + "(switch_ID, port, wwn) "
                                         + "VALUES (@switch_ID, @port, @wwn)";

                 OleDbCommand insertCommand = new OleDbCommand(insertStatement, myAccessConn);
                 for (int r = 0; r <= 31; r++)
                 {
                     // port id
                     // ip not port not need to insert data
                     if (item[r, 1] != null)
                     {
                         insertCommand.Parameters.Add("@port", OleDbType.Char).Value = item[r, 1];

                         // switch IP
                         insertCommand.Parameters.Add("@switch_ID", OleDbType.Char).Value = switch_id;


                         //wwn
                         if (item[r, 8] == null)
                             item[r, 8] = " ";
                         insertCommand.Parameters.Add("@wwn", OleDbType.Char).Value = item[r, 8];


                         //        insertCommand.Parameters.Add("@switch_ID", OleDbType.Char).Value = switch_id;
                         //       insertCommand.Parameters.Add("@port", OleDbType.Char).Value = item[2,1];
                         //       insertCommand.Parameters.Add("@wwn", OleDbType.Char).Value = (item[2,6]);

                         myAccessConn.Open();

                         try
                         {
                             int count = insertCommand.ExecuteNonQuery();

                         }
                         catch (OleDbException ex)
                         {
                             MessageBox.Show(ex.Message);
                         }
                         finally
                         {
                             myAccessConn.Close();
                             insertCommand.Parameters.Clear();
                         }
                     }
                 }

           return "rrrrr";
        }

        public static void WriteLogFile(string fileName, string methodName, string message)
        {

            try
            {

                if (!string.IsNullOrEmpty(message))
                {
                    String FilePathLog = "@" + Application.StartupPath + "\\Log_ssh.txt";
              
                    if (File.Exists(FilePathLog))
                    using (FileStream file = new FileStream(Application.StartupPath + "\\Log_ssh.txt", FileMode.OpenOrCreate, FileAccess.Write))
                    {

                        StreamWriter streamWriter = new StreamWriter(file);

                        streamWriter.WriteLine((((System.DateTime.Now + " - ") + fileName + " - ") + methodName + " - ") + message);

                        streamWriter.Close();
                    }
        //fileName wil  rewrite
                    else
                    using (FileStream file = new FileStream(Application.StartupPath + "\\Log_ssh.txt", FileMode.Append, FileAccess.Write))
                    {

                        StreamWriter streamWriter = new StreamWriter(file);

                        streamWriter.WriteLine((((System.DateTime.Now + " - ") + fileName + " - ") + methodName + " - ") + message);

                        streamWriter.Close();
                    }



                    }

                }

            

          catch
      {

         }

        } 


       

    }
     
}


