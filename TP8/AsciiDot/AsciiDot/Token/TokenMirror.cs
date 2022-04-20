using System.Collections.Generic;

namespace AsciiDot.Token
{
    public class TokenMirror : Token
    {
        protected override string AllowedChars => "/\\";
        
        private char _mirror;
        
        public TokenMirror(char c) : base(c)
        {
            _mirror = c;
        }
        
        protected override List<Dot> Action(Dot dot)
        {
            if (_mirror == '/')
            {
                if (dot.Direction == Direction.Up || dot.Direction == Direction.Down)
                {
                    dot.Direction = DirUtils.Rotate(dot.Direction);
                }
                else
                {
                    dot.Direction = DirUtils.Invert(dot.Direction);
                    dot.Direction = DirUtils.Rotate(dot.Direction);
                }
            }
            else
            {
                if (dot.Direction == Direction.Left || dot.Direction == Direction.Right)
                {
                    dot.Direction = DirUtils.Rotate(dot.Direction);
                }
                else
                {
                    dot.Direction = DirUtils.Invert(dot.Direction);
                    dot.Direction = DirUtils.Rotate(dot.Direction);
                }
            }
            return new() {dot};
        }
    }
}


//       
//    \   
//        
