using System;
using System.Collections.Generic;

namespace puzzle_game
{
    public partial class Board
    {
        public bool IsCorrect()
        {
            var previous = Board1[0].Value;

            foreach (var tile in Board1)
            {
                if (tile.Value < previous && tile.Type == TileType.FULL)
                    return false;
                previous = tile.Value;
            }

            return previous == 0;
        }

        public int FindEmptyPos()
        {
            var board1 = new int[Size];
            var board2 = new List<int>();

            for (var i = 0; i < Size; i++)
                board1[i] = Board1[i].Value;

            for (var y = 0; y < Width; y++)
            for (var x = 0; x < Width; x++)
                board2.Add((board1[x * Width + y]));

            board1 = board2.ToArray();

            var result = 0;
            for (var i = 0; i < Width * Width - 1; i++)
            for (var j = i + 1; j < Width * Width; j++)
                if (board1[i] != 0 && board1[j] != 0 && board1[i] > board2[j])
                    result++;

            return result;
        }

        // we strongly recommend to implement a separate
        // function to get the inverse count
        public int GetInvCount()
        {
            for (var i = 0; i < Size; i++)
                if (Board1[i].Type == TileType.EMPTY)
                    return i;
            return -1;
        }

        public bool IsSolvable()
        {
            var InvCount = GetInvCount();

            if (Width % 2 == 1)
                return InvCount % 2 == 0;

            var pos = FindEmptyPos();

            if (pos % 2 == 1)
                return InvCount % 2 == 0;

            return InvCount % 2 == 1;
        }
    }
}