using CodeFirstEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeFirstEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private CodeFirstEntityFrameworkContext db = new CodeFirstEntityFrameworkContext();

        // GET: Home
        public ActionResult Index(int? SchoolId, int? schoolClassId)
        {
            SchoolPage page = new SchoolPage();
            page.Schools = db.Schools.ToList();

            if (SchoolId != null)
            {
                ViewBag.CurrentSchoolId = SchoolId;
                page.Courses = db.Courses.Where(c => c.SchoolId == SchoolId).ToList();
            }
            if (schoolClassId != null)
            {
                ViewBag.CurrentschoolClassId = schoolClassId;
                SchoolClass schoolClass = db.SchoolClasses.Find(schoolClassId);
                page.Students = schoolClass.Students.ToList();

                page.Teacher = db.Teachers.Find(schoolClass.TeacherId);
            }


            return View(page);
        }

        [HttpPost]
        public ActionResult AddSchool(string SchoolName)
        {
            School newSchool = new School();
            newSchool.Name = SchoolName;

            db.Schools.Add(newSchool);

            db.SaveChanges();
            string referrer = Request.UrlReferrer.ToString();

            RouteValueDictionary tRVD = new RouteValueDictionary();
            foreach (string key in Request.QueryString.Keys)
            {
                tRVD[key] = Request.QueryString[key].ToString();
            }

            return RedirectToAction("Index", tRVD);

        }

        [HttpPost]
        public ActionResult DeleteSchool(string SchoolId)
        {
            int theSchoolId = int.Parse(SchoolId);
            School school = db.Schools.Find(theSchoolId);
            db.Schools.Remove(school);

            db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}