using System;
using System.Collections;
//using Tamir.SharpSsh;
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
using System.Diagnostics;
using System.IO.Compression;
using System.Globalization;
using Renci.SshNet;
using Renci.SshNet.Common;







namespace ssh_to_DB
{
    /// <summary>
    /// Summary description for SshExeTest.
    /// </summary>
    public class SshExeTest 
    {
        public static string ssh_command(string grep_string, string SpliterType, string host, string username, string password, int port)
        {
            var connectionInfo = new KeyboardInteractiveConnectionInfo(host, port, username);
            connectionInfo.AuthenticationPrompt += delegate(object sender, AuthenticationPromptEventArgs e)
            {
                foreach (var prompt in e.Prompts)
                {
                    if (prompt.Request.Equals("Password: ", StringComparison.InvariantCultureIgnoreCase))
                    {
                        prompt.Response = password;
                    }
                }



            };

            using (var client = new SshClient(connectionInfo))
            {
                client.Connect();
                var logs = client.RunCommand(grep_string);

                client.Disconnect();
                return logs.Result;
                

            }

        }
  



        public static void offlineDBInsert(string ipConnect)
        {
            string pathFile = "";
            string pathFileLog = "";

            
            pathFile = Application.StartupPath + "\\" + ipConnect;
            pathFileLog = pathFile + " \\core.txt";
            filter_core(pathFileLog, ipConnect, "core.txt");
            pathFileLog = pathFile + " \\mirror.txt";
            filter_file2(pathFileLog, ipConnect, "mirror.txt");
            pathFileLog = pathFile + " \\replication.txt";
            filter_file2(pathFileLog, ipConnect, "replication.txt");
            pathFileLog = pathFile + " \\management.txt";
            filter_file2(pathFileLog, ipConnect, "management.txt");
            pathFileLog = pathFile + " \\control.txt";
            filter_file2(pathFileLog, ipConnect, "control.txt");
            pathFileLog = pathFile + " \\servererror.txt";
            filter_file2(pathFileLog, ipConnect, "servererror.txt", "yyyy-MM-dd HH:mm:ss");
            
            pathFileLog = pathFile + " \\storage.txt";
            filter_file2(pathFileLog, ipConnect, "storage.txt");
            pathFileLog = pathFile + " \\site_connector.txt";
            filter_file2(pathFileLog, ipConnect, "site_connector.txt");
            pathFileLog = pathFile + " \\connectivity_tool.txt";
            filter_file2(pathFileLog, ipConnect, "connectivity_tool.txt");
            pathFileLog = pathFile + " \\client.txt";
            filter_file2(pathFileLog, ipConnect, "client.txt");
            pathFileLog = pathFile + " \\hlr.txt";
            filter_file2(pathFileLog, ipConnect, "hlr.txt");
            pathFileLog = pathFile + " \\connectors.txt";
            filter_file2(pathFileLog, ipConnect, "connectors.txt");
            pathFileLog = pathFile + " \\klr.txt";
            filter_file2(pathFileLog, ipConnect, "klr.txt");
            
                   
        }

        


