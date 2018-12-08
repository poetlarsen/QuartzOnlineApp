using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Console_App.Classes
{
    public class QuartzInvoiceInfo
    {
        public string Quartz_Invoice_ID { get; set; }
        public double Amt_Paid { get; set; }
        public DateTime Paid_Date_Time { get; set; }
        public string Dept_Name { get; set; }

//        Quartz_Invoice_ID int NOT NULL PRIMARY KEY,
//Amt_Paid int NOT NULL,
//Paid_Date_Time DATETIME NOT NULL,
//Dept_Name varchar(255) NOT NULL,
    }
}
