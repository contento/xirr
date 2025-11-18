using XIRREngine;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var sample1 = new List<(DateTime, double)>
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

var sample2 = new List<(DateTime, double)>
{
    (new DateTime(2001, 5, 1), 10000),
    (new DateTime(2002, 3, 1), 2000),
    (new DateTime(2002, 5, 1), -5500),
    (new DateTime(2002, 9, 1), 3000),
    (new DateTime(2003, 2, 1), 3500),
    (new DateTime(2003, 5, 1), -15000)
};

Console.WriteLine("XIRR for Sample 1: " + XIRRCalculator.CalculateXIRRWithFallback(sample1));
Console.WriteLine("XIRR for Sample 2: " + XIRRCalculator.CalculateXIRRWithFallback(sample2));
