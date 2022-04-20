using System;
using SherlocksGambit.Game;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Utils
{
    public class AiTemplate
    {
        /*
         * The constants in this file are essential for us to know how to use your AI. It ranges from how to instantiate
         * it in the client and leaderboard to how to correct your practical.
         * Please make sure to keep this file updated with the values you want your AI to be tested with!
         */
        
        public const int Depth = 0;
        public static readonly Func<Board, PlayerColor, double> HeuristicFunc = null;
        public const bool AlphaBetaPruning = false;

        /*
         * As soon as you have finished implementing a bonus, please set the corresponding field to True  
         * This will enable the client and the moulinette to utilize your bonuses
         * If you wish to add you own bonuses, notify us in your README
         */
        
        public const bool QuiescenceSearch = false;
        public const bool MoveHistory = false;
        public const bool RevertMove = false;
        public const bool Transposition = false;
        public const bool MoveOrdering = false;
    }
}