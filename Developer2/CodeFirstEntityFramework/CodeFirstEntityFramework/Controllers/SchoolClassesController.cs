using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstEntityFramework.Models;

namespace CodeFirstEntityFramework.Controllers
{
    public class SchoolClassesController : Controller
    {
        private CodeFirstEntityFrameworkContext db = new CodeFirstEntityFrameworkContext();

        // GET: SchoolClasses
        public ActionResult Index()
        {
            var schoolClasses = db.SchoolClasses.Include(s => s.Course);
            return View(schoolClasses.ToList());
        }

        // GET: SchoolClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            return View();
        }

        // POST: SchoolClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CourseId")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                db.SchoolClasses.Add(schoolClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", schoolClass.CourseId);
            return View(schoolClass);
        }

        // GET: SchoolClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", schoolClass.CourseId);
            return View(schoolClass);
        }

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CourseId")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", schoolClass.CourseId);
            return View(schoolClass);
        }

        // GET: SchoolClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolClass schoolClass = db.SchoolClasses.Find(id);
            db.SchoolClasses.Remove(schoolClass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
