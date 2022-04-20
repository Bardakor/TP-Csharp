using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenNumber : TokenChar
    {
        protected override string AllowedChars => "1234567890";
        
        public TokenNumber(char c) : base(c)
        {
            
        }
        
        protected override bool Update(Dot dot)
        {
            // TODO: do something with the dot
            return true;
        }
        
        protected override List<Dot> Action(Dot dot)
        {
            // TODO: do something with the dot
            return new();
        }
        
    }
}