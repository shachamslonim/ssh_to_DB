using System;
using System.Collections;

using System.Threading;
using System.IO;

namespace ssh_to_DB
{
    /// <summary>
    /// Summary description for Util.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// Get input from the user
        /// </summary>
        public static SshConnectionInfo GetInput(string host ,string UserName , string password_string)
        {
            Console.OpenStandardOutput(30);
            SshConnectionInfo info = new SshConnectionInfo();
            Console.Write("Enter Remote Host: ");
            //info.Host = "10.76.7.136";
            info.Host = host;
            Console.Write("Enter Username: ");
            //info.User = "admin";
            info.User = UserName;
            Console.Write("Use publickey authentication? [Yes|No] :");
            string resp = "NO";
            if (resp.ToLower().StartsWith("y"))
            {
                Console.Write("Enter identity key filename: ");
                info.IdentityFile = Console.ReadLine();
            }
            else
            {
                Console.Write("Enter Password: ");
                //
                //info.Pass = "password";
                info.Pass = password_string;
            }
            Console.WriteLine();
            return info;
        }
    }

    public struct SshConnectionInfo
    {
        public string Host;
        public string User;
        public string Pass;
        public string IdentityFile;
    }
}