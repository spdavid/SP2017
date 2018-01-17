using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginningWithPnP.CodeExaples
{
    public class PnP101
    {
        public static void CreateAList(ClientContext ctx, string Listname)
        {
            if (!ctx.Web.ListExists(Listname))
            {
                ctx.Web.CreateList(ListTemplateType.DocumentLibrary, Listname, false, true);
            }
            else
            {
                Console.WriteLine("List exists");
            }
        }

        public static void CreateTaskList(ClientContext ctx)
        {
            // ctx.Web.CreateList(ListTemplateType.Tasks, "pnptasklist", false, true, "lists/mytasks2");
             ctx.Web.CreateList(ListTemplateType.Tasks, "pnptasklist", true, urlPath:"lists/mytasks2");

        }

        public static void AddToLeftNav(ClientContext ctx)
        {
            ctx.Web.AddNavigationNode("Bing", new Uri("https://www.bing.com"), "Google", OfficeDevPnP.Core.Enums.NavigationType.QuickLaunch);
        }

        public static void CreateSubSite(ClientContext ctx)
        {
            ctx.Web.CreateWeb("Davids Sub Web", "davidsweb", "web for david", "STS#0", 1033);
        }

        public static void AddSharePointGroup(ClientContext ctx)
        {
            Group group = null;
            if (ctx.Web.GroupExists("Davids Group"))
            {
                group = ctx.Web.SiteGroups.GetByName("Davids Group");
                ctx.Load(group);
                ctx.ExecuteQuery();
            }
            else
            {
                group = ctx.Web.AddGroup("Davids Group", "", true);
            }

            ctx.Web.AddPermissionLevelToGroup("Davids Group", RoleType.Reader);

            ctx.Web.AddUserToGroup(group, "Luis@folkis2017.onmicrosoft.com");


            ctx.Web.AddPermissionLevelToUser("anna@folkis2017.onmicrosoft.com", RoleType.Contributor);
        }

        public static void AddFolderToDocLibrary(ClientContext ctx)
        {
            //List list = ctx.Web.GetListByTitle("Documents");
            List list = ctx.Web.Lists.GetByTitle("Documents");
            list.RootFolder.CreateFolder("Folder 2");
        }




        }
}
