using System;

namespace Maze
{
    public static class Program
    {
        public static void Main()
        {
            char[,] maze =
            {
                {'#', 'x', '#', '#', '#', '#'},
                {'#', 'o', '#', ' ', 'o', '#'},
                {'#', 'o', '#', ' ', 'o', '#'},
                {'#', 'o', 'o', 'o', 'o', '#'},
                {'#', 'o', 'o', '#', ' ', '#'},
                {'#', 'o', '#', '#', '#', '#'},
                {'#', 'o', 'o', 'o', 'o', '#'},
                {'#', '#', '#', '#', '@', '#'},
            };
            Console.WriteLine(Maze.IsPathValid(maze));
        }
    }
}