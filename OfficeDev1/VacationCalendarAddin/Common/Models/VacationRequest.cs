using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public string RequestTitle { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReasonForAbsense { get; set; }
        public string ApprovalStatus { get; set; }
        public string Comments { get; set; }


    }
}