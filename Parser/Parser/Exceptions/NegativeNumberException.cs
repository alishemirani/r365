using System;
namespace Parser.Exceptions
{
    public class NegativeNumberException : Exception
    {
        public NegativeNumberException(string message) : base(message)
        {

		}
	}
}
