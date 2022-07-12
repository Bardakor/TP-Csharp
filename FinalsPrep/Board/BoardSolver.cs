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
            // FIXME
            throw new NotImplementedException();
        }

        public int CalculateHeuristic()
        {
            // FIXME
            throw new NotImplementedException();
        }


        private Direction reverseDirection(Direction direction)
        {
            // FIXME
            throw new NotImplementedException();
        }

        public List<Direction> SolveBoard()
        {
            // FIXME
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