using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenEmpty : Token
    {
        /**
         * <summary>Allowed char to be an empty token</summary>
         *
         * this is only a space.
         */
        protected override string AllowedChars => " ";

        /**
         * <summary>Create a new empty token.</summary>
         * 
         * <param name="c">the character that represent an empty token</param>
         */
        public TokenEmpty(char c) : base(c)
        {
        }

        /**
         * <summary>Create a new empty token (quick shortcut).</summary>
         */
        public TokenEmpty() : base(' ')
        {
        }

        /**
         * <summary>Destroy the dot that arrive in a empty case</summary>
         * 
         * <param name="dot">The dot that arrive in the empty Token</param>
         * 
         * <returns>An empty list of dot.</returns>
         */
        protected override List<Dot> Action(Dot dot) =>
            new();
    }
}