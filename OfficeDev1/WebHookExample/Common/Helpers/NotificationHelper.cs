using Common.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
   public  class NotificationHelper
    {

        public static string ProcessNotification(NotificationModel notif)
        {

            string returnvalue = "";

            using (ClientContext ctx = ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/DavidClassic"))
            {
              List list =  ctx.Web.GetListById(notif.Resource.ToGuid());

                ChangeQuery query = new ChangeQuery(false, true);
                query.Item = true;
                query.ChangeTokenStart = null;
                query.ChangeTokenEnd = list.CurrentChangeToken;

                string changeTocken = ctx.Web.GetPropertyBagValueString("lastchange", null);
                if (changeTocken != null)
                {
                    ChangeToken ct = new ChangeToken();
                    ct.StringValue = changeTocken;
                    query.ChangeTokenStart = ct;

                }

                ChangeCollection changes = list.GetChanges(query);

                ctx.Load(changes);
                ctx.ExecuteQuery();

                // at this point you should find what change you want
                // and put the id of the item into another queue and
                // user another function to deal with it. 
                foreach (ChangeItem c in changes)
                {
                    switch (c.ChangeType)
                    {
                      
                        case ChangeType.Add:
                            returnvalue += ("item was added");
                            returnvalue += (c.ItemId.ToString());

                            break;
                        case ChangeType.Update:
                            returnvalue += ("item was updated");
                            returnvalue += (c.ItemId.ToString());
                            break;
                        case ChangeType.DeleteObject:
                            returnvalue += ("item was deleted");
                            returnvalue += (c.ItemId.ToString());
                            break;
                       
                        default:
                            break;
                    }
                    Console.Out.WriteLine();

                }

                ctx.Web.SetPropertyBagValue("lastchange", list.CurrentChangeToken.StringValue);


            }

            return returnvalue;
        }

    }
}
