using System;

namespace Monopoly
{
    public class Station : Property
    {
        public Station(string name, int price,
            int position) : base(name, price, position, 150)

        {
            this.name = name;
            this.price = price;
            this.position = position;
            this.rentCost = 150;
        }

        public override string ToString()
        {
            return "[STATION]" + base.ToString();
        }
    }
}