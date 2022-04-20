using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace puzzle_game
{
    public class Viewer
    {

        public static string convert_to_string(List<Direction> directions)
        {
            string result = "";

            List<(Direction, string)> lookup_table = new List<(Direction, string)>
            {
                (Direction.UP, "UP"),
                (Direction.DOWN, "DOWN"),
                (Direction.RIGHT, "RIGHT"),
                (Direction.LEFT, "LEFT")
            };
            
            foreach (Direction direction in  directions)
            {
                foreach ((Direction, string) item in lookup_table)
                    if (item.Item1 == direction) {
                        result += direction;
                        break;
                    }
                result += '\n';
            }
            
            return result;
        }
        
        // Asynchronous function for file writing
        public static async Task WriteInFile(string matrix, string steps) { 
            await File.WriteAllTextAsync("Viewer/steps.out", matrix + '\n' + steps);
        }
        
        public static void Parse(Board board, List<Direction> directions)
        {
            // Create matrix string
            string matrix = "";
            for (int i = 0; i < board.Size - 1; i++)
                matrix += board.Board1[i].Value + ";";
            matrix += board.Board1.Last().Value;
            string steps = convert_to_string(directions);
            WriteInFile(matrix, steps);
        }

        public static void LaunchViewer()
        {
            ProcessStartInfo viewer = new ProcessStartInfo();
            viewer.FileName = "Viewer/main.py";
            
            viewer.WorkingDirectory = Directory.GetCurrentDirectory();
            viewer.UseShellExecute = false;
            viewer.RedirectStandardError = true;
            viewer.RedirectStandardOutput = true;
            
            Console.WriteLine(viewer.WorkingDirectory);
            using (Process process = Process.Start(viewer))
            {
                using (StreamReader sr = process.StandardOutput)
                    Console.Write(sr.ReadToEnd());
                using (StreamReader sr = process.StandardError)
                    Console.Write(sr.ReadToEnd());
            } 
        }
    }
    
}