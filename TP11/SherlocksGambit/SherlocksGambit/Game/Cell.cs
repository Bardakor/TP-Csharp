using SherlocksGambit.Game.Pieces;

namespace SherlocksGambit.Game
{
    public class Cell
    {
        /// Characterize the state of a cell 
        public enum CellState
        {
            Friendly,
            Enemy,
            Free,
            OutOfBounds
        }
        
        /// Reference to the Board the cell is on
        public readonly Board Board;
        
        /// Reference to the current piece on the cell
        public BasePiece CurrentPiece;
        
        /// The position of the cell in board notation
        public readonly int BoardPosition;

        /**
         * <summary> Basic constructor for Cell </summary>
         * <param name="boardPosition"> Position of the cell in board notation </param>
         * <param name="board"> The Board the cell is on </param>
         * Initialize all attributes on this class
         */
        public Cell(int boardPosition, Board board)
        {
            BoardPosition = boardPosition;
            Board = board;
        }
    }
}