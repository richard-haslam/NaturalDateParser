using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NaturalDateParsing
{
    internal static class TimeParser
    {
        public static bool TryParse(string input, out TimeSpan timeOfDay)
        {
            timeOfDay = default;

            var timeInput = input.ToLowerInvariant().Trim();

            if (timeInput == "noon")
            {
                timeOfDay = new TimeSpan(12, 0, 0);
                return true;
            }
            else if (timeInput == "midnight")
            {
                timeOfDay = TimeSpan.Zero;
                return true;
            }

            timeInput = Regex.Replace(timeInput, @"\s+", "");

            string[] formats = {
                "h:mmtt", "htt", "hh:mmtt", "hmmtt",
                "H:mm", "HH:mm", "H", "HH", "Hmm"
            };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(timeInput, format, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var time))
                {
                    timeOfDay = time.TimeOfDay;
                    return true;
                }
            }

            if (DateTime.TryParse(timeInput, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fallback))
            {
                timeOfDay = fallback.TimeOfDay;
                return true;
            }

            return false;
        }
    }
}