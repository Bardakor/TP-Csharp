using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Monopoly
{
    public abstract class Property : Cell
    {
        private Player owner;
        protected int price;
        protected int rentCost;
        protected string name;

        public Player Owner
        {
            get => this.owner;
            set { this.owner = value; }
        }

        public int Price
        {
            get => this.price;
        }

        public int RentCost
        {
            get => this.rentCost;
        }

        public string Name
        {
            get => this.name;
        }

        public Property(string name, int price, int position, int rentCost)
        {
            this.name = name;
            this.price = price;
            this.rentCost = rentCost;
            this.position = position;
        }

        /*
        public override string ToString()
        {
            return name + "; " + price + "; " + position + "; " + rentCost;
        }
        */
    }
}