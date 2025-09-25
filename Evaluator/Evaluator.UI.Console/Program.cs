using Evaluator.Core;

Console.WriteLine("##### Program Expression Evaluator #####");
Console.WriteLine("");

var infix1 = "4*5/(4+6)";
var result1 = Expression.EvaluateInfix(infix1);
Console.WriteLine($"{infix1} = {result1}");
Console.WriteLine("");

var infix2 = "4*(5+6-(8/2^3)-7)-1";
var result2 = Expression.EvaluateInfix(infix2);
Console.WriteLine($"{infix2} = {result2}");
Console.WriteLine("");

var infix3 = "123*(4+6)";
var result3 = Expression.EvaluateInfix(infix3);
Console.WriteLine($"{infix3} = {result3}");
Console.WriteLine("");