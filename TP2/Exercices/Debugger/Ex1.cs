using System;

namespace Debugger
{
	public class Ex1
	{
		public static int Exo1() //FIXME
		{
			bool stop = false;
			int div = 42;
			while (!stop) 
			{
				bool isDivisor = Misc.IsDivisorOf (666, div);
				stop = isDivisor;
				++div;
			}
			return --div;
		}
	}
}

