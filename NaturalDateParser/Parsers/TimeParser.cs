using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NaturalDateParsing;

internal class TimeParser : INaturalDateParser
{
    private static readonly Regex TimeRegex = new(@"^(?<hour>\d{1,2})(:(?<minute>\d{2}))?\s*(?<ampm>am|pm)?$", RegexOptions.IgnoreCase);

    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);

    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        input = input.Trim().ToLowerInvariant();
        result = default;

        if (input == "noon")
        {
            result = referenceDate.Date.AddHours(12);
            return true;
        }
        if (input == "midnight")
        {
            result = referenceDate.Date;
            return true;
        }

        var match = TimeRegex.Match(input);
        if (!match.Success)
            return false;

        int hour = int.Parse(match.Groups["hour"].Value);
        int minute = match.Groups["minute"].Success ? int.Parse(match.Groups["minute"].Value) : 0;
        string ampm = match.Groups["ampm"].Value.ToLower();

        if (!string.IsNullOrEmpty(ampm))
        {
            if (ampm == "pm" && hour < 12) hour += 12;
            if (ampm == "am" && hour == 12) hour = 0;
        }

        if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60)
        {
            result = referenceDate.Date.AddHours(hour).AddMinutes(minute);
            return true;
        }

        return false;
    }
}