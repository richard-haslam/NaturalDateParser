using System;
using System.Text.RegularExpressions;


namespace NaturalDateParsing;

internal class DayOfWeekParser : INaturalDateParser
{
    private static readonly Regex Pattern = new Regex(
        @"^(?:(last|this)\s+)?(monday|tuesday|wednesday|thursday|friday|saturday|sunday)\b",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);

    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        result = default;
        var match = Pattern.Match(input.Trim());
        if (!match.Success)
            return false;

        var prefix = match.Groups[1].Value.ToLower(); // "last", "next", "this" or ""
        var dayOfWeekText = match.Groups[2].Value.ToLower();

        if (!Enum.TryParse(typeof(DayOfWeek), Capitalize(dayOfWeekText), out var dayObj))
            return false;

        var targetDay = (DayOfWeek)dayObj;
        var today = referenceDate.Date;
        var currentDay = today.DayOfWeek;

        int daysToAdd;

        switch (prefix)
        {
            case "last":
                daysToAdd = ((int)targetDay - (int)currentDay - 7) % 7;
                if (daysToAdd >= 0)
                    daysToAdd -= 7;
                result = today.AddDays(daysToAdd);
                return true;

            case "this":
                daysToAdd = ((int)targetDay - (int)currentDay + 7) % 7;
                result = today.AddDays(daysToAdd == 0 ? 0 : daysToAdd);
                return true;

            default:
                // No prefix — if today is the target day, return today, else next occurrence
                daysToAdd = ((int)targetDay - (int)currentDay + 7) % 7;
                result = today.AddDays(daysToAdd == 0 ? 0 : daysToAdd);
                return true;
        }
    }

    private static string Capitalize(string input) =>
        string.IsNullOrEmpty(input)
            ? input
            : char.ToUpperInvariant(input[0]) + input.Substring(1);
}