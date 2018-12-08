using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Console_App.Classes
{
    public class SupportHistInfo
    {
        public string CustomerID { get; set; }
        public string Employee_FirstName { get; set; }
        public string Employee_LastName { get; set; }
        public DateTime Call_Start { get; set; }
        public DateTime Call_End { get; set; }
        public string Call_Reason { get; set; }
        public string Support_Given { get; set; }

        //        Customer_ID,
        //Employee_FirstName varchar(255) NOT NULL,
        //Employee_LastName varchar(255) NOT NULL,
        //Call_Start DATETIME,
        //Call_End DATETIME,
        //Call_Reason varchar(255) NOT NULL,
        //Support_Given varchar(255) NOT NULL,
        //PRIMARY KEY CLUSTERED(Customer_ID, Call_Start)
        //FOREIGN KEY Customer_ID REFERENCES Customers(Customer_ID) ON UPDATE NO ACTION ON DELETE CASCADE

    }
}
