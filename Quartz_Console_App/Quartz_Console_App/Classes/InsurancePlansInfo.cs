using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz_Console_App.Classes
{
    public class InsurancePlansInfo
    {
        public string Plan_ID { get; set; }
        public int Plan_Age { get; set; }
        public string Plan_Rating_Area { get; set; }
        public string Metal_Tier { get; set; }
        public double Co_Insurance { get; set; }
        public double Premium { get; set; }
        public string Plan_type { get; set; }
        public double Deductible { get; set; }
    }
}
