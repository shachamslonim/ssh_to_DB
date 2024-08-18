using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ssh_to_DB.Fapi;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.ServiceModel;
using ssh_to_DB.Fapi;
using System.Windows.Forms;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Data;
//[System.Web.Services.WebServiceBindingAttribute(Name="urn:ItemManager",Namespace=


namespace ssh_to_DB 
{
    [Serializable]
    class FapiClient //:System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        

        public static bool ValidateRemoteCertificate(
   object sender,
   X509Certificate certificate,
   X509Chain chain,
   SslPolicyErrors policyErrors)
        {
            return true;
        }

        public static void Collect_logs(string ip )
        {
          //  getTrackedEventIDs = Fapi.getEventLogsByUID (latestEventLogUID, maxNumOfEventsToReturn)
            ///SystemEventUID yyy =
            ///
                                 


            Console.WriteLine("FAPI");
            // Expect100Continue line is to avoid the 505 error 
            ServicePointManager.Expect100Continue = false;
            // ServerCertificateValidationCallback is to ignore certificate  
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            // network credentials 
            string url = "https://"+ip +":7225/fapi/version4_3?wsdl";
            NetworkCredential nc = new NetworkCredential("admin", "admin");



            // Create a webrequest with the specified URL. 


           // WebRequest myWebRequest = WebRequest.Create("url");
            // initialization of the service client 
            FunctionalAPIImplService fapi = new FunctionalAPIImplService();
            fapi.Url = url;
            fapi.Credentials = System.Net.CredentialCache.DefaultCredentials;

            fapi.PreAuthenticate = true;
            // Create a webrequest with the specified URL. 
            fapi.Credentials = nc;
         //   fapi.Credentials = nc.GetCredential(new Uri(url), ""); 
            // our server is a bit slow - this gives you the possibility to give it some time
            fapi.Timeout = 800000;




            System.Data.DataTable table_for_DB = new DataTable();
            table_for_DB.Clear();
            table_for_DB.Columns.Add("table");
            table_for_DB.Columns.Add("date");
            table_for_DB.Columns.Add("ComandText");
            table_for_DB.Columns.Add("fileName");
            table_for_DB.Columns.Add("splitter");


            
           
         

            try
            {
                // collect log 
                SystemEventUID fff = new SystemEventUID();
                fff.uniqueId = 0;
         
                EventLog[] all_logs = fapi.getEventLogsByUID(fff, 10000);
                string dddd = all_logs[1].description;
                int event_id = all_logs[1].eventID;
                long ddd = all_logs[1].eventTime.timeInMicroSeconds;

                
                DateTime totototot = UnixTimeStampToDateTime((ddd/1000));

               /// MessageBox.Show(totototot.ToString());


                int milliSecond = 0;
                    List<int> RPA_list = DataBaseFun.select_list_int_GroupFun("SELECT TOP 1 Hours, Minutes, Seconds FROM Time_diff WHERE Type='RPA'; ", 3);
                    if (RPA_list[0] == 0 && RPA_list[1] == 0 && RPA_list[2] <= 3)
                    {
                        DataBaseFun.updateTime(RPA_list[0], RPA_list[1], RPA_list[2], "Activity", "INFO");
                    }

                    milliSecond = RPA_list[0] * 3600000 + RPA_list[1] * 60000 + RPA_list[2] * 1000; 

                for (int r = 0; r <= (all_logs.Length-1) ; r++)
                {
                    table_for_DB.Rows.Add("Activity", UnixTimeStampToDateTime((all_logs[r].eventTime.timeInMicroSeconds / 1000) + milliSecond), all_logs[r].description, all_logs[r].eventID, all_logs[r].eventLogInfo.level);
                }

                DataBaseFun.insert_table(table_for_DB);


            fapi.Timeout = 500000;

   
  }
  catch (NullReferenceException nre)
  {
   MessageBox.Show( "\nCan't call do remov CG,  is null.\n" + nre.Message);
  }
        }
        public static string[] get_uid_name()
        {
            ServicePointManager.Expect100Continue = false;
            // ServerCertificateValidationCallback is to ignore certificate  
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            // network credentials 
            NetworkCredential nc = new NetworkCredential("admin", "admin");
            // initialization of the service client 
            FunctionalAPIImplService fapi = new FunctionalAPIImplService();

            fapi.PreAuthenticate = true;
            fapi.Credentials = nc;
            // our server is a bit slow - this gives you the possibility to give it some time
            fapi.Timeout = 50000;

            string[] internalClusterNames = new string[3] ;


            int start = fapi.getSystemSettings().globalSystemConfiguration.clustersConfigurations.Length;
            for (int i = 0; i < start; i++)
            {
                internalClusterNames[i] = fapi.getSystemSettings().globalSystemConfiguration.clustersConfigurations[i].internalClusterName;
            }


            return  internalClusterNames;

        }
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
        
    }


