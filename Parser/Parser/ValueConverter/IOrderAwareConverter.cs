using System;
namespace Parser.ValueConverter
{
    public interface IOrderAwareConverter
    {
        int Order { get; }
    }
}
