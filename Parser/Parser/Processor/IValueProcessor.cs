using System;
using System.Collections.Generic;

namespace Parser.Processor
{
    public interface IValueProcessor
    {
        void Process(List<string> tokens);
    }
}
