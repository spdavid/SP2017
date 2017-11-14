using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFirstEntityFramework.Models
{
    public class School
    {
        public int Id { get; set; }
        [Display(Name = "School Name")]
        public string Name { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}