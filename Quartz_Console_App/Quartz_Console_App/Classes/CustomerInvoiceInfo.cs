using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Console_App.Classes
{
    public class CustomerInvoiceInfo
    {
        public int Invoice_ID { get; set; }
        public string Customer_ID { get; set; }
        public double Paid_Amount { get; set; }
        public DateTime Coverage_Start_Date { get; set; }
        public DateTime Coverage_End_Date { get; set; }
        public DateTime Paid_Date_Time { get; set; }
        public string Plan_ID { get; set; }
        public int Plan_Age { get; set; }
        public string Plan_Rating_Area { get; set; }

       // public string Address { get; set; }
        //        Invoice_ID int NOT NULL PRIMARY KEY, 
        //Invoice_Amt int NOT NULL, 
        //Period_Start_Date DATE NOT NULL,
        //Period_End_Date DATE NOT NULL, 
    }
}
