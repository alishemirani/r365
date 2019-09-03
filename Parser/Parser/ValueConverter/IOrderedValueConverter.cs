using System;
namespace Parser.ValueConverter
{
    public interface IOrderedValueConverter : IValueConverter
    {
        int Order { get; }
    }
}
