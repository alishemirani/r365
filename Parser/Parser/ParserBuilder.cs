using System;
using System.Collections.Generic;
using Parser.Processor;
using Parser.ValueConverter;

namespace Parser
{
    public class ParserBuilder
    {
        private List<IValueConverter> converters;
        private List<IValueProcessor> processors;
        private int defaultMaxValue = 1000;
        private Operation defaultOperation = Operation.Add;
        private bool defaultParseNegative = true;
        private char customDelimiter = '\n';

        public ParserBuilder()
        {
            converters = new List<IValueConverter> {
                new NumberConverter(),
                new InvalidNumberConverter() };

            processors = new List<IValueProcessor>();
        }

        public void SetMaxValue(int maxValue)
        {
            defaultMaxValue = maxValue;
        }

        public void SetOperation(Operation operation)
        {
            defaultOperation = operation;
        }

        public void SetParseNegative(bool parseNegative)
        {
            defaultParseNegative = parseNegative;
        }

        public void SetCustomDelimiter(char customDelimiter)
        {
            this.customDelimiter = customDelimiter;
        }

        public IParser Build(string expression)
        {
            converters.Add(new MaximumNumberConverter(defaultMaxValue));
            if (defaultParseNegative)
                processors.Add(new NegativeNumberValueProcessor());


            SimpleRegexParser simpleParser = new SimpleRegexParser(converters, processors, customDelimiter.ToString(), defaultOperation);
            if (simpleParser.CanParse(expression))
                return simpleParser;

            SingleCustomRegexParser singleParser = new SingleCustomRegexParser(converters, processors, customDelimiter.ToString(), defaultOperation);
            if (singleParser.CanParse(expression))
                return singleParser;

            MultipleCustomRegexParser multipleParser = new MultipleCustomRegexParser(converters, processors, customDelimiter.ToString(), defaultOperation);
            if (multipleParser.CanParse(expression))
                return multipleParser;

            SimpleOperationParser operationParser = new SimpleOperationParser(converters, processors, customDelimiter.ToString(), defaultOperation);
            return operationParser;
        }
    }
}
