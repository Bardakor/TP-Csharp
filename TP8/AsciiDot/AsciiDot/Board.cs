using System;
using System.Collections.Generic;
using System.IO;
using AsciiDot.Token;

namespace AsciiDot
{
    public class Board
    {
        /**
         * <summary>Dump file content.</summary>
         *
         * <param name="path">The path where the file need to be load</param>
         * 
         * <returns>Return the content of the file</returns>
         */
        private static string LoadContent(string path)
        {
            string content = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                    content = sr.ReadToEnd();
                
                // remove the last line break
                content = content.Substring(0, content.Length - 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return content;
        }

        /**
         * <summary>Constructor of Board</summary>
         * 
         * <param name="path">A path of a file in programming language based on ascii art: Ascii Dots</param>
         */
        public Board(string path)
        {
            string content = LoadContent(path);

            int height = content.Split('\n').Length;
            int width = 0;
            
            foreach (string line in content.Split('\n'))
                if (line.Length > width)
                    width = line.Length;
            
            char[][] board = new char[height][];
            Matrix = new Token.Token[width, height];
            
            for (int i = 0; i < height; i++)
            {
                string line = content.Split('\n')[i];
                board[i] = line.ToCharArray();
            }
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var tkn = Lexer.Lex(board, x, y);
                    Matrix[x, y] = tkn;
                }
            }
            
            
            
            Console.WriteLine(this);
            

        }

        /**
         * <summary>Matrix of token in the matrix</summary>
         * 
         * the first dimension is the rows and the second the  columns. in other word the grid is represent like in
         * maths graph (x, y).
         */
        public Token.Token[,] Matrix { get; }

        /**
         * <summary>get the number of column (x axes) in the matrix</summary>
         */
        public int Width => Matrix.GetLength(0);

        /**
         * <summary>get the number of row (y axes) in the matrix</summary>
         */
        public int Height => Matrix.GetLength(1);


        /**
         * <summary>check if a point is inside the board</summary>
         *
         * <param name="point">the point to test</param>
         *
         * <returns>True if the point is on the board. otherwise false.</returns>
         */
        private bool IsInside(Point point) =>
            IsInside(point.X, point.Y);

        /**
         * <summary>Check if the position (x, y) is in Matrix</summary>
         * 
         * <param name="x">The xth row in <c>Matrix</c>. Can be negative!</param>
         * <param name="y">The yth column in <c>Matrix</c>. Can be negative!</param>
         *
         * <returns>True if the point is on the board. otherwise false.</returns>
         */
        private bool IsInside(int x, int y) =>
            x >= 0 && x < Width &&
            y >= 0 && y < Height;

        /**
         * <summary>get the token at the given coordinate</summary>
         * 
         * <param name="point">The position of the token</param>
         *
         * <returns>
         * The token at the given point on the board. If it's outside the board the function return a
         * <see cref="TokenEmpty"/>
         * </returns>
         */
        public Token.Token Get(Point point) =>
            Get(point.X, point.Y);

        /**
         * <summary>get the token at the given coordinate</summary>
         * 
         * <param name="x">The xth row in <c>Matrix</c>. Can be negative!</param>
         * <param name="y">The yth column in <c>Matrix</c>. Can be negative!</param>
         *
         * <returns>
         * The token at the given coordinate on the board. If it's outside the board the function return a
         * <see cref="TokenEmpty"/>
         * </returns>
         */
        public Token.Token Get(int x, int y) =>
            IsInside(x, y)
                ? Matrix[x, y]
                : new TokenEmpty();

        /**
         * <summary>set the token at the given coordinate</summary>
         * 
         * <param name="point">The position of the token</param>
         * <param name="token">new token value</param>
         */
        public Token.Token Set(Point point, Token.Token token) =>
            Set(point.X, point.Y, token);

        /**
         * <summary>set the token at the given coordinate</summary>
         * 
         * <param name="x">The xth row in <c>Matrix</c>. Can't be negative or outside of the board!</param>
         * <param name="y">The yth column in <c>Matrix</c>. Can't be negative or outside of the board!</param>
         * <param name="token">new token value</param>
         */
        public Token.Token Set(int x, int y, Token.Token token) =>
            Matrix[x, y] = token;

         /**
         * <summary>
         *     Search all dots in <c>Matrix</c>
         *     and add the dot in a new list of type <c>Dot</c>
         * </summary>
         * <returns>Return a list of dots find in <c>Matrix</c></returns>
         */
        public List<Dot> StartDots()
        {
            var dots = new List<Dot>();
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (Matrix[x, y] is TokenStart dot)
                    {
                        dots.Add(new Dot(x, y, Direction.Right));
                    }
                }
            }

            return dots;
        }

        /**
         * <summary>
         *     Find the starting direction of the dot
         *     Check all direction in clockwise, starting with Direction.Up
         * </summary>
         * <param name="dot">A new dot</param>
         * <returns>Return true if find a direction, false if not</returns>
         */
        private bool FindStartDirection(Dot dot)
        {
            var direction = Direction.Up;
            
            

            return false;
        }

        /**
         * <summary>Check if the starting direction is correct</summary>
         * <param name="startDirection">The starting direction of a fresh dot</param>
         * <param name="token">
         *     The token obtained in Matrix by the movement of the fresh dot by
         *     <c>startDirection</c>
         * </param>
         * <returns>
         *     Return true if the fresh dot can be move on the direction
         *     <c>startDirection</c>, false if not
         * </returns>
         */
        private static bool IsStartToken(Direction startDirection, Token.Token token)
        {
            // TODO
            throw new NotImplementedException("TODO");
        }

        /**
          * <summary>Print the board in string</summary>
          * 
          * <example>
          * <code>
          *     Console.WriteLine(board);
          * </code>
          * </example>
          */
        public override string ToString()
        {
            var content = "";

            // loop over each row (stating by the end)
            for (var y = Height - 1; y >= 0; --y)
            {
                // loop over each column (stating by 0)
                for (var x = 0; x < Width; x++)
                    content += Matrix[x, y].Value;

                // add new line
                content += "\n";
            }

            // return the completed bord
            return content;
        }
    }
}