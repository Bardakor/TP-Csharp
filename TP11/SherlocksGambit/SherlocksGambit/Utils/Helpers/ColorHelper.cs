using System;
using System.Collections.Generic;

namespace SherlocksGambit.Utils.Helpers
{
    // Enum of the two players' color
    public enum PlayerColor
    {
        Black = 1,
        White = -1,
    }

    public static class ColorHelper
    {
        /// Gets the opponent color
        public static PlayerColor GetOpponentColor(PlayerColor currPlayer)
        {
            return (PlayerColor) ((int) currPlayer * -1);
        }
    }
}