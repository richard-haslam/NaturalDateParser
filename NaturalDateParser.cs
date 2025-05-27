using System;

namespace NaturalDateParsing
{
    public static class NaturalDateParser
    {
        public static bool TryParse(string input, out DateTime result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            // 1. Relative dates
            if (RelativeDateParser.TryParse(input, out result))
                return true;

            // 2. Days of week like "next Friday"
            if (DayOfWeekParser.TryParse(input, out result))
                return true;

            // 3. Time only, like "5pm"
            if (TimeParser.TryParse(input, out var time))
            {
                var now = DateTime.Now.Date;
                result = now + time;
                return true;
            }

            // 4. Exact date/time formats
            if (ExactFormatParser.TryParse(input, out result))
            {
                result = TimeZoneHelper.ApplyTimeZoneOffset(input, result);
                return true;
            }

            // 5. Fallback to DateTime.TryParse with culture info
            if (DateTime.TryParse(input, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.AssumeLocal, out result) ||
                DateTime.TryParse(input, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out result))
            {
                result = TimeZoneHelper.ApplyTimeZoneOffset(input, result);
                return true;
            }

            return false;
        }

        public static void AddCustomFormat(string format) => ExactFormatParser.AddCustomFormat(format);
    }
}