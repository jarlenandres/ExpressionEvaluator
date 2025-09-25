
using System.Globalization;

namespace Evaluator.Core
{
    public class Expression
    {
        public static double EvaluateInfix(string infix)
        {
            var postfix = InfixToPostfix(infix);
            Console.WriteLine(postfix);
            return Calculate(postfix);
        }
        
        private static string InfixToPostfix(string infix)
        {
            var output = new List<string>();
            var stack = new Stack<string>();

            var tokens = new List<string>();
            string postfix = string.Empty;
            foreach (char item in infix)
            {
                if (char.IsDigit(item) || item == '.' || item == ',')
                {
                    postfix += item;
                }
                else
                {
                    if (postfix != string.Empty)
                    {
                        tokens.Add(postfix);
                        postfix = string.Empty;
                    }
                    if (!char.IsWhiteSpace(item))
                    {
                        tokens.Add(item.ToString());
                    }
                }
            }
            if (postfix != string.Empty)
            {
                tokens.Add(postfix);
            }

            foreach (var token in tokens)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _)) // es número
                {
                    output.Add(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        output.Add(stack.Pop());
                    }
                    if (stack.Count > 0 && stack.Peek() == "(")
                        stack.Pop();
                }
                else
                {
                    while (stack.Count > 0 && PriorityInfix(stack.Peek()) >= PriorityInfix(token))
                    {
                        output.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
            }
            while (stack.Count > 0)
            {
                output.Add(stack.Pop());
            }

            return string.Join(" ", output);
        }

        private static int PriorityInfix(string op) => op switch
        {
            "^" => 4,
            "+" or "-" => 1,
            "*" or "/" or "%" => 2,
            _ => 0
        };

        private static double Calculate(string postfix)
        {
            var stack = new Stack<double>();
            var tokens = postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                {
                    stack.Push(num);
                }
                else
                {
                    double op2 = stack.Pop();
                    double op1 = stack.Pop();
                    stack.Push(Calculate(op1, token, op2));
                }
            }
            return stack.Pop();
        }

        private static double Calculate(double op1, string token, double op2) => token switch
        {
            "^" => Math.Pow(op1, op2),
            "*" => op1 * op2,
            "/" => op1 / op2,
            "+" => op1 + op2,
            "-" => op1 - op2,
            _ => throw new Exception("Invalid expression"),
        };
    }
}
