using System;
using System.Collections.Generic;

namespace Factory06
{
    public class Coat : Machine
    {
        private readonly uint[] upgrades = { 200, 500 };
        private readonly int maxLevel = 3;

        // TODO
        public Coat()
        {
            items = new List<Item>();
            type = MachineType.Coat;
            level = 1;
            capacity = 30;
        }

        // TODO
        public override bool Upgrade(ref long money)
        {
            if (level == maxLevel)
            {
                return false;
            }
            else
            {
                if (money >= upgrades[level - 1])
                {
                    money -= upgrades[level - 1];
                    level++;
                    if (level == 2)
                    {
                        capacity = capacity + 10;
                    }
                    else
                    {
                        capacity = capacity * 2;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // TODO
        public override bool Produce(uint count, ref long money)
        {
            if (count == 0)
            {
                return false;
            }
            else
            {
                if (money >= count * 7)
                {
                    while (money >= 7 || items.Count <= capacity)
                    {
                        money -= 7;
                        items.Add(new Item(ItemType.Coat));
                    }
                    return true;
                }
                else
                {
                    while (money >= 7 || items.Count <= capacity)
                    {
                        money -= 7;
                        items.Add(new Item(ItemType.Coat));
                    }
                    return false;
                }
            }
        }

        // TODO
        public override void Clear()
        {
            foreach (Item item in items)
            {
                Items.RemoveAt(items.IndexOf(item));
            }
        }

        // TODO
        public override uint Destroy()
        {
            Clear();
            level = 1;
            capacity = 30;
            return 120 / 3;
        }
    }
}