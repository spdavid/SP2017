using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BeginningWithPnP.Helpers
{
    public class ContextHelper
    {
        public static ClientContext GetClientContext(string url)
        {
            // get the context of your site. 
            ClientContext context = new ClientContext(url);

            // ask user for password and put it in a secure string
            string password = GetPassword();

            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

           string loginName = ConfigurationManager.AppSettings["loginName"];

            // give your context credentials
            context.Credentials = new SharePointOnlineCredentials(loginName, securePassword);

            return context;
        }


        public static string GetPassword()
        {
            string pass = "";
            Console.Write("Enter your password: ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    Console.Write("\b");
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass.TrimEnd("\r".ToCharArray());
        }
    }
}
