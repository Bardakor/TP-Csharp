using System;

namespace Monopoly
{
    public class Tax : Special
    {
        private int amount;

        public int Amount
        {
            get { return amount; }
        }

        public Tax(int amount, int position)
        {
            this.amount = amount;
            this.position = position;
        }

        public bool TaxPlayer(Player player)
        {
            if (player.Balance >= amount)
            {
                player.RetrieveMoney(amount);
                return true;
            }

            return false;
        }

        /*
        public override string ToString()
        {
            return "[Tax]: " + this.amount + "£";
        }
        */
    }
}