using System;

namespace Case1_HangedMan.Basics
{
    public static class Loops
    {
        public static ulong Factorial(ulong n)
        {
            ulong total = 1;
            while (n > 1)
            {
                total *= n;
                n -= 1;
            }

            return total;
        }

        public static int Power(int a, int b)
        {
            int total = 1;
            if (a == 0)
            {
                return 0;
            }

            while (b != 0)
            {
                total = total * a;
                b -= 1;
            }

            return total;
        }

        public static int DivisorSum(int n)
        {
            int div = n - 1;
            int total = 0;
            if (n < 0)
            {
                Console.Error.WriteLine("n must be positive \n" + -1);
                return total;
            }

            while (div != 0)
                if (n % div == 0)
                {
                    total += div;
                    div -= 1;
                }
                else
                {
                    div -= 1;
                }

            return total;
        }

        public static bool PerfectNumber(int c)
        {
            int y = DivisorSum(c);
            if (c == y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int DecodeBinary(string s)
        {
            int i = s.Length - 1;
            int j = 0;
            int res = 0;
            while (i != -1)
            {
                if (s[i] == '1')
                    res += Power(2, j);
                i -= 1;
                j += 1;
            }

            return res;
        }

        public static int CrackTheCode(string code)
        {
            int x = DecodeBinary(code);
            if (x < 6)
                return 0;
            while (!PerfectNumber(x))
            {
                x -= 1;
            }

            return x;
        }
    }
}