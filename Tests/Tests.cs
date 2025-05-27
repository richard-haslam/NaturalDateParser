using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class NaturalDateParserTests
{
    [Test]
    [TestCase("now")]
    [TestCase("today")]
    [TestCase("tomorrow")]
    [TestCase("yesterday")]
    public void TestBasicKeywords(string input)
    {
        Assert.That(NaturalDateParser.TryParse(input, out var result) is true);
        var now = DateTime.Now.Date;
        
        switch (input)
        {
            case "now":
                Assert.That(result, Is.InRange(DateTime.Now.AddSeconds(-5), DateTime.Now.AddSeconds(5)));
                break;
            case "today":
                Assert.That(result.Date, Is.EqualTo(now));
                break;
            case "tomorrow":
                Assert.That(result.Date, Is.EqualTo(now.AddDays(1)));
                break;
            case "yesterday":
                Assert.That(result.Date, Is.EqualTo(now.AddDays(-1)));
                break;
        }
    }

    [Test]
    [TestCase("2 days ago")]
    [TestCase("3 weeks from now")]
    [TestCase("1 month later")]
    public void TestRelativeDates(string input)
    {
        Assert.That(NaturalDateParser.TryParse(input, out var result) is true);
        var now = DateTime.Now;

        if (input.Contains("ago"))
            Assert.That(result, Is.LessThan(now));
        else
            Assert.That(result, Is.GreaterThan(now));
    }

    [Test]
    [TestCase("next Friday")]
    [TestCase("last Monday")]
    public void TestDayOfWeek(string input)
    {
        Assert.That(NaturalDateParser.TryParse(input, out var result) is true);
        Assert.That(result, Is.TypeOf<DateTime>());
        // Additional validation can be done by checking day of week and difference from today
    }

    [Test]
    [TestCase("5pm", 17, 0)]
    [TestCase("17:00", 17, 0)]
    [TestCase("noon", 12, 0)]
    [TestCase("midnight", 0, 0)]
    public void TestTimeParsing(string input, int expectedHour, int expectedMinute)
    {
        Assert.That(NaturalDateParser.TryParse(input, out var result) is true);
        Assert.That(result.Hour, Is.EqualTo(expectedHour));
        Assert.That(result.Minute, Is.EqualTo(expectedMinute));
    }

    [Test]
    [TestCase("25/05/2025")]
    [TestCase("2025-05-25")]
    [TestCase("05/25/2025")]
    public void TestExactFormats(string input)
    {
        Assert.That(NaturalDateParser.TryParse(input, out var result) is true);
        Assert.That(result.Year, Is.EqualTo(2025));
        Assert.That(result.Month, Is.InRange(1, 12));
        Assert.That(result.Day, Is.InRange(1, 31));
    }

    [Test]
    public void TestAddCustomFormat()
    {
        NaturalDateParser.AddCustomFormat("dd MMM yyyy");
        string input = "25 May 2025";
        Assert.That(NaturalDateParser.TryParse(input, out var result) is true);
        Assert.That(result.Year, Is.EqualTo(2025));
        Assert.That(result.Month, Is.EqualTo(5));
        Assert.That(result.Day, Is.EqualTo(25));
    }

    [Test]
    public void TestInvalidInput()
    {
        string input = "some invalid date string";
        Assert.That(NaturalDateParser.TryParse(input, out _) is false);
    }
}