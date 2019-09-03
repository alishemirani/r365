using System;
using System.Collections.Generic;
using Parser.ValueConverter;
using System.Linq;
using Parser.Processor;

namespace Parser
{
    public class SimpleAdditionParser : IParser
    {
        private readonly List<char> delimiters;
        private readonly List<IValueConverter> converters;
        private readonly List<IValueProcessor> valueProcessors;

        public SimpleAdditionParser(List<char> delimiters, List<IValueConverter> converters) : this(delimiters, converters, new List<IValueProcessor>()) { }

        public SimpleAdditionParser(List<char> delimiters, List<IValueConverter> converters, List<IValueProcessor> processors)
        {
            this.delimiters = delimiters;
            this.converters = converters;
            this.converters.Sort((a, b) => a.Order.CompareTo(b.Order));
            this.valueProcessors = processors;
        }


        public int calculateExpression(string expression)
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
            List<string> tokens = new List<string>();
            var prevIndex = 0;
            var index = expression.IndexOfAny(delimiters.ToArray());
            while (index >= 0)
            {
                tokens.Add(expression.Substring(prevIndex, index - prevIndex));
                prevIndex = index + 1;
                index = expression.IndexOfAny(delimiters.ToArray(), prevIndex);
            }
            if (prevIndex < expression.Length)
            {
                tokens.Add(expression.Substring(prevIndex, expression.Length - prevIndex));
            }
            return tokens;
        }
    }
}
