using System;

namespace puzzle_game
{
    public partial class Board
    {

        public string PadCenter(string s, int width, char c)
        {
            //return string s, padded right, left, or both ways with the character c until it reaches the width
            int padLeft = (width - s.Length) / 2;
            int padRight = width - s.Length - padLeft;
            string padded = "";
            for (int i = 0; i < padLeft; i++)
            {
                padded += c;
            }
            padded += s;
            for (int i = 0; i < padRight; i++)
            {
                padded += c;
            }
            return padded;
        }
        
        public void PrintLine(int i, int width, int longest_number)
        {
            //displays intermediate lines of the board
            string line = "";
            for (int j = 0; j < width; j++)
            {
                if (board[i * width + j].Type == TileType.EMPTY)
                {
                    line += PadCenter(" ", longest_number, ' ');
                }
                else
                {
                    line += PadCenter(board[i * width + j].Value.ToString(), longest_number, ' ');
                }
            }
            Console.WriteLine(line);
        }
        
        public void Print()
        {
            //displays the board
            int longest_number = 0;
            for (int i = 0; i < size; i++)
            {
                if (board[i].Value.ToString().Length > longest_number)
                {
                    longest_number = board[i].Value.ToString().Length;
                }
            }
            for (int i = 0; i < size; i++)
            {
                if (i % width == 0)
                {
                    PrintLine(i / width, width, longest_number);
                }
            }
            PrintLine(size / width, width, longest_number);
        }
        
    }
}