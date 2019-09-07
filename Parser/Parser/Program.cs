using System;
using System.Text.RegularExpressions;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("challenge-calulator");
            Console.WriteLine("Author: Ali Shemirani");
            Console.WriteLine("Description: application receives formula based on rules defined https://github.com/restaurant365/challenge-calculator" +
                Environment.NewLine + "and cacluate the result. it also receives accepts below arguments that must be separated with a space. you can mix and match every combination of available arguments" + Environment.NewLine);
            Console.WriteLine("--max={number} specifiy the maximum allowed number in formulas if it goes over the value specified it converts to 0, default value is 1000");
            Console.WriteLine("--delimiter={character} specify an alternative delimiter in formulas it is one character, default value is \\n");
            Console.WriteLine("--negative={true/false} specify whether application should process negative values, default value is false");
            Console.WriteLine("--operation={Add/Multiplication/Division/Substrac} select what operation should be performed for formula, default operation is Add");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Examples:");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("2,3,4,5 --max=4 --operation=Multiplication");
            Console.WriteLine("The result is 0" + Environment.NewLine);

            string input = Regex.Unescape(Console.ReadLine());
            while (!string.IsNullOrEmpty(input)) {

                ParserBuilder parserBuilder = new ParserBuilder();
                string[] argumentParts = input.Split(" ");
                for (int index=1; index < argumentParts.Length; index++)
                {
                    string argument = argumentParts[index];
                    if (argument.StartsWith("--max=", StringComparison.Ordinal))
                    {
                        int maxValue = int.Parse(argument.Substring("--max=".Length));
                        parserBuilder.SetMaxValue(maxValue);
                    }
                    else if (argument.StartsWith("--delimiter=", StringComparison.Ordinal))
                    {
                        char delimiter = Regex.Unescape(argument.Substring("--delimiter=".Length))[0];
                        parserBuilder.SetCustomDelimiter(delimiter);
                    }
                    else if (argument.StartsWith("--negative=", StringComparison.Ordinal))
                    {
                        bool parseNegative = bool.Parse(argument.Substring("--negative=".Length));
                        parserBuilder.SetParseNegative(parseNegative);
                    }
                    else if (argument.StartsWith("--operation=", StringComparison.Ordinal))
                    {
                        Operation operation = (Operation)Enum.Parse(typeof(Operation), argument.Substring("--operation=".Length));
                        parserBuilder.SetOperation(operation);
                    }
                }
                if (argumentParts.Length >= 1)
                {
                    string expression = argumentParts[0];
                    IParser parser = parserBuilder.Build(expression);
                    int result = parser.CalculateExpression(expression);
                    Console.WriteLine($"The result is: {result}");
                }

                input = Regex.Unescape(Console.ReadLine());
            }
            
        }
    }
}
