using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class SetupHelper
    {

        public static void RemoveVacationCalendar(ClientContext ctx)
        {
            if (ctx.Web.ListExists("Vacation Calendar"))
            {
                ctx.Web.GetListByTitle("Vacation Calendar").DeleteObject();
                ctx.ExecuteQuery();
            }
        }

        public static void CreateVacationCalendar(ClientContext ctx)
        {

            XMLFileSystemTemplateProvider provider = new XMLFileSystemTemplateProvider(@"C:\Users\david\source\repos\SP2017\OfficeDev1\VacationCalendarAddin\Common\XML", "");
            string templateName = "VacationCalendar.xml";
            ProvisioningTemplate template = provider.GetTemplate(templateName);
            ctx.Web.ApplyProvisioningTemplate(template);

        }

        public static void FixViewForCalendar(ClientContext ctx)
        {
            List list = ctx.Web.GetListByTitle("Vacation Calendar");

            ctx.Load(list.DefaultView);
            ctx.ExecuteQuery();

            list.DefaultView.ViewFields.Add("VCEmployee");
            list.DefaultView.ViewFields.Add("VCStartDate");
            list.DefaultView.ViewFields.Add("VCEndDate");
            list.DefaultView.ViewFields.Add("VCReason");
            list.DefaultView.ViewFields.Add("VCApporoved");
            list.DefaultView.ViewFields.Add("VCComments");

            list.DefaultView.Update();

            ctx.ExecuteQuery();


        }

        public static void AddCustomActionToVacationCalendarList(ClientContext ctx)
        {
            ctx.Load(ctx.Web, w => w.Url);
            ctx.ExecuteQuery();

           List list = ctx.Web.GetListByTitle("Vacation Calendar");

          UserCustomAction action=  list.UserCustomActions.Add();
            action.Name = "vacapproval";
            action.Location = "EditControlBlock";
            action.Title = "Approve or reject request";
            action.Sequence = 10;
             action.Url = "https://localhost:44335/home/index?SPHostUrl=" + ctx.Web.Url + "&listid={ListId}&ItemId={ItemId}";

            action.Update();

            ctx.ExecuteQuery();
        }



    }
}
