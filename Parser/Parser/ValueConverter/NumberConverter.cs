using System;
namespace Parser.ValueConverter
{
    public class NumberConverter : IValueConverter
    {
        public bool CanConvert(string value)
        {
            return int.TryParse(value, out _);
        }

        public int GetValue(string value)
        {
            return int.Parse(value);
        }
    }
}
