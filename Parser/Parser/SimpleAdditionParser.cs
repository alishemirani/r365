using System;
using System.Collections.Generic;
using Parser.ValueConverter;
using System.Linq;

namespace Parser
{
    public class SimpleAdditionParser : IParser
    {
        private readonly char delimiter;
        private readonly List<IValueConverter> converters;

        public SimpleAdditionParser(char delimiter, List<IValueConverter> converters)
        {
            this.delimiter = delimiter;
            this.converters = converters;
        }

        public int calculateExpression(string expression)
        {
            var tokens = expression.Split(delimiter);
            return tokens.Select(t =>
                converters
                .First(p => p.CanConvert(t)).GetValue(t))
                .Sum();
        }
    }
}
