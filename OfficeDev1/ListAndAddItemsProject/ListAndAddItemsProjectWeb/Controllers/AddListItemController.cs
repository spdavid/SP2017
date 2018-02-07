using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListAndAddItemsProjectWeb.Controllers
{
    public class AddListItemController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index(string SPHostUrl)
        {
            ViewBag.SPHostUrl = SPHostUrl;


            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (ClientContext ctx = spContext.CreateUserClientContextForSPHost())
            {
               List list = ctx.Web.GetListByTitle("ExampleList");
                ListItemCollection items = list.GetItems(CamlQuery.CreateAllItemsQuery());
                ctx.Load(items);
                ctx.ExecuteQuery();
                return View(items);
            }
          
        }

        [HttpPost]
        public ActionResult Index(string NewListItemTitle, string SPHostUrl)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (ClientContext ctx = spContext.CreateUserClientContextForSPHost())
            {
               ListItem item = ctx.Web.GetListByTitle("ExampleList").AddItem(new ListItemCreationInformation());
                item["Title"] = NewListItemTitle;
                item.Update();
                ctx.ExecuteQuery();


        

            }

          return RedirectToAction("Index", new { SPHostUrl = SPHostUrl, foo="wassap" });
        }

        [HttpPost]
        public ActionResult DeleteItem(int itemId, string SPHostUrl)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (ClientContext ctx = spContext.CreateUserClientContextForSPHost())
            {
                ListItem item = ctx.Web.GetListByTitle("ExampleList").GetItemById(itemId);
                item.DeleteObject();
                ctx.ExecuteQuery();
            }

            return RedirectToAction("Index", new { SPHostUrl = SPHostUrl });
        }

    }
}