using System;

namespace amortize
{
    public class Amortization
    {
        public decimal SalePrice { get; set; }
        public decimal DownPayment { get; set; }
        public decimal InterestRate { get; set; }


        public int NumberOfPayments { get; set; } // term

        public decimal MortgageInsurance { get; set; }
        public decimal ExtraPayment { get; set; }

        /*
        * Finds the next payment amount.
        */
        public double FindPayment(double interestRate, int numberOfPayments, double principalLeft)
        {
            /*
                Amortization Formula
                P = ( i((1+i)^n) ) / ( (1+i)^n - 1 )
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
    }
}