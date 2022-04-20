using System;
using SherlocksGambit.Game;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Utils
{
    public class AiProperties
    {
        /// Color the AI is playing as
        public readonly PlayerColor Color;

        /// Reference to the Board the AI is playing on
        public readonly Board Board;
        
        /// <summary> How deep the AI's search should be </summary>
        /// <remarks> depth 0  returns no move in our case </remarks>
        public readonly int MaxDepth;
        
        /// Function defined in Heuristic calculating how good the Board is for a given PlayerColor
        public readonly Func<Board, PlayerColor, double> HeuristicFunction;
        
        /// Enables Alpha Beta pruning which cuts short Boards that are too good for the opponent or too bad for the AI
        public readonly bool AlphaBetaPruning;
        
        /// Enables Quiescence search which will continue to search even after `MaxDepth` has been reach until a "quiet"
        /// Board is found, ergo, until no capture moves are possible by the opponent of the simulated color
        public readonly bool QuiescenceSearch;
        
        /// Enables MoveHistory which will keep track of previous moves done in order to prevent infinite loops which
        /// would disqualify the AI
        public readonly bool MoveHistory;
        
        /// Enables the reversion of moves, a huge improvement instead of making countless copies of the Board
        public readonly bool RevertMove;
        
        /// Enables Transposition which prevents the AI to re-compute already computed Boards thus saving time
        public readonly bool Transposition;
        
        /// Enables move ordering to improve the Alpha Beta pruning by having the most likely to be good moves evaluated
        /// first
        public readonly bool MoveOrdering;

        /**
         * <summary> Basic constructor for AiProperties </summary>
         * <param name="color"> Color the AI is playing as </param>
         * <param name="board"> Board on which the AI is playing</param>
         * <param name="maxDepth"> How deep the AI search should be </param>
         * <param name="heuristicFunction"> Heuristic to calculate how good a Board is for the AI </param>
         * <param name="alphaBetaPruning"> [Optional, default=false] Prune the tree with alpha and beta </param>
         * <param name="quiescenceSearch"> [BONUS][Optional, default=false] Search until a "quiet" Board </param>
         * <param name="moveHistory"> [BONUS][Optional, default=false] Remember previous moves </param>
         * <param name="revertMove"> [BONUS][Optional, default=false] Revert moves instead of copying Boards </param>
         * <param name="transposition"> [BONUS][Optional, default=false] Transpose to already calculated Boards </param>
         * <param name="moveOrdering"> [BONUS][Optional, default=false] Order moves for better pruning </param>
         * <remarks> `MoveHistory` on Board is set here! </remarks>
         */
        public AiProperties(int color, Board board, int maxDepth, 
            Func<Board, PlayerColor, double> heuristicFunction,
            bool alphaBetaPruning = false, bool quiescenceSearch = false, bool moveHistory = false, 
            bool revertMove = false, bool transposition = false, bool moveOrdering = false)
        {
            Color = (PlayerColor)color;
            Board = board;
            MaxDepth = maxDepth;
            HeuristicFunction = heuristicFunction;
            
            AlphaBetaPruning = alphaBetaPruning;
            QuiescenceSearch = quiescenceSearch;
            MoveHistory = moveHistory;
            RevertMove = revertMove;
            Transposition = transposition;
            MoveOrdering = moveOrdering;

            // Do not forget to update the Board's variable
            Board.MoveHistory = MoveHistory;
        }
    }
}