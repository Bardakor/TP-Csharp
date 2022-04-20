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

        public Tile(int value, bool empty) 
        {
            //type representes the state of the box, either empty or full and value is the number of the box
            this.value = value;
            if (empty)
            {
                this.type = TileType.EMPTY;
            }
            else
            {
                this.type = TileType.FULL;
            }
             
        }

        public Tile DeepCopy() 
        {
            //returns a deep copy of the tile
            return new Tile(this.value, this.type == TileType.EMPTY);
        }
    }
}