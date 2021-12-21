using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class Game
    {
        //Attributes
        private const int BeginCell = 200;
        private List<Cell> board;
        private int boardSize;
        public int BoardSize => boardSize;
        private List<Player> players;
        private int playersnumber;

        //Constructor
        public Game(int boardSize)
        {
            this.boardSize = boardSize;
            this.board = new List<Cell>();
            this.players = new List<Player>();
        }

        //Getters and Setters
        public List<Player> Players => players;
        public int PlayersNumber => playersnumber;
        public List<Cell> Board => board;

        //Methods
        public int RollDice()
        {
            Random rnd = new Random();
            int dice1 = rnd.Next(1, 7);
            int dice2 = rnd.Next(1, 7);
            return dice1 + dice2;
        }

        private int GetInput(Player player, Property property)
        {
            Console.WriteLine("No one owns the street you can buy it for " + property.Price + "£?, you have " +
                              player.Balance + "£ left. Press 1 to buy it, 0 otherwise.");
            string input = Console.ReadLine();
            if (input == "1")
            {
                return 1;
            }
            else if (input == "0")
            {
                return 0;
            }
            else
            {
                Console.WriteLine("Invalid input");
                return GetInput(player, property);
            }
        }

        public void BuyProperty(Property property, Player player, int input)
        {
            if (GetInput(player, property) == 1)
            {
                if (player.RetrieveMoney(property.Price))
                {
                    property.Owner = player;
                    player.Possessions.Add(property);
                    Console.WriteLine("You bought " + property.Name + " , your balance is now " + player.Balance + "£");
                }
                else
                {
                    Console.WriteLine("Insuficient funds");
                }
            }
        }

        public bool OnRegular(Cell cell, Player player)
        {
            Property cell1 = (Property)cell;
            //If a cell has no owner, get the decision of thr player
            if (cell1.Owner == null)
            {
                BuyProperty(cell1, player, GetInput(player, cell1));
            }
            //If a cell has an owner, check if the player is the owner
            else if (cell1.Owner == player)
            {
                Console.WriteLine("You own this property !");
            }
            //If a cell has an owner, check if the player is the owner
            else
            {
                Console.WriteLine("This cell is owned by " + cell1.Owner.Name + " and the rent cost is " +
                                  cell1.RentCost + "£");
                if (player.Balance >= cell1.RentCost)
                {
                    player.TransferTo(cell1.Owner, cell1.RentCost);
                    Console.WriteLine("You paid " + cell1.RentCost + " , your balance is now " + player.Balance + "£");
                }
                else
                {
                    Console.WriteLine("You don't have enough money to pay rent");
                    PlayerLost(player);
                    return false;
                }
            }

            return true;
        }

        public bool OnTax(Tax tax, Player player)
        {
            if (player.Position == tax.Position)
            {
                Console.WriteLine("You are now on a tax cell: you have to pay: " + tax.Amount + "£");
                if (tax.TaxPlayer(player))
                {
                    Console.WriteLine("You paid " + tax.Amount + "£, your balance is now " + player.Balance + "£");
                }
                else
                {
                    Console.WriteLine("You don't have enough money to pay rent");
                    PlayerLost(player);
                    return false;
                }
            }

            return true;
        }

        public bool PlayRound(Player player)
        {
            Console.WriteLine(player.Name + " it is your turn to play please press any key to roll the dice");
            //if the player is in prison, he has the righ to attempt to do a double
            if (player.jailed)
            {
                Jail.AttemptDiceDouble(player);
                return true;
            }
            //if the player is not in prison, he has to roll the dice
            else
            {
                int a = RollDice();

                //if he goes past by the begin cell you have to give him 200£
                if (player.Position + a > BeginCell)
                {
                    player.ReceiveMoney(200);
                }

                player.Move(a, boardSize);
                //if he lands on a cell, check what he can do depending on the type of cell
                Cell cell = board[player.Position];
                switch (cell)
                {
                    case Street:
                    case Company:
                    case Station:
                        return OnRegular(cell, player);
                    case Tax tax:
                        return OnTax(tax, player);
                    case Luck luck:
                        if (luck.GetEffect(player))
                            return true;
                        PlayerLost(player);
                        return false;
                    case Jail jail:
                        player.SendToJail();
                        return true;
                }
            }

            return true;
        }

        public bool PlayerWon()
        {
            if (playersnumber == 1)
            {
                Console.WriteLine("Player " + players[0].Name + " won!");
                return true;
            }

            return false;
        }

        public void PlayerLost(Player player)
        {
            players.Remove(player);

            foreach (Property property in player.Possessions)
            {
                board.Remove(property);
            }

            foreach (Property property in player.Possessions)
            {
                property.Owner = null;
            }
        }

        public void AddBoard(Cell cell)
        {
            boardSize++;
            board.Add(cell);
        }

        /*
        public void AddPlayer(Player player)
        {
            this.playersNumber++;
            this.players.Add(player);
        }
        */

        public void Play()
        {
            int i = 0;
            while (!PlayerWon())
            {
                Player player = players[i];
                bool survival = PlayRound(player);
                if (survival)
                {
                    i++;
                }

                i %= playersnumber;
            }
        }
    }

    /*
    public void DisplayBoard()
    {
        int position = 0;
        foreach (Cell c in this.board)
        {
            Console.WriteLine(c);
            foreach (Player player in players)
            {
                if (player.Position == position)
                    Console.WriteLine("\t* " + player.Name + " (" + player.Balance +
                                      "£)");
            }
            ++position;
        }
    }
    */
}