using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenChar : Token
    {
        protected override string AllowedChars => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
        
        public TokenChar(char c) : base(c)
        {
        }
    }
}