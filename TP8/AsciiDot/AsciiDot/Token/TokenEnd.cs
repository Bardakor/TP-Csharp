using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenEnd : Token
    {
        protected override string AllowedChars => "&";
        
        public TokenEnd(char c) : base(c)
        {
            
        }
        
        protected override List<Dot> Action(Dot dot)
        {
            return new();
        }
        
    }
}