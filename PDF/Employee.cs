using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //public Nullable<System.DateTime> StartContract { get; set; }
        public DateTime StartContract { get; set; }
        public DateTime EndContract { get; set; }
    }
}
