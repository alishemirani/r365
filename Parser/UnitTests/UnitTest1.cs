using System.Collections.Generic;
using NUnit.Framework;
using Parser;
using Parser.ValueConverter;

namespace Tests
{
    public class Tests
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
            parser = new SimpleAdditionParser(',', valueConverters);
        }

        [Test]
        public void TestValidExpressionWithDelimiter()
        {
            int result = parser.calculateExpression("2,2");
            Assert.AreEqual(4, result);
        }

        [Test]
        public void TestSingleNumberExpression()
        {
            int result = parser.calculateExpression("5000");
            Assert.AreEqual(5000, result);
        }

        [Test]
        public void TestExpression_OneNumber_String()
        {
            int result = parser.calculateExpression("5,tytyt");
            Assert.AreEqual(5, result);
        }

        [Test]
        public void TestExpression_OneNumber_BlankString()
        {
            int result = parser.calculateExpression("5,");
            Assert.AreEqual(5, result);
        }
    }
}