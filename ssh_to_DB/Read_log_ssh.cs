using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Data;
using System.IO;
using System.Windows.Forms;


namespace ssh_to_DB
{

    class Read_log_ssh
    {
        public static void read_RPA_logs(string host , string username ,string password ,int port ,string Type_log ,bool current) 
        {
            SshExeTest.WriteLogFile("1", "ead_RPA_logs", "Error: start with Fapi. \n{0}");
            // create the Grap funcrion to Fapi......
            SshExeTest.WriteLogFile("1", "ead_RPA_logs", "Error: The collect log is to long. \n{0}");
            List<string> linesFilter = SshExeTest.FapiCommand();
            string fapiGrepString = "zgrep -E '"; 
            foreach (string fapiFun in linesFilter) // Loop through List with foreach
            {
                fapiGrepString = fapiGrepString + fapiFun + "|";
                
            }
            
            fapiGrepString = fapiGrepString.Remove(fapiGrepString.Length - 1, 1) + "' /home/kos/RPServers/RPServers_logs/fapi/fapi_server.log*";
           
              
            DataTable all_row = DataBaseFun.select_all("log_RPA","RPA",Type_log);

               
                  //  string gggg = row[0].ToString;
                    //  dataGridView1.Rows.Add(all_row.Rows[i].ItemArray[0], all_row.Rows[i].ItemArray[1], all_row.Rows[i].ItemArray[2], all_row.Rows[i].ItemArray[3]);

            SshExeTest.WriteLogFile("1", "read_RPA_logs", "connect to RPA. \n{0}"); 
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
                        try
                        {
                            SshExeTest.WriteLogFile("1", "read_RPA_logs", "max_Time"); 
                            DateTime MaxTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                            client.Connect();

                            for (int i = 0; i < all_row.Rows.Count; i++)
                            {
                       
                                //     var yyyy = client.RunCommand("ls -la| grep history");
                                //     yyyy.Execute();
                                //     Func<string> fn = all_row.Rows[i].ItemArray[0].ToString();
                                string grep_string = all_row.Rows[i].ItemArray[0].ToString();
                                string file_name = all_row.Rows[i].ItemArray[1].ToString();
                                //see_string = see_string + "";
                                if (current)
                                 MaxTime = DataBaseFun.selectGroupFun("SELECT MAX(date) FROM Activity where LOG = '"  + file_name + "';" );
                                SshExeTest.WriteLogFile("1", "read_RPA_logs", "SELECT MAX(date) FROM" + MaxTime + "the file" + file_name); 
                                var logs = client.RunCommand(grep_string);
                                SshExeTest.WriteLogFile("1", "read_RPA_logs", "befor_execute");
                                //  var logs = client.RunCommand("zgrep -E 'HERE|Assertion|DLManager: deadlock suspected|moves to bitmap|highLoadDiskManagerSpace|dirtify volumes|dirtifyVolume|dirtify all:' /home/kos/replication/result.log.*");
                                logs.Execute();
                                SshExeTest.WriteLogFile("1", "read_RPA_logs", "after_execute");
                                switch (file_name)
                                {
                                    case "rreasons.log":
                                        {
                                            SshExeTest.WriteLogFile("1", "read_RPA_logs", "rreasons.log"); 
                                            using (System.IO.StringReader sr = new System.IO.StringReader(logs.Result))
                                            {
                                                string line;
                                                //  string linesss;
                                                if (!current)
                                                {
                                                    while ((line = sr.ReadLine()) != null)
                                                    {
                                                        SshExeTest.WriteLogFile("1", "read_RPA_logs", line);
                                                        SshExeTest.readToDB_Rreasons(line, host);

                                                    }
                                                }
                                                else
                                                {
                                                    while ((line = sr.ReadLine()) != null)
                                                    {
                                                        SshExeTest.readToDB_Rreasons(line, host, MaxTime);

                                                    }

                                                }
                                            }
                                           
                                            break;
                                        }

                                    //filter fapi file 
                                    case "core.txt":
                                        {
                                            SshExeTest.WriteLogFile("1", "read_RPA_logs", "core.txt"); 
                                            using (System.IO.StringReader sr = new System.IO.StringReader(logs.Result))
                                            {
                                                string line;
                                                //  string linesss;
                                                if (!current)
                                                {
                                                    while ((line = sr.ReadLine()) != null)
                                                    {
                                                        SshExeTest.readToDB_core(line, host);

                                                    }
                                                }
                                                else
                                                {
                                                    while ((line = sr.ReadLine()) != null)
                                                    {
                                                        SshExeTest.readToDB_core(line, host,MaxTime);

                                                    }

                                                }
                                            }
                                           
                                            break;
                                        }
                                    case "installationLogs":
                                        {
                                            SshExeTest.WriteLogFile("1", "read_RPA_logs", "installationLogs"); 
                                         /////////////   using (System.IO.StringReader sr = new System.IO.StringReader(logs.Result))
                                         ////////////   {
                                           //////////     string line;
                                                //  string linesss;
                                           //////////     while ((line = sr.ReadLine()) != null)
                                           ////////////////     {
                                            if (!current)
                                            {
                                                SshExeTest.readToDB_All(logs.Result, file_name, host, "yyyy-MM-dd HH:mm:ss");
                                            }
                                            else
                                            {
                                                SshExeTest.readToDB_All(logs.Result, file_name, host,MaxTime, "yyyy-MM-dd HH:mm:ss");
                                            }

                                          ////////////      }
                                          ////////  }
                                           
                                         break;
                                        }

                                    //The file that use default are :mirror.txt,replication.txt,management.txt,control.txt,storage.txt,connectivity_tool.txt,client.txt,hlr.txt,connectors.txt,klr.txt
                                    default:

                                   
                                        {
                                            SshExeTest.WriteLogFile("1", "read_RPA_logs", "default"); 
                                            //   parsing_file = "readToDB_All";
                                        //////////////   using (System.IO.StringReader sr = new System.IO.StringReader(logs.Result))
                                          ////////  {
                                                ////////////////string line;
                                                //  string linesss;
                                               ////////////// while ((line = sr.ReadLine()) != null)
                                               ///////////// {
                                               ///////////////     SshExeTest.readToDB_All(line, file_name, host);
                                            if (!current)
                                            {
                                                SshExeTest.readToDB_All(logs.Result, file_name, host);
                                            }
                                            else
                                            {
                                                SshExeTest.readToDB_All(logs.Result, file_name, host,MaxTime);
                                            }
                                             /////////   }
                                           //////// }
                                            
                                            break;
                                        }



                                }



                                /*                using (System.IO.StringReader sr = new System.IO.StringReader(logs.Result))
                                                {
                                                    string line;
                                                    //  string linesss;
                                                    while ((line = sr.ReadLine()) != null)
                                                    {
                                                        System.IO.File.WriteAllText(@"C:\Users\slonis\WriteText.txt", line);
                                                        // process line here
                                                    }
                                                }*/



                            }

                            var logFapi = client.RunCommand(fapiGrepString);

                            logFapi.Execute();
                          //////////////  using (System.IO.StringReader sr = new System.IO.StringReader(logFapi.Result))
                          /////////////  {
                            ///////////    string line;
                                //  string linesss;
                            ///////////////    while ((line = sr.ReadLine()) != null)
                            //////////////    {
                            if (logFapi.Result !="")
                                    SshExeTest.readToDB_FAPI(logFapi.Result, host);

                              /////////  }
                           ///////////////// }




                            client.Disconnect();
                        } //end try


                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection Error:" + ex.Message.ToString());

                        }

