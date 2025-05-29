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
}