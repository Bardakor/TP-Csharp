using System;
using System.Collections.Generic;
using System.IO;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Utils.Bonuses
{
    public static class ZobristHashing
    {   
        /// Path to the source file where random numbers are stored
        /// (from the directory's root: bin/Debug/net5.0/randNum.txt)
        private const string SourceFile = "randNum.txt";

        /// 3 dimensional array storing all possible boards (2 pieces, 2 player, `Board.NbPositions` cells)
        private static readonly uint[,,] PiecesArray = new uint[2, 2, Board.NbPositions];

        /**
         * <summary> Static constructor (will be called once at the first call to this class) </summary>
         */
        static ZobristHashing()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Returns the Zobrist key for a given Board </summary>
         * <param name="board"> Board for which you generate the Zobrist notation </param>
         * XOR together all the board pieces' value stored in `PiecesArray`.  
         * <a href="https://en.wikipedia.org/wiki/Zobrist_hashing"> Refer to Wikipedia </a> 
         * <returns> The calculated key </returns>
         */
        public static uint CalculateZobristKey(Board board)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Writes random numbers in `SourceFile` </summary>
         * <remarks>
         * There should be as many random numbers as they are values in `PiecesArray`  
         * Values should be separated with a comma (with none at the end)
         * </remarks>
         */
        private static void WriteRandNum()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Generates a random unsigned 32 bit integer </summary>
         * <returns> The generated number </returns>
         * <remarks>
         * This function was engineered for a board size of 8. If board size is bigger then 8, consider using a bigger
         * bit count to avoid collisions (ulong)
         * </remarks>
         */
        private static uint RandUInt32BitNum()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        /**
         * <summary> Reads the random numbers stored in `SourceFile` </summary>
         * <returns> A queue containing all the numbers read </returns>
         * <remarks>
         * You can read more about queues <a href="https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-5.0"> here </a>
         * This functions should be changed too if ulong are used instead of uint
         * [BONUS] If you want to do the modular board, makes sure to regenerate `SourceFile` of the wrong amount of
         * values are read
         * </remarks>
         */
        private static Queue<uint> ReadRandNum()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}