using StudentAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAssignment.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View(Helpers.StudentHelper.GetAllStudents());
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {


            return View(Helpers.StudentHelper.GetStudentById(id));
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                Helpers.StudentHelper.AddStudent(student);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Helpers.StudentHelper.GetStudentById(id));
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                Helpers.StudentHelper.UpdateStudent(student);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            Helpers.StudentHelper.DeleteStudent(id);

            return RedirectToAction("Index");
          //  return View(Helpers.StudentHelper.GetStudentById(id));
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Helpers.StudentHelper.DeleteStudent(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
