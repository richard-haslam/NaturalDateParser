using System;
using System.Text.RegularExpressions;

namespace NaturalDateParsing;

internal class DateTimeCombinationParser : INaturalDateParser
{
    // Matches e.g. "tomorrow at 5pm", "next Friday 08:00", "25/05/2025 15:30"
    private static readonly Regex DateTimeRegex = new(@"^(?<date>.+?)\s+(at\s+)?(?<time>\d{1,2}(:\d{2})?\s*(am|pm)?|noon|midnight)$", RegexOptions.IgnoreCase);

    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);

    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        var match = DateTimeRegex.Match(input.Trim());
        if (!match.Success)
        {
            result = default;
            return false;
        }

        string datePart = match.Groups["date"].Value.Trim();
        string timePart = match.Groups["time"].Value.Trim();

        // Try parse date part
        if (!NaturalDateParser.TryParse(datePart, new() { ReferenceDate = referenceDate }, out var date))
        {
            result = default;
            return false;
        }

        // Try parse time part
        if (!NaturalDateParser.TryParse(timePart, new() { ReferenceDate = date }, out var time))
        {
            result = default;
            return false;
        }

        // Combine date and time
        result = date.Date.AddHours(time.Hour).AddMinutes(time.Minute).AddSeconds(time.Second);
        return true;
    }
}