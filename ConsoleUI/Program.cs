using System;
using MathExpressionEvaluator;
using MathProject.DataLayer.Context;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DataBaseContext();

            var expressionEvaluator = new ExpressionEvaluator(context);

            Console.WriteLine("Enter a math Expression: ");
            var input = Console.ReadLine();

            var result = expressionEvaluator.Evaluate(input);

            Console.WriteLine();
            Console.WriteLine("Result is : " + result);
        }
    }
}
