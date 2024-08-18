using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssh_to_DB
{
    class UserConnect
    {


        public struct UserConectClass
    {

            public string ip;
            public string type;
            public string userName;
            public string password;    

    }


        public static UserConectClass setConnect(String ipConnect, String types, String User, String Pass)
        {
            UserConectClass connectInfo = new UserConectClass();
            connectInfo.ip = ipConnect;
            connectInfo.type=types;
            connectInfo.userName=User;
            connectInfo.password = Pass;
            return connectInfo;

        }

        public static UserConectClass setConnect(String line)
        {
            string[] words = line.Split(';');
            UserConectClass connectInfo = new UserConectClass();
            connectInfo.ip = words[0];
            connectInfo.type = words[1];
            connectInfo.userName = words[2];
            connectInfo.password = words[3];
             return connectInfo;
        }


        

    }

}
