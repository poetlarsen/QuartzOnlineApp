using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Console_App.Classes
{
    public class DependentsInfo
    {
        public string Dependent_ID { get; set; }
        public string Customer_ID { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zip { get; set; }
        public DateTime DOB { get; set; }
        public string Dependent_Type { get; set; }
    }
}
