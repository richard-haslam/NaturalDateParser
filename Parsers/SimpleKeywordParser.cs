using System;

namespace NaturalDateParsing;

internal class SimpleKeywordParser : INaturalDateParser
{
    public bool TryParse(string input, out DateTime result)
    {
        switch (input.ToLower())
        {
            case "today": { result = DateTime.Now.Date; return true; }
            case "tomorrow": { result = DateTime.Now.AddDays(1).Date; return true; }
            case "yesterday": { result = DateTime.Now.AddDays(-1).Date; return true; }
            default: { result = default;  return false; }
        }
    }
}