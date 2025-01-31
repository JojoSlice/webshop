using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Helpers
{
    public static class Account
    {
        public static int LoggedinID { get; private set; }
        public static bool Loggedin { get; private set; }
        public static bool Admin { get; private set; }

        public static void UpdateStatus(int id, bool isAdmin)
        {
            LoggedinID = id;
            Loggedin = true;
            Admin = isAdmin;
        }

        public static void ResetStatus()
        {
            LoggedinID = 0;
            Loggedin = false;
            Admin = false;
            Console.WriteLine("Have a nice day!");
        }
    }
}
