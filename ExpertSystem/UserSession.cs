using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    class UserSession
    {
        private static bool login = false;
        private static int userId;
        public static bool Login()
        {
            return login;
        }

        public static bool SetUser(int u)
        {
            if (login) return false;
            else
            {
                userId = u;
                return true;
            }
        }

        public static void Out()
        {
            login = false;
            userId =-1;
        }
        
    }
}