        //ssh_to_DB.Form1.change_lib("Connecting...");
        public static string GetInputDB(ref string switch_show, string switch_id)
        {
            int columnInsert=0;
            string input = switch_show;
            string[,] item = new string[32,10];
            int i = 0;
         
           
            string[] lines = input.Split('\n');
             foreach (string line in lines)
               {
                   
      
                   char[] delimiters = new char[] { ' '};
                   string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                 // The length of table switch ia 8 becouse it we need only 8 caloum
                   if (parts.Length > 9)
                   {
                       columnInsert = 9;
                   }
                   else
                   {
                       columnInsert = parts.Length;
                   }
                   for (int j = 0; j <= (columnInsert - 1); j++)
                   {
                       item[i,j]=parts[j];
                   }
                  i++;
                 }
             string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\connect.mdb";
                // string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;data source=C:\\Users\\slonis\\My Documents\\Visual Studio 2012\\Projects\\ssh_to_DB\\ssh_to_DB\\connect.mdb";
                
                 DataSet myDataSet = new DataSet();
                 OleDbConnection myAccessConn = null;
                 try
                 {
                     myAccessConn = new OleDbConnection(connectionString);
                 }
                 catch (Exception ex)
                 {
                     WriteLogFile("1", "button1_Click()", "Error: Failed to create a database connection. \n{0}"+ ex.Message);
                     return "Failed_create_DB_Connection";
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

           return "Connect_TO_DB_AND_insert";
        }









        public static void filter_file(String NameLogFile, String CommandRPA, String fileName)
        {
        
                Stream stream = File.Open(NameLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                using (StreamReader f = new StreamReader(stream))
                  
            {
                switch (fileName)
                {

                    //filter fapi file 
                    case "fapi_server.log":
                        {


                            //get the list of fapi command 
                            List<string> linesFilter = FapiCommand();
                            string line2;
                            while ((line2 = f.ReadLine()) != null)
                            {
                                try
                                {
                                    // 4
                                    // Insert logic here.
                                    // ...
                                    // "line" is a line in the file. Add it to our List.
                                    foreach (string fapiFun in linesFilter) // Loop through List with foreach
                                    {
                                        if (line2.IndexOf(fapiFun) != -1)
                                        {
                                            // linesFile.Add(line2);

                                            
                                            String MyString = line2.Substring(0, 19);
                                            DateTime MyDateTime;
                                            MyDateTime = new DateTime();
                                            //    MyDateTime = DateTime.ParseExact(MyString, "yyyy-MM-dd HH:mm:ss,FFF",null);
                                            
                                            MyDateTime = DateTime.ParseExact(MyString, "yyyy-MM-dd HH:mm:ss", null);
                                            //               MessageBox.Show(MyString);
                                            int ComandIndex = line2.IndexOf("COMMAND");
                                            if (ComandIndex == -1)
                                            {
                                                ComandIndex = line2.IndexOf("ERROR");
                                                if (ComandIndex == -1)
                                                {
                                                    ComandIndex = line2.IndexOf("DEBUG");
                                                    if (ComandIndex == -1)
                                                    {
                                                        ComandIndex = 10;
                                                                WriteLogFile("1", "Filter_file2", "on fapi_server.log not have the cut word at line : . \n{0}" + line2 + " \n{0}");

                                                    }
                                                }
                                                
                                            }
                                            string ComandText = line2.Substring(ComandIndex);
                                            bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);

                                        }
                                    }
                                                                }
                                catch (OleDbException e)
                                {
                                    MessageBox.Show("ensert error:" + e.Message.ToString());

                                }

                            } ///end while



                        }
                        break;
                    case "rreasons.log":
                        {
                            string line2;
                            while ((line2 = f.ReadLine()) != null)
                            {

                                readToDB_Rreasons(line2, CommandRPA);

                            }
                        }
                        break;
                    case "control.log":
                        {
                            string line2;
                            while ((line2 = f.ReadLine()) != null)
                            {
                                readToDB_Cotrol(line2, CommandRPA);

                            }
                        }
                        break;
                    //kate_file
                    case "kate_tasks.log":
                        {
                            try
                            {
                                DataTable table_for_DB = new DataTable();
                                table_for_DB.Clear();
                                table_for_DB.Columns.Add("table");
                                table_for_DB.Columns.Add("date");
                                table_for_DB.Columns.Add("ComandText");
                                table_for_DB.Columns.Add("fileName");
                                table_for_DB.Columns.Add("splitter");



                                //get the list of f command 
                                List<string> linesFilter = new List<string>(); //FapiCommand();
                              //  string diff_time = "Time difference kate machine with RPA_OS host RPA:";
                                linesFilter.Add("Start scenario");
                                string find_time_start_test = "INFO  - >>";
                                string nameTest = "Start scenario";
                                string finishTest = "Finish scenario";
                                linesFilter.Add("Executing");
                                linesFilter.Add("Step");
                                linesFilter.Add("ERROR");
                                linesFilter.Add("Start scenario");
                                string line2;
                            
                                DateTime MyDateTime = new DateTime();
                                DateTime MyDateTimeStart = new DateTime();
                             //   long hours = 0;
                            //    long minutes = 0;
                             //   long secuonds = 0;
                            //    bool positiveOrNegative = false;
                             //   bool haveTimeDiff = false;
                                //List<string> toBeInDB;
                                while ((line2 = f.ReadLine()) != null)
                                {//STRAT WHILE
                                    try
                                    {


                                        if (line2.IndexOf(find_time_start_test) != -1)
                                        {

                                            MyDateTimeStart = returnDate(line2, 0, 19);

                                            if (line2.IndexOf(nameTest) != -1)
                                            {
                                                
                                                //  bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, line2, fileName, CommandRPA);
                                                table_for_DB.Rows.Add("Activity", MyDateTime, line2, fileName, CommandRPA);
                                                
                                            }
                                        }
                                        else
                                        {
                                            if (line2.IndexOf(finishTest) != -1)
                                            {
                                                line2 = line2.Replace("Finish", "Start");
                                          
                                                bool checkInsert = DataBaseFun.insert_table("Kate", MyDateTimeStart, line2, fileName, CommandRPA);
                                            }
                                            foreach (string fapiFun in linesFilter) // Loop through List with foreach
                                            {


                                                if (line2.IndexOf(fapiFun) != -1)
                                                {
                                                    // linesFile.Add(line2);

                                                    Regex rgx = new Regex(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3}");
                                                    Match mat = rgx.Match(line2);
                                                    string StringDate = mat.ToString();
                                                    if (StringDate != "")
                                                    {
                                                        //DateTime MyDateTime;
                                                        MyDateTime = new DateTime();
                                                        //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
                                                        // MyDateTime = DateTime.Parse(StringDate);
                                                        MyDateTime = DateTime.ParseExact(StringDate, "yyyy-MM-dd HH:mm:ss,FFF", null);
                                                        int ComandIndex = 24;
                                                        string ComandText = line2.Substring(ComandIndex);
                                                        // bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                                                        table_for_DB.Rows.Add("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                                                        //bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                                                        break;
                                                    }





                                                }
                                            }
                                        }

                                    }
                                    catch (Exception em)
                                    {
                                        WriteLogFile("1", "Filter_file2", "error on the convert date. for kate \n{0}" + em.Message + " \n{0}");
                                        // f.Close();
                                    }


                                }/// end while 
    
                                DataBaseFun.insert_table(table_for_DB);
                                /// 

                            /*    if (positiveOrNegative == true)
                                {
                                    hours = hours * -1;
                                    minutes = minutes * -1;
                                    secuonds = secuonds * -1;
                                }*/

                              //  DataBaseFun.updateTime(hours, minutes, secuonds, "Activity");
                       
                          List<int> RPA_list = DataBaseFun.select_list_int_GroupFun("SELECT TOP 1 Hours, Minutes, Seconds FROM Time_diff WHERE Type='RPA'; ",3);


                                // update the hoars , min .secend 
                          DataBaseFun.updateTime(RPA_list[0], RPA_list[1], RPA_list[2], "Activity");
                          DataBaseFun.updateTime(RPA_list[0], RPA_list[1], RPA_list[2], "Kate");

                            }

                            catch (OleDbException exp)
                            {
                                MessageBox.Show("ensert error:" + exp.Message.ToString());

                            }

                        }

                        break;
                    default:
                        {
                            WriteLogFile("1", "button1_Click()", "The file name not exist on  the file list. \n{0}" );
                            break;
                        }

                }// end case




                  //  f.Close();   
                }
           
                stream.Close();
                    



        }



/// <summary>
/// //////////////////////////////////////////////////////
/// 


        public static void filter_file2(String NameLogFile, String CommandRPA, String fileName ,String formatConvert = "yyyy/MM/dd HH:mm:ss")
        {
             try
                {
            using (StreamReader f = new StreamReader(NameLogFile)) 
            {

                string line2;
                while ((line2 = f.ReadLine()) != null)
                {
                    readToDB_All(line2, fileName, CommandRPA);
                } //the end try

            }

            


            }

             catch (Exception e)
             {
                 WriteLogFile("1", "Filter_file2", "error on the convert date. \n{0}" + e.Message + " \n{0}" );

             }
        }


        public static void filter_core(String NameLogFile, String CommandRPA, String fileName, String formatConvert = "yyyy-MM-dd HH:mm")
        {
            if (File.Exists(NameLogFile))
            {
                using (StreamReader f = new StreamReader(NameLogFile))
                {
                    string line = " ";


                    while ((line = f.ReadLine()) != null)
                    {
                        readToDB_core(line, CommandRPA);


                    }
                }
            }
            else
            {
                MessageBox.Show("the file not exist !!!!! work on line");
            }
        }
/// ///////////////////////////////////////////////////////////





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
 


        
        public static void createUserConnect(String typeHost ,bool OffLine )
        {
     const string listhost = "rpa_list2.txt";

     //drop the Activity befor collect all the RPA 
    
            // 1
            // Declare new List.
            
            

            // 2
            // Use using StreamReader for disposing.
            using (StreamReader r = new StreamReader(listhost))
            {
                // 3
                // Use while != null pattern for loop
                string line=" ";
                long Countline = 0;
                while ((line = r.ReadLine()) != null  )
                {

                  //count the farst word for not count 
                    //
                    // Split string on spaces.
                    // ... This will separate all the words.
                    //
                   
                    int CountWord = 0;
                    string[] words = line.Split(';');
                    foreach (string word in words)
                    {

                        if (CountWord == 0)
                        {
                            if (word.IndexOf("#") != -1)
                            {
                                Countline = Countline - 1;
                                break;
                                //Console.WriteLine(word);
                            }
                        }
                     


       

                         //     Lines[Countline] = IP;


                             CountWord = CountWord + 1;
                    }

                    // 4
                    // Insert logic here.
                    // ...
                    // "line" is a line in the file. Add it to our List.
                    //HostInformation.Add(line);
                    if (line.IndexOf("RPA") != -1)
                    {
                       
                        UserConnect.UserConectClass sendUser = UserConnect.setConnect(line);
                        if (OffLine == false)
                        {
                            offlineDBInsert(sendUser.ip);
                        }
                        else
                        {
                        ////   RunExample(sendUser.type, sendUser.ip, sendUser.userName, sendUser.password);
                            offlineDBInsert(sendUser.ip);
                        }
                    }    
       
                    Countline = Countline + 1;
                }
               
            }

        //    return HostInformation;
        }

       
        public static void executeBatchFile(string file_name)
        {
        try
            {
                Process proc = null;
                string targetDir = Application.StartupPath;//this is where mybatch.bat lies
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = file_name;
                proc.StartInfo.Arguments = string.Format("10");//this is argument
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message,ex.StackTrace.ToString());
            }
        }


