using System;
using System.Linq;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Utils.Encryption
{
    public static class MoveEncryption
    {
        /**
         * <summary> Encrypts a move given a PlayerColor and a PathObject </summary>
         * <param name="playerColor"> Color of the player making the move </param>
         * <param name="path"> Object containing the path and enemies encountered </param>
         * <returns> A string representing the move </returns>
         * <example>
         * White's simple move: "22-17"  
         * Black's capture move: "(10x19)"
         * </example>
         */
        public static string Encrypt(PlayerColor playerColor, PathObject path)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        // We assume that `input` is correct (meaning nor null, nor empty, with correct coordinates and format)
        /**
         * <summary> Parses a given move and executes it on a given Board </summary>
         * <param name="board"> Board on which to perform the move </param>
         * <param name="input"> Encoded move to decrypt </param>
         * <returns> A boolean indicating if the move was successful or not </returns>
         * <remarks>
         * If `input` is null, return true. It being null signify that this is is first move of the game  
         * Otherwise, we assume that `input` is correct (meaning not empty, with correct coordinates and format)
         * </remarks>
         */
        public static bool Decrypt(Board board, string input)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}