using System;
using System.Collections.Generic;
using System.Globalization;

namespace NaturalDateParsing;

internal class ExactFormatParser : INaturalDateParser
{
    private static readonly List<string> _customFormats =
    [
        "dd/MM/yyyy",
        "d/M/yyyy",
        "dd/MM/yy",
        "d/M/yy",
        "yyyy-MM-dd",
        "yyyy/MM/dd",
        "MM/dd/yyyy",
        "M/d/yyyy",
        "MM-dd-yyyy",
        "M-d-yyyy",
        "dd-MM-yyyy",
        "d-M-yyyy",
        "dd-MM-yy",
        "d-M-yy",
        "yyyy-MM-dd HH:mm",
        "dd/MM/yyyy HH:mm",
        "MM/dd/yyyy hh:mm tt",
        "dd/MM/yyyy hh:mm tt",
        "yyyy-MM-ddTHH:mm",
        "HH:mm dd/MM/yyyy",
        "HH:mm:ss dd/MM/yyyy",
        "yyyyMMddHHmmss"
    ];

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, out result);

    public bool TryParse(string input, out DateTime result)
    {
        foreach (var format in _customFormats)
        {
            if (DateTime.TryParseExact(
                input,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out result))
            {
                return true;
            }
        }

        result = default;
        return false;
    }
}