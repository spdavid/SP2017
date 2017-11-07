using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstEntityFramework.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SchoolId { get; set; }
        public School School { get; set; }
        public virtual List<SchoolClass> SchoolClasses { get; set; }
}
}