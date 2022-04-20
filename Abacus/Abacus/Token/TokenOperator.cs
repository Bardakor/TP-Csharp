namespace Abacus.Token
{ 
    //enum of simple binary operators
        public enum Operator
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Modulo,
            Power,
        }

        public class TokenOperator : Token
        {
            public Operator Type;
            public bool Precedence;
            public TokenAssociativity TokenAssociativity;

            public TokenOperator(Operator op)
            {
                this.Type = op;
            }
        }

}