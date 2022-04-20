using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenReflector : Token
    {
        protected override string AllowedChars => "()";
        
        private char _reflectorChar;
        
        public TokenReflector(char c) : base(c)
        {
            
        }
        
        
        protected override List<Dot> Action(Dot dot)
        {
            
            
            
            return new();
        }
        
    }
}