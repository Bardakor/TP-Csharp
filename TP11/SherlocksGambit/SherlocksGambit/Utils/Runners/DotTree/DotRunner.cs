using System;
using System.Globalization;
using System.IO;
using SherlocksGambit.AI;

namespace SherlocksGambit.Utils.Runners.DotTree
{ 
    public class DotRunner : Runner
    {
        /**
         * <summary> Folder the DotHelper will write in </summary>
         * <remarks>
         * You can find it at the root of this practical inside the "SherlocksGambit" folder  
         * This folder should not be pushed as it will be considered a trash file. Use the given .gitignore
         * </remarks>
         */
        private readonly string _outputFolder = "../../../DotHelper/";

        /**
         * <summary> Overloading constructor for Runner </summary>
         * <param name="blackAi"> The AiBase for the Black player </param>
         * <param name="whiteAi"> The AiBase for the Black player </param>
         * <param name="print"> [Optional, default = true] Should the runner print on the console </param>
         * <remarks>
         * Append to `_outputFolder` to sort files according to the time and create all the necessary directories
         * </remarks>
         */
        public DotRunner(AiBase blackAi, AiBase whiteAi, bool print = true) : base(blackAi, whiteAi, print)
        {
            // Get the current date and time and format it of the sort: dd-mm-yy/hh-mm-ss
            var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            _outputFolder += date
                .Replace('/', '-')
                .Replace(' ', '/')
                .Replace(':', '-');

            // Create all the necessary directories
            Directory.CreateDirectory(_outputFolder);
        }

        /**
         * <summary> Override for `Run` on superclass Runner </summary>
         * <remarks>
         * Adds the support for the Dot Visualisation by creating DotNodes and opening StreamWriters  
         * For more information on this function, refer to the virtual one on the superclass Runner
         * </remarks>
         */
        public override void Run()
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
                        // Create the root node for the dot format, and generate a move 
                        var currRoot = new DotNode(playerColor);
                        LastMove = ai.GetThenCreate(LastMove, currRoot);

                        // Format the filename according to the current turn and the color of the AI playing
                        var filename = $"{_outputFolder}/{Turn.ToString()}-{playerColor.ToString()}.dot";
                        var sw = new StreamWriter(filename);
                        
                        // Fill the file with the dot representation of `currRoot`
                        sw.WriteLine("digraph G {\nrankdir=\"LR\";");
                        currRoot.ToDot(sw);
                        sw.WriteLine("}");
                        
                        // Close the StreamWriter and check if the move was correct
                        sw.Close();
                        MoveChecker(ai, playerColor);

                        // Throw an error if more than 500 turns have been played
                        // (you may change this value to suit your computer)
                        if (Turn > 500)
                            throw new Exception("That's a lot of moves 0.o");
                        
                        // Break here if you only want the black AI's turn (for testing purposes only)
                        // return;
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
    }
}