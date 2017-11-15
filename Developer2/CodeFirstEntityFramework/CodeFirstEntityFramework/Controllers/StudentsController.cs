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
    public class StudentsController : Controller
    {
        private CodeFirstEntityFrameworkContext db = new CodeFirstEntityFrameworkContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Courses = db.Courses;

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age")] Student student,List<string> Classes)
        {


            if (ModelState.IsValid)
            {
                student.SchoolClasses = new List<SchoolClass>();
                if (Classes != null)
                {
                    foreach (string classid in Classes)
                    {

                        student.SchoolClasses.Add(db.SchoolClasses.Find(int.Parse(classid)));
                    }
                }


                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            ViewBag.Courses = db.Courses;

            Student student = db.Students.Find(id);
            ViewBag.StudentClasses = student.SchoolClasses.Select(sc => sc.Id).ToList();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age")] Student student, List<string> Classes)
        {
            if (ModelState.IsValid)
            {

                Student dbstudent = db.Students.Find(student.Id);

                dbstudent.Name = student.Name;
                dbstudent.Age = student.Age;

                // remove all classes from student
                dbstudent.SchoolClasses.Clear();

                // re add any classes that were checked. 
                if (Classes != null)
                {
                    foreach (string classid in Classes)
                    {

                        dbstudent.SchoolClasses.Add(db.SchoolClasses.Find(int.Parse(classid)));
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
