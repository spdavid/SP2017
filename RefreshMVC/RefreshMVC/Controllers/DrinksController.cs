using RefreshMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RefreshMVC.Controllers
{
    public class DrinksController : Controller
    {
        // GET: Drinks
        public ActionResult Index()
        {
            return View(Helpers.FoodHelper.GetDrinks());
        }

        // GET: Drinks/Details/5
        public ActionResult Details(int id)
        {
            Drink drink = Helpers.FoodHelper.GetDrinks()
                .Where(d => d.Id == id)
                .FirstOrDefault(); 

            return View(drink);
        }

        // GET: Drinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drinks/Create
        [HttpPost]
        public ActionResult Create(Drink collection)
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

        // GET: Drinks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Drinks/Edit/5
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

        // GET: Drinks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Drinks/Delete/5
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
