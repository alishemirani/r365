using System;
using System.Collections.Generic;
using Parser.Exceptions;


namespace Parser.Processor
{
    public class NegativeNumberValueProcessor : IValueProcessor
    {
        public void Process(List<string> tokens)
        {
            List<string> invalidTokens = new List<string>();
            tokens.ForEach(t =>
            {
                if (IsNegativeValue(t))
                    invalidTokens.Add(t);
            });
            if (invalidTokens.Count > 0)
            {
                var invalidNumbers = string.Join(", ", invalidTokens);
                throw new NegativeNumberException($"invalid negative numbers {invalidNumbers}");
            }
        }

        private bool IsNegativeValue(string value)
        {
            int val;
            return int.TryParse(value, out val) && val < 0;
        }
    }
}