        //           List<string> range = FapiCommand();


   public static List<string> FapiCommand()
{  
    List<string> listRange = new List<string>();

    const string listFapi = "fapi_filter.txt";

    // 1
    // Declare new List.
    List<string> linesFilter = new List<string>();
    List<string> linesFile = new List<string>();

    // 2
    // Use using StreamReader for disposing.
    using (StreamReader r = new StreamReader(listFapi))
    {
        // 3
        // Use while != null pattern for loop
        string line;
        while ((line = r.ReadLine()) != null)
        {
            // 4
            // Insert logic here.
            // ...
            // "line" is a line in the file. Add it to our List.
            linesFilter.Add(line);
        }

    }

    return linesFilter;  
}

public static void Compress(FileInfo fi)
{
    // Get the stream of the source file.
    using (FileStream inFile = fi.OpenRead())
    {
        // Prevent compressing hidden and 
        // already compressed files.
        if ((File.GetAttributes(fi.FullName)
            & FileAttributes.Hidden)
            != FileAttributes.Hidden & fi.Extension != ".gz")
        {
            // Create the compressed file.
            using (FileStream outFile =
                        File.Create(fi.FullName + ".gz"))
            {
                using (GZipStream Compress =
                    new GZipStream(outFile,
                    CompressionMode.Compress))
                {
                    // Copy the source file into 
                    // the compression stream.
                    inFile.CopyTo(Compress);

                    Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                        fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                }
            }
        }
    }
}

public static void Decompress(FileInfo fi)
{
    // Get the stream of the source file.
    using (FileStream inFile = fi.OpenRead())
    {
        // Get original file extension, for example
        // "doc" from report.doc.gz.
        string curFile = fi.FullName;
        string origName = curFile.Remove(curFile.Length -
                fi.Extension.Length);

        //Create the decompressed file.
        using (FileStream outFile = File.Create(origName))
        {
            using (GZipStream Decompress = new GZipStream(inFile,
                    CompressionMode.Decompress))
            {
                // Copy the decompression stream 
                // into the output file.
                Decompress.CopyTo(outFile);

          //      Console.WriteLine("Decompressed: {0}", fi.Name);
            }
        }
    }
}

public static DateTime returnDate(string lineForDate, int startDateIndex, int lengthIndix, string formatConvert = "yyyy-MM-dd HH:mm:ss")
{

    String StringDate = lineForDate.Substring(startDateIndex, lengthIndix);
    //fix when date is not two char
    String StringDateReplace = StringDate.Substring(8, 1);
    if (StringDateReplace == " ")
    {
        StringDate = StringDate.Remove(8, 1);
        StringDate = StringDate.Insert(8, "0");
    }
    DateTime MyDateTime;
    MyDateTime = new DateTime();
    //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
    // MyDateTime = DateTime.Parse(StringDate);
    MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
    return MyDateTime;
  //  string ComandText = line2.Substring(endDateIndex + 1);

}

public static void readToDB_Cotrol(string line2, string CommandRPA)
{

                                try
                                {


                                    int startZipfie = line2.IndexOf("gz:");
                                    int endDateIndex = line2.IndexOf("]");
                                    int lengthIndix = 23;
                                    if (endDateIndex != -1 || startZipfie != -1)
                                    {
                                        String StringDate = line2.Substring(startZipfie + 1, lengthIndix);
                                        String StringDateReplace = StringDate.Substring(8, 1);
                                        if (StringDateReplace == " ")
                                        {
                                            StringDate = StringDate.Remove(8, 1);
                                            StringDate = StringDate.Insert(8, "0");
                                        }
                                        DateTime MyDateTime;
                                        MyDateTime = new DateTime();
                                        // MyDateTime = DateTime.Parse(StringDate);
                                        MyDateTime = DateTime.ParseExact(StringDate, "ddd MMM dd HH:mm:ss yyyy", null);
                                        string ComandText = line2.Substring(endDateIndex + 1);
                                        bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, "control.log", CommandRPA);

                                    }
                                }

