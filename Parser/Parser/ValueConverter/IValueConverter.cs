using System;
namespace Parser.ValueConverter
{
    public interface IValueConverter
    {
        bool CanConvert(string value);
        int GetValue(string value);
    }
}
