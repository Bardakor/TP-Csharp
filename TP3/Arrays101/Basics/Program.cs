using System;

namespace Basics
{
    public static class Program
    {
        public static void Main()
        {
            int[] array;
            array = new int[5];
            int[] bis = { 1, 2, 3, 4, 4, 5, 5 };
            int[] bis2 = { 5, 5 };
            Arrays.Print(bis);
            Arrays.Print(Arrays.Concat(bis, bis2));
            Console.WriteLine(Arrays.IsSorted(bis));
        }
    }
}