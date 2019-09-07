using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using Parser.ValueConverter;
using Parser.Processor;
using Parser.Exceptions;

namespace Parser
{
    public abstract class RegexParser : IParser
    {
        private readonly Regex regex;
        private readonly List<IValueConverter> converters;
        private readonly List<IValueProcessor> valueProcessors;
        private readonly string alternativeDelimiter;
        private readonly Operation operation;

        protected RegexParser(string regexPattern, string alternativeDelimiter) : this(regexPattern,
            new List<IValueConverter>
            {
                new NumberConverter(),
                new InvalidNumberConverter(),
                new MaximumNumberConverter(1000)
            }, new List<IValueProcessor>
            {
                new NegativeNumberValueProcessor()
            }, alternativeDelimiter, Operation.Add)
        { }

        protected RegexParser(string regexPattern, List<IValueConverter> converters, List<IValueProcessor> processors, string alternativeDelimiter, Operation operation)
        {
            this.alternativeDelimiter = alternativeDelimiter;
            this.regex = new Regex(regexPattern, RegexOptions.ExplicitCapture);
            this.valueProcessors = processors;
            this.converters = converters;
            this.operation = operation;
        }


        public int CalculateExpression(string expression)
        {
            IParser additionParser = null;
            string exp = expression;
            List<string> delimiters = new List<string>();
            if (!string.IsNullOrEmpty(alternativeDelimiter))
                delimiters.Add(alternativeDelimiter);

            if (regex != null && regex.IsMatch(expression))
            {
                var match = regex.Match(expression);
                var delims = match.Groups.Where(t => t.Name.Equals("delimiter", StringComparison.InvariantCultureIgnoreCase))
                    .SelectMany(t => t.Captures.Select(m=>m.Value))
                    .ToList();

                delimiters.AddRange(delims);

                additionParser = new SimpleOperationParser(converters, valueProcessors, delimiters, operation);
                exp = match.Groups["numbers"].Value;
            }
            else
            {
                additionParser = new SimpleOperationParser(converters, valueProcessors, delimiters, operation);
            }

            return additionParser.CalculateExpression(exp);
        }

        public bool CanParse(string expression)
        {
            return regex.IsMatch(expression);
        }
    }
}
