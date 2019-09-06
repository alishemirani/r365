using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
                new InvalidNumberConverter(),
                new MaximumNumberConverter(1000)
            };
            parser = new SimpleAdditionParser(valueConverters, Regex.Unescape("\n"));
        }

        [Test]
        public void TestMultiDelimiter()
        {
            int result = parser.CalculateExpression(Regex.Unescape("1\n2,3"));
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test_Bounds()
        {
            int result = parser.CalculateExpression("2,1001,6");
            Assert.AreEqual(8, result);
        }
        [Test]
        public void Test_Stretch_Goal1()
        {
            int result = parser.CalculateExpression("2,4,rrrr,1001,6");
            Assert.AreEqual(12, result);
        }
    }
}
