using System;

namespace AsciiDot
{
    public class Dot : Point
    {
        /**
         * <summary>Number of Dots that as been instantiate.</summary>
         */
        public static int Count { get; private set; }

        /**
         * <summary>The unique identifier of the instance.</summary>
         */
        public int Id { get; }

        /**
         * <summary>The direction where the dot move.</summary>
         */
        public Direction Direction;

        /**
         * <summary>The memory of the dot.</summary>
         */
        public Memory Memory;

        /**
         * <summary>Getter and setter of <c>Memory.CurrentEnvironment</c> value.</summary>
         */
        public Environment CurrentEnvironment
        {
            get => Memory.CurrentEnvironment;
            set => Memory.CurrentEnvironment = value;
        }

        /**
         * <summary>Getter and setter of <c>Memory.Value<\c> value.</summary>
         */
        public double Value
        {
            get => Memory.Value;
            set => Memory.Value = value;
        }

        /**
         * <summary>
         * Constructor of Dot.
         * </summary>
         * 
         * <param name="x">The position xth row in <c>Matrix</c></param>
         * <param name="y">The position yth row in <c>Matrix</c></param>
         * <param name="direction">The direction indicates the direction of the next move</param>
         */
        public Dot(int x, int y, Direction direction)
            : base(0, 0) // TODO
        {
            x = this.X;
            y = this.Y;
            Direction = direction;
        }

        /**
         * <summary>
         * Constructor of Dot.
         * </summary>
         * 
         * <param name="point">The position in the <c>Matrix</c></param>
         * <param name="direction">The direction indicates the direction of the next move</param>
         */
        public Dot(Point point, Direction direction)
            : base(null) // TODO
        {
            point = new Point(X, Y);
            direction = this.Direction;
        }

        /**
         * <summary>
         * Move the dot forward.
         * </summary>
         */
        public void Step()
        {
            //move the dot forward
            Step(Direction);
        }

        /**
         * <summary>
         * Add a new token to the dot queue.
         * </summary>
         * <param name="token">The token to add in the queue.</param>
         */
        public void Enqueue(Token.Token token) => Memory.Enqueue(token);

        /**
         * <summary>
         * Try to flush the queue and apply queued actions.
         * </summary>
         */
        public void Flush() => Memory.Flush();
    }
}