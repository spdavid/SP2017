using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstEntityFramework.Models
{
    public class SchoolPage
    {
        public List<School> Schools { get; set; }
        public List<Course> Courses { get; set; }

        public List<Student> Students { get; set; }
        public Teacher Teacher { get; set; }


    }
}