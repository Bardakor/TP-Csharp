using System;
using System.Collections.Generic;
using System.Linq;

namespace AsciiDot
{
    public class AsciiDot
    {
        private readonly Board board;
        public List<Dot> Dots { get; private set; }

        public List<(Point, Direction)> History = new();

        public AsciiDot(Board board)
        {
            // TODO
            throw new NotImplementedException("TODO");
        }

        private List<Dot> UpdateDot(Dot dot)
        {
            // TODO
            throw new NotImplementedException("TODO");
        }

        public void UpdateGame()
        {
            // TODO
            throw new NotImplementedException("TODO");
        }

        private bool IsRunning => Dots.Any();

        public void Launch(bool print)
        {
            // TODO
            throw new NotImplementedException("TODO");
        }


        public override string ToString()
        {
            var content = "";

            for (var y = 0; y < board.Height; ++y)
            {
                for (var x = 0; x < board.Width; x++)
                {
                    var point = new Point(x, board.Height - y - 1);
                    content += ColorNumber(point) + board.Get(point).Value;
                }

                content += "\x1b[0m\n";
            }

            return content;
        }

        private string ColorNumber(int x, int y) =>
            ColorNumber(new Point(x, y));

        private string ColorNumber(Point point) =>
            // use of bash code color: https://misc.flogisoft.com/bash/tip_colors_and_formatting
            CountDotsAt(point) switch
            {
                0 => "\x1b[0m",
                1 => "\x1b[31m",
                2 => "\x1b[32m",
                3 => "\x1b[33m",
                4 => "\x1b[34m",
                5 => "\x1b[35m",
                6 => "\x1b[36m",
                7 => "\x1b[37m",
                _ => "\x1b[30m",
            };

        private int CountDotsAt(Point point) =>
            Dots.Count(point.Equals) + board.Get(point).PointInside;
    }
}
