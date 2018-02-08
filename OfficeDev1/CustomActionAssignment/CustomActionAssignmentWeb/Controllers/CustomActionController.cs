using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;

namespace CustomActionAssignmentWeb.Controllers
{
    public class CustomActionController : Controller
    {
        // GET: CustomAction
        [SharePointContextFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string custactionType)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {

               Uri appUrl = Request.Url;

                List list =  ctx.Web.GetListByTitle("DavidsList");

                if (custactionType == "Remove")
                {
                    ctx.Load(list.UserCustomActions);
                    ctx.ExecuteQuery();

                    foreach (var a in list.UserCustomActions)
                    {
                        if (a.Name == "CustomName")
                        {
                            a.DeleteObject();
                            ctx.ExecuteQuery();
                        }
                    }
                }
                else
                {

                    UserCustomAction action = list.UserCustomActions.Add();
                    action.Title = "View item in addin";
                    action.Name = "CustomName";
                    action.Url = appUrl.Scheme + "://" + appUrl.Authority + "/CustomAction/ReadFromSharepoint" + appUrl.Query + "&ListId={ListId}&ListItemId={ItemId}";
                    action.Location = "EditControlBlock";
                    action.Sequence = 1;
                    action.Update();
                    ctx.ExecuteQuery();

                }

            }

                return View();
        }

        [SharePointContextFilter]
        public ActionResult ReadFromSharepoint(string ListId, int ListItemId)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {
              List list =  ctx.Web.GetListById(ListId.ToGuid());
                ListItem item = list.GetItemById(ListItemId);
                ctx.Load(item);
                ctx.ExecuteQuery();

                ViewBag.ListItemTitle = item["Title"].ToString();

            }
                return View();
        }
    }
}