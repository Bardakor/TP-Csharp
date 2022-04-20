using System;
using System.IO;
using System.Linq;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Bonuses;

namespace SherlocksGambit.Game
{
    public partial class Board
    {
        /// Boolean indicating whether history is enabled on this board
        public bool MoveHistory = false;

        /// `HistoryLength` is how many move you want to store in `_history`
        private const int HistoryLength = 5;
        
        /// `_history` hold Zobrist's board notations
        private readonly uint[] _history = new uint[HistoryLength];
        
        /// `_historyIndex` is where in `_history` the board is currently at
        private int _historyIndex;

        /**
         * <summary> [BONUS] Overload fot the Board constructor </summary>
         * <param name="history"> Copy of an history ready to assign </param>
         * <param name="historyIndex"> Where in `history` the previous board was </param>
         * Initialize all attributes on this class
         * <remarks>
         * Do not forget to initialize the other attributes of the Board!  
         * A partial class has access to its attributes across all its files  
         * We also do not wish to create to initialize pieces on this Board since this constructor will be called for
         * copy only (the pieces will get placed on the Board with the previously implemented `Copy` function)
         * </remarks>
         */
        private Board(uint[] history, int historyIndex)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> [BONUS] Constructs a new Board with the current Board's history and index onto it </summary>
         * <returns> Returns the copied board, initialized but with no pieces on top </returns>
         * <remarks>
         * After implementing this function, do not forget to call it in the `Copy` function  
         * However, this function should only be called if `MoveHistory` is set to true (value set by AiProperties.cs) 
         * </remarks>
         */
        private Board ConstructWithHistory()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        /**
         * <summary> [BONUS] Registers a move into the board's history </summary>
         * <returns> Returns a boolean indicating if the move was already present in the history </returns>
         * <remarks>
         * The history records Zobrist's hashes of the current Board  
         * A move should still be recorded even if it was already present in the history
         * </remarks>
         */
        public bool RegisterMove()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> [BONUS] Creates a modular Starting fen code </summary>
         * The FEN string must respect the constants `BoardSize` and `RowLength`
         * <returns> A starting FEN code suited for the Board's modular size </returns>
         * <remarks>
         * If this method is not implemented, it returns the starting FEN code for a basic 8 by 8 board  
         * Each player should always have 3 starting row.  
         * What differs is the number of pieces per row and the number of empty rows in the middle of the Board
         * </remarks>
         */
        public static string GenerateStartingFen()
        {
            return "mmmm/mmmm/mmmm/4/4/MMMM/MMMM/MMMM";
        }
    }
}