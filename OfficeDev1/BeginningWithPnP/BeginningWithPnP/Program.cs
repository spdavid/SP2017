using BeginningWithPnP.CodeExaples;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginningWithPnP
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ClientContext ctx = Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/David"))
            {
                //PnP101.CreateAList(ctx, "Davids pnp List");
                //PnP101.CreateTaskList(ctx);
                // PnP101.AddToLeftNav(ctx);
                // PnP101.CreateSubSite(ctx);
              //  PnP101.AddSharePointGroup(ctx);
                PnP101.AddFolderToDocLibrary(ctx);
            }

            Console.WriteLine("Press enter");
            Console.ReadLine();
        }
    }
}
