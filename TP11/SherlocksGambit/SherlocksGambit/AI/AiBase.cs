using System;
using System.Collections.Generic;
using System.Linq;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils;
using SherlocksGambit.Utils.Bonuses;
using SherlocksGambit.Utils.Encryption;
using SherlocksGambit.Utils.Helpers;
using SherlocksGambit.Utils.Runners.DotTree;

namespace SherlocksGambit.AI
{
    public abstract class AiBase
    {
        /// Constant to indicate a terrible Board
        protected const double Min = double.NegativeInfinity;
        
        /// Constant to indicate a divine Board
        protected const double Max = double.PositiveInfinity;

        /// AI's properties
        protected readonly AiProperties AiProperties;

        /// Reference to the current Board
        public readonly Board Board;
        
        /// Color of the AI
        protected readonly PlayerColor Color;

        /// The AI's maximum depth of search
        protected readonly int MaxDepth;

        /// Heuristic function to calculate how good a Board is
        protected readonly Func<Board, PlayerColor, double> HeuristicFunction;

        /// AI's stat for how long it took to generate a move
        public double ElapsedTime;
        
        /// AI's stat to indicate how good the move generated was for the AI
        public double WithHeuristic;
        
        /// AI's stat to register how many paths were explored while generating the move
        public long ExploredPaths;
        
        /// [BONUS] Reference to the AI's TranspositionTable
        protected readonly TranspositionTable TranspositionTable;
        
        /// [BONUS] Reference to the AI's MoveOrdering
        protected readonly MoveOrdering MoveOrdering;

        /**
         * <summary> Basic constructor for AiBase </summary>
         * <param name="properties"> AI's properties containing ready to assign attributes </param>
         * Initialize all attributes on this class
         */
        protected AiBase(AiProperties properties)
        {
            AiProperties = properties;

            // [BONUS]
            if (properties.Transposition)
                TranspositionTable = new TranspositionTable();

            // [BONUS]
            if (AiProperties.MoveOrdering)
                MoveOrdering = new MoveOrdering(TranspositionTable);

            Color = properties.Color;
            Board = properties.Board;
            MaxDepth = properties.MaxDepth;

            HeuristicFunction = properties.HeuristicFunction;
        }

        /**
         * <summary> Checks the current state of the game </summary>
         * <returns> A boolean to indicate if the AI has won or lost the game </returns>
         * <remarks> Loosing and winning are not linked, therefore be careful to check these two conditions </remarks>
         */
        public bool IsEoG(Board board)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Abstract function that generates a move </summary>
         * <remarks> You should implement this function in classes that inherit AiBase (i.e MiniMax.cs) </remarks>
         */
        protected abstract Tuple<double, PathObject> GenerateMove(Board currBoard, int currDepth,
            bool minimize = false, double alpha = Min, double beta = Max, DotNode root = null, bool quietMode = false);

        /**
         * <summary> Generates all the possibles paths for each of `currPlayer`'s pieces </summary>
         * <param name="currBoard"> Board on which to generate possible paths </param>
         * <param name="currPlayer"> PlayerColor used to retrieve pieces from the `currBoard`'s PieceManager </param>
         * <param name="enemyFlag"> If set to true, generate only capture paths </param>
         * <returns> A list of all generated PathObjects </returns>
         * <remarks>
         * `CheckPathing`'s last argument `prevEnemies` has a different meaning whether the List given is null or empty  
         * As soon as you find a path that is a "capture move", all possible paths returned should be capture paths  
         * To check if a PathObject is a "capture move", look at the number of elements in the PathObject's enemies list
         * </remarks>
         */
        protected static List<PathObject> GeneratePaths(Board currBoard, PlayerColor currPlayer, bool enemyFlag = false)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }

        /**
         * <summary> Gets a move, executes it and then creates a move </summary>
         * <param name="move"> Move to decrypt and execute </param>
         * <param name="root"> [Optional, default = null] DotNode provided by the DotRunner class </param>
         * <returns> A string representing the move encrypted </returns>
         * <remarks>
         * `move` is assumed not null nor empty and correctly formatted with good cell coordinates  
         * There are 2 special codes that could be returned: "WON!" and "LOST..."  
         * The first one gets returned if the move could not be executed while the latter one when no move could be
         * generated ergo, either the PathObject returned from `GenerateMove` is null or it's associated path is empty  
         * There is a way to pass a specific argument to a function call while conserving default values for previous
         * parameters. It would look lie this:
         * <code>
         * public bool func(int a, int b = 0, int c = 1, int d = 2) { return a + b + c + d; }
         * Console.WriteLine(func(1, c:4)) // stdout: 7
         * </code>
         * You should also update the AI's stats in here:  
         * Setting `ExploredPaths` to 0 before generating a move  
         * Setting `ElapsedTime` to its value (see DateTime and TimeSpan)  
         * Setting `WithHeuristic` to the correct value  
         * Don't forget to execute the move!
         * </remarks>
         */
        public string GetThenCreate(string move, DotNode root = null)
        {
            throw new NotImplementedException("TODO: REMOVE THIS!");
        }
    }
}