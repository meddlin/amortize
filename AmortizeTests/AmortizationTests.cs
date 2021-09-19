using AmortizeAPI;
using AmortizeAPI.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace AmortizeTests
{
    public class AmortizationTests
    {
        // Arrange, Act, Assert

        /// <summary>
        /// When submitting extra principal payments, the loan will be paid off faster
        /// than its set term. Without accounting for this, calculations can show a
        /// negative remaining balance--which would not happen.
        /// </summary>
        [Fact]
        public void FindAmortizedPayments__ExtraPayments__BalanceDoesNotGoNegative()
        {
            var amo = new Amortization(salePrice: 300000, downPayment: 60000, mortgageYears: 15, interestRate: 0.025);
            amo.ExtraPayment = 500.0;

            bool negativeDetected = false;

            List<AmortizedPart> payments = amo.FindAmortizedPayments();
            foreach(AmortizedPart pmt in  payments)
            {
                if (pmt.RemainingPrincipal < 0) negativeDetected = true;
            }

            Assert.False(negativeDetected);
        }

        /// <summary>
        /// When submitting extra principal payments, the loan will be paid off sooner.
        /// So, just like there shouldn't be a negative balance calculation, we should
        /// also have fewer payments than original set loan terms.
        /// </summary>
        [Fact]
        public void FindAmortizedPayments__ExtraPayments__FewerPaymentsThanTerms()
        {
            var amo = new Amortization(salePrice: 300000, downPayment: 60000, mortgageYears: 15, interestRate: 0.025);
            amo.ExtraPayment = 500.0;

            List<AmortizedPart> payments = amo.FindAmortizedPayments();

            Assert.True(payments.Count < amo.NumberOfPayments);
        }
    }
}
