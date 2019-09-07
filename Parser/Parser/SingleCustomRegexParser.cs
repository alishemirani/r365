using System;
using System.Collections.Generic;
using Parser.Processor;
using Parser.ValueConverter;

namespace Parser
{
    public class SingleCustomRegexParser : RegexParser
    {
        public SingleCustomRegexParser(string alternativeDelimiter) : base(GetRegex(alternativeDelimiter), alternativeDelimiter)
        {
        }
        public SingleCustomRegexParser(List<IValueConverter> converters, List<IValueProcessor> processors, string alternativeDelimiter) : this(
            converters, processors, alternativeDelimiter, Operation.Add)
        { }
        public SingleCustomRegexParser(List<IValueConverter> converters, List<IValueProcessor> processors, string alternativeDelimiter, Operation operation) :
            base(GetRegex(alternativeDelimiter), converters, processors, alternativeDelimiter, operation)
        { }

        private static string GetRegex(string alternativeDelimiter)
        {
            string delim = string.IsNullOrEmpty(alternativeDelimiter) ? "\n" : alternativeDelimiter;
            return $"//((?<delimiter>(\\D))|\\[(?<delimiter>[^\\]]*)\\]){delim}(?<numbers>(.*))";
        }
    }
}
