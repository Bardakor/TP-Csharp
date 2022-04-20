using System;
using System.Collections.Generic;
using System.Globalization;
using SherlocksGambit.AI;
using SherlocksGambit.Game;
using SherlocksGambit.Utils.Encryption;
using SherlocksGambit.Utils.Helpers;

namespace SherlocksGambit.Utils.Runners
{
    public class Runner
    {
        /// Boolean to indicate should the runner print on the console
        protected readonly bool Print;
        
        /// `turn` keeps track of the current turn number
        protected int Turn;

        /// Reference to a Board for move checking
        protected readonly Board MoveCheckerBoard;
        
        /// Last move made by any of the AIs
        protected string LastMove = "";

        /// Dictionary containing both black and white AI
        protected readonly Dictionary<PlayerColor, AiBase> Players = new();
        
        /**
         * <summary> Basic constructor for Runner </summary>
         * <param name="blackAi"> AiBase of the White player </param>
         * <param name="whiteAi"> AiBase of the White player </param>
         * <param name="print"> [Optional, default = true] Should the runner print on the console </param>
         */
        public Runner(AiBase blackAi, AiBase whiteAi, bool print = true)
        {
            Players[PlayerColor.Black] = blackAi;
            Players[PlayerColor.White] = whiteAi;
            MoveCheckerBoard = blackAi.Board.Copy();

            Print = print;
        }

        /**
         * <summary> Runs a game of checkers </summary>
         * <remarks>
         * Some returns have been commented, you may uncomment them for testing purposes in order to stop the game after
         * Black's first move or after one turn.  
         * You are allowed to change this function to enhance your testing experience as this function won't be tested  
         * There is a constant set at 500 for the max amount of turns, this avoids infinite loops. You can change this
         * value to fit your computer!
         * </remarks>
         */
        public virtual void Run()
        {
            // If `print` print the starting board
            if (Print)
                MoveCheckerBoard.Print();

            // Infinite loop that increments `_turn`
            for (;; Turn++)
            {
                // Generate a move from each AI
                foreach (var (playerColor, ai) in Players)
                {
                    try
                    {
                        // Generate a move
                        LastMove = ai.GetThenCreate(LastMove);

                        // Check if the move was correct
                        MoveChecker(ai, playerColor);

                        // Throw an error if more than 500 turns have been played
                        // (you may change this value to suit your computer)
                        if (Turn > 500)
                            throw new Exception("That's a lot of moves 0.o");
                        
                        // Break here if you only want the black AI's turn (for testing purposes only)
                        return;
                    }
                    // If you caught an exception, the game ended
                    catch (Exception e)
                    {
                        // If not a EoG exception then re throw the exception
                        if (e is not EoGException)
                            throw;

                        // Else print its message on the console and stop the loop
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
                
                // Break here if you only want 1 turn (for testing purposes only)
                // return;
            }
        }
        
        /**
         * <summary> Checks the validity of the move </summary>
         * <param name="ai"> The AiBase that made the move </param>
         * <param name="playerColor"> The PlayerColor of the `ai` </param>
         * <exception cref="EoGException"> $"{playerColor.ToString()} {LastMove}" </exception>
         * <exception cref="EoGException"> "Illegal move provided: " + LastMove </exception>
         * <exception cref="EoGException"> $"{playerColor.ToString()} WON!" </exception>
         * Checks if a move was valid using the MoveChecker Board  and throws an exception if an end of game state has
         * been reached
         */
        protected void MoveChecker(AiBase ai, PlayerColor playerColor)
        {
            // If the AI sent an EoG message, parses it 
            if (LastMove.Equals("WON!") || LastMove.Equals("LOST..."))
                throw new EoGException($"{playerColor.ToString()} {LastMove}");

            // Tries to execute the move
            if (!MoveEncryption.Decrypt(MoveCheckerBoard, LastMove))
                throw new EoGException("Illegal move provided: " + LastMove);

            // If `print` is true, print the elapsed time, heuristic of the move ans amount of explored path by the AI
            // followed by the new board and the move done
            if (Print)
            {
                Console.WriteLine("========");
                Console.WriteLine($"{playerColor.ToString()}:\n" +
                                  $"Decision made in {ai.ElapsedTime.ToString()}ms with heuristic " +
                                  $"{ai.WithHeuristic.ToString(CultureInfo.InvariantCulture)}\n" +
                                  $"{ai.ExploredPaths.ToString()} explored paths");
                
                MoveCheckerBoard.Print();
                Console.WriteLine(LastMove);
            }

            // If after this move the player playing won, stop the infinite loop
            if (MoveCheckerBoard.HasWon(playerColor))
                throw new EoGException($"{playerColor.ToString()} WON!");
        }
    }
}