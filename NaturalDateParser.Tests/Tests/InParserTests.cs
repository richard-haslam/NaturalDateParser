using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class InTests
{
    [Test]
    [TestCase("in 3 days", 3)]
    [TestCase("in 1 day", 1)]
    public void InDays(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("in 1 week", 7)]
    [TestCase("in 2 weeks", 14)]
    public void InWeeks(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("in 1 month", 1)]
    [TestCase("in 3 months", 3)]
    public void InMonths(string input, int offsetMonths)
    {
        var expected = DateTime.Now.Date.AddMonths(offsetMonths);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Year, Is.EqualTo(expected.Year));
        Assert.That(result.Month, Is.EqualTo(expected.Month));
        Assert.That(result.Day, Is.EqualTo(expected.Day).Or.LessThanOrEqualTo(DateTime.DaysInMonth(result.Year, result.Month))); // handles shorter months
    }

    [Test]
    [TestCase("in 1 year", 1)]
    [TestCase("in 5 years", 5)]
    public void InYears(string input, int offsetYears)
    {
        var expected = DateTime.Now.Date.AddYears(offsetYears);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Year, Is.EqualTo(expected.Year));
        Assert.That(result.Month, Is.EqualTo(expected.Month));
        Assert.That(result.Day, Is.EqualTo(expected.Day).Or.LessThanOrEqualTo(DateTime.DaysInMonth(result.Year, result.Month))); // handles Feb 29
    }

    [Test]
    [TestCase("in 5 days", 2025, 05, 10, 5)]
    public void ReferenceDate(string input, int year, int month, int day, int offsetDays)
    {
        var referenceDate = new DateTime(year, month, day);
        var expected = referenceDate.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, new() { ReferenceDate = referenceDate }, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }
}