using System;
using System.Collections.Generic;
using System.Text;

namespace ZeckLyn
{
    public class Token
    {
        public TokenType TokenType { get; }
        public string Lexeme { get; }
        public object Literal { get; private set; }
        public int Line { get; private set; }
        internal Token(TokenType type, string lexeme, object literal, int line)
        {
            //if (lexeme is null || line is '\0')
            //{
            //    throw new System.ArgumentException("Cannot be", nameof(lexeme));
            //}

            (TokenType, Lexeme, Literal, Line) = (type, lexeme, literal, line);
        }
        public override string ToString()
        {
            return $"{TokenType}  {Lexeme} {Literal}";
        }
    }
}
