using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class ThisParserTests
{
    [Test]
    [TestCase("This Monday", DayOfWeek.Monday, 2025, 06, 02)]
    [TestCase("this monday", DayOfWeek.Monday, 2025, 06, 02)]
    [TestCase("This Tuesday", DayOfWeek.Tuesday, 2025, 06, 03)]
    [TestCase("this tuesday", DayOfWeek.Tuesday, 2025, 06, 03)]
    [TestCase("This Wednesday", DayOfWeek.Wednesday, 2025, 06, 04)]
    [TestCase("this wednesday", DayOfWeek.Wednesday, 2035, 06, 04)]
    [TestCase("This Thursday", DayOfWeek.Thursday, 2025, 06, 05)]
    [TestCase("this thursday", DayOfWeek.Thursday, 2025, 06, 05)]
    [TestCase("This Friday", DayOfWeek.Friday, 2025, 06, 07)]
    [TestCase("this friday", DayOfWeek.Friday, 2025, 06, 07)]
    [TestCase("This Saturday", DayOfWeek.Saturday, 2025, 05, 31)]
    [TestCase("this saturday", DayOfWeek.Saturday, 2025, 05, 31)]
    [TestCase("This Sunday", DayOfWeek.Sunday, 2025, 06, 01)]
    [TestCase("this sunday", DayOfWeek.Sunday, 2025, 06, 01)]
    public void ThisDayOfWeek(string input, DayOfWeek expectedDay, int year, int month, int day)
    {
        var expectedDate = new DateTime(year, month, day);
        var options = new NaturalDateParserOptions() { ReferenceDate = new DateTime(2025, 05, 30) };

        Assert.That(NaturalDateParser.TryParse(input, options, out var result), Is.True);
        Assert.That(result.DayOfWeek, Is.EqualTo(expectedDay));
        Assert.That(result.Date, Is.GreaterThanOrEqualTo(expectedDate));
    }
}