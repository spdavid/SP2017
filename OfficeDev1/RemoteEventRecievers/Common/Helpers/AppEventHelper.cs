using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class AppEventHelper
    {
        public static void HandleInstalledEvent(ClientContext ctx)
        {
            ctx.Web.CreateList(ListTemplateType.Events, "Vacation Calender", false);
        }

        public static void HandleUnInstalledEvent(ClientContext ctx)
        {
            List list = ctx.Web.GetListByTitle("Vacation Calender");
            list.DeleteObject();
            ctx.ExecuteQuery();
        }



    }
}
