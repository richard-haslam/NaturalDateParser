using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NaturalDateParsing
{
    public static class NaturalDateParser
    {
        private static List<INaturalDateParser> _naturalDateParsers;

        static NaturalDateParser() => _naturalDateParsers = DiscoverParsers();

        /// <summary>
        /// Attempts to parse a natural language date expression into a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="input">The natural language date expression (e.g., "today", "next Friday").</param>
        /// <param name="result">When this method returns, contains the parsed <see cref="DateTime"/> if parsing was successful; otherwise, the default value.</param>
        /// <returns><c>true</c> if the parsing succeeded; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string input, out DateTime result)
        {
            input = input.Trim();
            result = default;

            if (string.IsNullOrEmpty(input)) return false;

            foreach (var parser in _naturalDateParsers)
            {
                if (parser.TryParse(input, out result))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Attempts to parse a natural language date expression into a <see cref="DateTime"/>, using custom parser options.
        /// </summary>
        /// <param name="input">The natural language date expression (e.g., "in 3 days", "last Monday").</param>
        /// <param name="options">Custom options that influence how the date is parsed, such as a reference date or locale settings.</param>
        /// <param name="result">When this method returns, contains the parsed <see cref="DateTime"/> if parsing was successful; otherwise, the default value.</param>
        /// <returns><c>true</c> if the parsing succeeded; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string input, NaturalDateParserOptions options, out DateTime result)
        {
            input = input.Trim();
            result = default;

            if (string.IsNullOrEmpty(input)) return false;

            foreach (var parser in _naturalDateParsers)
            {
                if (parser.TryParse(input, options, out result))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Registers a custom natural date parser to extend the parsing logic.
        /// </summary>
        /// <param name="naturalDateParser">An implementation of <see cref="INaturalDateParser"/> to be added to the parser chain.</param>
        public static void AddNaturalDateParser(INaturalDateParser naturalDateParser) =>
            _naturalDateParsers.Add(naturalDateParser);

        private static List<INaturalDateParser> DiscoverParsers()
        {
            var parserType = typeof(INaturalDateParser);
            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => parserType.IsAssignableFrom(type)
                               && type.IsClass
                               && !type.IsAbstract
                               && type.GetConstructor(Type.EmptyTypes) != null)
                .Select(type => (INaturalDateParser)Activator.CreateInstance(type))
                .ToList();
        }
    }
}