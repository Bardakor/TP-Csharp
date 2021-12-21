using System;

namespace Basics
{
    public static class References
    {
        /**
         * <summary>Swaps to integers.</summary>
         */
        public static void Swap(ref int a, ref int b)
        {
            (a, b) = (b, a);
        }

        /**
         * <summary> Computes the euclidean division of two integers.
         * Stores the remainder in the first integer. </summary>
         * <param name="a"> The dividend. Greater than or equal to 0.
         * This is also the variable that will handle the remainder </param>
         * <param name="b"> The divisor. Strictly greater than 0. </param>
         * <returns> Returns the quotient of the
         * euclidean division of <c>a</c> by <c>b</c>. </returns>
         */
        public static int EuclideanDivision(ref int a, int b)
        {
            var q = a / b;
            a = a % b;
            return q;
        }

        /**
         * <summary>Computes the square of a complex number.</summary>
         * <param name="re">The real part of the number.</param>
         * <param name="im">The imaginary part of the number.</param>
         */
        public static void ComplexSquare(ref int re, ref int im)
        {
            var re2 = re * re;
            var im2 = im * im;
            re = re2 - im2;
            im = 2 * re * im;
        }

    }
}