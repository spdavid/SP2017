using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstSharePointAddInWeb.Models
{
    public class SPList
    {
        public Guid ListId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

    }
}