using System;

namespace AsciiDot
{
    /**
     * <summary>
     * Represent a point in a 2D space.
     * </summary>
     */
    public class Point
    {
        /**
         * <summary>
         * Create a new point at the given coordinates.
         * </summary>
         *
         * <param name="x">The position xth row in <c>Matrix</c></param>
         * <param name="y">The position yth row in <c>Matrix</c></param>
         */


        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /**
         * <summary>
         * Duplication constructor.
         * </summary>
         *
         * Create a new point with the same coordinate as the given one.
         *
         * <param name="point">The point to duplicate</param>
         */
        public Point(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        /**
         * The index of the column (or x-axis).
         */
        public int X { get; private set; }

        /**
         * The index of the row (or y-axis).
         */
        public int Y { get; private set; }


        /**
         * <summary>
         * Make a copy of the point.
         * </summary>
         * 
         * <returns>A new point at the same coordinates</returns>
         */
        public Point Clone()
        {
            return new Point(this);
        }

        /**
         * <summary>
         * Move the dot by changing his coordinates X and Y
         * according to <paramref name="direction"/>.
         * </summary>
         *
         * <param name="direction">The direction to move the point</param>
         * 
         * <returns>The point itself.</returns>
         */
        protected Point Step(Direction direction)
        {
            X =+ DirUtils.DeltaX(direction);
            Y =+ DirUtils.DeltaY(direction);
            return this;
        }

        /**
         * <summary>
         * Get a new point that has moved to the given direction.
         * </summary>
         *
         * <param name="direction">The direction to move the point.</param>
         * 
         * <returns>
         * A new point that results of the move into the <paramref name="direction"/> direction.
         * </returns>
         */
        public Point MoveTo(Direction direction)
        {
            return this.Clone().Step(direction);
        }

        /**
         * <summary>
         * Check if 2 points are in the same position.
         * </summary>
         * 
         * This function is called when you use the <c>==</c> operator.
         *
         * <example>
         * <code>
         *     point1 == point2;
         * </code>
         * </example>
         *
         * <param name="obj">The object to test.</param>
         */
        public override bool Equals(object obj) =>
            obj is Point point && point.X == X && point.Y == Y;

        /**
         * <summary>
         * Get a string representation of the point. 
         * </summary>
         * 
         * This function is called when you try to convert a point into a string.
         * For example, when you use <c>Console.WriteLine</c>.
         *
         * <example>
         * <code>
         *     Console.WriteLine(new Point(3, 5));
         * </code>
         * 
         * this snippet give:
         *
         * <code>
         *     Point(3, 5)
         * </code>
         * </example>
         */
        public override string ToString() => $"Point({X}, {Y})";

        /**
         * <summary>
         * Get a hash of the given point.
         * </summary>
         * 
         * Mandatory to use HashSet and Dictionary. ;)
         * for example when you use <c>Console.WriteLine</c>.
         */
        public override int GetHashCode() =>
            HashCode.Combine(X, Y);
    }
}