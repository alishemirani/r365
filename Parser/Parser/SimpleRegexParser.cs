using System;
using System.Collections.Generic;
using Parser.Processor;
using Parser.ValueConverter;

namespace Parser
{
    public class SimpleRegexParser : RegexParser
    {
        private const string REGEX_PATTERN = "//(?<delimiter>(.))\n(?<numbers>(.*))";
        public SimpleRegexParser(string alternativeDelimiter) : base(REGEX_PATTERN, alternativeDelimiter)
        {
        }
        public SimpleRegexParser(List<IValueConverter> converters, List<IValueProcessor> processors, string alternativeDelimiter) :
            base(REGEX_PATTERN, converters, processors, alternativeDelimiter)
        { }
    }
}
