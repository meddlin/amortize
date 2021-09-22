using AmortizeAPI.Models;
using Microsoft.AspNetCore.Cors;
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
        public List<AmortizationTerm> CalculateAmortizationTable([FromBody] CalculationRequest request)
        {
            var amo = new Amortization(request);
            var results = amo.FindAmortizedPayments();

            return results;
        }

        [HttpPost]
        public double BasePayment([FromBody] CalculationRequest request)
        {
            var amo = new Amortization(request);
            var test = amo.FindMonthlyMortgageBase(0.003020833333, 360, 477000.0);

            return test;
        }

        [HttpPost]
        public double MonthlyPayment([FromBody] CalculationRequest request)
        {
            var amo = new Amortization(request);
            var basePay = amo.FindMonthlyMortgageBase(0.003020833333, 360, 477000.0);
            var test = amo.FindMonthlyPayment(basePay, 353.78, 458.0, 116.83, 0.0);

            return test;
        }

        [HttpPost]
        public double MortgageInsuranceRolloff([FromBody] CalculationRequest request)
        {
            var amo = new Amortization(request);
            return amo.MortgageInsuranceRolloffAmount(request.MortgageInsuranceCancelPercent);
        }
    }
}
