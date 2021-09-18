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
        public double Calculate()
        {
            var amo = new Amortization();
            var test = amo.FindPayment(0.03625, 360, 477000.0);

            return test;
        }
    }
}
