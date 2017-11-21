using CodeFirstEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstEntityFramework.Controllers
{
    public class ViewTestController : Controller
    {
        private CodeFirstEntityFrameworkContext db = new CodeFirstEntityFrameworkContext();

        // GET: ViewTest
        public ActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public ActionResult Index(School school, string Name)
        {
            db.Schools.Add(school);

            return View();
        }
    }
}