                                catch (OleDbException exp)
                                {
                                    MessageBox.Show("ensert error Control:" + exp.Message.ToString());

                                }

}
public static void readToDB_Rreasons(string line2, string CommandRPA)
{
    try
    {
        SshExeTest.WriteLogFile("1", "readToDB_Rreasons", line2);
       // Regex rgx = new Regex(@"\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}.\d{3}");
       // Regex rgx = new Regex(("\w{3})\s+((?<month>\w{3})\s+((?<date>)\d)\s((?<time>)[0-9:]+)\s+((?<year>)\d{4})
        // Tue Jun 30 13:55:46 2015
        int startDateIndex = line2.IndexOf("[");
        int endDateIndex = line2.IndexOf("]");
        if (endDateIndex != -1 || startDateIndex != -1)
        {
            int lengthIndix = endDateIndex - startDateIndex - 1;
            String StringDate = line2.Substring(startDateIndex + 1, lengthIndix);
            //fix when date is not two char
            String StringDateReplace = StringDate.Substring(8, 1);
            if (StringDateReplace == " ")
            {
                StringDate = StringDate.Remove(8, 1);
                StringDate = StringDate.Insert(8, "0");
            }
            DateTime MyDateTime;
            MyDateTime = new DateTime();
            // MyDateTime = DateTime.Parse(StringDate);
            MyDateTime = DateTime.ParseExact(StringDate, "ddd MMM dd HH:mm:ss yyyy", null);
            string ComandText = line2.Substring(endDateIndex + 1);
            bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, "rreasons.log", CommandRPA);
        }
    }
    catch (OleDbException ep)
    {
        WriteLogFile("1", "readToDB_Rreasons :", "error on the convert date. \n{0}" + ep.Message + " \n{0}");

    }

}

