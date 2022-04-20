namespace Abacus.Token
{
    //enum of functions
    public enum Functions
    {
        Sqrt,
        Max,
        Min,
        Facto,
        IsPrime,
        Fibo,
        Gcd,
    }
    
    public class TokenFunction : Token
    {
        public Functions Type;
        public int _numberOfArguments;

        public TokenFunction(Functions type)
        {
            this.Type = type;
            if (type == Functions.Facto || type == Functions.Fibo || type  == Functions.Sqrt || type == Functions.IsPrime)
            {
                _numberOfArguments = 1;
            }
            else
            {
                _numberOfArguments = 2;
            }
        }
        
    }
}