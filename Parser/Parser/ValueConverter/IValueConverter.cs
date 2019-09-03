using System;
namespace Parser.ValueConverter
{
    public interface IValueConverter
    {
        int Order { get; }
        bool CanConvert(string value);
        int GetValue(string value);
    }
}
