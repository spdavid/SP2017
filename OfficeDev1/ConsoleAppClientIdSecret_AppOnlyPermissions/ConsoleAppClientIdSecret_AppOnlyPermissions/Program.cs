using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppClientIdSecret_AppOnlyPermissions
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://folkis2017.sharepoint.com/sites/david";
            // get settings from your app.config file
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];


            AuthenticationManager authManager = new AuthenticationManager();
            using (ClientContext ctx = authManager.GetAppOnlyAuthenticatedContext(url, clientId, clientSecret))
            {
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();

                Console.WriteLine(ctx.Web.Title);

              if (!ctx.Web.ListExists("FromApp"))
                {
                    ctx.Web.CreateList(ListTemplateType.GenericList, "FromApp", false);

                }
                List list = ctx.Web.GetListByTitle("FromApp");

                // this will have createdby as SharePoint app
                 ListItem item = list.AddItem(new ListItemCreationInformation());
                item["Title"] = "App made this";
                item.Update();
                ctx.ExecuteQuery();

               User user = ctx.Web.EnsureUser("david@folkis2017.onmicrosoft.com");
                ctx.Load(user, u => u.Id);
                ctx.ExecuteQuery();

                ListItem item2 = list.AddItem(new ListItemCreationInformation());
                item2["Title"] = "App made this but with user";
                item2["Author"] = user.Id;
                item2["Editor"] = user.Id;
                item2.Update();
                ctx.ExecuteQuery();



            }


            Console.WriteLine("press enter to continue");
            Console.ReadLine();

        }
    }
}
