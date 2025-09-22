
namespace Evaluator.Core
{
    public class Expression
    {
        public static double Evaluate(string infix)
        {
            var postfix = InfixToPostfix(infix);
            return Calculate(postfix);
        }

        private static object InfixToPostfix(string infix)
        {
            var stack = new Stack<char>();
            var postfix = string.Empty;
            foreach(char item in infix)
            {

            }

            return postfix;
        }

        private static bool IsOperator(char item) => item == '^' || item == '*' || item == '/' || item == '%' || item == '+' || item == '-' || item == '(' || item == ')';

        private int PriorityInfix(char op) => op switch
        {
            '^' => 4,
            '*' or '/' or '%' => 2,
            '+' or '-' => 1,
            '(' => 5,
            _ => throw new Exception("Invalid expression"),
        };

        private int PriorityStack(double op) => op switch
        {
            '^' => 3,
            '*' or '/' or '%' => 2,
            '+' or '-' => 1,
            '(' => 0,
            _ => throw new Exception("Invalid expression"),
        };

        private static double Calculate(object postfix)
        {
            throw new NotImplementedException();
        }
    }
}