public static void readToDB_Rreasons(string line2, string CommandRPA, DateTime stop)
{
    try
    {
        int startDateIndex = line2.IndexOf("[");
        int endDateIndex = line2.IndexOf("]");
        if (endDateIndex != -1 || startDateIndex != -1)
        {
            int lengthIndix = endDateIndex - startDateIndex - 1;
            String StringDate = line2.Substring(startDateIndex + 1, lengthIndix);
            //fix when date is not two char
            String StringDateReplace = StringDate.Substring(8, 1);
            if (StringDateReplace == " ")
            {
                StringDate = StringDate.Remove(8, 1);
                StringDate = StringDate.Insert(8, "0");
            }
            DateTime MyDateTime;
            MyDateTime = new DateTime();
            // MyDateTime = DateTime.Parse(StringDate);
            MyDateTime = DateTime.ParseExact(StringDate, "ddd MMM dd HH:mm:ss yyyy", null);
            if (MyDateTime > stop)
            {
                string ComandText = line2.Substring(endDateIndex + 1);
                bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, "rreasons.log", CommandRPA);
            }
        }
    }
    catch (OleDbException ep)
    {
        WriteLogFile("1", "readToDB_Rreasons :", "error on the convert date. \n{0}" + ep.Message + " \n{0}");

    }

}

