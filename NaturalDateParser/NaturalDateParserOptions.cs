using System;

namespace NaturalDateParsing;

public record NaturalDateParserOptions
{
    public DateTime? ReferenceDate { get; set; } = null;
}