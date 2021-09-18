using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmortizeAPI.Controllers
{
    [ApiController]
    [Route("Amortize/[action]")]
    public class AmortizeController
    {
        // https://localhost:5001/swagger/index.html

        [HttpPost]
        public List<AmortizedPart> CalculateAmortizationTable()
        {
            var amo = new Amortization();
            var results = amo.FindAmortizedPayments();

            return results;
        }

        [HttpPost]
        public double BasePayment()
        {
            var amo = new Amortization();
            var test = amo.FindMonthlyMortgageBase(0.003020833333, 360, 477000.0);

            return test;
        }

        [HttpPost]
        public double MonthlyPayment()
        {
            var amo = new Amortization();
            var basePay = amo.FindMonthlyMortgageBase(0.003020833333, 360, 477000.0);
            var test = amo.FindMonthlyPayment(basePay, 353.78, 458.0, 116.83, 0.0);

            return test;
        }
    }
}
