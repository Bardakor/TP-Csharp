using System;
using System.Text;

namespace Basics
{
    public class Bonus
    {
        public static char CaesarChar(char c, uint n)
        {
            return (char) (((c + n - 65) % 26) + 65);
        }
    }
}