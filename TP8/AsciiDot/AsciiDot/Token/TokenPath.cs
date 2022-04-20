using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenPath : Token
    {
        protected override string AllowedChars => "-|+";
        
        public TokenPath(char c) : base(c)
        {
        }
    }
}