using System;
using System.Collections.Generic;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;

namespace SherlocksGambit.Utils.Helpers
{
    /// Enum of all the possible directions a piece can go in
    public enum Direction
    {
        UpperRight = -3,
        LowerLeft = 4,
        LowerRight = 5,
        UpperLeft = -4
    }

    public static class DirectionHelper
    {
        /**
         * <summary> Gets the list of all directions the given piece can move in </summary>
         * <param name="piece"> BasePiece asking for direction </param>
         * <returns> A list of all the possible direction the piece can move in </returns>
         * <remarks> The list is ordered in a clockwise way from upper right to upper left </remarks>
         */
        public static List<Direction> GetDirections(BasePiece piece)
        {
            if (piece is King)
                return new List<Direction> 
                    {Direction.UpperRight, Direction.LowerRight, Direction.LowerLeft, Direction.UpperLeft};
            
            return piece.Color == PlayerColor.Black ? new List<Direction> {Direction.LowerRight, Direction.LowerLeft} 
                : new List<Direction> {Direction.UpperRight, Direction.UpperLeft};
        }

        /**
         * <summary> Gets the board position of the next cell from a board position in a direction </summary>
         * <param name="pos"> Position in board notation of the current cell </param>
         * <param name="direction"> Direction in which to look for </param>
         * <returns> A position in board notation of the found cell or -1 if the cell was OutOfBounds </returns>
         * <remarks>
         * The board notation is different than the array notation!  
         * We chose to return -1 instead of raising and exception for performance issues
         * </remarks>
         */
        public static int GetCellPosInDirection(int pos, Direction direction)
        {
            // If `pos` is outside of bounds, return -1
            if (pos is <= 0 or > Board.NbPositions)
                return -1;

            // Check if you are trying to get an impossible direction. If so return -1
            switch (pos)
            {
                case <= Board.RowLength when direction is Direction.UpperLeft or Direction.UpperRight:
                case >= Board.NbPositions - Board.RowLength + 1
                    when direction is Direction.LowerLeft or Direction.LowerRight:
                case >= Board.RowLength and <= Board.NbPositions - Board.RowLength + 1
                    when pos % (Board.RowLength * 2) == Board.RowLength + 1 &&
                         direction is Direction.LowerLeft or Direction.UpperLeft ||
                         pos % (Board.RowLength * 2) == Board.RowLength &&
                         direction is Direction.LowerRight or Direction.UpperRight:
                    return -1;
            }

            // Return the according position
            return pos + (int) direction - (pos - 1) / Board.RowLength % 2;
        }
        
        /**
         * <summary> Checks if the piece is all the way at the end of the board </summary>
         * <param name="piece"> The BasePiece to check for </param>
         * <returns> A boolean. If true, the piece can be promoted </returns>
         * <remarks> [BONUS] A King could call this function but this is an expected behaviour for RevertMove </remarks>
         */
        public static bool IsOnKingsRow(BasePiece piece)
        {
            // If the king is at the end of the board returns true, else false
            return piece.CurrentCell.BoardPosition switch
            {
                <= Board.RowLength when piece.Color == PlayerColor.White => true,
                >= Board.NbPositions - Board.RowLength + 1 when piece.Color == PlayerColor.Black => true,
                _ => false
            };
        }
    }
}