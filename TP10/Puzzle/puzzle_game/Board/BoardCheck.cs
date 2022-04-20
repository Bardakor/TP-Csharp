using System;

namespace puzzle_game
{
    public partial class Board
    {

        public bool IsCorrect()
        {
            //check if all the tiles in board are ordered
            for (int i = 0; i < size; i++)
            {

                if (board[i].Value != i + 1)
                {
                    return false;
                }
                if (i == size - 1 && board[i].Type != TileType.EMPTY)
                {
                    return false;
                }
            }
            return true;
        }

        public int FindEmptyPos()
        {
            //return the index of the empty tile
            for (int i = 0; i < size; i++)
            {
                if (board[i].Type == TileType.EMPTY)
                {
                    return i;
                }
            }
            return -1;
        }

        // we strongly recommend to implement a separate
        // function to get the inverse count
        public int GetInvCount()
        {
            int invCount = 0;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (board[i].Value > board[j].Value)
                    {
                        invCount++;
                    }
                }
            }
            return invCount;
        }

        public bool IsSolvable()
        {
            //N is the size of the board
            int N = size;
            //if n is odd and the number of inversions is even, then the puzzle is solvable
            if (N % 2 == 1)
            {
                if (GetInvCount() % 2 == 0)
                {
                    return true;
                }
            }
            //if n is even and the blank is on an even row counting from the bottom and the numnber of inversion is odd, the puzzle is solvable
            else
            {
                if (FindEmptyPos() % 2 == 0)
                {
                    if (GetInvCount() % 2 == 1)
                    {
                        return true;
                    }
                }
                //if n is even and the blank is on an odd row counting from the bottom and the number of inversions is even, the puzzle is solvable
                else
                {
                    if (GetInvCount() % 2 == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}