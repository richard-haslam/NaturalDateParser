using NUnit.Framework;
using System;

namespace NaturalDateParsing.Tests;

[TestFixture]
public class CombinationParserTests
{
    [TestCase("tomorrow at 5pm", 1, 17, 0)]
    [TestCase("25/05/2025 15:30", 2025, 5, 25, 15, 30)]
    [TestCase("today at noon", 0, 12, 0)]
    public void DateTimeCombinationParser_ParsesDateAndTime(string input, int arg1, int arg2, int arg3 = 0, int arg4 = 0, int arg5 = 0)
    {
        if (input == "25/05/2025 15:30")
        {
            Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
            Assert.That(result, Is.EqualTo(new DateTime(arg1, arg2, arg3, arg4, arg5, 0)));
        }
        else
        {
            var today = DateTime.Now.Date;
            Assert.That(NaturalDateParser.TryParse(input, out var result), Is.True);
            var expectedDate = today.AddDays(arg1);
            Assert.That(result.Date, Is.EqualTo(expectedDate));
            Assert.That(result.Hour, Is.EqualTo(arg2));
            Assert.That(result.Minute, Is.EqualTo(arg3));
        }
    }

    [Test]
    public void DateTimeCombinationParser_ParsesNextFriday()
    {
        var today = DateTime.Now.Date;
        int daysUntilFriday = ((int)DayOfWeek.Friday - (int)today.DayOfWeek + 7) % 7;
        // Always add 7 days to get the Friday in the following week
        int daysUntilNextFriday = daysUntilFriday + 7;

        Assert.That(NaturalDateParser.TryParse("next Friday 08:00", out var result), Is.True);
        var expectedDate = today.AddDays(daysUntilNextFriday);
        Assert.That(result.Date, Is.EqualTo(expectedDate));
        Assert.That(result.Hour, Is.EqualTo(8));
        Assert.That(result.Minute, Is.EqualTo(0));
    }
}