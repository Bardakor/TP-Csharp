using System;

namespace MiniTests
{
    public static class SuspiciousFunctions
    {
        public static int Fibonacci(int n)
        {

            if (n == 0)
                return 0;

            if (n < 2)
                return n;
            return Fibonacci(n - 1) + Fibonacci(n - 2);

        }

        public static int ViceMax(int[] array)
        {
            var max = array[0];
            var seconMax = array[1];
            int i = 0;
            while (max == seconMax)
            {
                if (max == seconMax)
                    seconMax = array[i];
                i++;
            }

            foreach (var x in array)
            {
                if (x > max)
                {
                    seconMax = max ;
                    max = x;
                }
            }

            return seconMax;
        }
    }
}