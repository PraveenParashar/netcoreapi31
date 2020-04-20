using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {
        public int UserID { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int JobID { get; set; }
        public Address Address { get; set; }
        public object Id { get; set; }
    }
}
   
