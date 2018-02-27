using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Microsoft.SharePoint.Client.UserProfiles;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://folkis2017.sharepoint.com/sites/davidClassic";
            // get settings from your app.config file
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["Secret"];


            AuthenticationManager authManager = new AuthenticationManager();
            using (ClientContext ctx = authManager.GetAppOnlyAuthenticatedContext(url, clientId, clientSecret))
            {
                User user = ctx.Web.EnsureUser("David@folkis2017.onmicrosoft.com");
                ctx.Load(ctx.Web);
                ctx.Load(user);
                ctx.ExecuteQuery();
                //Console.WriteLine(ctx.Web.Title);
                //SetupHelper.CreateVacationCalendar(ctx);
                //SetupHelper.FixViewForCalendar(ctx);
                //SetupHelper.AddCustomActionToVacationCalendarList(ctx);

                SetupHelper.RemoveVacationCalendar(ctx);
                //SetupHelper.Install(ctx);

                //  UserProfileHelper.IsCurrentUserManagerOf(ctx, user, "Luis@folkis2017.onmicrosoft.com");


                //PeopleManager peopleManager = new PeopleManager(ctx);
                //PersonProperties personProperties = peopleManager.GetPropertiesFor(user.LoginName);
                //ctx.Load(personProperties);
                //ctx.ExecuteQuery();

                //var profileProps = personProperties.UserProfileProperties;
                //foreach (var key in profileProps.Keys)
                //{
                //    Console.WriteLine(key + " : " + profileProps[key]);
                //}



            }


            Console.WriteLine("press enter");
            Console.ReadLine();
        }
    }


}
