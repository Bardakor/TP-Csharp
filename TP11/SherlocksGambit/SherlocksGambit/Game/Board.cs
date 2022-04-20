using System;
using System.Linq;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Encryption;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Game
{
    public partial class Board
    {
        /// Constant for the size of the square Board (it must be even and greater than 6)
        public const int BoardSize = 8;
        
        /// Constant for the number of reachable Cells on the Board
        public const int NbPositions = BoardSize * BoardSize / 2;
        
        /// Constant for the number of reachable Cells per row
        public const int RowLength = NbPositions / BoardSize;

        /// Reference to this board's PieceManager
        public readonly PieceManager PieceManager;

        /// `Positions` gathers all the reachable cells and creates a getter
        public Cell[] Positions { get; } = new Cell[NbPositions];

        /**
         * <summary> Basic constructor for Board </summary>
         * <param name="fenCode"> [Optional, default=""] Initialize a board to a given FEN representation </param>
         * Initialize all attributes on this class
         * <remarks>
         * The board notation is different than the array notation  
         * The expected behavior when `fenCode` is empty or when it is null is different
         * </remarks>
         */
        public Board(string fenCode = "")
        {
            // Initialise `Positions` with new cells.
            // The board position of the cell is one more than its index in `Positions`
            for (var i = 0; i < NbPositions; i++)
                Positions[i] = new Cell(i + 1, this);

            // Generate a new starting fen code if an empty fen code was given (not null nor with content) 
            if (fenCode == "")
                fenCode = GenerateStartingFen();

            // Create a new PieceManager. Upon creation it will place all the pieces on the board
            PieceManager = new PieceManager(this, fenCode);
        }

        /**
         * <summary> Gets the CellState of a cell from a specific player's POV </summary>
         * <param name="targetPosition"> Position of the Cell in board notation </param>
         * <param name="color"> Color of the player making the search </param>
         * <returns> The CellState of the targeted cell </returns>
         * <remarks> Do not mix board notation and array notation </remarks>
         */
        public Cell.CellState GetCellState(int targetPosition, PlayerColor color)
        {
            // Check if target is out of bounds
            if (targetPosition is <= 0 or > NbPositions)
                return Cell.CellState.OutOfBounds;

            // Get the cell at `targetPosition` (its index in `Positions` is one less than its board position)
            // If there are no pieces on target, return free state
            var targetCell = Positions[targetPosition - 1];
            if (targetCell.CurrentPiece == null)
                return Cell.CellState.Free;

            // Compare the target's current piece's color with `color`
            // If same then one of the color's pieces is on the cell, otherwise it's an enemy piece
            return color == targetCell.CurrentPiece.Color ? Cell.CellState.Friendly : Cell.CellState.Enemy;
        }

        /**
         * <summary> Initializes and returns a copy of the Board </summary>
         * <returns> The copied Board </returns>
         * <remarks>
         * We do not wish to create a new Board with a starting FEN nor do we want to export the Board in FEN notation
         * as this would be too inefficient. Prefer initializing it empty and placing the pieces one by one.  
         * As our tests feature timeouts, we **STRONGLY** recommend our approach  
         * Do not forget alongside placing it on the Board to put in in the PieceManager  
         * If you have implemented Board(copy).cs, you should create the copy of the Board using `ConstructWithHistory`
         * </remarks>
         */
        public Board Copy()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Checks whether a player has won </summary>
         * <param name="playerColor"> The color of the player we are checking victory for </param>
         * <returns> A boolean </returns>
         * If you remember the victory conditions, one could win by achieving total domination of the Board
         * <remarks> Whether the opponent could move or not will not be checked here. </remarks>
         */
        public bool HasWon(PlayerColor playerColor)
        {
            //depending on the player color, we check if the player has won by checking if the player has at least one piece left and if the opponent has no pieces left
            if (playerColor == PlayerColor.Black)
            {
                return PieceManager.PiecesDictionary[playerColor].Count > 0 && PieceManager.PiecesDictionary[PlayerColor.White].Count == 0;
            }
            else
            {
                return PieceManager.PiecesDictionary[playerColor].Count > 0 && PieceManager.PiecesDictionary[PlayerColor.Black].Count == 0;
            }
        }

        /**
         * <summary> Checks whether a player has lost </summary>
         * * <param name="playerColor"> The color of the player we are checking defeat for </param>
         * <returns> A boolean </returns>
         * If you remember the loosing conditions, one could loose having no pieces left on the Board or if none of the
         * player's remaining pieces are able to move
         * <remarks> Whether the opponent could move or not will not be checked here. </remarks>
         */
        public bool HasLost(PlayerColor playerColor)
        {
            //verify that none of the player's pieces are able to move
            if (playerColor == PlayerColor.Black)
            {
                return PieceManager.PiecesDictionary[playerColor].Count == 0 && PieceManager.PiecesDictionary[PlayerColor.White].Count > 0;
            }
            else
            {
                return PieceManager.PiecesDictionary[playerColor].Count == 0 && PieceManager.PiecesDictionary[PlayerColor.Black].Count > 0;
            }
        }

        /**
         * <summary> Rudimentary printing function </summary>
         * <remarks>
         * Because of Rider's color theme, white pieces are in this function displayed as red  
         * For the same reasons, Black's pieces are blue  
         * If you have yet to have implemented the FEN encryption, it will print an empty string
         * </remarks>
         */
        public void Print()
        {
            // `index` is index of the cell in `Positions`
            var index = 0;

            // `y` starts at BoardSize - 1 because of the discontinuity of the origin.
            // `Positions`'s origin is top-left while the graphical origin is bottom left
            for (var y = BoardSize - 1; y >= 0; y--)
            {
                // Cell separator
                Console.Write("|");

                for (var x = 0; x < BoardSize; x++)
                {
                    BasePiece currPiece = null;
                    var reachableCell = y % 2 == 0 && x % 2 == 0 || y % 2 != 0 && x % 2 != 0;

                    // Only if the cell is reachable, update `currentPiece` with the piece on the cell
                    if (reachableCell)
                        currPiece = Positions[index++].CurrentPiece;

                    // `currentPiece` can be null. In that case the foreground color does not matter.
                    Console.ForegroundColor = currPiece?.Color == PlayerColor.White
                        ? ConsoleColor.DarkRed
                        : ConsoleColor.DarkBlue;

                    // If there is no piece then consider this cell as unreachable thus white
                    Console.BackgroundColor = reachableCell ? ConsoleColor.Black : ConsoleColor.White;

                    // Print the piece. If null print spaces
                    switch (currPiece)
                    {
                        case King or Man:
                            Console.Write(currPiece is King ? " K " : " M ");
                            break;
                        default:
                            Console.Write("   ");
                            break;
                    }

                    // Cell separator
                    Console.ResetColor();
                    Console.Write("|");
                }

                Console.WriteLine();
            }

            // Print the board's FEN representation
            Console.WriteLine(FenEncryption.Encrypt(this));
        }
    }
}