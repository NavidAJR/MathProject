using MathProject.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathProject.DataLayer.Entities;

namespace MathExpressionEvaluator
{
    public class ExpressionEvaluator
    {
        private readonly DataBaseContext _context;

        public ExpressionEvaluator(DataBaseContext context)
        {
            _context = context;
        }

        public float Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new Exception("Expression can not be empty.");


            expression = StringTrimmer(expression);
            var expressionClone = expression;


            int? indexOfDivide = expression.IndexOf('/');
            int? indexOfMultiple = expression.IndexOf('*');
            if (indexOfMultiple < indexOfDivide)
            {
                var highPriorityValues1 = expression.Split(new char[] { '+', '-', '/' });
                foreach (var value in highPriorityValues1)
                {
                    if (value.Contains('*'))
                    {
                        var items = value.Split('*');
                        double result = 1;
                        foreach (string item in items)
                        {
                            result *= double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }

                var highPriorityValues2 = expressionClone.Split(new char[] { '+', '-', '*' });
                foreach (var value in highPriorityValues2)
                {
                    if (value.Contains('/'))
                    {
                        var items = value.Split('/');
                        double result = double.Parse(items[0]);
                        foreach (string item in items)
                        {
                            if(item == items[0])
                                continue;

                            result /= double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }
            }
            else
            {
                var highPriorityValues2 = expressionClone.Split(new char[] { '+', '-', '*' });
                foreach (var value in highPriorityValues2)
                {
                    if (value.Contains('/'))
                    {
                        var items = value.Split('/');
                        double result = double.Parse(items[0]);
                        foreach (string item in items)
                        {
                            if (item == items[0])
                                continue;

                            result /= double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }

                var highPriorityValues1 = expressionClone.Split(new char[] { '+', '-', '/' });
                foreach (var value in highPriorityValues1)
                {
                    if (value.Contains('*'))
                    {
                        var items = value.Split('*');
                        double result = 1;
                        foreach (string item in items)
                        {
                            result *= double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }
            }





            int? indexOfSum = expressionClone.IndexOf('+');
            int? indexOfMinus = expressionClone.IndexOf('-');
            if (indexOfSum < indexOfMinus)
            {
                var lowPriorityValues1 = expressionClone.Split(new char[] { '*', '/', '-' });
                foreach (var value in lowPriorityValues1)
                {
                    if (value.Contains('+'))
                    {
                        var items = value.Split('+');
                        double result = 0;
                        foreach (string item in items)
                        {
                            result += double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }

                var lowPriorityValues2 = expressionClone.Split(new char[] { '*', '/', '+' });
                foreach (var value in lowPriorityValues2)
                {
                    if (value.Contains('-'))
                    {
                        var items = value.Split('-');
                        double result = double.Parse(items[0]);
                        foreach (string item in items)
                        {
                            if (item == items[0])
                                continue;

                            result -= double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }
            }
            else
            {
                var lowPriorityValues2 = expressionClone.Split(new char[] { '*', '/', '+' });
                foreach (var value in lowPriorityValues2)
                {
                    if (value.Contains('-'))
                    {
                        var items = value.Split('-');
                        double result = double.Parse(items[0]);
                        foreach (string item in items)
                        {
                            if (item == items[0])
                                continue;

                            result -= double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }

                var lowPriorityValues1 = expressionClone.Split(new char[] { '*', '/', '-' });
                foreach (var value in lowPriorityValues1)
                {

                    if (value.Contains('+'))
                    {
                        var items = value.Split('+');
                        double result = 0;
                        foreach (string item in items)
                        {
                            result += double.Parse(item);
                        }
                        expressionClone = expressionClone.Replace($"{value}", $"{result}");
                    }
                }
            }


            var finalResult = float.Parse(expressionClone);

            AddToDatabase(expression, finalResult);

            return finalResult;
        }

        private static string StringTrimmer(string expression)
        {
            expression = expression.Trim();
            expression = expression.Replace(" ", "");
            return expression;
        }

        private void AddToDatabase(string expression, float result)
        {
            _context.MathExpressions.Add(new MathExpression()
            {
                Expression = expression,
                Result = result
            });
            _context.SaveChanges();
        }
    }
}
