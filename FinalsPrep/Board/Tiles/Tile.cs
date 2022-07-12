using System;

namespace puzzle_game
{
    public enum TileType
    {
        EMPTY,
        FULL
    }
    
    public class Tile
    {
        
        private int value;
        
        private TileType type;

        public TileType Type => type;

        public int Value => value;

        public Tile(int value, bool empty) {
            // FIXME
            throw new NotImplementedException();
        }

        public Tile DeepCopy() {
            // FIXME
            throw new NotImplementedException();
        }
    }
}