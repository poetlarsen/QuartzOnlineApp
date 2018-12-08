using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Quartz_Console_App.Classes
{
    public class CustomerInfo
    {
        public string Customer_ID { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime DOB { get; set; }
        //public string ZipCode { get; set; }

    }
}
