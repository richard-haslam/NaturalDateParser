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