using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenDuplicate : Token
    {
        protected override string AllowedChars => "*";
        
        public TokenDuplicate(char c) : base(c)
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