using CodeFirstEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstEntityFramework.Controllers
{
    public class DefaultController : Controller
    {
        private CodeFirstEntityFrameworkContext db = new CodeFirstEntityFrameworkContext();
        // GET: Default
        public ActionResult Index()
        {
            ViewBag.Schools = db.Schools;
            
            return View();
        }
    }
}