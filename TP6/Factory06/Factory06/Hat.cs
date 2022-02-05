using System;
using System.Collections.Generic;

namespace Factory06
{
    public class Hat : Machine
    {
        private readonly uint [] upgrades = { 200, 300, 400 };
        private readonly int maxLevel = 4;

        // TODO
        public Hat()
        {
            items = new List<Item>();
            type = MachineType.Hat;
            level = 1;
            capacity = 300;
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
                        capacity = capacity + 300;
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
                if (money >= count * 2 )
                {
                    while(money >= 2 || items.Count <= capacity)
                    {
                        money -= 2;
                        items.Add(new Item(ItemType.Hat));
                    }
                    return true;
                }
                else
                {
                    while (money >= 2 || items.Count <= capacity)
                    {
                        money -= 2;
                        items.Add(new Item(ItemType.Hat));
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

        public override uint Destroy()
        {
            Clear();
            level = 1;
            capacity = 300;
            return 90/3;

        }
    }
}