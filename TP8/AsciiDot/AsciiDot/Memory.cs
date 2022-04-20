using System;
using System.Collections.Generic;

namespace AsciiDot
{
    public enum Environment
    {
        Default,
        Output,
        SingleQuote,
        DoubleQuote,
        Affectation
    }
    
    public class Memory
    {
        /**
         * <summary>The stored value of the dot.</summary>
         */
        public double Value;
        
        /**
         * <summary>In Witch environment the dot is.</summary>
         * <see cref="Environment"/>
         */
        public Environment CurrentEnvironment;
        
        /**
         * <summary>Queue of visited tokens.</summary>
         */
        public List<Token.Token> Queue;

        /**
         * <summary>
         * Constructor of Memory.
         * </summary>
         */
        public Memory()
        {
            Value = 0;
            CurrentEnvironment = Environment.Default;
            Queue = new List<Token.Token>();
        }

        /**
         * <summary>
         * Copy constructor of Memory.
         * </summary>
         * 
         * <param name="memory">The memory to clone.</param>
         */
        private Memory(Memory memory)
        {
            Value = memory.Value;
            CurrentEnvironment = memory.CurrentEnvironment;
            Queue = new List<Token.Token>(memory.Queue);
        }

        public Memory Clone() => new Memory(this);

        public void Enqueue(Token.Token token) => Queue.Add(token);
        
        /**
         * <summary>
         * Try to flush the queue and apply queued actions: Assignment or Display or nothing.
         * </summary>
         */
        public void Flush()
        {
            string token = "";
            //if token is empy return nothing
            if (Queue.Count == 0)
            {
                return;
            }
            foreach (var t in Queue)
            {
                token += t.Value;
            }
            //if the queue starts with $ we must apply Display method to the token
            if (token[0] == '$')
            {
                Display(token);
                Queue.Clear();
                return;
            }
            //if the queue starts with # we must apply Assignment method to the token
            if (token[0] == '#')
            {
                Assignment(token);
                Queue.Clear();
                return;
            }



        }

        /**
         * <summary>Apply assignment action</summary>
         * 
         * <param name="str">The input string beginning with '#'.</param>
         */
        public void Assignment(string str)
        {
            //The Assignment method takes a string starting with # and applies the corresponding rules to display it on the standard output. 
            bool input = false;
            while (str.Length > 0 && (str[0] == '#' || str[0] == '?'))
            {
                if (str[0] == '?') input = true;
                str = str.Substring(1);
            }
            if (input)
            {
                System.Console.WriteLine("Enter int value : ");
                try
                {
                    Value = Convert.ToDouble(System.Console.ReadLine());
                }
                catch (Exception)
                {
                    Value = 0;
                }
            }
        }

        /**
         * <summary>Apply display action</summary>
         * 
         * <param name="str">The input string beginning with '$'.</param>
         */
        public void Display(string str)
        {
            bool line_break = false;
            bool memory = false;
            bool unicode = false;

            while (str.Length > 0 && (str[0] =='$' || str[0] == '#' || str[0] == '_'))
            {
                if (str[0] == '_') line_break = true;
                else if (str[0] == 'a') unicode = true;
                else if (str[0] == '#') memory = true;
                str = str.Substring(1);
            }
            string line_end = line_break ? "" : "\n";

            string output = "";

            if (memory && unicode)
            {
                int b = (int) Value;
                output = ((char) b).ToString();
            }

            else if (memory)
                output = Value.ToString();
            
            else if (str[0] == '\"' || str[0] == '\'')
            {
                str = str.Substring(1);
                while (str[0] != '\"')
                {
                    output += str[0];
                    str = str.Substring(1);
                }
            }
            else
            {
                throw new Exception ("ABORT MISSION");
            }
            System.Console.WriteLine(output + line_end);
        }
    }
}