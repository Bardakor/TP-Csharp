using System;

namespace Monopoly
{
    public class Company : Property
    {
        public Company(string name, int price,
            int position) : base(name, price, position, 100)
        {
            this.name = name;
            this.price = price;
            this.position = position;
            this.rentCost = 100;
        }

        public override string ToString()
        {
            return "[Company] " + base.ToString();
        }
    }
}