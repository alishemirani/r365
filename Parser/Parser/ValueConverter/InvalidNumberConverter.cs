using System;
namespace Parser.ValueConverter
{
    public class InvalidNumberConverter : IValueConverter
    {
        public int Order => 3;

        public bool CanConvert(string value)
        {
            return !int.TryParse(value, out _);
        }

        public int GetValue(string value)
        {
            return 0;
        }
    }
}
