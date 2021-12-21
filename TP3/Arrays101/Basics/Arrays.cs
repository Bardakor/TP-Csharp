using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace Basics
{
    public static class Arrays
    {
        /**
         * <summary>Swaps two elements in an array.</summary>
         */
        public static void Swap(int[] arr, int i, int j)
        {
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }

        /**
         * <summary>Prints an array of integers.</summary>
         * <param name="arr"> The array of integers - might be empty.</param>
         */
        public static void Print(int[] arr)
        {
            Console.WriteLine("[{0}]", String.Join(" | ", arr));
        }

        /**
         * <summary>Returns the second greatest element of an array.</summary>
         * <param name="arr">An array of size at least 2.</param>
         * <returns>The second greatest element in <c>arr</c>.</returns>
         */
        public static int ViceMax(int[] arr)
        {
            int aux = arr[0] < arr[1] ? arr[0] : arr[1];
            int max = arr[0] == aux ? arr[1] : arr[0];
            for (int i = 2; i < arr.Length; i++)
            {
                if (arr[i] >= max)
                {
                    aux = max;
                    max = arr[i];
                }
                else if (arr[i] > aux && arr[i] != max)
                {
                    aux = arr[i];
                }
            }

            return aux;
        }

        /**
     * <summary>Reverses the array <code>arr</code> in place.</summary>
     * <param name="arr">An array of integers.</param>
     */
        public static void Reverse(int[] arr)
        {
            for (int i = 0; i < arr.Length / 2; i++)
            {
                int aux = arr[i];
                arr[i] = arr[arr.Length - i - 1];
                aux = arr[arr.Length - i - 1];
            }
        }

        /**
 * <summary>Concatenates to arrays.</summary>
 * <param name="lhs">The left part of the resulting array.</param>
 * <param name="rhs">The right part of the resulting array?</param>
 * <returns>Returns the concatenation of <c>lhs</c> and <c>rhs</c></returns>
 */
        public static int[] Concat(int[] lhs, int[] rhs)
        {
            int[] aux = new int [lhs.Length + rhs.Length];
            for (int i = 0; i <= lhs.Length - 1; i++)
            {
                aux[i] = lhs[i];
            }

            for (int a = 0; a <= rhs.Length - 1; a++)
            {
                aux[a + lhs.Length] = rhs[a];
            }

            return aux;
        }

        /**
 * <summary>Determines whether a given array is sorted in ascending order.</summary>
 * <param name="arr">An array of integers.</param>
 */
        public static bool IsSorted(int[] arr)
        {
            int j = arr.Length - 1;
            if (j < 1) return true;
            int ai = arr[0], i = 1;
            while (i <= j && ai <= (ai = arr[i])) i++;
            return i > j;
        }

        /**
 * <summary>Sorts an array in-place in ascending order using insertion sort.</summary>
 * <param name="arr">An array of integers.</param>
 */
        public static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; i++)
            {
                int aux = arr[i];
                int j = i - 1;


                while (j >= 0 && arr[j] > aux)
                {
                    arr[j + 1] = arr[j];
                    j += 1;
                }

                arr[j + 1] = aux;
            }
        }

        /**
 * <summary>Sorts an array in ascending order using any sorting algorithm.</summary>
 * <param name="arr">An array of integers.</param>
 *
 * BONUS
 */
        //Bubble Sort
        public static void OtherSort(int[] arr)
        {
            var n = arr.Length;
            if (n <= 1)
            {
                return;
            }
            while (n != 0)
            {
                var aux = 0;
                for (var i = 1; i < n; i++)
                {
                    if (arr[i - 1] <= arr[i])
                        continue;
                    References.Swap(ref arr[i - 1], ref arr[i]);
                    aux = i;
                }
                n = aux;
            }
        }
    }
}