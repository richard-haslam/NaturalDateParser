using System;


namespace NaturalDateParsing;

internal class ThisParser : INaturalDateParser
{
    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);

    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        switch (input.ToLower().Replace("this coming ", "this "))
        {
            case "this monday": { result = GetThisDay(referenceDate, DayOfWeek.Monday); return true; }
            case "this tuesday": { result = GetThisDay(referenceDate, DayOfWeek.Tuesday); return true; }
            case "this wednesday": { result = GetThisDay(referenceDate, DayOfWeek.Wednesday); return true; }
            case "this thursday": { result = GetThisDay(referenceDate, DayOfWeek.Thursday); return true; }
            case "this friday": { result = GetThisDay(referenceDate, DayOfWeek.Friday); return true; }
            case "this saturday": { result = GetThisDay(referenceDate, DayOfWeek.Saturday); return true; }
            case "this sunday": { result = GetThisDay(referenceDate, DayOfWeek.Sunday); return true; }
            default: { result = default; return false; }
        }
    }

    private static DateTime GetThisDay(DateTime referenceDate, DayOfWeek targetDay)
    {

        int daysUntilTargetDay = ((int)targetDay - (int)referenceDate.DayOfWeek + 7) % 7;
        return referenceDate.AddDays(daysUntilTargetDay) == referenceDate
                    ? referenceDate.AddDays(7)
                    : referenceDate.AddDays(daysUntilTargetDay);
    }
}