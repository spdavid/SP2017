using StudentAssignment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace StudentAssignment.Helpers
{
    public class DBHelper
    {

        public static void SaveDB(List<Student> students)
        {
            string FileLocation = ConfigurationManager.AppSettings["DBLocation"];

            string json = JsonConvert.SerializeObject(students);

            System.IO.File.WriteAllText(FileLocation, json);

        }

        public static List<Student> GetDB()
        {
             string FileLocation = ConfigurationManager.AppSettings["DBLocation"];
           // string FileLocation = HttpRuntime.AppDomainAppPath + @"\App_Data\db.json";
            if (System.IO.File.Exists(FileLocation))
            {
                string json = System.IO.File.ReadAllText(FileLocation);

                List<Student> studentsdb = JsonConvert.DeserializeObject<List<Student>>(json);

                return studentsdb;
            }
            else
            {
                List<Student> studentsdb = new List<Student>();
               
                    

                    studentsdb.Add(new Student() { Id = 1, Age = 24, City = "New York", Name = "Dan Larsson", PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/9/9a/Henrik_Larsson_in_Jan_2014.jpg" });
                
                return studentsdb;

            }
        }
    }
}