using MyFirstSharePointAddInWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstSharePointAddInWeb.Controllers
{
    public class DogController : Controller
    {
        // GET: Dog
        public ActionResult Index()
        {
            // get from datasource : sharepoint

            List<Dog> dogs = new List<Dog>();

            dogs.Add(new Dog() { Age = 3, Color = "Green", Name = "Rufas" });
            dogs.Add(new Dog() { Age = 3, Color = "Blue", Name = "Rufas2" });
            dogs.Add(new Dog() { Age = 3, Color = "Red", Name = "Rufas3" });
            dogs.Add(new Dog() { Age = 3, Color = "Yellow", Name = "Rufas4" });



            return View(dogs);
        }

        // GET: Dog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dog/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
