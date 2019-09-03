using System;
namespace Parser.ValueConverter
{
    public class NumberConverter : IValueConverter
    {
        public virtual int Order => 1;

        public virtual bool CanConvert(string value)
        {
            return int.TryParse(value, out _);
        }

        public virtual int GetValue(string value)
        {
            return int.Parse(value);
        }
    }
}
