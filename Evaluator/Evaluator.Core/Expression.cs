
namespace Evaluator.Core
{
    public class Expression
    {
        public static double Evaluate(string infix)
        {
            var postfix = InfixToPostfix(infix);
            Console.WriteLine(postfix);
            return Calculate(postfix.ToString());
        }

        private static object InfixToPostfix(string infix)
        {
            var stack = new Stack<char>();
            var postfix = string.Empty;
            foreach(char item in infix)
            {
                if (IsOperator(item))
                {
                    if (item == ')')
                    {
                        do
                        {
                            postfix += stack.Pop();
                        } while (stack.Peek() != '(');
                        stack.Pop();
                    }
                    else
                    { 
                        if (stack.Count > 0)
                        {
                            if (PriorityInfix(item) > PriorityStack(stack.Peek()))
                            {
                                stack.Push(item);
                            }
                            else
                            {
                                postfix += stack.Pop();
                                stack.Push(item);
                            }
                        }
                        else
                        {
                            stack.Push(item);
                        }
                    }
                }
                else
                {
                    while (char.IsDigit(item))
                    {
                        postfix += item;
                        break;
                    }
                }
            }
            while (stack.Count > 0)
            {
                postfix += stack.Pop();
            }
            return postfix;
        }

        private static bool IsOperator(char item) => item is '^' or '*' or '/' or '%' or '+' or '-' or '(' or ')';

        private static int PriorityInfix(char op) => op switch
        {
            '^' => 4,
            '*' or '/' or '%' => 2,
            '+' or '-' => 1,
            '(' => 5,
            _ => throw new Exception("Invalid expression"),
        };

        private static int PriorityStack(double op) => op switch
        {
            '^' => 3,
            '*' or '/' or '%' => 2,
            '+' or '-' => 1,
            '(' => 0,
            _ => throw new Exception("Invalid expression"),
        };

        private static double Calculate(string postfix)
        {
            var stack = new Stack<double>();
            foreach (char item in postfix)
            {
                if (IsOperator(item))
                {
                    var op2 = stack.Pop();
                    var op1 = stack.Pop();
                    stack.Push(Calculate(op1, item, op2));
                }
                else
                {
                    stack.Push(Convert.ToDouble(item.ToString()));
                }
            }
            return stack.Peek();
        }

        private static double Calculate(double op1, char item, double op2) => item switch
        {
            '^' => Math.Pow(op1, op2),
            '*' => op1 * op2,
            '/' => op1 / op2,
            '+' => op1 + op2,
            '-' => op1 - op2,
            _ => throw new Exception("Invalid expression"),
        };
    }
}
