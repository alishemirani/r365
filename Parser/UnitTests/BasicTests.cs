using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Parser;
using Parser.ValueConverter;

namespace Tests
{
    public class BasicTests
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
            parser = new SimpleAdditionParser(valueConverters);
        }

        [Test]
        public void TestValidExpressionWithDelimiter()
        {
            int result = parser.CalculateExpression("2,2");
            Assert.AreEqual(4, result);
        }

        [Test]
        public void TestSingleNumberExpression()
        {
            int result = parser.CalculateExpression("5000");
            Assert.AreEqual(5000, result);
        }

        [Test]
        public void TestExpression_OneNumber_String()
        {
            int result = parser.CalculateExpression("5,tytyt");
            Assert.AreEqual(5, result);
        }

        [Test]
        public void TestExpression_OneNumber_BlankString()
        {
            int result = parser.CalculateExpression("5,");
            Assert.AreEqual(5, result);
        }

        [Test]
        public void TestExpression_MultipleNumbers()
        {
            int result = parser.CalculateExpression("5,6,4");
            Assert.AreEqual(15, result);
        }

        [Test]
        public void TestExpression_MultipleNumbers1()
        {
            int result = parser.CalculateExpression("1,2,3,4,5,6,7,8,9,10,11,12");
            Assert.AreEqual(78, result);
        }

        [Test]
        public void TestExpression_EmptyExpression()
        {
            int result = parser.CalculateExpression("");
            Assert.AreEqual(0, result);
        }

    }
}