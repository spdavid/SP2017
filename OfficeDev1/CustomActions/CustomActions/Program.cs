using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;

using OfficeDevPnP.Core.Entities;

namespace CustomActions
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ClientContext ctx = Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/DavidClassic"))
            {
                CustomActionEntity custAction = new CustomActionEntity();
                custAction.Name = "binklink";
                custAction.Sequence = 1008;
                custAction.Url = "https://bing.com";
                custAction.Group = "SiteActions";
                custAction.Location = "Microsoft.SharePoint.StandardMenu";
                custAction.Description = "bing link";
                custAction.Title = "Bing";

                ctx.Site.AddCustomAction(custAction);


                List list = ctx.Web.GetListByTitle("Documents");

                ctx.Load(list.UserCustomActions);
                ctx.ExecuteQuery();

                foreach (var action in list.UserCustomActions)
                {
                    if (action.Name == "customname")
                    {
                        action.DeleteObject();
                        ctx.ExecuteQuery();
                    }
                }

                
               UserCustomAction listAction = list.UserCustomActions.Add();
                listAction.Location = "EditControlBlock";
                listAction.Name = "customname";
                listAction.Title = "Do Something 2";
                listAction.Url = "https://fakeurl?listid={ListId}&ItemId={ItemId}";
                listAction.Sequence = 5;
                listAction.Update();

                ctx.ExecuteQuery();

            }



        }
    }
}
