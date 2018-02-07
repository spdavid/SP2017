using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListAndAddItemsProjectWeb.Controllers
{
    public class ShowWebTitleController : Controller
    {
        public ActionResult Index()
        {

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();

                ViewBag.WebTitle = ctx.Web.Title;


            }
        
                return View();
        }
    }
}
