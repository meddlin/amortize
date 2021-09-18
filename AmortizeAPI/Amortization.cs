using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmortizeAPI
{
    public class Amortization
    {
        public List<AmortizedPart> AmortizationTable { get; set; }

        public decimal SalePrice { get; set; }
        public decimal DownPayment { get; set; }
        public decimal InterestRate { get; set; }

        public int NumberOfPayments { get; set; } // term

        public decimal HomeInsurance { get; set; }
        public decimal PropertyTax { get; set; }
        public decimal MortgageInsurance { get; set; }

        public decimal ExtraPayment { get; set; }

        /// <summary>
        /// Determine the 
        /// </summary>
        /// <returns></returns>
        public List<AmortizedPart> FindAmortizedPayments()
        {
            AmortizationTable = new List<AmortizedPart>();
            double remainingPrincipal = 477000.0;
            int termCounter = 1;

            double extraMonthlyPayment = 200.0 + 500.0 + 900.0 + 1200.0; // budget + retirement + rent + bonus

            for (var term = 360; term > 0; term--)
            {
                var basePay = FindMonthlyMortgageBase(0.003020833333, term, remainingPrincipal);
                var monthlyPayment = FindMonthlyPayment(basePay, mortIns: 353.78, propertyTax: 458.0, homeInsurance: 116.83, extraMonthlyPayment);

                (double p, double i) = CalculatePrincipalInterest((basePay + extraMonthlyPayment), 0.003020833333, remainingPrincipal, termCounter);

                remainingPrincipal = remainingPrincipal - p;
                AmortizationTable.Add(new AmortizedPart() { 
                    Term = termCounter, 
                    MonthlyPayment = monthlyPayment, 
                    Principal = p, 
                    Interest = i, 
                    RemainingPrincipal = remainingPrincipal,
                    ExtraPayment = extraMonthlyPayment
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

        public (double, double) CalculatePrincipalInterest(double monthlyBasePayment, double monthlyInterest, double remainingPrincipal, int specificTerm)
        {
            double principal = (monthlyBasePayment - monthlyInterest * remainingPrincipal) * Math.Pow((1 + monthlyInterest), (specificTerm - 1));
            double interest = monthlyBasePayment - principal;

            return (principal, interest);
        }

        public double FindMonthlyPayment(double principalInt, double mortIns, double propertyTax, double homeInsurance, double extraPrincipalPayment)
        {
            // TODO : return "principal + interest" separate from "escrow items"

            return principalInt + mortIns + propertyTax + homeInsurance + extraPrincipalPayment;
        }
    }
}
