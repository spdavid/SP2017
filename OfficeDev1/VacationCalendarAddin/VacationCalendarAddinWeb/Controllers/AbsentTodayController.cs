using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Helpers;
using Common.Models;

namespace VacationCalendarAddinWeb.Controllers
{
    public class AbsentTodayController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        { var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {
                if (ctx != null)
                {
                 List<VacationRequest> requests =  VacationItemHelper.GetAbsentToday(ctx);
                 return View(requests);

                }
            }
                    return View();
        }
    }
}
