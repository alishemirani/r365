using System;
namespace Parser.ValueConverter
{
    public interface IProcessingStrategyAware
    {
        ProcessingStrategy Strategy { get; }
    }
}
