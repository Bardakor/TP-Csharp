using System;


namespace puzzle_game
{
    public partial class Board
    {
        
        public Direction[] GetPossibleDirections()
        {
            //returns an array of possible directions
            Direction[] directions = new Direction[4];
            int emptyPos = FindEmptyPos();
            int row = emptyPos / width;
            int col = emptyPos % width;
            if (row > 0)
            {
                directions[0] = Direction.UP;
            }
            if (row < width - 1)
            {
                directions[1] = Direction.DOWN;
            }
            if (col > 0)
            {
                directions[2] = Direction.LEFT;
            }
            if (col < width - 1)
            {
                directions[3] = Direction.RIGHT;
            }
            return directions;
        }
        
        public void SwapTile(int i1, int i2)
        {
            //if tiles are not of the same type, raise an exception
            if (board[i1].Type != board[i2].Type)
            {
                throw new ArgumentException("tiles must be of the same type");
            }
            //swaps the tiles at index i1 and i2
            Tile temp = board[i1];
            board[i1] = board[i2];
            board[i2] = temp;
        }
        
        public bool MoveDirection(Direction direct)
        {
            //returns true if the move is possible
            int emptyPos = FindEmptyPos();
            int row = emptyPos / width;
            int col = emptyPos % width;
            switch (direct)
            {
                case Direction.UP:
                    SwapTile(emptyPos, emptyPos - width);
                    break;
                case Direction.DOWN:
                    SwapTile(emptyPos, emptyPos + width);
                    break;
                case Direction.LEFT:
                    SwapTile(emptyPos, emptyPos - 1);
                    break;
                case Direction.RIGHT:
                    SwapTile(emptyPos, emptyPos + 1);
                    break;
                default:
                    throw new ArgumentException("invalid direction");
            }
            return true;
        }
        
        
        public void Shuffle(int nbr) 
        {
            //nbr must be positive
            if (nbr < 0)
            {
                throw new ArgumentException("nbr must be positive");
            }
            //shuffles the board nbr times, each time choosing a random direction but keeping it solvable
            for (int i = 0; i < nbr; i++)
            {
                //use Boardcheck.IsSolvable() to check if the board is solvable
                Direction[] directions = GetPossibleDirections();
                var rnd = new Random();
                int random = rnd.Next(0, directions.Length);
                MoveDirection(directions[random]);
            }
        }
        
    }
}