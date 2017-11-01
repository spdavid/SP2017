using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAssignment.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        
    }
}