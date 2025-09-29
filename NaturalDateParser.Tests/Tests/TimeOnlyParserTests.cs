using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class TimeParserTests
{
    [TestCase("5pm", 17, 0)]
    [TestCase("17:00", 17, 0)]
    [TestCase("08:30", 8, 30)]
    [TestCase("noon", 12, 0)]
    [TestCase("midnight", 0, 0)]
    public void TimeParser_ParsesTimeOnly(string input, int expectedHour, int expectedMinute)
    {
        var today = DateTime.Now.Date;
        Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
        Assert.That(result.Date, Is.EqualTo(today));
        Assert.That(result.Hour, Is.EqualTo(expectedHour));
        Assert.That(result.Minute, Is.EqualTo(expectedMinute));
    }
}