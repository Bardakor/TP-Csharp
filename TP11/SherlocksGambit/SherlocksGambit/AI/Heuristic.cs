using System;
using System.Linq;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.AI
{
    public static class Heuristic
    {
        /**
         * <summary> This defines how advantageous a position is over the others </summary>
         * <remarks> You are allowed to tweak it its values as you deem fit </remarks>
         */
        private static readonly double[] PositionsWeights = {
            2, 2, 2, 2,
            1, 1, 1, 1.5,
            1.5, 1, 1, 1,
            1, 1.25, 1.25, 1.5,
            1.5, 1.25, 1.25, 1,
            1, 1, 1, 1.5,
            1.5, 1, 1, 1,
            2, 2, 2, 2,
        };
        
        /**
         * <summary> Heuristic where Kings are as valuable as Men </summary>
         * <param name="board"> Board on which to calculate the heuristic </param>
         * <param name="currPlayer"> Player for which the heuristic is calculated </param>
         * <returns> The sum of all the pieces on the board </returns>
         * <remarks>
         * In order to calculate the heuristic in regards to one player, such player should have its pieces' value be
         * positive while the opponent should have theirs be negative
         * </remarks>
         */
        public static double SimpleHeuristic(Board board, PlayerColor currPlayer)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        /**
         * <summary> Heuristic where Kings are valued twice as much as Men </summary>
         * <param name="board"> Board on which to calculate the heuristic </param>
         * <param name="currPlayer"> Player for which the heuristic is calculated </param>
         * <returns> The sum of all the pieces on the board </returns>
         * <remarks>
         * In order to calculate the heuristic in regards to one player, such player should have its pieces' value be
         * positive while the opponent should have theirs be negative  
         * The rule "Kings are valued twice as Men" stands for both parties, whether the final value is negative or
         * positive
         * </remarks>
         */
        public static double ComputeHeuristic(Board board, PlayerColor currPlayer)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Heuristic which returns a random evaluation of the board </summary>
         * <param name="board"> Board on which to calculate the heuristic </param>
         * <param name="currPlayer"> Player for which the heuristic is calculated </param>
         * <returns> A random double between -1 and 1 </returns>
         * <remarks>
         * You are allowed to widen the range of the randomness HOWEVER it must be centered on 0.
         * </remarks>
         */
        public static double RandomHeuristic(Board board, PlayerColor currPlayer)
        {
            // You can increase the range to decrease difficulty for the other AI [-1,1] is the hardest
            return new Random(DateTime.Now.Millisecond).Next(-1, 1);
        }

        /**
         * <summary>
         * Heuristic where kings are valued twice as much as men and where their positions on the board matter
         * </summary>
         * <param name="board"> Board on which to calculate the heuristic </param>
         * <param name="currPlayer"> Player for which the heuristic is calculated </param>
         * <returns> The sum of all the pieces on the board </returns>
         * <remarks>
         * In order to calculate the heuristic in regards to one player, such player should have its pieces' value be
         * positive while the opponent should have theirs be negative  
         * The rule "Kings are valued twice as Men" stands for both parties, whether the final value is negative or
         * positive  
         * When using the provided double array, do not forget to convert positions in board notation to positions in
         * array notation! Indeed, the position of the piece is directly linked to the weight of that position's index
         * </remarks>
         */
        public static double PositionWeightedHeuristics(Board board, PlayerColor currPlayer)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}