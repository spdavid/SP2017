using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;

namespace FirstNameLastNameAssignmentWeb.Services
{
    public class AppEventReceiver : IRemoteEventService
    {
        /// <summary>
        /// Handles app events that occur after the app is installed or upgraded, or when app is being uninstalled.
        /// </summary>
        /// <param name="properties">Holds information about the app event.</param>
        /// <returns>Holds information returned from the app event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();

            if (properties.EventType == SPRemoteEventType.ItemUpdated || properties.EventType == SPRemoteEventType.ItemAdded)
            {
                // other remote events will use 

                        Common.Helpers.QueueHelper.AddToQueue("changename", properties.ItemEventProperties.WebUrl + "," + properties.ItemEventProperties.ListItemId);

                
            }


                    // on app events use TokenHelper.CreateAppEventClientContext
                    if (properties.EventType == SPRemoteEventType.AppInstalled || properties.EventType == SPRemoteEventType.AppUninstalling)
            {
                using (ClientContext ctx = TokenHelper.CreateAppEventClientContext(properties, useAppWeb: false))
                {
                    if (ctx != null)
                    {
                        if (properties.EventType == SPRemoteEventType.AppInstalled)
                        {
                            Common.Helpers.QueueHelper.AddToQueue("install", properties.AppEventProperties.HostWebFullUrl.ToString() + "," + GetWebServiceUrl());
                        }
                        else
                        {
                            Common.Helpers.QueueHelper.AddToQueue("uninstall", properties.AppEventProperties.HostWebFullUrl.ToString());

                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This method is a required placeholder, but is not used by app events.
        /// </summary>
        /// <param name="properties">Unused.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
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
