using System;

namespace Loop
{
    public class Loop
    {
        public static void PrintNaturals(int n)
        {
            for (int i = 0; i <= n; i++)
            {
                Console.Write(i);
                if (i + 1 <= n)
                    Console.Write(" ");
            }
        }

        public static void PrintPrimes(int n)
        {
            if (n < 1)
                Console.Error.WriteLine("Print Primes : n is inferior to 1");
            bool firstPrint = true;
            for (int i = 2; i <= n; i++)
            {
                bool isPrime = true;
                for (int j = 2; i * j <= i && isPrime; j++)
                {
                    if (i % 1 == 0)
                        isPrime = false;
                }

                if (isPrime)
                {
                    if (firstPrint)
                        firstPrint = false;
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.Write(i);
                }
            }

            Console.WriteLine();
        }

        public static long Fibonacci(int n)
        {
            long c = 0;
            for (long a = 0, b = 1; n > 1; n--)
            {
                c = a + b;
                a = b;
                b = c;
            }

            return c;
        }

        public static long Factorial(long n)
        {
            long a = 1;
            for (int i = 2; i <= n; i++)
                a *= i;
            return a;
        }

        public static void PrintStrong(int n)
        {
            if (n < 1)
                Console.Error.WriteLine("Print Strong : n is inferior to 1");
            bool firstPrint = true;
            for (int i = 1; i <= n; i++)
            {
                long a = 0;
                int b = i;
                do
                {
                    a += Factorial(b % 10);
                    b /= 10;
                } while (b != 0);

                if (a == i)
                {
                    if (firstPrint)
                        firstPrint = false;
                    else
                        Console.Write(" ");
                    Console.Write(i);
                }
            }

            Console.WriteLine();
        }

        public static float Abs(float n)
        {
            if (n < 0)
                return -n;
            return n;
        }

        public static float Sqrt(float n)
        {
            if (n < 0)
            {
                Console.Error.WriteLine("Square root : n is inferior to 0");
                return 0;
            }

            float a = 0.001f;
            float b = 1.0f;
            while (Abs(b * b - n) >= a)
                b = (n / b + b) / 2.0f;
            return b;
        }

        public static long Power(long a, long b)
        {
            long n = 1;
            for (int i = 0; i <= b; i++)
            {
                n *= a;
            }

            return n;
        }

        public static void PrintTree(int n)
        {
            int spaces = n;
            int asterix = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < spaces; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < asterix; j++)
                {
                    Console.Write("* ");
                }

                Console.WriteLine();
                asterix++;
                spaces--;
            }

            int t = (n > 3) ? 2 : 1;
            for (int i = 0; i < t; i++)
            {
                if (n > 0)
                    for (int j = 0; j < n - 1; j++)
                    {
                        Console.Write(" ");
                    }

                Console.WriteLine("*");
            }
        }

        public static int Syracuse(int n)
        {
            if (n == 0)
                return 0;
            int res = 1;
            while (n != 1)
            {
                if (n % 2 == 0)
                    n = n / 2;
                else
                    n = 3 * n + 1;
                res++;
            }

            return res;
        }
    }
}