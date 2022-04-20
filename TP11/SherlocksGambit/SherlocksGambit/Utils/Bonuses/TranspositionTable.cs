using System;
using SherlocksGambit.Game;

namespace SherlocksGambit.Utils.Bonuses
{
    public class TranspositionTable
    {
        /// Enum for Entry Statuses
        public enum Status
        {
            /// The lookup failed: there were no record of the given board or Transposition is not enabled
            LookupFailed = int.MinValue,

            /// The exact heuristic is stored for the given board
            ExactValue = 0,

            /// The heuristic stored for this board is a lower bound
            LowerBound = 1,

            /// The heuristic stored for this board is an upper bound
            UpperBound = 2
        }

        /// Size representing how many entries should be stored. This number can/should be changed
        private const uint TableSize = 420000;
        
        /// Array of Entry of size `TableSize`
        public readonly Entry[] Entries;
        
        /**
         * <summary> Basic constructor for TranspositionTable </summary>
         */
        public TranspositionTable()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        /**
         * <summary> Returns the index in the table the given board is or should be in </summary>
         * <param name="board"> The board to look for in the table </param>
         * <returns> The calculated index </returns>
         * <remarks>
         * The Boards should be at their zobrist's notation (modulo `TableSize`) in the table  
         * The index returned does not guarantee a value in the table
         * </remarks>
         */
        public static uint Index(Board board)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        /**
         * <summary> Returns the PathObject in the table for a given index calculated thanks to a given board </summary>
         * <param name="board"> The board to look for in the table </param>
         * <returns> The stored PathObject related to `board` </returns>
         * <remarks> The returned PathObject might be null </remarks>
         */
        public PathObject GetStoredMove(Board board)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        /**
         * <summary> Returns, if found, the heuristic stored for a given board </summary>
         * <param name="board"> Board to look for in the table </param>
         * <param name="depth"> Maximum depth acceptable. You should not return a result deeper than the search </param>
         * <param name="alpha"> The current alpha of the search </param>
         * <param name="beta"> The current beta of the search </param>
         * <returns> If found the heuristic found. If not a failed lookup should be returned </returns>
         * <remarks>
         * If the heuristic found is an upper bound or a lower bound, make sure to return this value only if it causes
         * a alpha or beta cut-off
         * </remarks>
         */
        public double LookupEvaluation(Board board, int depth, double alpha, double beta)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Stores a given board, its eval, depth and move as an entry in the table </summary>
         * <param name="id"> Used for dot printing </param>
         * <param name="board"> Board on which the evaluation was calculated </param>
         * <param name="eval"> Heuristic to store </param>
         * <param name="depth"> How deep the search was </param>
         * <param name="evalType"> What was the status of the evaluation </param>
         * <param name="move"> Move that gave `eval` </param>
         * <remarks>
         * All given parameters are ready to use  
         * The Boards should be at their zobrist's notation (modulo `TableSize`) in the table
         * </remarks>
         */
        public void StoreEvaluation(int id, Board board, double eval, int depth, Status evalType, PathObject move)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        // Table's element that stores the data
        public struct Entry
        {
            /// `Id` is used for dot printing
            public readonly int Id;
            
            /// `Key` stores the Zobrist's hash of the board
            public readonly uint Key;
            
            /// `Value` is the heuristic of the Entry
            public readonly double Value;
            
            /// `Depth` is how deep the search was
            public readonly int Depth;
            
            /// `NodeType` stores the status of the Entry
            public readonly Status NodeType;

            /// `PathObj` is the move associated to `Value`
            public readonly PathObject PathObj;
            
            /**
             * <summary> Basic constructor for Entry </summary>
             * <remarks> All given parameters are ready to assign </remarks>
             */
            public Entry(int id, uint key, double value, int depth, Status nodeType, PathObject pathObj)
            {
                throw new NotImplementedException("TODO: REMOVE THIS!");
            }
        }
    }
}