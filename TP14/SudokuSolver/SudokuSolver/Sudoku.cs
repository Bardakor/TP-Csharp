using System;

namespace SudokuSolver
{
    public class Sudoku
    {
        public const int BoardSize = 9;

        public int[,] Board;


        public Sudoku(int[,] board)
        {
            board = new int[BoardSize, BoardSize];

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    board[i, j] = 0;
                }
            }

            Board = board;
        }

        public void Load(string str)
        {
            if (str.Length != 81)
                throw new ArgumentException();

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    char c = str[y * 9 + x];
                    if (c == '.')
                        c = '0';
                    else if (c < '1' || c > '9')
                        throw new ArgumentException();
                    Board[y, x] = c - '0';
                }
            }
        }

        public Sudoku(int difficulty)
        {
            Board = new int[BoardSize, BoardSize];

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }


        public bool already_in_Square(int x, int y, int value)
        {
            x = x / 3;
            y = y / 3;

            for (int Y = 0; Y < 3; Y++)
            {
                for (int X = 0; X < 3; X++)
                {
                    if (Board[Y + y * 3, X + x * 3] == value)
                        return true;
                }
            }

            return false;
        }

        public bool already_in_column(int x, int val)
        {
            for (int y = 0; y < 9; y++)
            {
                if (Board[y, x] == val)
                    return true;
            }

            return false;
        }

        public bool already_in_row(int y, int val)
        {
            for (int x = 0; x < 9; x++)
            {
                if (Board[y, x] == val)
                    return true;
            }

            return false;
        }

        public bool IsBoardValid()
        {
            //verify that already_in_Square is false for all values and that already_in_column and already_in_row are false for all values

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (already_in_Square(x, y, Board[y, x]) || already_in_column(x, Board[y, x]) ||
                        already_in_row(y, Board[y, x]))
                        return false;
                }
            }

            return true;
        }

        public bool is_column_solved(int x)
        {
            for (int i = 1; i <= 9; i++)
            {
                bool found = false;
                for (int y = 0; !found && y < 9; y++)
                {
                    if (Board[y, x] == i)
                        found = true;
                }

                if (!found)
                    return false;
            }

            return true;
        }

        public bool is_line_solved(int y)
        {
            for (int i = 1; i <= 9; i++)
            {
                bool found = false;
                for (int x = 0; !found && x < 9; x++)
                {
                    if (Board[y, x] == i)
                        found = true;
                }

                if (!found)
                    return false;
            }

            return true;
        }

        public bool is_square_solved(int x, int y)
        {
            x /= 3;
            y /= 3;

            for (int i = 1; i <= 9; i++)
            {
                bool found = false;
                for (int Y = 0; !found && Y < 3; Y++)
                {
                    for (int X = 0; !found && X < 3; X++)
                    {
                        if (Board[Y + y * 3, X + x * 3] == i)
                            found = true;
                    }
                }

                if (!found)
                    return false;
            }

            return true;
        }

        public bool IsSolved()
        {
            //verify that there are no 0s in the board

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (Board[y, x] == 0)
                        return false;
                }
            }

            for (int i = 0; i < BoardSize; i++)
            {
                if (!is_column_solved(i) || !is_line_solved(i) || !is_square_solved(i / 3, i % 3))
                    return false;
            }

            return true;
        }

        public void SetNextCoords(ref int nextX, ref int nextY)
        {
            if (nextX >= 9)
            {
                nextX = 0;
                nextY++;
            }
        }

        public bool solve_rec(int x, int y)
        {
            if (y >= 9)
                return true;

            int nextX = x + 1;
            int nextY = y;
            SetNextCoords(ref nextX, ref nextY);

            if (Board[y, x] != 0)
                return solve_rec(nextX, nextY);

            for (int i = 1; i <= 9; i++)
            {
                if (!already_in_column(x, i) && !already_in_row(y, i) && !already_in_Square(x, y, i))
                {
                    Board[y, x] = i;
                    if (solve_rec(nextX, nextY))
                        return true;
                    Board[y, x] = 0;
                }
            }

            return false;
        }

        public bool Solve()
        {
            return solve_rec(0, 0);
        }

        public static void Play()
        {
            //make the player choose a difficulty in the console
            Console.WriteLine("Choose a difficulty:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");

            int difficulty = Convert.ToInt32(Console.ReadLine());

            if (difficulty == 1)
            {
                for (int i = 0; i < 45; i++)
                {
                    Sudoku sudoku = new Sudoku(difficulty);
                    sudoku.Solve();
                    sudoku.Print();
                }
            }
            else if (difficulty == 2)
            {
                for (int i = 0; i < 30; i++)
                {
                    Sudoku sudoku = new Sudoku(difficulty);
                    sudoku.Solve();
                    sudoku.Print();
                }
            }

            else if (difficulty == 3)
            {
                for (int i = 0; i < 20; i++)
                {
                    Sudoku sudoku = new Sudoku(difficulty);
                    sudoku.Solve();
                    sudoku.Print();
                }
            }
        }

        #region Provided

        private const int HorizontalMargin = 2;

        private void Print()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                if (i == 0)
                    PrintLine('┌', '┬', '┐');
                else
                    PrintLine('├', '┼', '┤');

                for (int j = 0; j < BoardSize; j++)
                {
                    Console.Write('│');
                    PrintWithMargins(Board[i, j] == 0 ? " " : Board[i, j].ToString());
                }

                Console.WriteLine('│');
            }

            PrintLine('└', '┴', '┘');
        }

        private void PrintLine(char start, char middle, char end)
        {
            int caseSize = (2 + HorizontalMargin * 2);

            Console.Write(start);
            for (int i = 0; i < caseSize * BoardSize - 1; i++)
            {
                Console.Write((i + 1) % caseSize == 0 ? middle : '─');
            }

            Console.WriteLine(end);
        }

        private void PrintWithMargins(string value)
        {
            for (int i = 0; i < HorizontalMargin * 2 + 1; i++)
            {
                Console.Write(i == HorizontalMargin ? value : ' ');
            }
        }

        #endregion
    }
}