                        finally
                        {
                            client.Disconnect();

                        }
                    }
            
        }

 
        public static void read_RPA_SPLITER(string SpliterType , string host, string username, string password, int port)
        {
            DataTable all_row = DataBaseFun.select_all("log_Splitter", SpliterType,"");

            for (int i = 0; i < all_row.Rows.Count; i++)
            {
                //     var yyyy = client.RunCommand("ls -la| grep history");
                //     yyyy.Execute();
                //     Func<string> fn = all_row.Rows[i].ItemArray[0].ToString();
                string grep_string = all_row.Rows[i].ItemArray[0].ToString();
                string file_name = all_row.Rows[i].ItemArray[1].ToString();
                switch (SpliterType)
                {
                    case "ESX":
                        {
                            execute_ssh_command(grep_string, "ESX", host, username, password, port);
                            //    DataTable all_row = DataBaseFun.select_all("log_SPLITTER", SpliterType);
                        }

                        break;
                    case "Vplex":


                        break;
                    case "CX":

                        break;
                    case "Smetrix":

                        break;
                    default:
                        break;
                }
            }
          }

        public static void execute_ssh_command(string grep_string, string SpliterType, string host, string username, string password, int port)
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
                try
                {
                    // your code 


                    client.Connect();
                   
                    var logs = client.RunCommand(grep_string);

                    //  var logs = client.RunCommand("zgrep -E 'HERE|Assertion|DLManager: deadlock suspected|moves to bitmap|highLoadDiskManagerSpace|dirtify volumes|dirtifyVolume|dirtify all:' /home/kos/replication/result.log.*");
                    logs.Execute();
                    //DataTable all_row =
                    SshExeTest.readToDB_ESX_Lines(logs.Result, SpliterType, host);
                }
                //  SshExeTest.readToDB_All(
                catch (AggregateException e)
                {

                            MessageBox.Show("Connection Error:" + e.Message.ToString());
                            SshExeTest.WriteLogFile("1", "splitter", "Error: The collect log is to long. \n{0}" + e.Message);


                }
                finally
                {
                    client.Disconnect();

                }

              

            }

        }




    }
}
