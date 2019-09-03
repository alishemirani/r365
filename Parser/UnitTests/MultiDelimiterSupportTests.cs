using System;
using System.Collections.Generic;
using NUnit.Framework;
using Parser;
using Parser.ValueConverter;

namespace UnitTests
{
    public class MultiDelimiterSupportTests
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
            parser = new SimpleAdditionParser(new List<char> { ',', '\n' }, valueConverters);
        }

        [Test]
        public void TestMultiDelimiter()
        {
            int result = parser.calculateExpression("1\n2,3");
            Assert.AreEqual(6, result);
        }
    }
}
