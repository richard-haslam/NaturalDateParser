using System;
using System.Text.RegularExpressions;

namespace NaturalDateParsing;

internal class AgoParser : INaturalDateParser
{
    private static readonly Regex AgoRegex = new(@"(\d+)\s+(day|week|month|year)s?\s+ago", RegexOptions.IgnoreCase);

    public bool TryParse(string input, out DateTime result)
    {
        result = default;
        var match = AgoRegex.Match(input);

        if (!match.Success)
            return false;

        int value = int.Parse(match.Groups[1].Value);
        string unit = match.Groups[2].Value.ToLower();

        var now = DateTime.Now;

        result = unit switch
        {
            "day" => now.AddDays(-value),
            "week" => now.AddDays(-value * 7),
            "month" => now.AddMonths(-value),
            "year" => now.AddYears(-value),
            _ => now
        };

        return true;
    }
}