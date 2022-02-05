using System;

namespace Factory06
{
    class Program
    {
        static void Main(string [] args)
        {
            // You can write your test here.
            // But don't forget to remove them, and keep the next lines
            // as they are for your last push.
            Game game = new Game(100, 500);
            Bot bot = new MyBot();
            
            Console.WriteLine("Score: " + game.Launch(bot));
        }
    }
}
