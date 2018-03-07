using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Helpers
{
    public class NameHelper
    {
        public static void Install(string url, string webserviceurl)
        {
            ClientContext ctx = ContextHelper.GetClientContext(url);
          List list = ctx.Web.GetListByTitle("Person");

            list.AddRemoteEventReceiver("added2", webserviceurl, EventReceiverType.ItemAdded, EventReceiverSynchronization.Synchronous, true);
            list.AddRemoteEventReceiver("updated2", webserviceurl, EventReceiverType.ItemUpdated, EventReceiverSynchronization.Synchronous, true);

        }

        public static void UnInstall(string url)
        {
            ClientContext ctx = ContextHelper.GetClientContext(url);

            List list = ctx.Web.GetListByTitle("Person");
            list.GetEventReceiverByName("added2").DeleteObject();
            list.GetEventReceiverByName("updated2").DeleteObject();

            ctx.ExecuteQuery();
        }

            private static string GetWebServiceUrl()
        {
            System.ServiceModel.OperationContext op = System.ServiceModel.OperationContext.Current;
            System.ServiceModel.Channels.Message msg = op.RequestContext.RequestMessage;
            Uri url = msg.Headers.To;
            return url.ToString();
        }

        public static void UpdateLastName(int ListItemId, string webUrl)
        {
            ClientContext ctx = ContextHelper.GetClientContext(webUrl);

            List list = ctx.Web.GetListByTitle("Person");
            ListItem item = list.GetItemById(ListItemId);
            ctx.Load(item);
            ctx.ExecuteQuery();

            string FirstName = item["Title"].ToString();
            string LastName = item["persLName"].ToString();
            string FullName = FirstName + " " + LastName;

            if (item["persFullName"] == null || FullName != item["persFullName"].ToString())
            {
                item["persFullName"] = FullName;
                item.SystemUpdate();
                ctx.ExecuteQuery();
            }
            
        }


    }
}