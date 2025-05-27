using System;
using System.Text.RegularExpressions;

namespace NaturalDateParsing;

internal class InParser : INaturalDateParser
{
    private static readonly Regex InRegex = new(@"in\s+(\d+)\s+(day|week|month|year)s?", RegexOptions.IgnoreCase);

    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);

    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        result = default;
        var match = InRegex.Match(input);

        if (!match.Success)
            return false;

        int value = int.Parse(match.Groups[1].Value);
        string unit = match.Groups[2].Value.ToLower();

        result = unit switch
        {
            "day" => referenceDate.AddDays(value),
            "week" => referenceDate.AddDays(value * 7),
            "month" => referenceDate.AddMonths(value),
            "year" => referenceDate.AddYears(value),
            _ => referenceDate
        };

        return true;
    }
}
