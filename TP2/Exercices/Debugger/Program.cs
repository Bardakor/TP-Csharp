using System;

namespace Debugger
{
	public class Program
	{
		static void Main()
		{
			if (Ex1.Exo1 () == 74)
				Console.WriteLine ("Exercise 1: OK\n");
			if (Ex2.Exo2 () == 45)
				Console.WriteLine ("Exercise 2: OK\n");
			if (Ex3.Exo3 ())
				Console.WriteLine ("Exercise 3: OK");
		}
	}
}

