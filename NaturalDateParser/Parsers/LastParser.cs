using System;

namespace NaturalDateParsing;

internal class LastParser : INaturalDateParser
{
    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);


    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        switch (input.ToLower())
        {
            case "last year": { result = referenceDate.Date.AddYears(-1); return true; }
            case "last month": { result = referenceDate.AddMonths(-1).Date; return true; }
            case "last week": { result = referenceDate.AddDays(-7).Date; return true; }
            case "last monday": { result = GetLastWeekday(referenceDate, DayOfWeek.Monday); return true; }
            case "last tuesday": { result = GetLastWeekday(referenceDate, DayOfWeek.Tuesday); return true; }
            case "last wednesday": { result = GetLastWeekday(referenceDate, DayOfWeek.Wednesday); return true; }
            case "last thursday": { result = GetLastWeekday(referenceDate, DayOfWeek.Thursday); return true; }
            case "last friday": { result = GetLastWeekday(referenceDate, DayOfWeek.Friday); return true; }
            case "last saturday": { result = GetLastWeekday(referenceDate, DayOfWeek.Saturday); return true; }
            case "last sunday": { result = GetLastWeekday(referenceDate, DayOfWeek.Sunday); return true; }
            default: { result = default; return false; }
        }
    }

    private static DateTime GetLastWeekday(DateTime referenceDate, DayOfWeek targetDay)
    {
        var startOfPreviousWeek = MoveToStartOfPreviousWeek(referenceDate);
        int daysUntilTargetDay = ((int)targetDay - (int)DayOfWeek.Monday + 7) % 7;

        return startOfPreviousWeek.AddDays(daysUntilTargetDay);
    }

    private static DateTime MoveToStartOfPreviousWeek(DateTime referenceDate)
    {
        int currentDayOfWeek = (int)referenceDate.DayOfWeek;
        int daysSinceMonday = (currentDayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        int totalDaysToSubtract = daysSinceMonday + 7;

        return referenceDate.Date.AddDays(-totalDaysToSubtract);
    }

}