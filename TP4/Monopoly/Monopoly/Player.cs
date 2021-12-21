using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class Player
    {
        public bool jailed;

        private List<Property> possessions;
        private int balance;
        private int position;
        private string name;

        public List<Property> Possessions
        {
            get { return possessions; }
        }

        public int Balance
        {
            get { return balance; }
            set => balance = value;
        }

        public int Position
        {
            get { return position; }
        }

        public string Name
        {
            get { return nameof(Player); }
        }

        public Player(string name, int initialBalance, int initialPosition)
        {
            this.name = name;
            this.balance = initialBalance;
            this.position = initialPosition;
            this.jailed = false;
            this.possessions = new List<Property>();
        }

        public void ReceiveMoney(int amount)
        {
            balance += amount;
        }

        public bool RetrieveMoney(int amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                return true;
            }

            return false;
        }

        public bool Buy(Property p)
        {
            if (balance >= p.Price)
            {
                balance -= p.Price;
                possessions.Add(p);
                return true;
            }

            return false;
        }

        public bool Sell(Property p)
        {
            if (possessions.Contains(p))
            {
                possessions.Remove(p);
                balance += p.Price;
                return true;
            }

            return false;
        }

        public bool TransferTo(Player p, int amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                p.balance += amount;
                return true;
            }

            return false;
        }

        public bool SellTo(Property p, Player player)
        {
            if (possessions.Contains(p))
            {
                possessions.Remove(p);
                player.ReceiveMoney(p.Price);
                return true;
            }

            return false;
        }

        public void Move(int vector, int boardSize)
        {
            position = (position + vector) % boardSize;
        }

        public void SendToJail()
        {
            jailed = true;
        }

        /*
        public override string ToString()
        {
            string pos = this.possessions.Count > 0 ? this.possessions[0].Name : "";

            for (int i = 1; i < this.possessions.Count; i++)
                pos += ", " + this.possessions[i].Name;
            
            return "player: '" + this.name + "'\n"
                       + "balance: " + this.balance + " £\n"
                       + "position: " + this.position + "\n"
                       + "possessions: " + pos;
        }
        */
    }
}