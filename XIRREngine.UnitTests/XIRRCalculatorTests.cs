using System;
using System.Collections.Generic;
using XIRREngine;
using Xunit;

namespace XIRREngine.UnitTests
{
    public class XIRRTests
    {
        [Fact]
        public void CalculateXIRR_Sample1_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2012, 6, 1), 0.01),
                (new DateTime(2012, 7, 23), 3042626.18),
                (new DateTime(2012, 11, 7), -491356.62),
                (new DateTime(2012, 11, 30), 631579.92),
                (new DateTime(2012, 12, 1), 19769.5),
                (new DateTime(2013, 1, 16), 1551771.47),
                (new DateTime(2013, 2, 8), -304595),
                (new DateTime(2013, 3, 26), 3880609.64),
                (new DateTime(2013, 3, 31), -4331949.61)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 9.99, 10.01);
        }

        [Fact]
        public void CalculateXIRR_Sample2_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2001, 5, 1), 10000),
                (new DateTime(2002, 3, 1), 2000),
                (new DateTime(2002, 5, 1), -5500),
                (new DateTime(2002, 9, 1), 3000),
                (new DateTime(2003, 2, 1), 3500),
                (new DateTime(2003, 5, 1), -15000)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 0.09, 0.1);
        }

        [Fact]
        public void CalculateXIRR_EmptyCashFlows_ThrowsException()
        {
            var cashFlows = new List<(DateTime, double)>();

            Assert.Throws<InvalidOperationException>(() => XIRRCalculator.CalculateXIRRWithFallback(cashFlows));
        }

        [Fact]
        public void CalculateXIRR_SingleCashFlow_ThrowsException()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2025, 11, 18), 1000)
            };

            Assert.Throws<InvalidOperationException>(() => XIRRCalculator.CalculateXIRRWithFallback(cashFlows));
        }

        [Fact]
        public void CalculateXIRR_NegativeCashFlows_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2020, 1, 1), -10000),
                (new DateTime(2021, 1, 1), -5000),
                (new DateTime(2022, 1, 1), 20000)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 0.18, 0.19);
        }

        [Fact]
        public void CalculateXIRR_ZeroCashFlows_ThrowsException()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2020, 1, 1), 0),
                (new DateTime(2021, 1, 1), 0),
                (new DateTime(2022, 1, 1), 0)
            };

            Assert.Throws<InvalidOperationException>(() => XIRRCalculator.CalculateXIRRWithFallback(cashFlows));
        }

        [Fact]
        public void CalculateXIRR_LargeCashFlows_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2010, 1, 1), -1000000000),
                (new DateTime(2020, 1, 1), 2000000000)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 0.07, 0.08);
        }

        [Fact]
        public void CalculateXIRR_ShortTimeFrame_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2025, 11, 1), -1000),
                (new DateTime(2025, 11, 18), 1100)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 6.7, 6.8);
        }

        [Fact]
        public void CalculateXIRR_LongTimeFrame_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(1900, 1, 1), -100),
                (new DateTime(2000, 1, 1), 1000)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 0.02, 0.03);
        }

        [Fact]
        public void CalculateXIRR_AlternatingCashFlows_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2020, 1, 1), -1000),
                (new DateTime(2021, 1, 1), 500),
                (new DateTime(2022, 1, 1), -500),
                (new DateTime(2023, 1, 1), 2000)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 0.29, 0.3);
        }

        [Fact]
        public void CalculateXIRR_IdenticalDates_ThrowsException()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2025, 11, 18), -1000),
                (new DateTime(2025, 11, 18), 2000)
            };

            Assert.Throws<InvalidOperationException>(() => XIRRCalculator.CalculateXIRRWithFallback(cashFlows));
        }

        [Fact]
        public void CalculateXIRR_NonChronologicalOrder_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2025, 11, 18), 2000),
                (new DateTime(2020, 1, 1), -1000)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 0.12, 0.13);
        }

        [Fact]
        public void CalculateXIRR_SmallCashFlows_ReturnsExpectedResult()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2020, 1, 1), -0.01),
                (new DateTime(2021, 1, 1), 0.02)
            };

            double result = XIRRCalculator.CalculateXIRRWithFallback(cashFlows);
            Assert.InRange(result, 0.99, 1.01);
        }

        [Fact]
        public void CalculateXIRR_SinglePositiveCashFlow_ThrowsException()
        {
            var cashFlows = new List<(DateTime, double)>
            {
                (new DateTime(2025, 11, 18), 1000)
            };

            Assert.Throws<InvalidOperationException>(() => XIRRCalculator.CalculateXIRRWithFallback(cashFlows));
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }
}
