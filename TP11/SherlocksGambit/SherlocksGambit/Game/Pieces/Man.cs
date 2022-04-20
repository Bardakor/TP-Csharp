using System;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Game.Pieces
{
    public class Man : BasePiece
    {
        /**
         * <summary> Basic constructor for Man </summary>
         * <param name="newTeamColor"> PlayerColor the BasePiece should belong to </param>
         * Initialize `ScanDistance`
         * <remarks>
         * This constructor already calls the constructor of the superclass BasePiece  
         * A Man can scan for enemies one cell away from it!
         * </remarks>
         */
        public Man(PlayerColor newTeamColor) : base(newTeamColor)
        {
            ScanDistance = 1;
        }
        
        /**
         * <summary> Override for the abstract method `Copy` in super class BasePiece </summary>
         * <returns> The copied Man </returns>
         * <remarks>
         * You only need to create a new Man with the same PlayerColor as the current Man
         * The placing of this new King will be handled in `Copy` in the Board.cs
         * </remarks>
         */
        public override Man Copy()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Override for the virtual overloaded `Move` method in super class BasePiece </summary>
         * <returns> A boolean indicating if the move was successful or not </returns>
         * This function should call the base `Move` method, then, if the Man is on king's row, promote it
         * <remarks>
         * You can always call the base function using:
         * <code> base.FunctionName(FunctionParameters); </code>
         * </remarks>
         */
        public override bool Move(PathObject pathObject, bool simulation = false)
        {
            // Call the overloaded base method `Move` in the super class
            var moved = base.Move(pathObject, simulation);
            
            // If the move was successful, check for promotion
            if (moved && DirectionHelper.IsOnKingsRow(this))
                Board.PieceManager.PromotePiece(this, CurrentCell, Color);

            // Return whether or not the move was successful
            return moved;
        }
    }
}
