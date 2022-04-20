using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Abacus.Token;


namespace Abacus
{
    public class Parser : Lexer
    {
        public static Dictionary<string, int[]> operators = new Dictionary<string, int[]>();
        public static Dictionary<string, Interface> functions = new Dictionary<string, Interface>();

        public static void AddFunction(string functionName, Interface functionInstance)
        {
            functions.Add(functionName, functionInstance);
        }

        private static int GetPrecedence(string s)
        {
            int max = 0;
            //if the passed is a function, make it the highest precedence
            //get the max precedence value for all maths operators and add 1

            if (functions.ContainsKey(s))
                max = Math.Max(operators[operators.Keys.First()][0], operators[operators.Keys.Last()][0]) + 1;
            else
                max = operators[s][0];

            return max;
        }

        //Get the association of the operator passed in
        private static int GetAssociation(string s)
        {
            return operators[s][1];
        }

        private static void ParseUnary(ref List<string> expr)
        {
            for (int i = 0; i < expr.Count; i++)
            {
                //we have a minus
                if (expr[i] == "-")
                {
                    if (i == 0 || expr[i - 1] == "(" || operators.ContainsKey(expr[i - 1]))
                    {
                        expr.Insert(i, "(");
                        expr.Insert(i + 1, "0");
                        expr.Insert(i + 4, ")");
                    }
                }
                //we have a +
                else if (expr[i] == "+")
                {
                    if (i == 0 || expr[i - 1] == "(" || operators.ContainsKey(expr[i - 1]))
                        expr.RemoveAt(i);
                }
            }
        }


        //modify the method so that it throws an error if the parenthesis are not balanced
        
        public static Queue<string> InfixToPostFix(string expression)
        {
            //if there is an unbalanced parenthesis, throw an error
            if (expression.Count(x => x == '(') != expression.Count(x => x == ')'))
            {
                Console.Error.WriteLine("Unbalanced Parenthesis");
                Environment.Exit(2);
            }
            
            Queue<string> queue = new Queue<string>();
            Stack<string> stack = new Stack<string>();

            operators.Add("^", new int[] { 5, 1 });
            operators.Add("*", new int[] { 4, 0 });
            operators.Add("/", new int[] { 4, 0 });
            operators.Add("+", new int[] { 3, 0 });
            operators.Add("-", new int[] { 3, 0 });


            string pattern = @"(?<=[-+*/(),^<>=&])(?=.)|(?<=.)(?=[-+*/(),^<>=&])";

            expression = expression.Replace(" ", "");
            Regex regExPattern = new Regex(pattern);
            List<string> expr = new List<string>(regExPattern.Split(expression));


            ParseUnary(ref expr);

            expr.ForEach(s =>
            {
                if (operators.ContainsKey(s))
                {
                    //while the stack is not empty and the top of the stack is not an (
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        if ((GetAssociation(s) == 0 && GetPrecedence(s) <= GetPrecedence(stack.Peek())) ||
                            (GetAssociation(s) == 1 && GetPrecedence(s) < GetPrecedence(stack.Peek()))
                        )
                            queue.Enqueue(stack.Pop());
                        else
                            break;
                    }

                    stack.Push(s);
                }

                else if (functions.ContainsKey(s))
                {
                    stack.Push(s);
                }

                else if (s == "(")
                {
                    stack.Push(s);
                }

                else if (s == ")")
                {
                    while (stack.Count != 0 && stack.Peek() != "(")
                        queue.Enqueue(stack.Pop());

                    stack.Pop();
                }


                else if (s == ",")
                {
                    while (stack.Peek() != "(")
                    {
                        queue.Enqueue(stack.Pop());
                    }
                         
                }
                else
                    queue.Enqueue(s);
            });


            while (stack.Count != 0)
                queue.Enqueue(stack.Pop());

            return queue;
        }

        //method that transforms Queue<string> into a string while and adds a space between each token
        public static string TransformQueueToString(Queue<string> queue)
        {
            string expression = "";
            while (queue.Count > 0)
            {
                expression += queue.Dequeue() + " ";
            }

            return expression;
        }
    }
}