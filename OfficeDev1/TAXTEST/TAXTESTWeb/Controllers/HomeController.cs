using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client.Taxonomy;

namespace TAXTESTWeb.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            User spUser = null;

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {
                if (ctx != null)
                {

                   
                  List list=  ctx.Web.GetListByTitle("TaxonomyTest");

                  ListItem item =  list.GetItemById(2);
                    ctx.Load(item, i => i["TAXTEST"]);
                    ctx.ExecuteQuery();

                    ViewBag.Items = item["TAXTEST"] as TaxonomyFieldValueCollection;

                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
