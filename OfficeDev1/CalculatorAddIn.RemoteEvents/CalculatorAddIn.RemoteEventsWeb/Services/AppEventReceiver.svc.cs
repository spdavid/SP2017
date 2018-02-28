using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;

namespace CalculatorAddIn.RemoteEventsWeb.Services
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

            // remove event recievers

            if (properties.EventType == SPRemoteEventType.ItemAdded ||
                properties.EventType == SPRemoteEventType.ItemUpdated)
            {
                // use CreateRemoteEventReceiverClientContext for remove events. 
                using (ClientContext ctx = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
                {
                    Helpers.CaclulatorHelper.DoCalcuation(properties.ItemEventProperties.ListItemId, ctx);


                }

            }


            if (properties.EventType == SPRemoteEventType.ItemAdding || properties.EventType == SPRemoteEventType.ItemUpdating)
            {
                // use CreateRemoteEventReceiverClientContext for remove events. 
                using (ClientContext ctx = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
                {
                  bool IsValid =  Helpers.CaclulatorHelper.ValidateCalculation(properties.ItemEventProperties.ListItemId, properties.ItemEventProperties.AfterProperties, ctx);

                    if(!IsValid)
                    {
                        result.ErrorMessage = "You cannot devide by zero";
                        result.Status = SPRemoteEventServiceStatus.CancelWithError;
                        
                    }

                }

            }




            // app events 
            using (ClientContext ctx = TokenHelper.CreateAppEventClientContext(properties, useAppWeb: false))
            {
                if (ctx != null)
                {
                    if (properties.EventType == SPRemoteEventType.AppInstalled)
                    {
                        Helpers.AppEventsHelper.Install(ctx);
                    }
                    if (properties.EventType == SPRemoteEventType.AppUninstalling)
                    {
                        Helpers.AppEventsHelper.UnInstall(ctx);
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
