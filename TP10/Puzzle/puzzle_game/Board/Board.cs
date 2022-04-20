using System;

namespace puzzle_game
{
    public partial class Board
    {
        private int size;
        private int width;
        private bool solved = false;
        private Tile[] board;

        public bool Solved => solved;

        public int Size => size;

        public int Width => width;

        public Tile[] Board1 => board;

        public Board(int size)
        {
            this.size = size;
            this.width = (int)Math.Sqrt(size);
            this.board = new Tile[size];
            if (size < 1 || (size % 1 != 0))
            {
                throw new ArgumentException("size must be a positive integer that is a perfect square");
            }
            for (int i = 0; i < size; i++)
            {
                board[i] = new Tile(i + 1, Solved);
            }
        }

        public Board DeepCopy()
        {
            Board newBoard = new Board(size);
            for (int i = 0; i < size; i++)
            {
                newBoard.board[i] = this.board[i];
            }
            return newBoard;
        }

        public bool AreConsecutive(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] + 1 != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        public void Fill(int[] array)
        {
            if (array.Length != size)
            {
                throw new ArgumentException("array must be of size " + size);
            }
            if (!AreConsecutive(array))
            {
                throw new ArgumentException("array must be consecutive");
            }
            for (int i = 0; i < size; i++)
            {
                //if the tile has value 0, it is Tile.Empty
                if (array[i] == 0)
                {
                    board[i] = new Tile(0, true);
                }
                else
                {
                    board[i] = new Tile(array[i], false);
                }
            }
        }

        public void Fill()
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i + 1;
            }
            array[size - 1] = 0;
            Fill(array);
        }

    }
}
