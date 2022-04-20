namespace AsciiDot.Token
{
    public class TokenStart : Token
    {
        public TokenStart(char c)
            : base(c)
        {
        }

        protected override string AllowedChars => ".•";
    }
}