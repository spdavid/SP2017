using StudentAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAssignment.Helpers
{
    public class StudentHelper
    {
       //private static List<Student> StudentsDB = new List<Student>();


        public static List<Student> GetAllStudents()
        {
            var StudentsDB = DBHelper.GetDB();

            return StudentsDB;

        }
        public static Student GetStudentById(int id)
        {
            var StudentsDB = DBHelper.GetDB();
            return StudentsDB.Where(s => s.Id == id).FirstOrDefault();
        }


        public static void AddStudent(Student newStudent)
        {
            var StudentsDB = DBHelper.GetDB();
            List<Student> sudentsOrderedById = StudentsDB.OrderByDescending(s => s.Id).ToList();


            int latestId = 0;
            if (sudentsOrderedById.Count != 0)
            {
                latestId = sudentsOrderedById.FirstOrDefault().Id;
            }
            newStudent.Id = latestId + 1;

            StudentsDB.Add(newStudent);

            DBHelper.SaveDB(StudentsDB);
        }


        public static void UpdateStudent(Student student)
        {
            var StudentsDB = DBHelper.GetDB();
            Student toUpdate = StudentsDB.Where(s => s.Id == student.Id).FirstOrDefault();
            toUpdate.Name = student.Name;
            toUpdate.PictureUrl = student.PictureUrl;
            toUpdate.City = student.City;
            toUpdate.Age = student.Age;

            DBHelper.SaveDB(StudentsDB);
        }


        public static void DeleteStudent(int id)
        {
            var StudentsDB = DBHelper.GetDB();
            Student toDelete = StudentsDB.Where(s => s.Id == id).FirstOrDefault();
            StudentsDB.Remove(toDelete);
            DBHelper.SaveDB(StudentsDB);
        }
    }
}