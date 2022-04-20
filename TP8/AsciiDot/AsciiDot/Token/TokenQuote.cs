using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenQuote : Token
    {
        protected override string AllowedChars => "'\"";
        
        public TokenQuote(char c) : base(c)
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