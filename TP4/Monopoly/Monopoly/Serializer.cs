using System;

namespace Monopoly
{
    public static class Serializer
    {
        private static Street HandleStreet(string[] arr, int position)
        {
            if (arr.Length < 5)
                throw new Exception("Loading Error: Not enough arguments to build Street");
            return new Street(arr[1], Int32.Parse(arr[2]), position,
                Int32.Parse(arr[3]), Street.ColorOfString(arr[4]));
        }

        private static Company HandleCompany(string[] arr, int position)
        {
            if (arr.Length < 3)
                throw new Exception("Loading Error: Not enough arguments to build Company");
            return new Company(arr[1], Int32.Parse(arr[2]), position);
        }

        private static Station HandleStation(string[] arr, int position)
        {
            if (arr.Length < 3)
                throw new Exception("Load Error: Not enough arguments to build Station");
            return new Station(arr[1], Int32.Parse(arr[2]), position);
        }

        private static Tax HandleTax(string[] arr, int position)
        {
            if (arr.Length < 2)
                throw new Exception("Load Error: Not enough arguments to build Tax");
            return new Tax(Int32.Parse(arr[1]), position);
        }

        public static Game Load(string filename)
        {
            var fileLines = System.IO.File.ReadLines(filename);
            Game game = new Game(0);
            game.AddBoard(new BeginCell());
            foreach (string line in fileLines)
            {
                string[] cmd = line.Split(';');
                switch (cmd[0])
                {
                    case "Street":
                        game.AddBoard(HandleStreet(cmd, game.BoardSize));
                        break;
                    case "Company":
                        game.AddBoard(HandleCompany(cmd, game.BoardSize));
                        break;
                    case "Station":
                        game.AddBoard(HandleStation(cmd, game.BoardSize));
                        break;
                    case "Tax":
                        game.AddBoard(HandleTax(cmd, game.BoardSize));
                        break;
                    case "Luck":
                        game.AddBoard(new Luck(game.BoardSize));
                        break;
                    case "Jail":
                        game.AddBoard(new Jail(game.BoardSize));
                        break;
                    default:
                        throw new Exception("Invalid token in parsing of '"
                                            + filename + "'\nGot: '" + cmd[0] + "'");
                }
            }

            return game;
        }
    }
}