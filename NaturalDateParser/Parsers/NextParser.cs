using System;
using System.Diagnostics;

namespace NaturalDateParsing;

internal class NextParser : INaturalDateParser
{
    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);


    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        switch (input.ToLower())
        {
            case "next year": { result = referenceDate.Date.AddYears(1); return true; }
            case "next month": { result = referenceDate.AddMonths(1).Date; return true; }
            case "next week": { result = referenceDate.AddDays(7).Date; return true; }
            case "next monday": { result = GetNextWeekday(referenceDate, DayOfWeek.Monday); return true; }
            case "next tuesday": { result = GetNextWeekday(referenceDate, DayOfWeek.Tuesday); return true; }
            case "next wednesday": { result = GetNextWeekday(referenceDate, DayOfWeek.Wednesday); return true; }
            case "next thursday": { result = GetNextWeekday(referenceDate, DayOfWeek.Thursday); return true; }
            case "next friday": { result = GetNextWeekday(referenceDate, DayOfWeek.Friday); return true; }
            case "next saturday": { result = GetNextWeekday(referenceDate, DayOfWeek.Saturday); return true; }
            case "next sunday": { result = GetNextWeekday(referenceDate, DayOfWeek.Sunday); return true; }
            default: { result = default; return false; }
        }
    }

    private static DateTime GetNextWeekday(DateTime referenceDate, DayOfWeek targetDay)
    {
        var startOfNextWeek = MoveToStartOfNextWeek(referenceDate);
        int daysUntilTargetDay = ((int)targetDay - (int)DayOfWeek.Monday + 7) % 7;

        return startOfNextWeek.AddDays(daysUntilTargetDay);
    }

    private static DateTime MoveToStartOfNextWeek(DateTime referenceDate)
    {
        int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)referenceDate.DayOfWeek + 7) % 7;
        if (daysUntilNextMonday == 0) daysUntilNextMonday = 7;

        return referenceDate.AddDays(daysUntilNextMonday);
    }
}