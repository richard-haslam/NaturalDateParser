using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class DayOfWeekParserTests
{
    [Test]
    [TestCase("This Monday", DayOfWeek.Monday)]
    [TestCase("this monday", DayOfWeek.Monday)]
    [TestCase("This Tuesday", DayOfWeek.Tuesday)]
    [TestCase("this tuesday", DayOfWeek.Tuesday)]
    [TestCase("This Wednesday", DayOfWeek.Wednesday)]
    [TestCase("this wednesday", DayOfWeek.Wednesday)]
    [TestCase("This Thursday", DayOfWeek.Thursday)]
    [TestCase("this thursday", DayOfWeek.Thursday)]
    [TestCase("This Friday", DayOfWeek.Friday)]
    [TestCase("this friday", DayOfWeek.Friday)]
    [TestCase("This Saturday", DayOfWeek.Saturday)]
    [TestCase("this saturday", DayOfWeek.Saturday)]
    [TestCase("This Sunday", DayOfWeek.Sunday)]
    [TestCase("this sunday", DayOfWeek.Sunday)]
    public void ThisDayOfWeek(string input, DayOfWeek expectedDay)
    {
        var now = DateTime.Now;
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.DayOfWeek, Is.EqualTo(expectedDay));
        Assert.That(result.Date, Is.GreaterThanOrEqualTo(now.Date));
        Assert.That(result.Date, Is.LessThan(now.Date.AddDays(7)));
    }

    [Test]
    [TestCase("Last Monday", DayOfWeek.Monday)]
    [TestCase("last monday", DayOfWeek.Monday)]
    [TestCase("Last Tuesday", DayOfWeek.Tuesday)]
    [TestCase("last tuesday", DayOfWeek.Tuesday)]
    [TestCase("Last Wednesday", DayOfWeek.Wednesday)]
    [TestCase("last wednesday", DayOfWeek.Wednesday)]
    [TestCase("Last Thursday", DayOfWeek.Thursday)]
    [TestCase("last thursday", DayOfWeek.Thursday)]
    [TestCase("Last Friday", DayOfWeek.Friday)]
    [TestCase("last friday", DayOfWeek.Friday)]
    [TestCase("Last Saturday", DayOfWeek.Saturday)]
    [TestCase("last saturday", DayOfWeek.Saturday)]
    [TestCase("Last Sunday", DayOfWeek.Sunday)]
    [TestCase("last sunday", DayOfWeek.Sunday)]
    public void LastDayOfWeek(string input, DayOfWeek expectedDay)
    {
        var now = DateTime.Now;
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.DayOfWeek, Is.EqualTo(expectedDay));
        Assert.That(result.Date, Is.LessThan(now.Date));
        Assert.That(result.Date, Is.GreaterThanOrEqualTo(now.Date.AddDays(-7)));
    }

    [Test]
    [TestCase("Monday", DayOfWeek.Monday)]
    [TestCase("monday", DayOfWeek.Monday)]
    [TestCase("Tuesday", DayOfWeek.Tuesday)]
    [TestCase("tuesday", DayOfWeek.Tuesday)]
    [TestCase("Wednesday", DayOfWeek.Wednesday)]
    [TestCase("wednesday", DayOfWeek.Wednesday)]
    [TestCase("Thursday", DayOfWeek.Thursday)]
    [TestCase("thursday", DayOfWeek.Thursday)]
    [TestCase("Friday", DayOfWeek.Friday)]
    [TestCase("friday", DayOfWeek.Friday)]
    [TestCase("Saturday", DayOfWeek.Saturday)]
    [TestCase("saturday", DayOfWeek.Saturday)]
    [TestCase("Sunday", DayOfWeek.Sunday)]
    [TestCase("sunday", DayOfWeek.Sunday)]
    public void NoPrefixDayOfWeek(string input, DayOfWeek expectedDay)
    {
        var now = DateTime.Now;
        var today = now.Date;

        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.DayOfWeek, Is.EqualTo(expectedDay));

        if (today.DayOfWeek == expectedDay)
        {
            Assert.That(result.Date, Is.EqualTo(today), "Should match today's date");
        }
        else
        {
            Assert.That(result.Date, Is.GreaterThan(today), "Should be after today");
            Assert.That(result.Date, Is.LessThanOrEqualTo(today.AddDays(7)), "Should be within the next 7 days");
        }
    }

    [Test]
    [TestCase("next friday", 2025, 05, 10, 6)]
    public void ReferenceDate(string input, int year, int month, int day, int offsetDays)
    {
        var referenceDate = new DateTime(year, month, day);
        var expected = referenceDate.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, new() { ReferenceDate = referenceDate }, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }
}