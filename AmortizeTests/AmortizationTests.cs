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

            List<AmortizationTerm> payments = amo.FindAmortizedPayments();
            foreach(AmortizationTerm pmt in  payments)
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

            List<AmortizationTerm> payments = amo.FindAmortizedPayments();

            Assert.True(payments.Count < amo.NumberOfPayments);
        }

        /// <summary>
        /// Ensure the list of <c>AmortizedPart</c> is returned to the client in sequential
        /// order of terms(t) = t => [1, 2, n, ..., n+1].
        /// </summary>
        [Fact]
        public void FindAmortizedPayments__NormalCalculation__TermsShouldBeOrdered()
        {

        }



        [Fact]
        public void MortgageInsuranceRolloffTerm__NoRolloffIndicated__ReturnLastTerm()
        {

        }

        [Fact]
        public void MortgageInsuranceRolloffTerm__80Percent__ReturnSomeTerm()
        {

        }

        [Fact]
        public void MortgageInsuranceRolloffTerm__AutomaticCutoff__EndPmiAfterAutoYearMark()
        {
            /*
             * Research if PMI rolls off a loan automatically at 10 or 20 years (or some
             * other value).
             * 
             * Amortization table should show PMI has automatically rolled off, even if
             * 80% (78%?) of loan value hasn't been paid.
             */
        }

        [Fact]
        public void MortgageInsuranceRolloffTerm__MakeSureTermsAreComparedInOrder()
        {
            /*
             * In the MortgageInsuranceRolloffTerm() method, we loop over the amortization terms
             * checking for the first term...where the remaining balance is less than the 
             * rolloff value...
             * 
             * However, the AmortizationTerms are contained in a <c>List</c> which doesn't
             * have a guaranteed order.
             */
        }
    }
}
