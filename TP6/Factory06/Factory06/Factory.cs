using System;
using System.Collections.Generic;

namespace Factory06
{
    public class Factory
    {
        private long money;
        private int maxNbMachines = 50;
        private List<Machine> machines;

        // TODO
        // Getters
        // Add a getter Money
        public long Money
        {
            get { return money; }
        }

        // TODO
        public Factory(long initialMoney)
        {
            this.money = initialMoney;
            this.machines = new List<Machine>();
            this.maxNbMachines = 50;
        }

        // TODO
        /**
         * Return a list of all the machines of the corresponding type.
         * <param name="type">The type of machine to search for</param>
         */
        public List<Machine> GetMatchMachines(MachineType type)
        {
            List<Machine> matchMachines = new List<Machine>();
            foreach (Machine machine in machines)
            {
                if (machine.Type == type)
                {
                    matchMachines.Add(machine);
                }
            }
            return matchMachines;
        }

        // TODO
        /**
         * Returns a machine of the specified type which still has
         * some capacity left to produce an item.
         * <param name="type">The type of the machine to search for</param>
         */
        public Machine FindAvailableMachine(MachineType type)
        {
            List<Machine> matchMachines = GetMatchMachines(type);
            foreach (Machine machine in matchMachines)
            {
                if (machine.Capacity > 0)
                {
                    return machine;
                }
            }
            return null;
        }

        // TODO
        /**
         * Build a new machine of the specified type if the factory
         * has enough money and places.
         * Returns true if built, false otherwise
         * <param name="type">The type of the machine to build</param>
         */
        public bool Build(MachineType type)
        {
            switch (type)
            {
                case MachineType.Coat:
                    if ((money > 120) && (machines.Count < maxNbMachines))
                    {
                        money -= 120;
                        return true;
                    }
                    break;
                case MachineType.Hat:
                    if ((money > 90) && (machines.Count < maxNbMachines))
                    {
                        money -= 90;
                        return true;
                    }
                    break;
                case MachineType.Flask:
                    if ((money > 200) && (machines.Count < maxNbMachines))
                    {
                        money -= 200;
                        return true;
                    }
                    break;
            }

            return false;

        }

        // TODO
        /**
         * Try to produce count items from machines of the specified type
         * in the factory.
         * Returns true if count items were produced, false otherwise.
         * <param name="type">The type of machine to search for</param>
         * <param name="count">The number of items to produce</param>
         */
        public bool Produce(MachineType type, int count)
        {
            Machine machine = FindAvailableMachine(type);
            if (machine != null)
            {
                switch (type)
                {
                    case MachineType.Coat:
                        return machine.Produce(Convert.ToUInt32(count), ref money);
                    case MachineType.Hat:
                        return machine.Produce(Convert.ToUInt32(count), ref money);
                    case MachineType.Flask:
                        return machine.Produce(Convert.ToUInt32(count), ref money);
                }
            }
            return false;
        }

        // TODO
        /**
        * Upgrade all machine on the factory if enough money.
        * Returns true if all upgrade were done, false otherwise.
        */
        public bool UpgradeAll()
        {
            foreach (Machine machine in machines)
            {
                if (!machine.Upgrade(ref money))
                {
                    return false;
                }
            }
            return true;
        }

        // TODO
        /**
        * Upgrade up to count machine on the factory of the specified type
        * and level if the factory has enough money.
        * Returns true if count upgrades were done, false otherwise
        * <param name="type">The type of the machines to upgrade</param>
        * <param name="level">The level of the machines to upgrade</param>
        * <param name="count">The number of machine to upgrade</param>
         */
        public bool UpgradeMatch(MachineType type, int level, int count)
        {
            List<Machine> matchMachines = GetMatchMachines(type);
            int nbMachines = 0;
            foreach (Machine machine in matchMachines)
            {

                if (machine.Level == level && nbMachines < count)
                {
                    if (!machine.Upgrade(ref money))
                    {
                        return false;
                    }
                    nbMachines++;
                }
                if (nbMachines == count)
                {
                    return true;
                }
            }
            return false;
        }

        // TODO
        /**
         * Destroy all the machines in the factory.
         * Returns the total money gained, and also updates the factory's money.
         */
        public uint DestroyAll()
        {
            uint totalMoney = 0;
            foreach (Machine machine in machines)
            {
                totalMoney += machine.Destroy();
            }
            money += totalMoney;
            return totalMoney;
        }

        // TODO
        /**
         * Destroy all the machines in the factory of the specified type.
         * Returns the total money gained, and also updates the factory's money.
         * <param name="type">The type of machine to destroy</param>
         */
        public uint DestroyMatch(MachineType type)
        {
            uint totalMoney = 0;
            List<Machine> matchMachines = GetMatchMachines(type);
            foreach (Machine machine in matchMachines)
            {
                totalMoney += machine.Destroy();
            }
            money += totalMoney;
            return totalMoney;
        }

        // TODO
        /**
         * Collect all the items on the factory.
         */
        public List<Item> CollectAll()
        {
            List<Item> items = new List<Item>();
            foreach (Machine machine in machines)
            {
                foreach(Item item in machine.Items)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        // TODO
        /**
         * Collect all the items on the factory from the machine of the
         * corresponding type.
         * <param name="type">The type of machine to collect from</param>
         */
        public List<Item> CollectMatch(MachineType type)
        {
            List<Item> items = new List<Item>();
            List<Machine> matchMachines = GetMatchMachines(type);
            foreach (Machine machine in matchMachines)
            {
                foreach (Item item in machine.Items)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        // TODO
        /**
         * Sell all the machines' items on the factory.
         * Returns the total money gained, and updates the factory's money.
         */
        public uint SellAll()
        {
            uint totalMoney = 0;
            foreach (Machine machine in machines)
            {
                
                foreach (Item item in machine.Items)
                {
                    totalMoney += item.Sell();
                }
            }
            money += totalMoney;
            return totalMoney;
        }

        // TODO
        /**
         * Sell all the items on the factory from the machine of the
         * corresponding type.
         * <param name="type">The type of machine to sell items</param>
         */
        public uint SellMatch(MachineType type)
        {
            uint totalMoney = 0;
            List<Machine> matchMachines = GetMatchMachines(type);
            foreach (Machine machine in matchMachines)
            {
                foreach (Item item in machine.Items)
                {
                    totalMoney += item.Sell();
                }
            }
            money += totalMoney;
            return totalMoney;
        }

        // TODO
        /**
         * Clear all machines items on the factory.
         */
        public void ClearAll()
        {
            foreach (Machine machine in machines)
            {
                machine.Clear();
            }
        }

        // TODO
        /**
         * Clear the items on the factory from the machine of the
         * corresponding type.
         * <param name="type">The type of machine to clear items</param>
         */
        public void ClearMatch(MachineType type)
        {
            List<Machine> matchMachines = GetMatchMachines(type);
            foreach (Machine machine in matchMachines)
            {
                machine.Clear();
            }
        }
    }
}