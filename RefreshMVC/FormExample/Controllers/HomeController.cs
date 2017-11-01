using FormExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormExample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string foo, string Name, someojb customObject)
        {
            string s = Request.QueryString["someKey"];

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formvalues, string foo, string Name, someojb customObject)
        {

            return View();
        }


        public ActionResult AnotherPage(FormCollection formvalues, string foo, string Name, someojb customObject)
        {
           
            return View();
        }


    }
}