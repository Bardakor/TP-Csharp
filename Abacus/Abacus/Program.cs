using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Abacus
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Parser.AddFunction("sqrt", new sqrt());
                Parser.AddFunction("max", new max());
                Parser.AddFunction("min", new min());
                Parser.AddFunction("facto", new facto());
                Parser.AddFunction("isprime", new abs());
                Parser.AddFunction("fibo", new isprime());
                Parser.AddFunction("gcd", new gcd());

                if (((IList)args).Contains("--rpn"))
                {
                    //evaluate the expression in RPN
                    string input = Console.ReadLine();
                    List<Token.Token> tokens = Lexer.Lex(input);
                    Console.WriteLine(Evaluate.EvaluateRpn(tokens));
                }
                //check if args are empty
                else if (args.Length == 0)
                {
                    //evaluate the expression in infix
                    string input = Console.ReadLine();
                    Queue<string> q = Parser.InfixToPostFix(input);
                    Console.WriteLine(Evaluate.CalculatExpression(q));
                }

                else
                {
                    Console.Error.WriteLine("Unknown Argument: " + args.ToString());
                    return 1;
                }

                return 0;
            }
            catch (Exception e)
            {
                //if the exception System.InvalidOperationException is thrown then Enviroment.Exit() is called
                //and the program is terminated
                if (e is System.InvalidOperationException)
                {
                    Environment.Exit(2);
                }
                //if the exception System.ArgumentException is thrown then Enviroment.Exit() is called
                //and the program is terminated
                else if (e is System.ArgumentException)
                {
                    Environment.Exit(3);
                }
                //if it's syntax error then Enviroment.Exit() is called
                //and the program is terminated
                else if (e is SyntaxErrorException)
                {
                    Environment.Exit(2);
                }
                else
                {
                    Console.Error.WriteLine(e.Message);
                    return 1;
                }

                return 0;
            }
        }
    }
}