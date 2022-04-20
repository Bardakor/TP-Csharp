using System;
using System.Collections;
using System.Collections.Generic;
using Abacus.Token;

namespace Abacus
{
    public class Lexer
    {
        //this is the method that generates the list of tokens used in the parser and the evaluator
        public static List<Token.Token> Lex(string s)
        {
            List<Token.Token> tokenlist = new List<Token.Token>();
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case '+':
                        tokenlist.Add(new TokenOperator(Operator.Add));
                        break;
                    case '-':
                        tokenlist.Add(new TokenOperator(Operator.Subtract));
                        break;
                    case '*':
                        tokenlist.Add(new TokenOperator(Operator.Multiply));
                        break;
                    case '/':
                        tokenlist.Add(new TokenOperator(Operator.Divide));
                        break;
                    case '^':
                        tokenlist.Add(new TokenOperator(Operator.Power));
                        break;
                    case '%':
                        tokenlist.Add(new TokenOperator(Operator.Modulo));
                        break;
                    //add more cases here for functions (Sqrt, Max, Min, Factorial, IsPrime, Fibonacci, gcd)
                    case 's':
                        if (i + 4 < s.Length && s.Substring(i, 4) == "sqrt")
                        {
                            tokenlist.Add(new TokenFunction(Functions.Sqrt));
                            i += 3;
                        }

                        break;
                    case 'm':
                        if (s.Substring(i, 3) == "max") //&& i + 3 < s.Length)
                        {
                            tokenlist.Add(new TokenFunction(Functions.Max));
                            i += 2;
                        }
                        else if (s.Substring(i, 3) == "min")
                        {
                            tokenlist.Add(new TokenFunction(Functions.Min));
                            i += 2;
                        }

                        break;
                    case 'f':
                        if (s.Substring(i, 4) == "facto")
                        {
                            tokenlist.Add(new TokenFunction(Functions.Facto));
                            i += 3;
                        }
                        else if (s.Substring(i, 4) == "fibo")
                        {
                            tokenlist.Add(new TokenFunction(Functions.Fibo));
                            i += 3;
                        }

                        break;
                    case 'i':
                        if (s.Substring(i, 7) == "isprime")
                        {
                            tokenlist.Add(new TokenFunction(Functions.IsPrime));
                            i += 6;
                        }

                        break;
                    case 'g':
                        if (s.Substring(i, 3) == "gcd")
                        {
                            tokenlist.Add(new TokenFunction(Functions.Gcd));
                            i += 2;
                        }

                        break;
                    //case where s[i] is a number
                    case >= '0' and <= '9':
                        string number = "";
                        while (i < s.Length && char.IsDigit(s[i]))
                        {
                            number += s[i];
                            i++;
                        }

                        tokenlist.Add(new TokenNumber(Int32.Parse(number)));
                        break;
                    case ' ':
                        break;
                    default:
                        Console.Error.WriteLine("invalid character: " + s[i]);
                        Environment.Exit(2);
                        break;
                    
                }
            }

            return tokenlist;
        }
    }
}