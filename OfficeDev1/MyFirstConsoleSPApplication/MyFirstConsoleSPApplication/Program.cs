using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Security;

namespace MyFirstConsoleSPApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ClientContext ctx = Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/David"))
            {
                CodeExamples.CSOM101.GetWebTitle(ctx);
            }

                Console.WriteLine("press enter to continue");
            Console.ReadLine();

        }

       
    }
}
