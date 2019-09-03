using System;
using System.Collections.Generic;
using NUnit.Framework;
using Parser;
using Parser.Exceptions;
using Parser.Processor;
using Parser.ValueConverter;

namespace UnitTests
{
    public class NegativeNumberTests
    {
        private IParser parser;

        [SetUp]
        public void Setup()
        {
            var valueConverters = new List<IValueConverter>()
            {
                new NumberConverter(),
                new InvalidNumberConverter()
            };
            var valueProcessors = new List<IValueProcessor>()
            {
                new NegativeNumberValueProcessor()
            };
            parser = new SimpleAdditionParser(
                new List<char> { ',', '\n' },
                valueConverters,
                valueProcessors);
        }

        [Test]
        public void TestNegativeNumbers()
        {
            Assert.Throws<Exception>(delegate
                {
                    parser.calculateExpression("-4\n-2\n3");
                }, "invalid negative numbers - 4, -2\n");
        }
    }
}
