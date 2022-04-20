using System;
using System.Collections.Generic;
using SherlocksGambit.Utils.Encryption;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Game.Pieces
{
    public class PieceManager
    {
        /// List of Men and Kings for each color
        public readonly Dictionary<PlayerColor, List<BasePiece>> PiecesDictionary = new();

        /**
         * <summary> Basic constructor for PieceManager </summary>
         * <param name="board"> Board the PieceManager is responsible for </param>
         * <param name="fenCode"> FEN code describing where the pieces are on the Board </param>
         * <remarks>
         * If `fenCode` is null, no pieces should be placed but if it is empty, the Board should be initialized with the
         * starting positions
         * </remarks>
         */
        public PieceManager(Board board, string fenCode)
        {
            // Initialize dictionaries for players' pieces
            PiecesDictionary[PlayerColor.White] = new List<BasePiece>();
            PiecesDictionary[PlayerColor.Black] = new List<BasePiece>();

            // Decode the fen-encoded board (can be null)
            if (fenCode != null)
                FenEncryption.Decrypt(this, board, fenCode);
        }
        
        /**
         * <summary> Adds a BasePiece to the dictionary </summary>
         * <param name="piece"> The BasePiece to add </param>
         * <remarks>
         * Do not forget to add the piece to the correct list of player pieces!  
         * The different lists are stored by their PlayerColor in `PiecesDictionary`
         * </remarks>
         */
        public void AddPiece(BasePiece piece)
        {
            PiecesDictionary[piece.Color].Add(piece);
        }

        /**
         * <summary> Removes a BasePiece off the dictionary </summary>
         * <param name="piece"> The BasePiece to remove </param>
         * <remarks>
         * Do not forget to remove the piece from the correct list of player pieces!  
         * The different lists are stored by their PlayerColor in `PiecesDictionary`
         * </remarks>
         */
        public void RemovePiece(BasePiece piece)
        {
            PiecesDictionary[piece.Color].Remove(piece);
        }

        /**
         * <summary> Promotes a Man to a King </summary>
         * <param name="man"> Man to promote </param>
         * <param name="cell"> Cell on which the Man is on </param>
         * <param name="teamColor"> PlayerColor of the Man to promote </param>
         * <remarks>
         * The rise of a King means the death of a Man (poetic alright)  
         * Do not forget to place the new King on the according cell and add it to `PiecesDictionary`!
         * </remarks>
         */
        public void PromotePiece(Man man, Cell cell, PlayerColor teamColor)
        {
            // Kill the current piece
            man.Kill();

            // Create the new king piece and place it on the cell
            var promotedPiece = new King(teamColor);
            promotedPiece.Place(cell);
            
            // Add the king to the corresponding list of pieces according to `teamColor`
            PiecesDictionary[teamColor].Add(promotedPiece);
        }
        
        /**
         * <summary> [BONUS] Demotes a King to a Man </summary>
         * <param name="king"> King to demote </param>
         * <param name="cell"> Cell on which the King is on </param>
         * <param name="teamColor"> PlayerColor of the King to demote </param>
         * <remarks>
         * You cannot demote a King. A King dies but never surrender! (Do what you have to do)  
         * Do not forget to place the new Man on the according cell and add it to `PiecesDictionary`!
         * </remarks>
         */
        public void DemotePiece(King king, Cell cell, PlayerColor teamColor)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}