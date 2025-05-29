using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class NextParserTests
{
    [Test]
    [TestCase("next year")]
    [TestCase("Next year")]
    public void NextYear(string input)
    {
        var expected = DateTime.Now.Date.AddYears(1);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("next month")]
    [TestCase("Next month")]
    public void NextMonth(string input)
    {
        var expected = DateTime.Now.Date.AddMonths(1);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("next week")]
    [TestCase("Next week")]
    public void NextWeek(string input)
    {
        var expected = DateTime.Now.Date.AddDays(7);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("next monday", 02, 06, 2025)]
    [TestCase("next tuesday", 03, 06, 2025)]
    [TestCase("next wednesday", 04, 06, 2025)]
    [TestCase("next thursday", 05, 06, 2025)]
    [TestCase("next friday", 06, 06, 2025)]
    [TestCase("next saturday", 07, 06, 2025)]
    [TestCase("next sunday", 08, 06, 2025)]          
    public void NextDayOfWeek(string input, int day, int month, int year)
    {
        var expected = new DateTime(year, month, day);
        var options = new NaturalDateParserOptions()
        {
            ReferenceDate = new(2025, 05, 29)
        };

        Assert.That(NaturalDateParser.TryParse(input, options, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }
}