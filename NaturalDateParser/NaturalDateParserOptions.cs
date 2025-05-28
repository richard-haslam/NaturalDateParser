using System;

namespace NaturalDateParsing;

/// <summary>
/// Specifies options that influence how natural language dates are parsed.
/// </summary>
public record NaturalDateParserOptions
{
    /// <summary>
    /// Gets or sets the reference date used for relative date expressions (e.g., "tomorrow", "in 3 days").
    /// </summary>
    public DateTime? ReferenceDate { get; set; } = null;
}