using System;

namespace MiniTests
{
    public static class EmptyFunctions
    {
        public static int GetDigit(int n, int digit)
        {
            int i = 1;
            while (i < digit)
            {
                n = n / 10;
                i++;
            }

            return (n % 10);
        }

        public static bool IsNumberPalindrome(int n)
        {
            int reversed = 0;
            int original = n;

            while (n > 0)
            {
                reversed = reversed * 10 + n % 10;
                n = n / 10;
            }

            return (reversed == original);
        }
    }
}