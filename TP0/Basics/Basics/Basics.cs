using System;

namespace Basics
{
    public class Basics
    {
        public static void HelloWorld()
        {
            Console.WriteLine("Hello World!");
        }

        public static void Welcome()
        {
            Console.WriteLine("Hello, What's your name");
            string name = Console.ReadLine();
            Console.WriteLine("Welcome to 221B Baker Street, " + name + "!");
        }

        public static void ComputeAge()
        {
            Console.WriteLine("What's your year of birth?");
            int age = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Looks like you are " + (2021 - age) + "!");
        }

        public static double Pow(double x, int n)
        {
            double aux;
            if (n == 0)
                return 1;
            aux = Pow(x, n / 2);
            if (n % 2 == 0)
                return aux * aux;
            else
            {
                if (n > 0)
                    return x * aux * aux;
                else
                {
                    return (aux * aux) / x;
                }
            }
        }

        public static uint Factorial(uint n)
        {
            if (n <= 1)
                return 1;
            n = n * (Factorial(n - 1));
            return n;
        }


        private static bool __IsPrime(uint n, uint i)
        {
            if (n <= 2)
                return (n == 2) ? true : false;
            if (n % i == 0)
                return false;
            if (i * i > n)
                return true;
            i += 1;
            return __IsPrime(n, i + 1);
        }

        public static bool IsPrime(uint n)
        {
            return __IsPrime(n, 2);
        }

        private static uint __Fibo(uint n, uint a, uint b)
        {
            if (n == 0)
            {
                return a;
            }

            return __Fibo((n - 1), b, (a + b));
        }

        public static uint Fibonacci(uint n)
        {
            //using the auxiliary function __Fibo
            return __Fibo(n, 0, 1);
        }


        private static string __Sherlock(uint a, uint b)
        {
            //Failed Try :
            
            //string s3 = ", Sherlock, ";
            //string s5 = ", Holmes, ";
            //string s15 = ", Sherlock Holmes, ";
            //if (n == 0)
            //    return "";
            //if (counter % 15 == 0)
            //    return s15 + __Sherlock(n - 1, counter + 1);
            //if (counter % 3 == 0)
            //    return s3 + __Sherlock(n - 1, counter + 1);
            //if (counter % 5 == 0)
            //    return s5 + __Sherlock(n - 1, counter + 1);
            //return __Sherlock(n - 1, counter + 1) + ", " + n;

            string result;
            if (a >= b)
            {
                uint mod3 = b % 3;
                if (mod3 == 0 && b % 5 == 0)
                {
                    result = "Sherlock Holmes";
                }
                else if (mod3 == 0)
                {
                    result = "Sherlock";
                }
                else if (b % 5 == 0)
                {
                    result = "Holmes";
                }
                else
                {
                    result = b.ToString();
                }

                if (b == 1)
                {
                    return result + __Sherlock(a, ++b);
                }

                return ", " + result + __Sherlock(a, ++b);
            }
            else
            {
                return "";
            }
        }

        public static string SherlockHolmes(uint n)
        {
            //uses the auxiliary function __Sherlock
            return __Sherlock(n, 1);
        }
    }
}