public static void readToDB_All_old(string line2, string fileName, string CommandRPA, String formatConvert = "yyyy/MM/dd HH:mm:ss")
{


    try
    {

        // check if it the same format

        int startDateIndex = 0;
        if (line2.IndexOf("/") == 0)
        {

            startDateIndex = line2.IndexOf(":") + 1;
        }

        int endDateIndex = line2.IndexOf("-");
        if (line2.IndexOf("HERE") != -1)
        {
            //the lange of the Date
            endDateIndex = startDateIndex + 19;
        }
        //not calculation the Millie secund
        int lengthIndix = 19;

        //   if (lengthIndix >= 14 || lengthIndix <= 24)
        // {

        if (endDateIndex != -1 && startDateIndex != -1)
        {
            String StringDate = line2.Substring(startDateIndex, lengthIndix);
            //fix when date is not two char
            String StringDateReplace = StringDate.Substring(8, 1);
            if (StringDateReplace == " ")
            {
                StringDate = StringDate.Remove(8, 1);
                StringDate = StringDate.Insert(8, "0");
            }
            DateTime MyDateTime;
            MyDateTime = new DateTime();
            //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
            // MyDateTime = DateTime.Parse(StringDate);
            MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
            string ComandText = line2.Substring(endDateIndex + 1);
            bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
        }
        //  }
    }
    catch (Exception em)
    {
        WriteLogFile("1", "Filter_file2", "error on the convert date. \n{0}" + em.Message + " \n{0}");

    }

}
public static void readToDB_All(string line2, string fileName, string CommandRPA, String formatConvert = "yyyy/MM/dd HH:mm:ss.fff")
{
    DataTable table_for_DB = new DataTable();
    table_for_DB.Clear();
    table_for_DB.Columns.Add("table");
    table_for_DB.Columns.Add("date");
    table_for_DB.Columns.Add("ComandText");
    table_for_DB.Columns.Add("fileName");
    table_for_DB.Columns.Add("splitter");
    // if (formatConvert == "yyyy/MM/dd HH:mm:ss.fff")

    Regex rgx = new Regex(@"\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}.\d{3}");

    if (formatConvert == "yyyy-MM-dd HH:mm:ss")
    {
        rgx = new Regex(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}");
    }
    try
    {



        using (System.IO.StringReader sr = new System.IO.StringReader(line2))
        {

            string line;
            //  string linesss;
            while ((line = sr.ReadLine()) != null)
            {


                // check if it the same format

                int startDateIndex = 0;
                /*     if (line.IndexOf("/") == 0)
                     {

                         startDateIndex = line.IndexOf(":") + 1;
                     }*/

                int endDateIndex = line.IndexOf("-");
                if (line.IndexOf("HERE") != -1)
                {
                    //the lange of the Date
                    endDateIndex = startDateIndex + 19;
                }
                
         //       Regex rgx = new Regex(@"\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}.\d{3}");
                Match mat = rgx.Match(line);
                string StringDate = mat.ToString();
                if (StringDate != "")
                {
                    DateTime MyDateTime;
                    MyDateTime = new DateTime();
                    //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
                    // MyDateTime = DateTime.Parse(StringDate);
                    MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
                    string ComandText = line.Substring(endDateIndex + 1);
                    // bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                    table_for_DB.Rows.Add("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                }



            } //while end
        } // using end

        //  }
    }
    catch (Exception em)
    {
        WriteLogFile("1", "Filter_file2", "error on the convert date. \n{0}" + em.Message + " \n{0}");

    }
    DataBaseFun.insert_table(table_for_DB);
}

public static void readToDB_All(string line2, string fileName, string CommandRPA, DateTime stop, String formatConvert = "yyyy/MM/dd HH:mm:ss.fff")
{
    DataTable table_for_DB = new DataTable();
    table_for_DB.Clear();
    table_for_DB.Columns.Add("table");
    table_for_DB.Columns.Add("date");
    table_for_DB.Columns.Add("ComandText");
    table_for_DB.Columns.Add("fileName");
    table_for_DB.Columns.Add("splitter");
    // if (formatConvert == "yyyy/MM/dd HH:mm:ss.fff")

    Regex rgx = new Regex(@"\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}.\d{3}");

    if (formatConvert == "yyyy-MM-dd HH:mm:ss")
    {
        rgx = new Regex(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}");
    }
    try
    {



        using (System.IO.StringReader sr = new System.IO.StringReader(line2))
        {

            string line;
            //  string linesss;
            while ((line = sr.ReadLine()) != null)
            {


                // check if it the same format

                int startDateIndex = 0;
                /*     if (line.IndexOf("/") == 0)
                     {

                         startDateIndex = line.IndexOf(":") + 1;
                     }*/

                int endDateIndex = line.IndexOf("-");
                if (line.IndexOf("HERE") != -1)
                {
                   
                    endDateIndex = startDateIndex + 19;
                }

                //       Regex rgx = new Regex(@"\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}.\d{3}");
                Match mat = rgx.Match(line);
                string StringDate = mat.ToString();
                if (StringDate != "")
                {
                    DateTime MyDateTime;
                    MyDateTime = new DateTime();
                    //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
                    // MyDateTime = DateTime.Parse(StringDate);
                    MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
                    if (MyDateTime > stop)
                    {
                        string ComandText = line.Substring(endDateIndex + 1);
                        // bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                        table_for_DB.Rows.Add("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                    }

                }



            } //while end
        } // using end

        //  }
    }
    catch (Exception em)
    {
        WriteLogFile("1", "Filter_file2", "error on the convert date. \n{0}" + em.Message + " \n{0}");

    }
    DataBaseFun.insert_table(table_for_DB);
}

public static void readToDB_ESX_Lines(string line2, string fileName, string CommandRPA, String formatConvert = "yyyy/MM/dd HH:mm:ss.fff")
{
    DataTable table_for_DB = new DataTable();
    table_for_DB.Clear();
    table_for_DB.Columns.Add("table");
    table_for_DB.Columns.Add("date");
    table_for_DB.Columns.Add("ComandText");
    table_for_DB.Columns.Add("fileName");
    table_for_DB.Columns.Add("splitter");

    try
    {
        using (StringReader reader = new StringReader(line2))
        {
            string line;
            //  string linesss;
            while ((line = reader.ReadLine()) != null)
            {


                // check if it the same format

                int startDateIndex = 0;
           /*     if (line.IndexOf("/") == 0)
                {

                    startDateIndex = line.IndexOf(":") + 1;
                }*/

                int endDateIndex = line.IndexOf("-");
                if (line.IndexOf("HERE") != -1)
                {
                    //the lange of the Date
                    endDateIndex = startDateIndex + 19;
                }

                Regex rgx = new Regex(@"\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}.\d{3}");
                     Match mat = rgx.Match(line);
                    string StringDate= mat.ToString();
                    if (StringDate != "")
                    {
                        DateTime MyDateTime;
                        MyDateTime = new DateTime();
                        //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
                        // MyDateTime = DateTime.Parse(StringDate);
                        MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
                        string ComandText = line.Substring(endDateIndex + 1);
                        // bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                        table_for_DB.Rows.Add("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                    }



            } //while end
        } // using end

        //  }
    }
    catch (Exception em)
    {
        WriteLogFile("1", "Filter_file2", "error on the convert date. \n{0}" + em.Message + " \n{0}");

    }
    DataBaseFun.insert_table(table_for_DB);
}



public static void readToDB_All_Lines(string line2, string fileName, string CommandRPA, String formatConvert = "yyyy/MM/dd HH:mm:ss")
{
    DataTable table_for_DB = new DataTable();
    table_for_DB.Clear();
    table_for_DB.Columns.Add("table");
    table_for_DB.Columns.Add("date");
    table_for_DB.Columns.Add("ComandText");
    table_for_DB.Columns.Add("fileName");
    table_for_DB.Columns.Add("splitter");
    
    try
    {
        using (StringReader reader = new StringReader(line2))
        {
            string line;
            //  string linesss;
            while ((line = reader.ReadLine()) != null)
            {
               

        // check if it the same format

        int startDateIndex = 0;
        if (line.IndexOf("/") == 0)
        {

            startDateIndex = line.IndexOf(":") + 1;
        }

        int endDateIndex = line.IndexOf("-");
        if (line.IndexOf("HERE") != -1)
        {
            //the lange of the Date
            endDateIndex = startDateIndex + 19;
        }
        //not calculation the Millie secund
        int lengthIndix = 19;

        //   if (lengthIndix >= 14 || lengthIndix <= 24)
        // {

        if (endDateIndex != -1 && startDateIndex != -1)
        {
            String StringDate = line.Substring(startDateIndex, lengthIndix);
            //fix when date is not two char
            String StringDateReplace = StringDate.Substring(8, 1);
            if (StringDateReplace == " ")
            {
                StringDate = StringDate.Remove(8, 1);
                StringDate = StringDate.Insert(8, "0");
            }
            DateTime MyDateTime;
            MyDateTime = new DateTime();
            //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
            // MyDateTime = DateTime.Parse(StringDate);
            MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
            string ComandText = line.Substring(endDateIndex + 1);
           // bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
            table_for_DB.Rows.Add("Activity", MyDateTime, ComandText, fileName, CommandRPA);
        }



            } //while end
        } // using end
       
        //  }
    }
    catch (Exception em)
    {
        WriteLogFile("1", "Filter_file2", "error on the convert date. \n{0}" + em.Message + " \n{0}");

    }
 DataBaseFun.insert_table(table_for_DB);
}




public static void readToDB_core(string line, string CommandRPA )
{
    try
    {
                        long Countline = 0;
                        string StringDate = " ";
                        string ComandText = " ";
                        //count the farst word for not count 
                        //
                        // Split string on spaces.
                        // ... This will separate all the words.
                        //

                        // int CountWord = 0;
                        string[] words = line.Split(' ');
                        foreach (string word in words)
                        {
                            Countline++;
                            if (Countline == 6)
                                StringDate = word;
                            if (Countline == 7)
                                StringDate = StringDate + " " + word;
                            if (Countline == 8)
                                ComandText = word;

                        }

                        DateTime MyDateTime;
                        MyDateTime = new DateTime();

                        MyDateTime = DateTime.ParseExact(StringDate, "yyyy-MM-dd HH:mm", null);
                        bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, "core", CommandRPA);
        }
   
     catch (OleDbException ep)
      {
               WriteLogFile("1", "core file ", "error on the convert date. \n{0}" + ep.Message + " \n{0}");

     }
    }
public static void readToDB_core(string line, string CommandRPA ,DateTime stop)
{
    try
    {
        long Countline = 0;
        string StringDate = " ";
        string ComandText = " ";
        //count the farst word for not count 
        //
        // Split string on spaces.
        // ... This will separate all the words.
        //

        // int CountWord = 0;
        string[] words = line.Split(' ');
        foreach (string word in words)
        {
            Countline++;
            if (Countline == 6)
                StringDate = word;
            if (Countline == 7)
                StringDate = StringDate + " " + word;
            if (Countline == 8)
                ComandText = word;

        }

        DateTime MyDateTime;
        MyDateTime = new DateTime();

        MyDateTime = DateTime.ParseExact(StringDate, "yyyy-MM-dd HH:mm", null);
        if (MyDateTime > stop)
        {
            bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, "core", CommandRPA);
        }
    }

    catch (OleDbException ep)
    {
        WriteLogFile("1", "core file ", "error on the convert date. \n{0}" + ep.Message + " \n{0}");

    }
}

public static void readToDB_FAPI_old(string line, string CommandRPA)
{
    try
    {
        CultureInfo enUS = new CultureInfo("en-US"); 
        DateTime MyDateTime;
        MyDateTime = new DateTime();
        int startDateIndex = 0;
        if (line.IndexOf("/") == 0)
        {

            startDateIndex = line.IndexOf(":") + 1;
        }

        int endDateIndex = line.IndexOf("-");
        //not calculation the Millie secund
        int lengthIndix = 19;

        //   if (lengthIndix >= 14 || lengthIndix <= 24)
        // {

        if (endDateIndex != -1 && startDateIndex != -1)
        {
            String StringDate = line.Substring(startDateIndex, lengthIndix);
            //fix when date is not two char
            String StringDateReplace = StringDate.Substring(8, 1);
            if (StringDateReplace == " ")
            {
                StringDate = StringDate.Remove(8, 1);
                StringDate = StringDate.Insert(8, "0");
            }

            if (!DateTime.TryParseExact(StringDate, "yyyy-MM-dd HH:mm:ss", enUS,
                           DateTimeStyles.None, out MyDateTime))
            {
        //    else
                MyDateTime = DateTime.ParseExact("2014-04-14 13:24:27", "yyyy-MM-dd HH:mm:ss", null);
                WriteLogFile("1", "Filter_file2", "The Data format is not correct : . \n{0}" + StringDate + " \n{0}");
            }


        }

        //            
        int ComandIndex = line.IndexOf("COMMAND");
        if (ComandIndex == -1)
        {
            ComandIndex = line.IndexOf("ERROR");
            if (ComandIndex == -1)
            {
                //ComandIndex = line.IndexOf("DEBUG"); not test add I remove it 
               // if (ComandIndex == -1)
                //{
                    ComandIndex = 10;//ComandIndex 10 is go to garbege
                    WriteLogFile("1", "Filter_file2", "on fapi_server.log not have the cut word at line : . \n{0}" + line + " \n{0}");

                //}
            }

        }
        if (ComandIndex != 10)
        {
        string ComandText = line.Substring(ComandIndex);

        bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, "Fapi", CommandRPA);
        }
    }

    catch (OleDbException ep)
    {
        WriteLogFile("1", "FAPI: ", "error on the convert date. \n{0}" + ep.Message + " \n{0}");

    }
} // end function 
    
