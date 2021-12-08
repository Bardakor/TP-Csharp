namespace Debugger
{
	public class Misc
	{
		public static bool IsDivisorOf(int number, int divisor)
		{
			return number % divisor == 0;
		}

		public static uint GetLength(int[] array)
		{
			return (uint)(array.Length);
		}

	}
}

