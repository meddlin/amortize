using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmortizeAPI.Models;

namespace AmortizeAPI
{
    public class Amortization
    {
        public List<AmortizationTerm> AmortizationTable { get; set; }

        public double SalePrice { get; set; }
        public double DownPayment { get; set; }
        public double StartingPrincipal { get; set; }
        public double AnnualInterestRate { get; set; }
        public double MonthlyInterestRate { get; set; }

        public int NumberOfPayments { get; set; } // term

        public double HomeInsurance { get; set; }
        public double PropertyTax { get; set; }
        public double MortgageInsurance { get; set; }

        public double ExtraPayment { get; set; }


        public Amortization(double salePrice, double downPayment, int mortgageYears, double interestRate)
        {
            SalePrice = salePrice;
            DownPayment = downPayment;
            StartingPrincipal = salePrice - downPayment;

            AnnualInterestRate = interestRate;
            MonthlyInterestRate = interestRate / 12;

            NumberOfPayments = mortgageYears * 12;
        }

        public Amortization(double salePrice, double downPayment, int mortgageYears, double interestRate, double homeIns, double propTax, double mortIns)
        {
            SalePrice = salePrice;
            DownPayment = downPayment;
            StartingPrincipal = salePrice - downPayment;

            AnnualInterestRate = interestRate;
            MonthlyInterestRate = interestRate / 12;

            NumberOfPayments = mortgageYears * 12;

            HomeInsurance = homeIns;
            PropertyTax = propTax;
            MortgageInsurance = mortIns;
        }

        public Amortization(double salePrice, double downPayment, int mortgageYears, double interestRate, double homeIns, double propTax, double mortIns, double extra)
        {
            SalePrice = salePrice;
            DownPayment = downPayment;
            StartingPrincipal = salePrice - downPayment;

            AnnualInterestRate = interestRate;
            MonthlyInterestRate = interestRate / 12;

            NumberOfPayments = mortgageYears * 12;

            HomeInsurance = homeIns;
            PropertyTax = propTax;
            MortgageInsurance = mortIns;

            ExtraPayment = extra;
        }

        public Amortization(CalculationRequest request)
        {
            SalePrice = request.SalePrice;
            DownPayment = request.DownPayment;
            StartingPrincipal = request.SalePrice - request.DownPayment;

            NumberOfPayments = request.MortgageDuration * 12;

            AnnualInterestRate = request.InterestRate;
            MonthlyInterestRate = request.InterestRate / 12;

            HomeInsurance = request.HomeInsurance;
            PropertyTax = request.PropertyTax;
            MortgageInsurance = request.MortgageInsurance;
            ExtraPayment = request.ExtraMonthlyPayment;
        }

        /// <summary>
        /// Calculate the series of payments for the amortization table
        /// </summary>
        /// <returns></returns>
        public List<AmortizationTerm> FindAmortizedPayments()
        {
            AmortizationTable = new List<AmortizationTerm>();
            double remainingPrincipal = StartingPrincipal;
            int termCounter = 1;

            for (var term = NumberOfPayments; term > 0; term--)
            {
                var basePay = FindMonthlyMortgageBase(MonthlyInterestRate, term, remainingPrincipal);
                var monthlyPayment = FindMonthlyPayment(basePay, MortgageInsurance, PropertyTax, HomeInsurance, ExtraPayment);

                (double p, double i) = CalculatePrincipalInterest((basePay + ExtraPayment), MonthlyInterestRate, remainingPrincipal, termCounter);

                remainingPrincipal = remainingPrincipal - p;

                // break loop if balance is paid off sooner
                if (remainingPrincipal < 0) break;

                AmortizationTable.Add(new AmortizationTerm() { 
                    Term = termCounter, 
                    MonthlyPayment = monthlyPayment, 
                    Principal = p, 
                    Interest = i, 
                    RemainingPrincipal = remainingPrincipal,
                    ExtraPayment = ExtraPayment
                });

                termCounter++;
            }

            return AmortizationTable;
        }

        /*
        * Finds the next payment amount.
        */
        public double FindMonthlyMortgageBase(double interestRate, int numberOfPayments, double principalLeft)
        {
            /*
                Amortization Formula
                P = ( i((1+i)^n) ) / ( (1+i)^n - 1 )

                i = monthly interest rate = annual interest rate / 12
                n = number of terms => 30 years = 360 months (i.e. terms)
            */

            double i = interestRate;
            int n = numberOfPayments;
            double p = principalLeft;

            double numerator = 0.0;
            double denominator = 0.0;

            // numerator = 1 + i;
            // numerator = Math.Pow(numerator, i);
            // numerator = i * numerator;

            numerator = i * (Math.Pow((double)1 + i, n));

            // denominator = Math.Pow((double)1 + i, n);
            // denominator = denominator - (double)1;

            denominator = (Math.Pow((double)1 + i, n)) - (double)1;

            return p * (numerator / denominator);
        }

        /// <summary>
        /// Calculate separate principal and interest pieces of a single payment.
        /// </summary>
        /// <param name="monthlyBasePayment"></param>
        /// <param name="monthlyInterest"></param>
        /// <param name="remainingPrincipal"></param>
        /// <param name="specificTerm">The specific term of the loan to calculate for</param>
        /// <returns></returns>
        public (double, double) CalculatePrincipalInterest(double monthlyBasePayment, double monthlyInterest, double remainingPrincipal, int specificTerm)
        {
            double principal = (monthlyBasePayment - monthlyInterest * remainingPrincipal) * Math.Pow((1 + monthlyInterest), (specificTerm - 1));
            double interest = monthlyBasePayment - principal;

            return (principal, interest);
        }

        /// <summary>
        /// Total combined monthly payment of mortgage, escrow items, and extra principal payments.
        /// </summary>
        /// <param name="principalInt"></param>
        /// <param name="mortIns"></param>
        /// <param name="propertyTax"></param>
        /// <param name="homeInsurance"></param>
        /// <param name="extraPrincipalPayment"></param>
        /// <returns></returns>
        public double FindMonthlyPayment(double principalInt, double mortIns, double propertyTax, double homeInsurance, double extraPrincipalPayment)
        {
            return principalInt + mortIns + propertyTax + homeInsurance + extraPrincipalPayment;
        }

        /// <summary>
        /// Calculate the principal amount when mortgage insurance will roll off.
        /// </summary>
        /// <returns></returns>
        public double MortgageInsuranceRolloffAmount(double pmiPercentage)
        {
            return StartingPrincipal * pmiPercentage;
        }

        public AmortizationTerm MortgageInsuranceRolloffTerm(CalculationRequest request)
        {
            var termToReach = new AmortizationTerm();

            // calculate pmi rolloff
            double rolloffAmount = MortgageInsuranceRolloffAmount(request.MortgageInsuranceCancelPercent);

            // run amortization table
            List<AmortizationTerm> terms = FindAmortizedPayments();

            // compare pmi rolloff amount to remaining balance of each term
            for (var i = 0; i < terms.Count; i++)
            {
                if (terms[i].RemainingPrincipal <= rolloffAmount)
                    termToReach = terms[i];

                break;
            }

            return termToReach;
        }
    }
}
