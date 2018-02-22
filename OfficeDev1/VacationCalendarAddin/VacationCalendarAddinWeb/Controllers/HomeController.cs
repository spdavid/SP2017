using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Models;
using Common.Helpers;

namespace VacationCalendarAddinWeb.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index(string listid, int? ItemId)
        {
            

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {
                if (ctx != null)
                {
                    VacationRequest request = VacationItemHelper.GetRequestFromSharePoint(listid.ToGuid(), ItemId.Value, ctx);
                    return View(request);
                }
            }

            return View();

           
        }


        [HttpPost]
        public ActionResult Index(string listid, int? ItemId, string comments, string requestsubmit)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {
                if (ctx != null)
                {
                    if (requestsubmit != null)
                    {
                        bool isapproved = (requestsubmit == "Approve");
                        VacationItemHelper.ApproveRejectRequest(listid.ToGuid(), ItemId.Value, ctx, comments, isapproved);
                    }

                    VacationRequest request = VacationItemHelper.GetRequestFromSharePoint(listid.ToGuid(), ItemId.Value, ctx);
                    return View(request);
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
