using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenValue : Token
    {
        protected override string AllowedChars => "#";
        
        public TokenValue(char c) : base(c)
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