using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Parser;
using Parser.Processor;
using Parser.ValueConverter;

namespace UnitTests
{
    public class SingleCustomRegexParserTests
    {
        private IParser defaultParser;

        [SetUp]
        public void Setup()
        {
            var valueConverters = new List<IValueConverter>()
            {
                new NumberConverter(),
                new InvalidNumberConverter(),
                new MaximumNumberConverter(1000)
            };
            var valueProcessors = new List<IValueProcessor>()
            {
                new NegativeNumberValueProcessor()
            };
            defaultParser = new SingleCustomRegexParser(
                valueConverters,
                valueProcessors,
                Regex.Unescape("\n"));
        }

        [Test]
        public void Test_Simple_Parser()
        {
            var parser = new SingleCustomRegexParser(Regex.Unescape("\n"));
            int result = parser.CalculateExpression(Regex.Unescape("//;\n2;5"));
            Assert.AreEqual(result, 7);
        }
        [Test]
        public void TestSingleCustomeParseFailOnDelimiterLengthGreaterThan1()
        {
            var parser = new SingleCustomRegexParser(Regex.Unescape("\n"));
            Assert.AreEqual(false, parser.CanParse(Regex.Unescape("//**\n2**5")));
        }
        [Test]
        public void TestSingleCustomParseSucceed()
        {
            var parser = new SingleCustomRegexParser(Regex.Unescape("\n"));
            Assert.AreEqual(true, parser.CanParse(Regex.Unescape("//[**]\n2**5")));
        }
        [Test]
        public void TestNegativeNumbers()
        {
            Assert.Throws<Exception>(delegate
            {
                defaultParser.CalculateExpression(Regex.Unescape("5\n-2\n3"));
            }, "invalid negative numbers -2\n");
        }
        [Test]
        public void TestMultiDelimiter()
        {
            int result = defaultParser.CalculateExpression(Regex.Unescape("1\n2,3"));
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test_Bounds()
        {
            int result = defaultParser.CalculateExpression(Regex.Unescape("2,1001,6"));
            Assert.AreEqual(8, result);
        }

        [Test]
        public void Test_Stretch_Goal1()
        {
            int result = defaultParser.CalculateExpression(Regex.Unescape("2,4,rrrr,1001,6"));
            Assert.AreEqual(12, result);
        }
    }
}
