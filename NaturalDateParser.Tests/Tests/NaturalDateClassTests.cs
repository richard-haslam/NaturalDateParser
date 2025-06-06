using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class NaturalDateClassTests
{
    [Test]
    [TestCase("today")]
    public void NaturalDateClassParsableTests(string input)
    {
        var expected = DateTime.Now.Date;
        var naturalDate = new NaturalDateTime(input);
        Assert.That(naturalDate.DateTime, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("Not a date")]
    public void NaturalDateClassNotParsableTests(string input)
    {
        Assert.Throws<FormatException>(() => new NaturalDateTime(input));
    }

    [Test]
    [TestCase("tomorrow")]
    public void NaturalDateClassCastToDateTimeTests(string input)
    {
        var expected = DateTime.Now.Date.AddDays(1);
        var naturalDate = new NaturalDateTime(input);
        Assert.That((DateTime)naturalDate, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("tomorrow")]
    public void NaturalDateClassCastToDateOnlyTests(string input)
    {
        var expected = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1));
        var naturalDate = new NaturalDateTime(input);
        Assert.That((DateOnly)naturalDate, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("yesterday")]
    public void NaturalDateClassPreservesOriginalStringTests(string input)
    {
        var naturalDate = new NaturalDateTime(input);
        Assert.That(naturalDate.OriginalString, Is.EqualTo(input));
    }
}