public static void readToDB_FAPI(string line2, string CommandRPA)
{
    DataTable table_for_DB = new DataTable();
    table_for_DB.Clear();
    table_for_DB.Columns.Add("table");
    table_for_DB.Columns.Add("date");
    table_for_DB.Columns.Add("ComandText");
    table_for_DB.Columns.Add("fileName");
    table_for_DB.Columns.Add("splitter");
    Regex rgx = new Regex(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3}");
    string formatConvert = "yyyy-MM-dd HH:mm:ss,fff";


    try
    {
        using (System.IO.StringReader sr = new System.IO.StringReader(line2))
        {

            string line;
            //  string linesss;
            while ((line = sr.ReadLine()) != null)
            {


            /////////////// int endDateIndex = line.IndexOf("-");
             int ComandIndex = line.IndexOf("COMMAND");
                  if (ComandIndex == -1)
                     {
                    ComandIndex = line.IndexOf("ERROR ");
                   if (ComandIndex == -1)
                       {
                //ComandIndex = line.IndexOf("DEBUG"); not test add I remove it 
               // if (ComandIndex == -1)
                //{
                    ComandIndex = 10;//ComandIndex 10 is go to garbege
                    WriteLogFile("1", "read_DB_fapi", "on fapi_server.log not have the cut word at line : . \n{0}" + line + " \n{0}");

                //}
                       }

                     }
                  string ComandText = "";
                  if (ComandIndex != 10)
                  {
                       ComandText = line.Substring(ComandIndex);
                  }
                  else
                  {
                      ComandText = line.Substring(ComandIndex);
                  }


               
                Match mat = rgx.Match(line);
                string StringDate = mat.ToString();
                if (StringDate != "")
                {
                    DateTime MyDateTime;
                    MyDateTime = new DateTime();
                    //      String formatConvert = "yyyy/MM/dd HH:mm:ss";
                    // MyDateTime = DateTime.Parse(StringDate);
                    MyDateTime = DateTime.ParseExact(StringDate, formatConvert, null);
                 ////////////   string ComandText = line.Substring(endDateIndex + 1);
                    // bool checkInsert = DataBaseFun.insert_table("Activity", MyDateTime, ComandText, fileName, CommandRPA);
                    table_for_DB.Rows.Add("Activity", MyDateTime, ComandText, "Fapi", CommandRPA);
                }



            } //while end
        } // using end

        //  }
    }
    catch (Exception em)
    {
        WriteLogFile("1", "Filter_file2", "error on the convert date. \n{0}" + em.Message + " \n{0}");

    }
    DataBaseFun.insert_table(table_for_DB);
}


}
        



    
     
}


