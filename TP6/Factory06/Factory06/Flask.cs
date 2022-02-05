using System;
using System.Collections.Generic;

namespace Factory06
{
    public class Flask : Machine
    {
        private readonly uint [] upgrades = { 300 };
        private readonly int maxLevel = 2;

        // TODO
        public Flask()
        {
            items = new List<Item>();
            type = MachineType.Flask;
            level = 1;
            capacity = 20;
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
                    if(level == 2)
                    {
                        capacity = capacity + 4;
                    }
                    else
                    {
                        capacity = capacity *2;
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
                if (money >= count * 21 )
                {
                    while(money >= 21 || items.Count <= capacity)
                    {
                        money -= 21;
                        items.Add(new Item(ItemType.Flask));
                    }
                    return true;
                }
                else
                {
                    while (money >= 21 || items.Count <= capacity)
                    {
                        money -= 21;
                        items.Add(new Item(ItemType.Flask));
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
            capacity = 20;
            return 200/3;
        }
    }
}