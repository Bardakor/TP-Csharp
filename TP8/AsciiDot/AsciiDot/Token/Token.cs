using System.Collections.Generic;

namespace AsciiDot.Token
{
    public abstract class Token
    {
        /**
         * <summary>
         * The char that represent the token (in the board).
         * </summary>
         */
        public char Value { get; }

        /**
         * <summary>
         * String that contain all the possible char for this token.
         * </summary>
         */
        protected abstract string AllowedChars { get; }

        /**
         * <summary>
         * Number of point that is contain in the token. <see cref="TokenOperator"/>
         * </summary>
         */
        public virtual int PointInside => 0;

        /**
         * <summary>Create a new token from the char.</summary>
         * 
         * Check if the char is in the allowed chars.
         *
         * <param name="c">The letter that define the token.</param>
         * 
         * <exception cref="LexerError">The char <paramref name="c"/> is not a allowed.</exception>
         */
        protected Token(char c) =>
            Value = AllowedChars is null || !AllowedChars.Contains(c)
                ? throw new LexerError(c)
                : c;

        /**
         * <summary></summary>
         *
         * <param name="dot">The dot</param>
         */
        protected virtual bool Update(Dot dot)
        {
            if (dot.CurrentEnvironment is Environment.SingleQuote or Environment.DoubleQuote)
            {
                dot.Enqueue(this);
                return true;
            }

            dot.Flush();
            dot.CurrentEnvironment = Environment.Default;

            return false;
        }

        public List<Dot> Apply(Dot dot) =>
            Update(dot)
                ? new List<Dot> {dot}
                : Action(dot);


        /**
         * <summary>Apply the token action.</summary>
         *
         * <param name="dot">The to to apply the token action.</param>
         *
         * <returns>The list of updated dots (this is useful to remove and add point into the board)</returns>
         */
        protected virtual List<Dot> Action(Dot dot) =>
            // by default do nothing (juste return the dot)
            new() {dot};
    }
}