﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstNameLastNameAssignmentWeb.Helpers
{
    public class NameHelper
    {
        public static void Install(ClientContext ctx)
        {
          List list = ctx.Web.GetListByTitle("Person");

            list.AddRemoteEventReceiver("added", GetWebServiceUrl(), EventReceiverType.ItemAdded, EventReceiverSynchronization.Synchronous, true);
            list.AddRemoteEventReceiver("updated", GetWebServiceUrl(), EventReceiverType.ItemUpdated, EventReceiverSynchronization.Synchronous, true);

        }

        public static void UnInstall(ClientContext ctx)
        {
            List list = ctx.Web.GetListByTitle("Person");
            list.GetEventReceiverByName("added").DeleteObject();
            list.GetEventReceiverByName("updated").DeleteObject();

            ctx.ExecuteQuery();
        }

            private static string GetWebServiceUrl()
        {
            System.ServiceModel.OperationContext op = System.ServiceModel.OperationContext.Current;
            System.ServiceModel.Channels.Message msg = op.RequestContext.RequestMessage;
            Uri url = msg.Headers.To;
            return url.ToString();
        }

        public static void UpdateLastName(int ListItemId, string ListTitle, ClientContext ctx)
        {

       

            List list = ctx.Web.GetListByTitle(ListTitle);
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