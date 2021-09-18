using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmortizeAPI.Models
{
    public class CalculationRequest
    {
        public double SalePrice { get; set; } = 530000;
        public double DownPayment { get; set; } = 53000;
        public int MortgageDuration { get; set; } = 30;
        public double InterestRate { get; set; } = 0.03625;

        public double HomeInsurance { get; set; } = 116.83;
        public double PropertyTax { get; set; } = 458.0;
        public double MortgageInsurance { get; set; } = 353.78;

        public double ExtraMonthlyPayment { get; set; } = 500.0;
    }
}
