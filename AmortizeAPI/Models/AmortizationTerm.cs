using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmortizeAPI.Models
{
    public class AmortizationTerm
    {
        public int Term { get; set; }
        public double MonthlyPayment { get; set; }
        public double Principal { get; set; }
        public double Interest { get; set; }
        public double RemainingPrincipal { get; set; }
        public double ExtraPayment { get; set; }
    }
}
