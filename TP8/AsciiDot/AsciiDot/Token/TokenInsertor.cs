using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenInsertor : Token
    {
        protected override string AllowedChars => "><^v";
        
        private char _insertedChar;
        
        public TokenInsertor(char c) : base(c)
        {
            
        }
        
        
        protected override List<Dot> Action(Dot dot)
        {
            
            
            return new();
        }
        
    }
}