using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monopoly
{
    class Program
    {
        static void Main()
        {
            Game game = Serializer.Load("../../../GameLayout/game");
            //Do not modify the line above
        }
    }
}