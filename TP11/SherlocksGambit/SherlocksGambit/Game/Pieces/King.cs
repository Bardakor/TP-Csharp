using System;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Game.Pieces
{
    public class King : BasePiece
    {
        /**
         * <summary> Basic constructor for King </summary>
         * <param name="newTeamColor"> PlayerColor the BasePiece should belong to </param>
         * Initialize `ScanDistance`
         * <remarks>
         * This constructor already calls the constructor of the superclass BasePiece  
         * A King can scan for enemies all across the Board!  
         * But don't forget to take into consideration the Cell the King is on! It should not be accounted for!
         * </remarks>
         */
        public King(PlayerColor newTeamColor) : base(newTeamColor)
        {
            ScanDistance = Board.BoardSize - 1;
        }

        /**
         * <summary> Override for the abstract method `Copy` in super class BasePiece </summary>
         * <returns> The copied King </returns>
         * <remarks>
         * You only need to create a new King with the same PlayerColor as the current King
         * The placing of this new King will be handled in `Copy` in the Board.cs
         * </remarks>
         */
        public override King Copy()
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
        
        /**
         * <summary> [BONUS] Override for the virtual method `RevertMove` in super class BasePiece </summary>
         * If the King os on king's row, you must demote it to a Man and continue the reverting on the move on the man
         * Else, revert the piece like you would with any piece by calling `RevertMove` of the superclass
         * <remarks>
         * Be careful, when demoting you lose the reference to the current Cell and Board!
         * You can always call the base function using:
         * <code> base.FunctionName(FunctionParameters); </code>
         * </remarks>
         */
        public override void RevertMove(PathObject pathObject)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}