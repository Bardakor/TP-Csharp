using System;
using System.Collections.Generic;
using System.Linq;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Helpers;
using Direction = SherlocksGambit.Utils.Helpers.Direction;

namespace SherlocksGambit.Utils.Bonuses
{
    public class MoveOrdering
    {
        /// Array to store the score of all possible moves
        private readonly int[] _moveScores;

        /// Unreasonable maximum amount of possible moves in a single turn
        private const int MaxMoveCount = 128;

        /// Points multiplied by this factor if the move allows the opponent to capture your piece the next turn
        /// Feel free to tweak this value as you see fit
        private const int UnderAttackMultiplier = -50;

        /// Points multiplied by this factor each time the move capture an enemy piece
        /// Feel free to tweak this value as you see fit
        private const int CaptureMultiplier = 10;
        
        /// Points added if the move allows your piece to promote
        /// Feel free to tweak this value as you see fit
        private const int PromotionValue = 100;

        /// Reference to the Board's TranspositionTable
        private readonly TranspositionTable _transpositionTable;

        /**
         * <summary> Basic constructor for MoveOrdering </summary>
         * <param name="transpositionTable"> The board's TranspositionTable (may be null) </param>
         * Initialize `_moveScores` and `_transpositionTable`
         */
        public MoveOrdering(TranspositionTable transpositionTable)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Move ordering logic </summary>
         * <param name="board"> The board on which the moves are simulated </param>
         * <param name="moves"> List of all possible moves found </param>
         * For every move, add its score in `_moveScores`  
         * A score is calculated with all the opponent pieces the move captured. A good example of a function to
         * calculate this score would be to multiply the value of the piece captured bu the `CaptureMultiplier` and to
         * deduce the value of the current piece. (Kings taking Men should be evaluated less than a Men taking Kings)  
         * To this score you can add `PromotionValue` is the piece can be promoted else you need to see if the piece can
         * be captured by an enemy piece next turn. (this part should be irrelevant with Quiescence Search)  
         * If the piece is under attack, multiply by `UnderAttackMultiplier` the max amount of pieces that could be
         * captured and add it to the final score.  
         * Finally, you passed a TranspositionTable to the constructor and that the current board is already in the
         * table and that its suggested move is the one we are trying, add a really big number to the score  
         * Once you have filled `_moveScores` with according scores, call `Sort`
         * <remarks>
         * You are free to change the way you calculate scores, add conditions or remove some.  
         * Be mindful of the difference between board notation and array notation.  
         * You must copy the board in order to execute the move.
         * </remarks>
         */
        public void OrderMoves(Board board, List<PathObject> moves)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Calculated the value of a given piece </summary>
         * <param name="piece"> The BasePiece from which we want the value </param>
         * You are free to return any value. For the ref we chose that Kings are five times as valuable as Men
         * <returns> The value calculated </returns>
         */
        private static int GetPieceValue(BasePiece piece)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> In place sort of moves according to their score in `_moveScores` </summary>
         * <param name="moves"> The list of moves to sort </param>
         * You can choose the sort algorithm of your choosing. But remember to have optimisations in mind
         * <remarks> `_moveScores` can be considered correctly initialized </remarks>
         */
        private void Sort(List<PathObject> moves)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}