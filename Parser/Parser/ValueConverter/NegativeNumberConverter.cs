using System;
using Parser.Exceptions;

namespace Parser.ValueConverter
{
    public class NegativeNumberConverter : IEachValueConverter
    {
        public bool CanConvert(string value)
		{
            var val = 0;
			return int.TryParse(value, out val) && val < 0;
		}

		public int GetValue(string value)
		{
            var val = int.Parse(value);
            if (val < 0)
                throw new NegativeNumberException(val);
            return val;
        }
	}
}
