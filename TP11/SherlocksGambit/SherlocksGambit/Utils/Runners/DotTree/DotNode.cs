using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Utils.Runners.DotTree
{
    public sealed class DotNode
    {
        /// `Id` refers to the id of this DotNode (the hash value of this object)
        public readonly int Id;

        /// PlayerColor of the one making the move to reach this DotNode
        private readonly PlayerColor _playerColor;
        
        /// Name of the color to display on the Dot Visualizer
        private readonly string _color;

        /// List of this node's children
        private readonly List<DotNode> _children = new ();

        /// `Value` is the heuristic of this DotNode
        public double Value;
        
        /// `Board` is the Fen encoded Board
        public string Board;
        
        /// `Move` stored the encoded move required to reach this DotNode
        public string Move;
        
        /// [BONUS] Link to transposed node
        public int TransposedNode;

        /**
         * <summary> Basic constructor for DotNode </summary>
         * <param name="playerColor"> PlayerColor of the player making the move to reach this DotNode </param>
         * <remarks>
         * By default, every other attribute is left with its default value.  
         * It is up to you to set them accordingly following the guide given in the subject
         * </remarks>
         */
        public DotNode(PlayerColor playerColor)
        {
            Id = GetHashCode();
            _playerColor = playerColor;
            _color = playerColor == PlayerColor.Black ? "Blue" : "Red";
        }

        /**
         * <summary> Adds a child to this node </summary>
         * <returns> The newly created child </returns>
         */
        public DotNode AddChild()
        {
            var newNode = new DotNode(ColorHelper.GetOpponentColor(_playerColor));
            _children.Add(newNode);
            
            return newNode;
        }

        /**
         * <summary> Append this node's dot representation to the file opened by `sw` </summary>
         * <param name="sw"> StreamWriter pointing to the file to write in given by DotRunner </param>
         * <remarks> `sw` is considered to be valid or null. It should not point to an unopened file </remarks>
         */
        public void ToDot(StreamWriter sw)
        {
            // If there is no StreamWriter return 
            if (sw is null)
                return;
            
            // If there is a TransposedNode, directly link to it
            if (TransposedNode != 0)
            {
                sw.WriteLine($"\"{TransposedNode.ToString()}\" [color=Green label=\"{Move}\"];");
                return;
            }

            // Write in `sw` this node's dot representation
            sw.WriteLine($"\"{Id.ToString()}\" [label=\"{Move}\"];");
            sw.WriteLine($"\"{Id.ToString()}\" [shape=record color={_color} label=" +
                         $"\"{{ VALUE={Value.ToString(CultureInfo.InvariantCulture)} | BOARD={Board} " +
                         "| {{ <children> CHILDREN }}}}\"];");

            // Recursively call `ToDot` for each children
            foreach (var child in _children)
            {
                sw.Write($"\"{Id.ToString()}\":children -> ");
                child.ToDot(sw);
            }
        }
    }
}