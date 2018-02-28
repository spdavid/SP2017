using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorAddIn.RemoteEventsWeb.Helpers
{
    public class AppEventsHelper
    {

        public static void Install(ClientContext ctx)
        {
            // add remove event to our list
            List list =  ctx.Web.GetListByTitle("Calculator");
            string url = GetWebServiceUrl();

            list.AddRemoteEventReceiver("added4", url, EventReceiverType.ItemAdded, EventReceiverSynchronization.Synchronous, true);
            list.AddRemoteEventReceiver("updated4", url, EventReceiverType.ItemUpdated, EventReceiverSynchronization.Synchronous, true);
            list.AddRemoteEventReceiver("updating4", url, EventReceiverType.ItemUpdating, EventReceiverSynchronization.Synchronous, true);
            list.AddRemoteEventReceiver("ading4", url, EventReceiverType.ItemAdding, EventReceiverSynchronization.Synchronous, true);


        }
        public static void UnInstall(ClientContext ctx)
        {
            //  remove our remote event. 
            List list = ctx.Web.GetListByTitle("Calculator");
            list.GetEventReceiverByName("added4").DeleteObject();
            list.GetEventReceiverByName("updated4").DeleteObject();
            list.GetEventReceiverByName("updating4").DeleteObject();
            list.GetEventReceiverByName("ading4").DeleteObject();


            ctx.ExecuteQuery();
        }

        private static string GetWebServiceUrl()
        {
            System.ServiceModel.OperationContext op = System.ServiceModel.OperationContext.Current;
            System.ServiceModel.Channels.Message msg = op.RequestContext.RequestMessage;
            Uri url = msg.Headers.To;
            return url.ToString();
        }

    }
}