using System;
using System.Text.RegularExpressions;

namespace NaturalDateParsing
{
    internal static class RelativeDateParser
    {
        public static bool TryParse(string input, out DateTime result)
        {
            result = default;

            var lower = input.ToLowerInvariant();
            var now = DateTime.Now;

            var relativeMatch = Regex.Match(lower, @"(\d+)\s+(second|minute|hour|day|week|month|year)s?\s+(ago|from now|later)");
            if (relativeMatch.Success)
            {
                int value = int.Parse(relativeMatch.Groups[1].Value);
                string unit = relativeMatch.Groups[2].Value;
                string direction = relativeMatch.Groups[3].Value;

                TimeSpan offset = unit switch
                {
                    "second" => TimeSpan.FromSeconds(value),
                    "minute" => TimeSpan.FromMinutes(value),
                    "hour" => TimeSpan.FromHours(value),
                    "day" => TimeSpan.FromDays(value),
                    "week" => TimeSpan.FromDays(7 * value),
                    "month" => TimeSpan.FromDays(30 * value),
                    "year" => TimeSpan.FromDays(365 * value),
                    _ => TimeSpan.Zero
                };

                result = direction.Contains("ago") ? now - offset : now + offset;
                return true;
            }

            // Handle simple keywords:
            switch (lower)
            {
                case "now":
                    result = now;
                    return true;
                case "today":
                    result = now.Date;
                    return true;
                case "tomorrow":
                    result = now.Date.AddDays(1);
                    return true;
                case "yesterday":
                    result = now.Date.AddDays(-1);
                    return true;
            }

            return false;
        }
    }
}