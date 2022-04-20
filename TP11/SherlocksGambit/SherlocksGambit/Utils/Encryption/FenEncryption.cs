using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Utils.Encryption
{
    public static class FenEncryption
    {
        /**
         * <summary> Encodes a board in its FEN notation </summary>
         * <param name="board"> Board on which to place the newly created pieces </param>
         * <remarks>
         * The FEN notation and our array notation are inverted. It's up to you to find a way to coincide both notations  
         * Indeed FEN starts at the lower left, left to right, bottom up  
         * While `Cells` starts at the upper left, left to right, top down
         * </remarks>
         */
        public static string Encrypt(Board board)
        {
            string fen = "";
            string line = "";
            //using Board.Boardsize, traverse the board and add the pieces to the fen string using a switch statement

            int emptyCells = 0;
            for (int j = 0; j < board.Positions.Length; j++)
            {
                //make a switch statement to add the pieces to the fen string using CurrentPiece and emptyCell
                switch (board.Positions[j].CurrentPiece)
                {
                    case King:
                        if (emptyCells > 0)
                        {
                            line += emptyCells.ToString();
                            emptyCells = 0;
                        }
                        //verify the color of the king
                        if (board.Positions[j].CurrentPiece.Color == PlayerColor.White)
                        {
                            line += "k";
                        }
                        else
                        {
                            line += "K";
                        }

                        break;
                    case Man:
                        if (emptyCells > 0)
                        {
                            line += emptyCells.ToString();
                            emptyCells = 0;
                        }
                        //verify the color of the Man
                        if (board.Positions[j].CurrentPiece.Color == PlayerColor.White)
                        {
                            line += "m";
                        }
                        else
                        {
                            line += "M";
                        }

                        break;
                    case null:
                        //if the cell is empty, add a number of empty cells to the fen string
                        emptyCells++;
                        break;
                }
                //if the cell is not empty, add the number of empty cells to the fen string
                
                if (j % Board.RowLength  == 3 && j != 0)
                {
                    if (emptyCells > 0)
                    {
                        line += emptyCells.ToString();
                        emptyCells = 0;
                    }

                    fen = line + fen;
                    line = "";
                    fen = "/" + fen;
                    
                }
            }


            fen = fen.Remove(0,1);
            return fen;
        }

        /**
         * <summary>
         * Decodes a FEN-encoded board and places the pieces on the given board and in `PiecesDictionary`
         * </summary>
         * <param name="pieceManager"> PieceManager in which to put the newly created pieces </param>
         * <param name="board"> Board on which to place the newly created pieces </param>
         * <param name="fenCode"> FEN to decode. It is assumed to be not null </param>
         * <example> This is a valid FEN code: mmmm/mmmm/4/4/4/4/MMMM/MMMM </example>
         * Your code should throw exceptions if the code cannot be decrypted
         * <exception cref="ArgumentException"> "Invalid board size" </exception>
         * <exception cref="ArgumentException"> "Wrong number of pieces in a single row" </exception>
         * <exception cref="ArgumentException"> "Invalid empty space amount" </exception>
         * <exception cref="ArgumentException"> "Invalid character" </exception>
         * <remarks>
         * The FEN notation and our array notation are inverted. It's up to you to find a way to coincide both notations  
         * Indeed FEN starts at the lower left, left to right, bottom up  
         * While `Cells` starts at the upper left, left to right, top down
         * </remarks>
         */
        public static void Decrypt(PieceManager pieceManager, Board board, string fenCode)
        {
            // Split the string by row and check if there is the exact number of rows
            var rows = fenCode.Split('/');
            if (rows.Length != Board.BoardSize)
                throw new ArgumentException("Invalid board size");

            // FEN notations starts from the lower left corner and goes to the upper right corner row by row
            // In order to match our representation of the board we need to reverse the array `rows` that way we have a
            // left to right and top to bottom representation of the board

            // `index` is the index of the cell in `board.Positions`
            var index = 0;
            foreach (var row in rows.Reverse())
            {
                // `rowPieceCounter` makes sure that there is EXACTLY row's length pieces per row (pieces can be null)
                var rowPieceCounter = 0;

                // Iterate through this row's code segment
                foreach (var c in row)
                {
                    // If you already placed 4 pieces (pieces could have been null), throw an exception
                    if (rowPieceCounter >= Board.RowLength)
                        throw new ArgumentException("Wrong number of pieces in a single row");

                    // Temporary variables for code readability
                    var tmpChar = c;
                    var pieceColor = PlayerColor.White;

                    // If `c` is a digit it then indicates how many cells to skip
                    // (hence `CurrentPiece` for those cells will be null)
                    if (char.IsDigit(c))
                    {
                        // Calculates the amount to skip. If greater than for or less than 0 (should not happen haha)
                        // then throw and exception
                        var amount = c - '0';
                        if (amount is <= 0 or > Board.RowLength)
                            throw new ArgumentException("Invalid empty space amount");

                        // Update the index and the piece counter
                        index += amount;
                        rowPieceCounter += amount;

                        // Skips to the next character
                        continue;
                    }

                    // If `c` is an upper character, set the new piece's color to black and lower it
                    if (char.IsUpper(c))
                    {
                        tmpChar = char.ToLower(c);
                        pieceColor = PlayerColor.Black;
                    }

                    // At this point you have a lowercase char. If it is not an 'm' or and 'k' it is not recognized and
                    // thus an exception is thrown
                    if (tmpChar is not ('m' or 'k'))
                        throw new ArgumentException("Invalid character");

                    // Create the new piece with its appropriate type and color
                    BasePiece newPiece = tmpChar is 'm' ? new Man(pieceColor) : new King(pieceColor);

                    // Put the new piece in the board and in the pieceManager's lists
                    pieceManager.AddPiece(newPiece);
                    newPiece.Place(board.Positions[index]);

                    // Update the index the amount of pieces in the current row
                    index++;
                    rowPieceCounter++;
                }

                // If at the end of the row's code segment you did not add a total of 4 pieces (null included) an
                // exception must be thrown
                if (rowPieceCounter != Board.RowLength)
                    throw new ArgumentException("Wrong number of pieces in a single row");
            }
        }
    }
}