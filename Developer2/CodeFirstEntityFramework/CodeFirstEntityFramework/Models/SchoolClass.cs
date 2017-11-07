using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstEntityFramework.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }


    }
}