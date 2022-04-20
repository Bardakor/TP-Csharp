using System;
using System.Collections.Generic;
using System.Linq;
using Abacus.Token;

namespace Abacus
{
    public static class Evaluate
    {
        //method that returns the computation of the two first numbers in the list and the operator that follows them
        public static int Compute(List<Token.Token> tokenlist)
        {
            int result = 0;


            {
                int num2;
                int num1;
                if (tokenlist[1] is TokenNumber && tokenlist[2] is TokenNumber)
                {
                    //convert tokenlist[i] to an int    
                    num1 = Convert.ToInt32(tokenlist[1].ToString());
                    num2 = Convert.ToInt32(tokenlist[2].ToString());
                }
                else
                {
                    throw new Exception("Invalid Token: token[1] or token[2] isn't a Int");
                }

                Operator op;
                if (tokenlist[3] is TokenOperator)
                {
                    op = ((TokenOperator)tokenlist[3]).Type;
                }
                else
                {
                    throw new Exception("Invalid token: " + tokenlist[3]);
                }

                if (num2 != 0)
                {
                    switch (op)
                    {
                        case Operator.Add:
                            result = num1 + num2;
                            break;
                        case Operator.Subtract:
                            result = num1 - num2;
                            break;
                        case Operator.Multiply:
                            result = num1 * num2;
                            break;
                        case Operator.Divide:
                            result = num1 / num2;
                            break;
                        case Operator.Power:
                            result = (int)Math.Pow(num1, num2);
                            break;
                        case Operator.Modulo:
                            result = num1 % num2;
                            break;
                        default:
                            throw new Exception("Invalid operator: " + op);
                    }
                }
            }

            return result;
        }

        public static double CalculatExpression(Queue<String> postfix)
        {
            Stack<double> stack = new Stack<double>();
            postfix.ToList<String>().ForEach(token =>
            {
                if (Parser.operators.ContainsKey(token))
                {
                    if (stack.Count > 1)
                    {
                        double num1 = stack.Pop();
                        double num2 = stack.Pop();
                        switch (token)
                        {
                            case "+":
                                stack.Push(num2 + num1);
                                break;
                            case "-":
                                stack.Push(num2 - num1);
                                break;
                            case "*":
                                stack.Push(num2 * num1);
                                break;
                            case "/":
                                stack.Push(num2 / num1);
                                break;
                            default :
                                Console.Error.WriteLine("Invalid operator: " + token);
                                Environment.Exit(2);
                                break;
                        }
                    }
                }
                //check if it exists and execute with required parameters
                else if (Parser.functions.ContainsKey(token))
                {
                    Interface func = Parser.functions[token];

                    //pop off the required amount of parameters
                    double[] paramList = new double[func.TotalParameters];
                    for (int i = 0; i < paramList.Length; i++)
                        paramList[i] = stack.Pop();

                    //execute the function
                    Array.Reverse(paramList);
                    func.Parameters = paramList;
                    stack.Push(func.Execute());
                }
                else
                    stack.Push(Convert.ToDouble(token));
            });
            return stack.Pop();
        }

        //method that builds a stack from the list of tokens. Every time the following pattern is found: int -> int -> operator,
        //pop the two ints from the stack, compute the result and push the result back on the stack
        public static int EvaluateRpn(List<Token.Token> tokenlist)
        {
            Stack<int> stack = new Stack<int>();
            int result = 0;

            foreach (Token.Token token in tokenlist)
            {
                if (token is TokenNumber number)
                {
                    //push the number on the stack
                    stack.Push((number.value));
                }
                //if in the stack, there are only numbers and no operators, return an error

                else if (token is TokenOperator @operator)
                {
                    int num2 = stack.Pop();
                    int num1 = stack.Pop();
                    Operator op = @operator.Type;

                    if (num2 != 0)
                    {
                        switch (op)
                        {
                            case Operator.Add:
                                result = num1 + num2;
                                break;
                            case Operator.Subtract:
                                result = num1 - num2;
                                break;
                            case Operator.Multiply:
                                result = num1 * num2;
                                break;
                            case Operator.Divide:
                                result = num1 / num2;
                                break;
                            case Operator.Power:
                                result = (int)Math.Pow(num1, num2);
                                break;
                            case Operator.Modulo:
                                result = num1 % num2;
                                break;
                            default:
                                Console.Error.WriteLine("Invalid operator: " + op);
                                Environment.Exit(2);
                                break;
                        }
                    }

                    if (num2 == 0 && op == Operator.Divide)
                    {
                        Console.Error.WriteLine("Division by zero");
                        Environment.Exit(3);
                    }

                    stack.Push(result);
                }
                //if the token is a function
                else if (token is Token.TokenFunction @function)
                {
                    //int num1 = stack.Pop();
                    //int num2 = stack.Pop();
                    Functions func = @function.Type;
                    int arguments = @function._numberOfArguments;

                    switch (func)
                    {
                        case Functions.Facto:
                            int num1 = stack.Pop();
                            result = Factorial(num1);
                            break;
                        case Functions.Fibo:
                            int num2 = stack.Pop();
                            result = Fibonacci(num2);
                            break;
                        case Functions.Gcd:
                            int num3 = stack.Pop();
                            int num4 = stack.Pop();
                            result = Gcd(num3, num4);
                            break;
                        case Functions.Max:
                            int num5 = stack.Pop();
                            int num6 = stack.Pop();
                            result = Max(num5, num6);
                            break;
                        case Functions.Min:
                            int num7 = stack.Pop();
                            int num8 = stack.Pop();
                            result = Min(num7, num8);
                            break;
                        case Functions.Sqrt:
                            int num9 = stack.Pop();
                            result = (int)Math.Sqrt(num9);
                            break;
                        case Functions.IsPrime:
                            int num10 = stack.Pop();
                            result = IsPrime(num10);
                            break;
                        default:
                            Console.Error.WriteLine("Invalid function: " + func);
                            Environment.Exit(2);
                            break;
                    }

                    stack.Push(result);
                }
            }

            //check is stack.count == 1 else error
            if (stack.Count == 1)
                return stack.Pop();
            else
            {
                Console.Error.WriteLine("Invalid expression");
                Environment.Exit(2);
                return 0;
            }
        }


        private static int IsPrime(int num1)
        {
            if (num1 == 1)
                return 0;
            else if (num1 == 2)
                return 1;
            else if (num1 % 2 == 0)
                return 0;
            else
            {
                for (int i = 3; i <= Math.Sqrt(num1); i += 2)
                {
                    if (num1 % i == 0)
                        return 0;
                }

                return 1;
            }
        }

        private static int Max(int num1, int num2)
        {
            if (num1 > num2)
                return num1;
            else
                return num2;
        }

        private static int Min(int num1, int num2)
        {
            if (num1 < num2)
                return num1;
            else
                return num2;
        }

        private static int Gcd(int num1, int num2)
        {
            if (num1 == 0)
                return num2;
            else if (num2 == 0)
                return num1;
            else
            {
                while (num1 != num2)
                {
                    if (num1 > num2)
                        num1 -= num2;
                    else
                        num2 -= num1;
                }

                return num1;
            }
        }

        private static int Fibonacci(int num1)
        {
            if (num1 == 0)
                return 0;
            else if (num1 == 1)
                return 1;
            else
                return Fibonacci(num1 - 1) + Fibonacci(num1 - 2);
        }

        private static int Factorial(int num1)
        {
            if (num1 == 0)
                return 1;
            else
                return num1 * Factorial(num1 - 1);
        }
    }
}