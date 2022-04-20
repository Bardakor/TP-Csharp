using System;
using System.Linq;
using SherlocksGambit.Game;
using SherlocksGambit.Utils;
using SherlocksGambit.Utils.Bonuses;
using SherlocksGambit.Utils.Encryption;
using SherlocksGambit.Utils.Helpers;
using SherlocksGambit.Utils.Runners.DotTree;

namespace SherlocksGambit.AI
{
    public class MiniMax : AiBase
    {
        /**
         * <summary> Basic constructor for MiniMax </summary>
         * <remarks> This should stay empty as we are simply calling the superclass' constructor </remarks>
         */
        public MiniMax(AiProperties properties) : base(properties)
        {
        }
        
        /**
         * <summary> Return the max of the 2 parameters </summary>
         * <param name="a"> First number to compare </param>
         * <param name="b"> Second number to compare </param>
         * <returns> The biggest out of the twp </returns>
         * <remarks> This function was added to support reflection used in the client and leaderboard </remarks>
         */
        private static double MaxDouble(double a, double b) => a > b ? a : b;
        
        /**
         * <summary> Return the min of the 2 parameters </summary>
         * <param name="a"> First number to compare </param>
         * <param name="b"> Second number to compare </param>
         * <returns> The biggest out of the twp </returns>
         * <remarks> This function was added to support reflection used in the client and leaderboard </remarks>
         */
        private static double MinDouble(double a, double b) => a < b ? a : b;

        /**
         * <summary> Generates a move </summary>
         * <param name="currBoard"> Current Board on which to perform the search </param>
         * <param name="currDepth"> Current depth of the search </param>
         * <param name="minimize"> Indicate whether you are trying to minimize or maximize the heuristic </param>
         * <param name="alpha"> [Optional, default = Min] Current Alpha of the search </param>
         * <param name="beta"> [Optional, default = Maz] Current Beta of the search </param>
         * <param name="root"> [Optional, default = null] Used for dot printing, current node of the tree </param>
         * <param name="quietMode"> [Optional, default = false] Indicate if you are in a Quiescence search </param>
         * <returns>
         * A tuple with the as key the heuristic returned from the search and as value the PathObject which yields this
         * heuristic
         * </returns>
         * <remarks>
         * You are alternating the search's PlayerColor depending on `minimize`. Consider using `GetOpponentColor()`  
         * Start by implementing the version without Alpha-Beta pruning. Adding this optimisation will be easier on code
         * that already works!  
         * To call the heuristic function use Func.Invoke(). This should only be called when max depth has been reach!
         * As a reminder, max depth 0 should not return any move  
         * We are showing you how to implement the DotVisualizer. Refer to the subject to add this tool to this function
         * Friendly reminder to no confuse board notation with array notation!  
         * Do not forget to copy the Board (if you are not doing the RevertMove bonus) because such Boards would be
         * passed as reference (which would, this node's children, change the current Board)  
         * You should update the AI's `ExploredPath` stat in here  
         * The MiniMax algorithm is by definition recursive!  
         * [BONUS]  
         * It is in this function that you are expected to implement the Quiescence Search, TranspositionTable,
         * MoveOrdering, MoveHistory and RevertMove
         * </remarks>
         */
        protected override Tuple<double, PathObject> GenerateMove(Board currBoard, int currDepth, bool minimize = false,
            double alpha = Min, double beta = Max, DotNode root = null, bool quietMode = false)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}