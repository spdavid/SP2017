using Common.Helpers;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
           // string xmlfile = AppDomain.CurrentDomain.BaseDirectory + @"xml/xmlfile.xml";



            string url = "https://folkis2017.sharepoint.com/sites/david";
            // get settings from your app.config file
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];


            AuthenticationManager authManager = new AuthenticationManager();
            using (ClientContext ctx = authManager.GetAppOnlyAuthenticatedContext(url, clientId, clientSecret))
            {
                AppEventHelper.HandleInstalledEvent(ctx);
                Console.WriteLine("app installed. press enter to uninstall it");
                Console.ReadLine();
                AppEventHelper.HandleUnInstalledEvent(ctx);
            }
        }
    }
}
