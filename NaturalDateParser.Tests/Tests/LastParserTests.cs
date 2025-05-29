using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class LastParserTests
{
    [Test]
    [TestCase("last year")]
    [TestCase("last year")]
    public void LastYear(string input)
    {
        var expected = DateTime.Now.Date.AddYears(-1);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("last month")]
    [TestCase("Last month")]
    public void LastMonth(string input)
    {
        var expected = DateTime.Now.Date.AddMonths(-1);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("last week")]
    [TestCase("Last week")]
    public void lastWeek(string input)
    {
        var expected = DateTime.Now.Date.AddDays(-7);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("last monday", 19, 05, 2025)]
    [TestCase("last tuesday", 20, 05, 2025)]
    [TestCase("last wednesday", 21, 05, 2025)]
    [TestCase("last thursday", 22, 05, 2025)]
    [TestCase("last friday", 23, 05, 2025)]
    [TestCase("last saturday", 24, 05, 2025)]
    [TestCase("last sunday", 25, 05, 2025)]          
    public void LastDayOfWeek(string input, int day, int month, int year)
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