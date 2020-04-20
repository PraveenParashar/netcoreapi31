using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class JobDetails
    {
        public int UserID { get; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public double Experience { get; set; }
        public double salary {get;set;}
    }
}
