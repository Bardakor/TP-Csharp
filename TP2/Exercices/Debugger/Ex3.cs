using System;

namespace Debugger
{
	public class Ex3
	{
		/*
		 *  This function sort the array and return true.
		 */
		public static bool Exo3() //FIXME maybe
		{
			int[] array = { 5, 1337, 42, 666, 1, 3, 1024};
			for (uint i = 0; i < Misc.GetLength (array); ++i) 
			{
				SubFunction1 (array, i, array [i]);
			}
			return SubFunction2 (array);
		}

		private static void SubFunction1(int[] arr, uint count, int val) //FIXME maybe
		{
			uint i = count;
			while (i > 0 && arr[i-1] > val) 
			{
				arr [i] = arr [i - 1];
				--i;
			}
			arr [i] = val;	
		}

		/*
		 *  This function check if an array is sorted.
		 */
		private static bool SubFunction2(int[] arr) //FIXME maybe
		{
			uint i = Misc.GetLength(arr) - 1;
			for (; i > 1 && arr[i - 1] <= arr[i]; --i)
				continue;
			return i == 1;
		}
	}
}

