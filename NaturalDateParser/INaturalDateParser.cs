using System;

namespace NaturalDateParsing;

/// <summary>
/// Defines a contract for implementing custom natural language date parsers.
/// </summary>
public interface INaturalDateParser
{
    /// <summary>
    /// Attempts to parse a natural language date expression into a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="input">The input string representing a natural language date (e.g., "tomorrow", "next Friday").</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="DateTime"/> if successful; otherwise, the default value.</param>
    /// <returns><c>true</c> if the parsing succeeded; otherwise, <c>false</c>.</returns>
    public bool TryParse(string input, out DateTime result);

    /// <summary>
    /// Attempts to parse a natural language date expression into a <see cref="DateTime"/>, using the provided parser options.
    /// </summary>
    /// <param name="input">The input string representing a natural language date.</param>
    /// <param name="options">Parsing options such as the reference date or time zone.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="DateTime"/> if successful; otherwise, the default value.</param>
    /// <returns><c>true</c> if the parsing succeeded; otherwise, <c>false</c>.</returns>
    public bool TryParse(string input, NaturalDateParserOptions options, out DateTime result);
}