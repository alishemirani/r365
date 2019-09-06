using System;
namespace Parser
{
    public interface IParser
    {
        bool CanParse(string expression);
        int CalculateExpression(string expression);
    }
}
