using System.Collections.Generic;
using System.Linq;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Game.Pieces
{
    public abstract class BasePiece
    {
        /// Reference to the Board the BasePiece is on
        protected Board Board;

        /// PlayerColor the piece belongs to
        public readonly PlayerColor Color;

        /// `currentCell` is the cell on which the piece is currently
        public Cell CurrentCell;

        /// `ScanDistance` is how many cells the piece can scan for enemies in a single move
        protected int ScanDistance;

        /**
         * <summary> Basic constructor for BasePiece </summary>
         * <param name="newTeamColor"> PlayerColor the BasePiece should belong to </param>
         * <remarks>
         * `ScanDistance` is set by the subclass (either King or Man)  
         * And the Board and Cell the piece is on will be updated by the method `Place`
         * </remarks>
         */
        protected BasePiece(PlayerColor newTeamColor)
        {
            Color = newTeamColor;
        }

        /**
         * <summary> Abstract method to copy the BasePiece </summary>
         * <remarks>
         * You should implement this function in classes that inherit BasePiece (i.e. King.cs or Man.cs)
         * </remarks>
         */
        public abstract BasePiece Copy();

        /**
         * <summary> Places the BasePiece on the provided cell </summary>
         * <param name="newCell"> Cell on which to place the piece on </param>
         * <remarks>
         * This is where you update the BasePiece's `CurrentCell` and `Board`!  
         * The Board the BasePiece will be on is an attribute of the Cell class  
         * Do not forget to update the Cell's attributes too!
         * </remarks>
         */
        public void Place(Cell newCell)
        {
            CurrentCell = newCell;
            CurrentCell.CurrentPiece = this;

            Board = CurrentCell.Board;
        }

        /**
         * <summary> Kills the BasePiece </summary>
         * <param name="simulation">
         * [BONUS][Optional, default = false] Used to keep a reference to where the piece used to be on to revert a move
         * </param>
         * <remarks>
         * When killing a piece, make sure to remove any reference to it in the Board and PieceManager
         * </remarks>
         */
        public void Kill(bool simulation = false)
        {
            CurrentCell.CurrentPiece = null;
            Board.PieceManager.RemovePiece(this);

            // If you are simulating a move we need to keep track of which cell the piece was on
            // If you are not, reset `CurrentCell` to null
            if (!simulation)
                CurrentCell = null;
        }

        /**
         * <summary> Gets all possible paths in all possible diagonals </summary>
         * <param name="pos"> Position in board notation of where the search starts </param>
         * <param name="prevEnemies"> List of previously encountered enemies (can be null) </param>
         * <returns> The list of all found paths from `pos` </returns>
         * <remarks>
         * You can get all possible diagonals the BasePiece can move in with the DirectionHelper!  
         * If `prevEnemies` is not null (can be empty), every potential path will require an enemy ("capture move")
         * Thus if `prevEnemies` is not null, all paths returned from `CreateCellPath` without enemies can be ignored  
         * As soon as you find a path with enemies on the way (a "capture move"), all returned PathObjects must also
         * contain enemies on their path. Ergo, as soon as a "capture move" is possible, you must return only capture
         * moves
         * You are allowed to stray away from our instructions to speed up the search as long as it still holds the same
         * expected behavior  
         * </remarks>
         */
        public List<PathObject> CheckPathing(int pos, List<BasePiece> prevEnemies)
        {
            var capturing = prevEnemies is not null;
            var res = new List<PathObject>();

            // Try to create a path in all the piece's possible diagonals
            foreach (var dir in DirectionHelper.GetDirections(this))
            {
                // Get possible paths (if any) in this direction. If none was found, try another direction
                var possiblePaths = CreateCellPath(pos, dir, prevEnemies);
                if (possiblePaths is null)
                    continue;

                // `foundEnemies` gathers all the paths with enemies to check if you have encountered enemies
                var foundEnemies = possiblePaths.FindAll(pathObj => pathObj.Enemies.Count > (prevEnemies?.Count ?? 0));

                switch (foundEnemies.Count > 0)
                {
                    // If you did not encounter enemies but you should have (prevEnemies is not null or you encountered
                    // enemies in another direction) continue
                    case false when capturing:
                        continue;
                    // Else set `possiblePaths` to be all the "capture move" found and set `capturing` to true
                    case true:
                        possiblePaths = foundEnemies;
                        capturing = true;
                        break;
                }

                // Add `possiblePaths` to `res`
                res.AddRange(possiblePaths);
            }

            return res;
        }

        /**
         * <summary> Returns a list of possible paths (if any) from a given position in a given direction </summary>
         * <param name="pos"> Position in board notation from which to start the search </param>
         * <param name="direction"> Direction in which to do the search </param>
         * <param name="enemies"> List of previously encountered enemies (can be null) </param>
         * <returns> A list of PathObjects representing all the possible paths found </returns>
         * <remarks>
         * This function will call the previously defined `CheckPathing` in order to allow multiple "jumps"
         * This function will return null if no paths were found
         * </remarks>
         */
        private List<PathObject> CreateCellPath(int pos, Direction direction, List<BasePiece> enemies)
        {
            // Create a temporary variable to store the position of the piece
            var currPos = pos;

            // Create the resulting list of PathObjects
            // and a temporary list of Cells initialised with the cell the piece is currently on
            List<PathObject> res = null;
            var path = new List<Cell> {Board.Positions[currPos - 1]};

            // If `enemies` is null then initialise the list, otherwise clone it
            enemies = enemies is null ? new List<BasePiece>() : enemies.GetRange(0, enemies.Count);

            // Loop until no moves left
            // If `Movement` is greater than one then the piece is a king and can only move more than 1 cell if and only
            // if there is a enemy piece to capture on the path
            for (var i = 0; i < ScanDistance; i++)
            {
                // Update the current position in the direction specified
                // The function returns the new position or -1 if out of bounds
                currPos = DirectionHelper.GetCellPosInDirection(currPos, direction);

                // Gets the cell state (if `currPos` == -1 => out of bounds)
                var cellState = currPos == -1 ? Cell.CellState.OutOfBounds : Board.GetCellState(currPos, Color);

                // If the cell is an enemy, try to take it
                if (cellState == Cell.CellState.Enemy)
                {
                    // Checks if you already took the enemy piece. If uou did, a path in this direction is not viable
                    var enemy = Board.Positions[currPos - 1].CurrentPiece;
                    if (enemies.Contains(enemy))
                        return res;

                    // Update the current position in the direction specified
                    // The function returns the new position or -1 if out of bounds
                    currPos = DirectionHelper.GetCellPosInDirection(currPos, direction);

                    // If `currPos` == 1 then cell is out of bounds, returns `res`
                    // Else if the cell is not free then impossible move, return `res`
                    if (currPos == -1 || Board.GetCellState(currPos, Color) != Cell.CellState.Free)
                        return res;

                    // Adds the enemy cell to `_enemyEncountered`
                    enemies.Add(enemy);

                    // Update the possible path with the cell after the enemy
                    path.Add(Board.Positions[currPos - 1]);

                    // Reset the previously found (if piece is a king and if any) simple move because if a capture is
                    // possible you must take it. If `res` was null, create it
                    res?.Clear();
                    res ??= new List<PathObject>();

                    // Create a copy of the current path
                    var pathCopy = path.GetRange(0, path.Count);

                    // Check if you can capture more pieces from here
                    var nextPaths = CheckPathing(currPos, enemies);

                    // If you could capture more pieces after the initial capture
                    if (nextPaths.Count != 0)
                    {
                        // For each additional path found, concatenate their path with the current possible path and add
                        // them to `res`
                        res.AddRange(nextPaths.Select(pathObj =>
                            new PathObject(pathCopy.Concat(pathObj.Path).ToList(), pathObj.Enemies)));
                    }
                    // If you could not add the current path and a copy of the enemies encountered to `res`
                    else
                        res.Add(new PathObject(pathCopy, enemies.GetRange(0, enemies.Count)));

                    // You have captured an enemy and checked if you could capture more, now you can just return `res`
                    // as there is nothing else to do in this direction
                    return res;
                }

                // If the cell is not free you cannot build a path in this direction
                if (cellState != Cell.CellState.Free)
                    return res;

                // You have fround a free cell, add it to the current possible path list
                path.Add(Board.Positions[currPos - 1]);

                // If you are a king and that you have already used your simple move, you can now only add a possible
                // path to the result if and only if there is an enemy to capture on that path
                if (ScanDistance > 1 && i > 0)
                    continue;

                // Create `res` if null then add to `res` a new PathObject with a copy of the current path and a copy of
                // the list of enemies encountered
                res ??= new List<PathObject>();
                res.Add(new PathObject(path.GetRange(0, path.Count), enemies.GetRange(0, enemies.Count)));
            }

            return res;
        }

        /**
         * <summary> Checks if the BasePiece can move </summary>
         * <param name="enemyNeeded"> If set to true, this forces a move to be a "capture move" </param>
         * <returns> A boolean to indicate whether the BasePiece could move or not </returns>
         * <remarks>
         * A BasePiece that can move is one that as able to generate paths in any possible direction
         * </remarks>
         */
        public bool CanMove(bool enemyNeeded)
        {
            return CheckPathing(CurrentCell.BoardPosition, enemyNeeded ? new List<BasePiece>() : null).Count != 0;
        }

        /**
         * <summary> Recreates all possible paths from a position to look for a target </summary>
         * <param name="pos"> Position in board notation from which to start the path recreation </param>
         * <param name="target"> Cell to look for in the possible paths generated </param>
         * <param name="previousEnemies"> List of previously encountered enemies (can be null) </param>
         * <returns> The best PathObject to reach that target </returns>
         * <remarks>
         * To get the best PathObject out of all the one generated that contain the target, simply favor distance  
         * Distance can be calculated by the sum of all Cells visited (including enemies captured)  
         * You should not have to worry about clockwise selection if you correctly used the given helper functions
         * However, if multiple PathObjects have the same distance, return the first one
         * </remarks>
         */
        private PathObject RecreatePath(int pos, Cell target, List<BasePiece> previousEnemies)
        {
            PathObject res = null;
            var distance = 0;
            
            foreach (var path in CheckPathing(pos, previousEnemies)
                         .FindAll(pathTuple => pathTuple.Path.Contains(target)))
            {
                var newDistance = path.Path.Count + path.Enemies.Count;
                if (newDistance <= distance)
                    continue;

                distance = newDistance;
                res = path;
            }

            return res;
        }

        /**
         * <summary>
         * Checks if any of the player's pieces could have captured an enemy piece but the move chosen was a simple one
         * </summary>
         * <param name="ate"> Boolean indicating if the action done was a capture or not </param>
         * <returns> A boolean indicating if the piece could have captured a piece </returns>
         * <remarks>
         * A very easy way to check this is to look for all possible paths that requires an enemy for all of the
         * player's pieces. If there are any but the action done was not a capture, return true  
         * The color of the player for which we want to check is the color of the current BasePiece  
         * We are making use of the lazy evaluation here! Read some documentation about it since it is critical for
         * performance reasons
         * </remarks>
         */
        private bool CheckCouldHaveCaptured(bool ate)
        {
            return !ate && Board.PieceManager.PiecesDictionary[Color].Any(piece => piece.CanMove(true));
        }

        /**
         * <summary> Prequel to its overloaded version, recreates a PathObject to a target Cell </summary>
         * <param name="target"> Cell to move to </param>
         * <param name="action"> Either '-' or 'x' indicating a simple move or a capture one </param>
         * <returns> A boolean whether or not the move was executed correctly </returns>
         * <remarks>
         * `target` and `action` are considered to be valid  
         * Do not forget to check if the move was valid i.e if the piece captured when able to!  
         * You also should not recreate path that are useless. If the action is a capture one, only capture paths must
         * be recreated  
         * To execute the move, this function should then call its overloaded version
         * </remarks>
         */
        public bool Move(Cell target, char action)
        {
            // If action is wrong (could have eaten) then return false else recreates the path and return the result of
            // the overloaded function `Move` with it
            var path = RecreatePath(CurrentCell.BoardPosition, target, action == 'x' ? new List<BasePiece>() : null);
            return !CheckCouldHaveCaptured(action == 'x') && Move(path);
        }

        /**
         * <summary>
         * Overload for `Move` function replacing its parameters with a PathObject. Moves the BasePiece
         * </summary>
         * <param name="pathObj"> PathObject that contains the move the perform </param>
         * <param name="simulation">
         * [BONUS][Optional, default = false] Used to revert a move when killing pieces
         * </param>
         * <returns> A boolean whether or not the move was executed correctly </returns>
         * If the move was a "capture move", you must kill all the enemies that were on the way  
         * Don't forget to update the current Cell the BasePiece is on as well as the destination Cell and the BasePiece
         * itself  
         * <remarks>
         * `pathObj` and its `Path` attribute are not guaranteed to be non-null  
         * This function could be called without its overloaded self, you must thus check id the move was valid by
         * checking if the move was a capture on when the piece had the opportunity to capture an enemy piece  
         * Friendly reminder: to know if a PathObject was capture one, consider looking at whether or not enemies were
         * encountered along the way  
         * You can access the starting Cell and destination Cell through the `Path` attribute of the PathObject  
         * This function is overriden by the Man class to check for promotion
         * </remarks>
         */
        public virtual bool Move(PathObject pathObj, bool simulation = false)
        {
            // If the path is null or if you could have captured but the move is a "simple move" then return
            if (pathObj?.Path is null || CheckCouldHaveCaptured(pathObj.Enemies.Count != 0))
                return false;

            // Capture all the pieces on cells in `pathObj.Enemies`
            foreach (var enemy in pathObj.Enemies)
                enemy.Kill(simulation);

            // Update the `CurrentPiece` of the begin and end cell 
            CurrentCell.CurrentPiece = null;
            CurrentCell = pathObj.Path.Last();
            CurrentCell.CurrentPiece = this;
            return true;
        }

        /**
         * <summary> [BONUS] Undoes a move </summary>
         * <param name="pathObj"> PathObject that contains the move the perform </param>
         * Don't forget to update the current Cell the BasePiece is on as well as the origin Cell and the BasePiece
         * itself  
         * <remarks>
         * `pathObj` and its `Path` attribute are not guaranteed to be non-null  
         * You must revive all the pieces that it killed on the way!  
         * Do not forget to add them back to the PieceManager and place them on the Board  
         * This function is overriden by the King class to check for demotion
         * </remarks>
         */
        public virtual void RevertMove(PathObject pathObj)
        {
            // If the path is null then return
            if (pathObj?.Path is null)
                return;

            // Un-kill the piece by replacing it on the correct cell and re-adding it to the piece manager
            foreach (var enemy in pathObj.Enemies)
            {
                enemy.CurrentCell.CurrentPiece = enemy;
                Board.PieceManager.AddPiece(enemy);
            }

            // Update the `CurrentPiece` of the begin and end cell 
            CurrentCell.CurrentPiece = null;
            CurrentCell = pathObj.Path.First();
            CurrentCell.CurrentPiece = this;
        }
    }
}