using System;
using System.Collections.Generic;
using Parser.Processor;
using Parser.ValueConverter;

namespace Parser
{
    public class MultipleCustomRegexParser : RegexParser
    {
        public MultipleCustomRegexParser(string alternativeDelimiter) : base(GetRegex(alternativeDelimiter), alternativeDelimiter)
        {
        }
        public MultipleCustomRegexParser(List<IValueConverter> converters, List<IValueProcessor> processors, string alternativeDelimiter) :
            base(GetRegex(alternativeDelimiter), converters, processors, alternativeDelimiter)
        { }

        private static string GetRegex(string alternativeDelimiter)
        {
            string delim = string.IsNullOrEmpty(alternativeDelimiter) ? "\n" : alternativeDelimiter;
            return $"//((?<delimiter>(\\D))|\\[(?<delimiter>[^\\]]*)\\]|(\\[(?<delimiter>[^\\]])*\\])+){delim}(?<numbers>(.*))";
        }
    }
}
