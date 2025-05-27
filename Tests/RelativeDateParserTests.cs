using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class RelativeDateParserTests
{
    [Test]
    [TestCase("today", 0)]
    [TestCase("yesterday", -1)]
    [TestCase("tomorrow", 1)]
    public void ParsesBasicRelativeDates(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("2 days ago", -2)]
    [TestCase("1 day ago", -1)]
    [TestCase("in 3 days", 3)]
    [TestCase("in 1 day", 1)]
    public void ParsesRelativeDays(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("1 week ago", -7)]
    [TestCase("2 weeks ago", -14)]
    [TestCase("in 1 week", 7)]
    [TestCase("in 2 weeks", 14)]
    public void ParsesRelativeWeeks(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("1 month ago", -1)]
    [TestCase("2 months ago", -2)]
    [TestCase("in 1 month", 1)]
    [TestCase("in 3 months", 3)]
    public void ParsesRelativeMonths(string input, int offsetMonths)
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
    [TestCase("in 1 year", 1)]
    [TestCase("in 5 years", 5)]
    [TestCase("last year", -1)]
    [TestCase("next year", 1)]
    public void ParsesRelativeYears(string input, int offsetYears)
    {
        var expected = DateTime.Now.Date.AddYears(offsetYears);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Year, Is.EqualTo(expected.Year));
        Assert.That(result.Month, Is.EqualTo(expected.Month));
        Assert.That(result.Day, Is.EqualTo(expected.Day).Or.LessThanOrEqualTo(DateTime.DaysInMonth(result.Year, result.Month))); // handles Feb 29
    }

    [Test]
    [TestCase("1 hour ago", -1)]
    [TestCase("in 2 hours", 2)]
    [TestCase("5 minutes ago", -5)]
    [TestCase("in 30 minutes", 30)]
    public void ParsesRelativeTime(string input, int offsetAmount)
    {
        var now = DateTime.Now;
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);

        if (input.Contains("minute"))
        {
            var expected = now.AddMinutes(offsetAmount);
            Assert.That(Math.Abs((result - expected).TotalMinutes), Is.LessThan(1));
        }
        else if (input.Contains("hour"))
        {
            var expected = now.AddHours(offsetAmount);
            Assert.That(Math.Abs((result - expected).TotalMinutes), Is.LessThan(1));
        }
    }
}