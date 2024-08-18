using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;
using System.Threading;
using System.Collections;
using System.IO;
using ssh_to_DB.WebReference;

namespace ssh_to_DB
{
    static class Main_connect
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            SshExeTest.RunExample();
        //    consistencyGroupSettings ddd = ssh_to_DB.WebReference.getAllGroupSets;
        }
    }
}