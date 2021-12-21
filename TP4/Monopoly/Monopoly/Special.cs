using System;

namespace Monopoly
{
    public abstract class Special : Cell
    {
        protected bool ModifyBudget(Player player, int amount)
        {
            if (amount < 0)
            {
                if (player.Balance < -amount)
                {
                    return false;
                }
                else
                {
                    player.Balance += amount;
                    return true;
                }
            }
            else
            {
                player.Balance += amount;
                return true;
            }
        }
    }
}