using System;

namespace Monopoly
{
    public class Jail : Special
    {
        public Jail(int position)
        {
            this.position = position;
        }

        static Random random = new Random();

        public static void AttemptDiceDouble(Player player)
        {
            if (random.NextDouble() <= 1.0 / 6.0)
            {
                player.jailed = false;
                Console.WriteLine("Congratulations you are now free of prison !");
            }
            else
                Console.WriteLine("You did not do a double therefore you're still in jail");
        }

        public override string ToString()
        {
            return "[Jail]";
        }
    }
}