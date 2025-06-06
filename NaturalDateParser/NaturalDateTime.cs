using System;

namespace NaturalDateParsing;

public class NaturalDateTime
{
    private DateTime _dateTime;
    private string _originalString;


    public NaturalDateTime(string naturalDatetimeString)
    {
        _originalString = naturalDatetimeString;

        if (!NaturalDateParser.TryParse(naturalDatetimeString, out _dateTime))
        {
            throw new FormatException($"Unable to parse '{naturalDatetimeString}' as a natural date.");
        }
    }

    public static explicit operator DateTime(NaturalDateTime naturalDateTime) => naturalDateTime._dateTime;
    public static explicit operator DateOnly(NaturalDateTime naturalDateTime) => DateOnly.FromDateTime(naturalDateTime._dateTime);

    public DateTime DateTime => _dateTime;
    public string OriginalString => _originalString;
}