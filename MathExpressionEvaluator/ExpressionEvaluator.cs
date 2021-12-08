using MathProject.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathProject.DataLayer.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

            
            while (expressionClone.Contains("*") || expressionClone.Contains("/"))
            {

                int indexOfDivide = expressionClone.IndexOf('/');
                int indexOfMultiple = expressionClone.IndexOf('*');

                if ( indexOfMultiple > 0 &&  (indexOfMultiple < indexOfDivide || (indexOfDivide < 0 && indexOfMultiple > 0)))
                {
                    var leftElementString = LeftElementGenerator(expressionClone, indexOfMultiple);
                    var leftElement = double.Parse(leftElementString);

                    var rightElementString = RightElementGenerator(expressionClone, indexOfMultiple);
                    var rightElement = double.Parse(rightElementString);
                    

                    string value = leftElement + "*" + rightElement;
                    double result = leftElement * rightElement;

                    expressionClone = expressionClone.Replace($"{value}", $"{result}");
                }
                else
                {
                    var leftElementString = LeftElementGenerator(expressionClone, indexOfDivide);
                    var leftElement = double.Parse(leftElementString);

                    var rightElementString = RightElementGenerator(expressionClone, indexOfDivide);
                    var rightElement = double.Parse(rightElementString);


                    string value = leftElement + "/" + rightElement;
                    double result = leftElement / rightElement;

                    expressionClone = expressionClone.Replace($"{value}", $"{result}");
                }
            }


            //if (indexOfMultiple < indexOfDivide)
            //{
            //    var highPriorityValues1 = expression.Split(new char[] { '+', '-', '/' });
            //    foreach (var value in highPriorityValues1)
            //    {
            //        if (value.Contains('*'))
            //        {
            //            var items = value.Split('*');
            //            double result = 1;
            //            foreach (string item in items)
            //            {
            //                result *= double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }

            //    var highPriorityValues2 = expressionClone.Split(new char[] { '+', '-', '*' });
            //    foreach (var value in highPriorityValues2)
            //    {
            //        if (value.Contains('/'))
            //        {
            //            var items = value.Split('/');
            //            double result = double.Parse(items[0]);
            //            foreach (string item in items)
            //            {
            //                if(item == items[0])
            //                    continue;

            //                result /= double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }
            //}
            //else
            //{
            //    var highPriorityValues2 = expressionClone.Split(new char[] { '+', '-', '*' });
            //    foreach (var value in highPriorityValues2)
            //    {
            //        if (value.Contains('/'))
            //        {
            //            var items = value.Split('/');
            //            double result = double.Parse(items[0]);
            //            foreach (string item in items)
            //            {
            //                if (item == items[0])
            //                    continue;

            //                result /= double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }

            //    var highPriorityValues1 = expressionClone.Split(new char[] { '+', '-', '/' });
            //    foreach (var value in highPriorityValues1)
            //    {
            //        if (value.Contains('*'))
            //        {
            //            var items = value.Split('*');
            //            double result = 1;
            //            foreach (string item in items)
            //            {
            //                result *= double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }
            //}




            while (expressionClone.Contains("+") || expressionClone.Contains("-"))
            {
                int indexOfSum = expressionClone.IndexOf('+');
                int indexOfMinus = expressionClone.IndexOf('-');

                if (indexOfSum > 0 && (indexOfSum < indexOfMinus || (indexOfMinus < 0 && indexOfSum > 0)))
                {
                    var leftElementString = LeftElementGenerator(expressionClone, indexOfSum);
                    var leftElement = double.Parse(leftElementString);

                    var rightElementString = RightElementGenerator(expressionClone, indexOfSum);
                    var rightElement = double.Parse(rightElementString);


                    string value = leftElement + "+" + rightElement;
                    double result = leftElement + rightElement;

                    expressionClone = expressionClone.Replace($"{value}", $"{result}");
                }
                else
                {
                    var leftElementString = LeftElementGenerator(expressionClone, indexOfMinus);
                    var leftElement = double.Parse(leftElementString);

                    var rightElementString = RightElementGenerator(expressionClone, indexOfMinus);
                    var rightElement = double.Parse(rightElementString);


                    string value = leftElement + "-" + rightElement;
                    double result = leftElement - rightElement;

                    expressionClone = expressionClone.Replace($"{value}", $"{result}");
                }
            }


            //if (indexOfSum < indexOfMinus)
            //{
            //    var lowPriorityValues1 = expressionClone.Split(new char[] { '*', '/', '-' });
            //    foreach (var value in lowPriorityValues1)
            //    {
            //        if (value.Contains('+'))
            //        {
            //            var items = value.Split('+');
            //            double result = 0;
            //            foreach (string item in items)
            //            {
            //                result += double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }

            //    var lowPriorityValues2 = expressionClone.Split(new char[] { '*', '/', '+' });
            //    foreach (var value in lowPriorityValues2)
            //    {
            //        if (value.Contains('-'))
            //        {
            //            var items = value.Split('-');
            //            double result = double.Parse(items[0]);
            //            foreach (string item in items)
            //            {
            //                if (item == items[0])
            //                    continue;

            //                result -= double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }
            //}
            //else
            //{
            //    var lowPriorityValues2 = expressionClone.Split(new char[] { '*', '/', '+' });
            //    foreach (var value in lowPriorityValues2)
            //    {
            //        if (value.Contains('-'))
            //        {
            //            var items = value.Split('-');
            //            double result = double.Parse(items[0]);
            //            foreach (string item in items)
            //            {
            //                if (item == items[0])
            //                    continue;

            //                result -= double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }

            //    var lowPriorityValues1 = expressionClone.Split(new char[] { '*', '/', '-' });
            //    foreach (var value in lowPriorityValues1)
            //    {

            //        if (value.Contains('+'))
            //        {
            //            var items = value.Split('+');
            //            double result = 0;
            //            foreach (string item in items)
            //            {
            //                result += double.Parse(item);
            //            }
            //            expressionClone = expressionClone.Replace($"{value}", $"{result}");
            //        }
            //    }
            //}


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

        private string LeftElementGenerator(string expression, int index)
        {
            var leftElements = new List<string>();
            for (var i = 1; i < expression.Length; i++)
            {
                char item;
                try
                {
                    item = expression.ElementAt(index - i);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    break;
                }

                if (char.IsDigit(item) || item == '.')
                    leftElements.Add(item.ToString());
                else
                    break;

            }

            var leftElement = "";
            leftElements.Reverse();
            foreach (var element in leftElements)
            {
                leftElement += element;
            }

            return leftElement;
        }

        private string RightElementGenerator(string expression, int indexOfMultiple)
        {
            var rightElements = new List<string>();
            for (var i = 1; i < expression.Length; i++)
            {
                char item;
                try
                {
                    item = expression.ElementAt(indexOfMultiple + i);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    break;
                }

                if (char.IsDigit(item) || item == '.')
                    rightElements.Add(item.ToString());
                else
                    break;

            }

            var rightElement = "";
            foreach (var element in rightElements)
            {
                rightElement += element;
            }

            return rightElement;
        }
    }
}

