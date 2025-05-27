using System;

namespace NaturalDateParsing;

internal class SimpleKeywordParser : INaturalDateParser
{
    public bool TryParse(string input, out DateTime result) =>
        TryParse(input, DateTime.Now, out result);

    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result) =>
        TryParse(input, options.ReferenceDate ?? DateTime.Now, out result);


    private static bool TryParse(string input, DateTime referenceDate, out DateTime result)
    {
        switch (input.ToLower())
        {
            case "today": { result = referenceDate.Date; return true; }
            case "tomorrow": { result = referenceDate.AddDays(1).Date; return true; }
            case "yesterday": { result = referenceDate.AddDays(-1).Date; return true; }
            default: { result = default; return false; }
        }
    }
}