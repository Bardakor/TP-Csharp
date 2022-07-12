using System;
using System.Linq;

namespace puzzle_game
{
    public partial class Board
    {
        public bool Solved { get; }

        public int Size { get; }

        public int Width { get; }

        public Tile[] Board1 { get; private set; }

        public Board(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Size must be positive");

            if (Math.Sqrt(size) % 1 != 0)
                throw new ArgumentException("Size must e perfect square");

            Size = size;
            Width = Convert.ToInt32(Math.Sqrt(size));
            Board1 = new Tile[size];
            Solved = false;
        }

        public Board DeepCopy()
        {
            var copy = (Board)MemberwiseClone();

            copy.Board1 = new Tile[copy.Size];

            for (var i = 0; i < copy.Size; i++)
                copy.Board1[i] = Board1[i].DeepCopy();

            return copy;
        }

        public bool AreConsecutive(int[] arr)
        {
            if (arr.Length < 1)
                return false;

            var min = arr.Min();
            var max = arr.Min();

            if (max - min + 1 != arr.Length)
                return false;

            var visited = new bool[arr.Length];

            foreach (var e in arr)
            {
                if (visited[e - min])
                    return false;

                visited[e - min] = true;
            }

            return true;
        }

        public void Fill(int[] array)
        {
            if (array.Length != Size)
                throw new ArgumentException("Size not matching");
            if (!AreConsecutive(array))
                throw new ArgumentException("Array must be composed of consecutive elements");

            var holes = 0;

            for (var i = 0; i < Size; i++)
            {
                var item = array[i];

                holes += item == 0 ? 1 : 0;

                Board1[i] = new Tile(item, item == 0);
            }

            if (holes != 1)
                throw new ArgumentException("There is more than one hole !");
        }

        public void Fill()
        {
            // FIXME
            throw new NotImplementedException();
        }
    }
}