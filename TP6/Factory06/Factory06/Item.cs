
 using System;

namespace Factory06
{
    public class Item
    {
        private readonly uint price;
        private ItemType type;
        
        //get price
        public uint Price
        {
            get { return price; }
        }
        //get type
        public ItemType Type
        {
            get { return type; }
        }

        // TODO
        public Item(ItemType type)
        {
            this.type = type;
            switch (type)
            {
                case ItemType.Hat:
                    price = 2;
                    break;
                case ItemType.Coat:
                    price = 7;
                    break;
                case ItemType.Flask:
                    price = 21;
                    break;
                default:
                    throw new ArgumentException("Invalid item type");
            }
            
        }

        // TODO
        /**
         * Sell the item.
         * A hat is worth 3 times its price.
         * A coat is worth 4 times its price.
         * A flask is worth 6 times its price.
         */
        public uint Sell()
        {
            uint price = this.price;
            switch (type)
            {
                case ItemType.Hat:
                    price = price * 3;
                    break;
                case ItemType.Coat:
                    price = price * 4;
                    break;
                case ItemType.Flask:
                    price = price * 6;
                    break;
            }
            return price;
        }
    }
}