using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetChangesExample
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ClientContext ctx = Helpers.ContextHelper.GetContext("https://zalolabs.sharepoint.com/sites/DEMO"))
            {

                ctx.Web.SetPropertyBagValue("davidsprop", "Hello World");
                //ctx.Site.RootWeb.SetPropertyBagValue("davidsprop", "Hello World");
                //ctx.Web.GetPropertyBagValueString("davidsprop", "");

                List list = ctx.Web.GetListByTitle("SiteRequest");
                
                ChangeQuery query = new ChangeQuery(false, false);
                query.Item = true;
                query.Add = true;
                query.Update = true;
                query.DeleteObject = true;
                // setting your own value
                //query.ChangeTokenStart  = new ChangeToken();
                //query.ChangeTokenStart.StringValue = string.Format("1;3;{0};{1};-1", list.Id.ToString(), DateTime.Now.AddMinutes(-5).ToUniversalTime().Ticks.ToString());
                var startChangeToken = list.GetPropertyBagValueString("lastchangetoken", null);
                if (startChangeToken != null)
                {
                    query.ChangeTokenStart = new ChangeToken();
                    query.ChangeTokenStart.StringValue = startChangeToken;
                }

             


                query.ChangeTokenEnd = list.CurrentChangeToken;
                
                ChangeCollection changes = list.GetChanges(query);

                ctx.Load(changes);
                ctx.ExecuteQuery();

                foreach (ChangeItem change in changes)
                {

                    Console.WriteLine(change.ChangeType.ToString());
                    Console.WriteLine(change.ItemId);

                }

                list.SetPropertyBagValue("lastchangetoken", list.CurrentChangeToken.StringValue);
               

                Console.ReadLine();


            }
        }
    }
}
