using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NaturalDateParsing
{
    internal static class DayOfWeekParser
    {
        public static bool TryParse(string input, out DateTime result)
        {
            result = default;
            var lower = input.ToLowerInvariant();
            var now = DateTime.Now;

            var dowMatch = Regex.Match(lower, @"(next|last)?\s*(monday|tuesday|wednesday|thursday|friday|saturday|sunday)");
            if (!dowMatch.Success)
                return false;

            var direction = dowMatch.Groups[1].Value;
            var dayName = dowMatch.Groups[2].Value;
            var targetDay = Enum.Parse<DayOfWeek>(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(dayName));
            int diff = (targetDay - now.DayOfWeek + 7) % 7;
            if (direction == "last") diff -= 7;
            if (direction == "next" && diff == 0) diff = 7;
            result = now.Date.AddDays(diff);

            // Check for time (e.g. "at 5pm")
            var timeMatch = Regex.Match(lower, @"at\s+([\w: ]+)$");
            if (timeMatch.Success && TimeParser.TryParse(timeMatch.Groups[1].Value, out var time))
                result = result.Date + time;

            result = TimeZoneHelper.ApplyTimeZoneOffset(input, result);
            return true;
        }
    }
}
