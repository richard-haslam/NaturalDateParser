using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateParsing
{
    internal static class TimeZoneHelper
    {
        private static readonly Dictionary<string, TimeSpan> _timeZoneOffsets = new(StringComparer.OrdinalIgnoreCase)
        {
            ["UTC"] = TimeSpan.Zero,
            ["PST"] = TimeSpan.FromHours(-8),
            ["PDT"] = TimeSpan.FromHours(-7),
            ["EST"] = TimeSpan.FromHours(-5),
            ["EDT"] = TimeSpan.FromHours(-4),
            ["CET"] = TimeSpan.FromHours(1),
            ["IST"] = TimeSpan.FromHours(5.5)
        };

        public static DateTime ApplyTimeZoneOffset(string input, DateTime result)
        {
            var tzMatch = Regex.Match(input, @"\b([A-Z]{2,4})\b$");
            if (tzMatch.Success && _timeZoneOffsets.TryGetValue(tzMatch.Groups[1].Value, out var offset))
            {
                // Convert result to UTC + offset (example behavior)
                result = DateTime.SpecifyKind(result, DateTimeKind.Utc).Add(offset);
            }
            return result;
        }
    }
}
