using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class ExactParserTests
{
    [TestCase("25/05/2025", 2025, 5, 25)]
    [TestCase("2025-05-25", 2025, 5, 25)]
    [TestCase("05/25/2025", 2025, 5, 25)]
    [TestCase("20250525123000", 2025, 5, 25, 12, 30, 0)]
    [TestCase("25/05/25", 2025, 5, 25)]
    [TestCase("25-05-2025", 2025, 5, 25)]
    [TestCase("2025-05-25 15:45", 2025, 5, 25, 15, 45, 0)]
    [TestCase("15:45 25/05/2025", 2025, 5, 25, 15, 45, 0)]
    [TestCase("15:45:30 25/05/2025", 2025, 5, 25, 15, 45, 30)]
    [TestCase("25/05/2025 03:15 PM", 2025, 5, 25, 15, 15, 0)]
    public void TryParse_ValidFormats_ReturnsTrueAndCorrectDate(string input, int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
    {
        var success = NaturalDateParser.TryParse(input, out DateTime result);
        Assert.That(success is true);
        Assert.That(result, Is.EqualTo(new DateTime(year, month, day, hour, minute, second)));
    }


    [TestCase("invalid date")]
    [TestCase("32/01/2025")]
    [TestCase("2025/13/01")]
    [TestCase("2025-25-12 99:99")]
    [TestCase("random text")]
    public void TryParse_InvalidFormats_ReturnsFalse(string input)
    {
        var success = NaturalDateParser.TryParse(input, out DateTime result);

        Assert.That(success is false);
        Assert.That(result, Is.EqualTo(default(DateTime)));
    }
}