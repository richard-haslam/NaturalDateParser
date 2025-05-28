using System;

namespace NaturalDateParsing;

public interface INaturalDateParser
{
    public bool TryParse(string input, out DateTime result);
    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result);
}