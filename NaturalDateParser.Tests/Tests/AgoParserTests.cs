using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class AgoParserTests
{
    [Test]
    [TestCase("2 days ago", -2)]
    [TestCase("1 day ago", -1)]
    public void DaysAgo(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("1 week ago", -7)]
    [TestCase("2 weeks ago", -14)]
    public void WeeksAgo(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("1 month ago", -1)]
    [TestCase("2 months ago", -2)]
    public void MonthsAgo(string input, int offsetMonths)
    {
        var expected = DateTime.Now.Date.AddMonths(offsetMonths);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Year, Is.EqualTo(expected.Year));
        Assert.That(result.Month, Is.EqualTo(expected.Month));
        Assert.That(result.Day, Is.EqualTo(expected.Day).Or.LessThanOrEqualTo(DateTime.DaysInMonth(result.Year, result.Month))); // handles shorter months
    }

    [Test]
    [TestCase("1 year ago", -1)]
    [TestCase("2 years ago", -2)]
    public void YearsAgo(string input, int offsetYears)
    {
        var expected = DateTime.Now.Date.AddYears(offsetYears);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Year, Is.EqualTo(expected.Year));
        Assert.That(result.Month, Is.EqualTo(expected.Month));
        Assert.That(result.Day, Is.EqualTo(expected.Day).Or.LessThanOrEqualTo(DateTime.DaysInMonth(result.Year, result.Month))); // handles Feb 29
    }

    [Test]
    [TestCase("2 days ago", 2025, 05, 10, -2)]
    public void ReferenceDate(string input, int year, int month, int day, int offsetDays)
    {
        var referenceDate = new DateTime(year, month, day);
        var expected = referenceDate.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, new() { ReferenceDate = referenceDate }, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }
}