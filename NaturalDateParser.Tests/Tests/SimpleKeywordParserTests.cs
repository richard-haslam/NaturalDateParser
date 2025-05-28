using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class SimpleKeywordParserTests
{
    [Test]
    [TestCase("today", 0)]
    [TestCase("Today", 0)]
    public void Today(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("yesterday", -1)]
    [TestCase("Yesterday", -1)]
    public void Yesterday(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("tomorrow", 1)]
    [TestCase("Tomorrow", 1)]
    public void Tomorrow(string input, int offsetDays)
    {
        var expected = DateTime.Now.Date.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }
    
    [Test]
    [TestCase("today", 2025, 05, 10, 0)]
    [TestCase("tomorrow", 2025, 05, 10, 1)]
    [TestCase("yesterday", 2025, 05, 10, -1)]
    public void ReferenceDate(string input, int year, int month, int day, int offsetDays)
    {
        var referenceDate = new DateTime(year, month, day);
        var expected = referenceDate.AddDays(offsetDays);
        Assert.That(NaturalDateParser.TryParse(input, new() { ReferenceDate = referenceDate }, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(expected));
    }
}