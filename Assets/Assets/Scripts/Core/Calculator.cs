using System;
using System.Collections.Generic;
using System.Globalization;

namespace GameBee.Calculator
{
    public static class Calculator
    {
        private const string InvalidExpressionMessage = "Invalid expression.";

        public static double Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw InvalidExpression();

            List<double> numbers = new List<double>();
            List<char> operators = new List<char>();
            int index = 0;

            while (index < expression.Length)
            {
                char current = expression[index];
                if (ExpressionSyntax.IsOperator(current))
                {
                    bool isUnary = numbers.Count == 0 || numbers.Count == operators.Count;
                    if (isUnary)
                    {
                        if (current != '+' && current != '-')
                            throw InvalidExpression();
                    }
                    else
                    {
                        operators.Add(ExpressionSyntax.NormalizeOperator(current));
                        index++;
                        continue;
                    }
                }

                numbers.Add(ReadNumber(expression, ref index));
            }

            if (numbers.Count == 0 || numbers.Count != operators.Count + 1)
                throw InvalidExpression();

            CollapsePriorityOperations(numbers, operators);

            double result = numbers[0];
            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == '+')
                {
                    result += numbers[i + 1];
                }
                else
                {
                    result -= numbers[i + 1];
                }
            }

            return result;
        }

        private static double ReadNumber(string expression, ref int index)
        {
            int start = index;
            bool seenDecimal = false;

            if (index < expression.Length && (expression[index] == '+' || expression[index] == '-'))
                index++;

            while (index < expression.Length)
            {
                char current = expression[index];
                if (char.IsDigit(current))
                {
                    index++;
                    continue;
                }

                if (current == '.')
                {
                    if (seenDecimal)
                        throw InvalidExpression();

                    seenDecimal = true;
                    index++;
                    continue;
                }

                if (ExpressionSyntax.IsOperator(current))
                    break;

                throw InvalidExpression();
            }

            if (start == index)
                throw InvalidExpression();

            string token = expression.Substring(start, index - start);
            if (!double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                throw InvalidExpression();

            return value;
        }

        private static void CollapsePriorityOperations(List<double> numbers, List<char> operators)
        {
            int index = 0;
            while (index < operators.Count)
            {
                char op = operators[index];
                if (op != '*' && op != '/')
                {
                    index++;
                    continue;
                }

                double right = numbers[index + 1];
                if (op == '/' && right == 0d)
                    throw new DivideByZeroException();

                double left = numbers[index];
                numbers[index] = op == '*' ? left * right : left / right;
                numbers.RemoveAt(index + 1);
                operators.RemoveAt(index);
            }
        }

        private static FormatException InvalidExpression()
        {
            return new FormatException(InvalidExpressionMessage);
        }
    }
}
