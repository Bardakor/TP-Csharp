using System.Collections.Generic;

namespace Factory06
{
    public abstract class Machine
    {
        protected List <Item> items;
        protected MachineType type;
        protected int level;
        protected uint capacity;

        // TODO
        // Getters
        // Add a getter Items
        // Add a getter Type
        // Add a getter Level
        // Add a getter Capacity
        public List<Item> Items
        {
            get { return items; }
        }
        public MachineType Type
        {
            get { return type; }
        }
        public int Level
        {
            get { return level; }
        }
        public uint Capacity
        {
            get { return capacity; }
        }

        // TODO
        /**
         * Given a specific amount of money, try to upgrade the machine.
         * Returns true if possible and done, false otherwise.
         * <param name="money">The factory's money</param>
         */
        public abstract bool Upgrade(ref long money);
        
        // TODO
        /**
         * Produces count items if enough money. It also updates the money.
         * Returns true if it has produced count items, false otherwise.
         * <param name="count">Number of item to produce</param>
         * <param name="money">The factory's money</param>
         */
        public abstract bool Produce(uint count, ref long money);

        // TODO
        /**
         * Just clear the machine's list of item.
         */
        public abstract void Clear();

        // TODO
        /**
         * Destroy the machine : remove all its items, and returns a third
         * of its price.
         */
        public abstract uint Destroy();
    }
}