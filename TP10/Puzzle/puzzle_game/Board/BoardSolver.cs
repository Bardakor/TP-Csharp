using System;
using System.Collections.Generic;
using System.Linq;
using puzzle_game.SimpleGraph;
using static puzzle_game.Board;

namespace puzzle_game
{
    public partial class Board
    {

        private int ManhattanDistance(int i1, int i2)
        {
            //returns the manhattan distance between the two tiles
            int row1 = i1 / width;
            int col1 = i1 % width;
            int row2 = i2 / width;
            int col2 = i2 % width;
            return Math.Abs(row1 - row2) + Math.Abs(col1 - col2);
        }

        public int CalculateHeuristic()
        {
            //returns the heuristic value of the board
            int heuristic = 0;
            for (int i = 0; i < size; i++)
            {
                if (board[i].Type == TileType.FULL)
                {
                    heuristic += ManhattanDistance(i, board[i].Value - 1);
                }
            }
            return heuristic;
        }


        private Direction reverseDirection(Direction direction)
        {
            //returns the reverse direction of the given direction
            switch (direction)
            {
                case Direction.UP:
                    return Direction.DOWN;
                case Direction.DOWN:
                    return Direction.UP;
                case Direction.LEFT:
                    return Direction.RIGHT;
                case Direction.RIGHT:
                    return Direction.LEFT;
                default:
                    throw new ArgumentException("direction is not valid");
            }
        }

        public List<Direction> SolveBoard()
        {
            List<Direction> solution = new List<Direction>();
            int heuristic = CalculateHeuristic();
            MinHeap<Board> open = new MinHeap<Board>(size * size);
            throw new NotImplementedException();
        }

        public void ApplyMovements(List<Direction> directions)
        {
            foreach (Direction direction in directions)
                MoveDirection(direction);
        }
        
        /* BONUS */
        public List<Direction> SolveBoardBonus()
        {
            // FIXME
            throw new NotImplementedException();
        } 
    }
}