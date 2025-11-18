using System;
using System.Collections.Generic;
using System.Linq;

namespace XIRREngine
{
    public static class XIRRCalculator
    {
        public static double CalculateXIRR(List<(DateTime Date, double Amount)> cashFlows, double guess = 0.1)
        {
            const double tolerance = 1e-6;
            const int maxIterations = 100;

            if (cashFlows == null || cashFlows.Count < 2)
            {
                throw new InvalidOperationException("Cash flows must contain at least two entries.");
            }

            if (cashFlows.All(cf => cf.Amount == 0))
            {
                throw new InvalidOperationException("Cash flows cannot all be zero.");
            }

            if (cashFlows.GroupBy(cf => cf.Date).Any(g => g.Count() > 1))
            {
                throw new InvalidOperationException("Cash flows cannot have duplicate dates.");
            }

            if (!cashFlows.Any(cf => cf.Amount > 0) || !cashFlows.Any(cf => cf.Amount < 0))
            {
                throw new InvalidOperationException("Cash flows must include both positive and negative values.");
            }

            // Ensure cash flows are sorted by date
            cashFlows = cashFlows.OrderBy(cf => cf.Date).ToList();

            double rate = guess;
            for (int i = 0; i < maxIterations; i++)
            {
                double fValue = cashFlows.Sum(cf => cf.Amount / Math.Pow(1 + rate, (cf.Date - cashFlows[0].Date).TotalDays / 365.0));
                double fDerivative = cashFlows.Sum(cf => -cf.Amount * (cf.Date - cashFlows[0].Date).TotalDays / 365.0 / Math.Pow(1 + rate, 1 + (cf.Date - cashFlows[0].Date).TotalDays / 365.0));

                if (Math.Abs(fDerivative) < tolerance)
                {
                    throw new InvalidOperationException("Derivative too small; XIRR calculation cannot proceed.");
                }

                double newRate = rate - fValue / fDerivative;
                if (Math.Abs(newRate - rate) < tolerance)
                {
                    return newRate;
                }

                rate = newRate;

                if (Math.Abs(rate) < tolerance)
                {
                    throw new InvalidOperationException("Rate too small; XIRR calculation cannot proceed.");
                }

                if (i == maxIterations - 1)
                {
                    throw new InvalidOperationException("XIRR calculation exceeded maximum iterations.");
                }
            }

            throw new InvalidOperationException("XIRR calculation did not converge.");
        }

        public static double CalculateXIRRWithFallback(List<(DateTime Date, double Amount)> cashFlows, double guess = 0.1)
        {
            // Apply validation checks
            if (cashFlows == null || cashFlows.Count < 2)
            {
                throw new InvalidOperationException("Cash flows must contain at least two entries.");
            }

            if (cashFlows.All(cf => cf.Amount == 0))
            {
                throw new InvalidOperationException("Cash flows cannot all be zero.");
            }

            if (cashFlows.GroupBy(cf => cf.Date).Any(g => g.Count() > 1))
            {
                throw new InvalidOperationException("Cash flows cannot have duplicate dates.");
            }

            if (!cashFlows.Any(cf => cf.Amount > 0) || !cashFlows.Any(cf => cf.Amount < 0))
            {
                throw new InvalidOperationException("Cash flows must include both positive and negative values.");
            }

            try
            {
                return CalculateXIRR(cashFlows, guess);
            }
            catch (InvalidOperationException)
            {
                return CalculateXIRRUsingBisection(cashFlows);
            }
        }

        private static double CalculateXIRRUsingBisection(List<(DateTime Date, double Amount)> cashFlows)
        {
            // Apply validation checks
            if (cashFlows == null || cashFlows.Count < 2)
            {
                throw new InvalidOperationException("Cash flows must contain at least two entries.");
            }

            if (cashFlows.All(cf => cf.Amount == 0))
            {
                throw new InvalidOperationException("Cash flows cannot all be zero.");
            }

            if (cashFlows.GroupBy(cf => cf.Date).Any(g => g.Count() > 1))
            {
                throw new InvalidOperationException("Cash flows cannot have duplicate dates.");
            }

            if (!cashFlows.Any(cf => cf.Amount > 0) || !cashFlows.Any(cf => cf.Amount < 0))
            {
                throw new InvalidOperationException("Cash flows must include both positive and negative values.");
            }

            const double tolerance = 1e-6;
            const int maxIterations = 100;
            double lower = -0.99, upper = 10.0;

            for (int i = 0; i < maxIterations; i++)
            {
                double mid = (lower + upper) / 2;
                double fMid = cashFlows.Sum(cf => cf.Amount / Math.Pow(1 + mid, (cf.Date - cashFlows[0].Date).TotalDays / 365.0));

                if (Math.Abs(fMid) < tolerance)
                {
                    return mid;
                }

                if (fMid > 0)
                {
                    lower = mid;
                }
                else
                {
                    upper = mid;
                }

                if (Math.Abs(upper - lower) < tolerance)
                {
                    return (upper + lower) / 2;
                }
            }

            throw new InvalidOperationException("Bisection method did not converge.");
        }
    }
}
