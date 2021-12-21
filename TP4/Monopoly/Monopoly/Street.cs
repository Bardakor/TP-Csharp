using System;

namespace Monopoly
{
    public class Street : Property
    {
        public enum Color
        {
            Red,
            Yellow,
            Blue,
            Brown,
        }

        public static Color ColorOfString(string str)
        {
            switch (str)
            {
                case "Red":
                    return Color.Red;
                case "Blue":
                    return Color.Blue;
                case "Brown":
                    return Color.Brown;
                case "Yellow":
                    return Color.Yellow;
                default:
                    throw new Exception("ColorOfString: '" + str + "'"
                                        + " is invalid color");
            }
        }

        private Color color;

        public Street(string name, int price,
            int position, int rentCost, Color color) : base(name, price, position, rentCost)
        {
            this.name = name;
            this.price = price;
            this.position = position;
            this.rentCost = rentCost;
            this.color = color;
        }

        /*
        public override string ToString()
        {
            return "[Street] {" + this.color + "} " + base.ToString();
        }
        */
    }
}