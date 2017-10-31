using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RefreshMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SetViewBag();

            List<string> cars = new List<string>();
            cars.Add("Volvo");
            cars.Add("Audi");
            cars.Add("Mazda");
            cars.Add("KIA");

            ViewBag.carsarray = cars;

            return View();
        }

        public ActionResult Index2()
        {
            SetViewBag();
            return View();
        }

        public void SetViewBag()
        {
            ViewBag.foo = "bar";
            ViewBag.MyName = "David";
        }
    }
}