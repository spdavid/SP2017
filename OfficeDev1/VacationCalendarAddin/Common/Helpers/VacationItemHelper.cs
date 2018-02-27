using Common.Models;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
   public class VacationItemHelper
    {

        public static VacationRequest GetRequestFromSharePoint(Guid listId, int ItemId, ClientContext ctx)
        {
            // get the vacation calendar list
            List list = ctx.Web.GetListById(listId);

            ListItem item = list.GetItemById(ItemId);
            ctx.Load(item);
            ctx.ExecuteQuery();
            VacationRequest request = GetRequestFromItem(item);

            return request;


        }

        private static VacationRequest GetRequestFromItem(ListItem item)
        {
            VacationRequest request = new VacationRequest();
            request.Id = item.Id;
            request.RequestTitle = item["Title"].ToString();
            request.StartDate = DateTime.Parse(item["VCStartDate"].ToString());
            request.EndDate = DateTime.Parse(item["VCEndDate"].ToString());
            FieldUserValue epmloyee = item["VCEmployee"] as FieldUserValue;
            request.EmployeeName = epmloyee.LookupValue;

            TaxonomyFieldValue reason = item["VCReason"] as TaxonomyFieldValue;
            request.ReasonForAbsense = reason.Label;

            if (item["VCComments"] != null)
            {
                request.Comments = System.Web.HttpUtility.HtmlDecode(item["VCComments"].ToString());
            }

            if (item["VCApporoved"] != null)
            {
                request.ApprovalStatus = item["VCApporoved"].ToString();
            }

            return request;
        }

        public static void ApproveRejectRequest(Guid listId, int ItemId, ClientContext ctx, string comments, bool isApproved)
        {
            // get the vacation calendar list
            List list = ctx.Web.GetListById(listId);

            ListItem item = list.GetItemById(ItemId);
            ctx.Load(item);
            ctx.ExecuteQuery();

            item["VCComments"] = comments;

            if (isApproved)
            {
                item["VCApporoved"] = "Approved";
            }
            else
            {
                item["VCApporoved"] = "Denied";

            }

            item.Update();

            ctx.ExecuteQuery();



        }


        public static List<VacationRequest> GetAbsentToday(ClientContext ctx)
        {
            List list = ctx.Web.GetListByTitle("Vacation Calendar");
            CamlQuery query = new CamlQuery();
            query.ViewXml =
                  @"<View>  
                        <Query> 
                           <Where><And><And><Leq><FieldRef Name='VCStartDate' /><Value Type='DateTime'><Today /></Value></Leq><Geq><FieldRef Name='VCEndDate' /><Value Type='DateTime'><Today /></Value></Geq></And><Eq><FieldRef Name='VCApporoved' /><Value Type='Choice'>Approved</Value></Eq></And></Where><OrderBy><FieldRef Name='Title' /></OrderBy> 
                        </Query> 
                         <ViewFields><FieldRef Name='VCComments' /><FieldRef Name='Title' /><FieldRef Name='VCStartDate' /><FieldRef Name='VCEndDate' /><FieldRef Name='VCReason' /><FieldRef Name='VCEmployee' /><FieldRef Name='VCApporoved' /></ViewFields> 
                  </View>";
          ListItemCollection items =  list.GetItems(query);
            ctx.Load(items);
            ctx.ExecuteQuery();

            List<VacationRequest> requests = new List<VacationRequest>();

            foreach (ListItem item in items)
            {
                VacationRequest request = GetRequestFromItem(item);
                requests.Add(request);
            }

            return requests;

        }




    }
}
