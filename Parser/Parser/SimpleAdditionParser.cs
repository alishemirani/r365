using System;
using System.Collections.Generic;
using Parser.ValueConverter;
using System.Linq;
using Parser.Processor;

namespace Parser
{
    public class SimpleAdditionParser : IParser
    {
        private readonly List<string> delimiters;
        private readonly List<IValueConverter> converters;
        private readonly List<IValueProcessor> valueProcessors;

        public SimpleAdditionParser(List<IValueConverter> converters) : this(converters, new List<string>()) { }

        public SimpleAdditionParser(List<IValueConverter> converters, string alternativeDelimiter) : this(converters, new List<string> { alternativeDelimiter }) { }

        public SimpleAdditionParser(List<IValueConverter> converters, List<string> alternativeDelimiter) : this(converters, new List<IValueProcessor>(), alternativeDelimiter) { }

        public SimpleAdditionParser(List<IValueConverter> converters, List<IValueProcessor> processors, string alternativeDelimiter) :
            this(converters, processors, new List<string> { alternativeDelimiter })
        { }

        public SimpleAdditionParser(List<IValueConverter> converters, List<IValueProcessor> processors):
            this(converters, processors, new List<string>()) { }

        public SimpleAdditionParser(List<IValueConverter> converters, List<IValueProcessor> processors, List<string> alternativeDelimiter) 
        {
            this.delimiters = new List<string> { "," };
            if (alternativeDelimiter != null)
                delimiters.AddRange(alternativeDelimiter);
            this.converters = converters;
            this.converters.Sort((a, b) => b.Order.CompareTo(a.Order));
            this.valueProcessors = processors;
        }

        public int CalculateExpression(string expression)
        {
            var tokens = GetTokens(expression);
            var exceptionMessages = "";

            valueProcessors.ForEach(t =>
            {
                try
                {
                    t.Process(tokens);
                }
                catch(Exception ex)
                {
                    exceptionMessages += ex.Message + Environment.NewLine;
                }
            });

            if (exceptionMessages.Length > 0)
            {
                throw new Exception(exceptionMessages);
            }

            return tokens.Select(t =>
                        converters
                        .First(p => p.CanConvert(t)).GetValue(t))
                        .Sum();
        }

        private List<string> GetTokens(string expression)
        {
            return expression.Split(delimiters.ToArray(), StringSplitOptions.None).ToList();
        }

        public bool CanParse(string expression)
        {
            return true;
        }
    }
}
