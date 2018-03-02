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
                using (ClientContext ctx = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
                {
                    if (ctx != null)
                    {
                        Helpers.NameHelper.UpdateLastName(properties.ItemEventProperties.ListItemId, properties.ItemEventProperties.ListTitle, ctx);
                    }
                }
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
                            Helpers.NameHelper.Install(ctx);
                        }
                        else
                        {
                            Helpers.NameHelper.UnInstall(ctx);

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

    }
}
