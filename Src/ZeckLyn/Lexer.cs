using System;
using System.Collections.Generic;
using System.Text;

namespace ZeckLyn
{

    public class Lexer
    {
        public Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
        {
            ["nil"] = TokenType.NIL, // null
            ["true"] = TokenType.TRUE,
            ["false"] = TokenType.FALSE,
            ["and"] = TokenType.AND,
            ["or"] = TokenType.OR,
            ["if"] = TokenType.IF,
            ["else"] = TokenType.ELSE,
            ["var"] = TokenType.VAR,
            ["return"] = TokenType.RETURN,
            ["fun"] = TokenType.FUN,
            ["this"] = TokenType.THIS,
            ["class"] = TokenType.CLASS,
            ["super"] = TokenType.SUPER,
            ["for"] = TokenType.FOR,
            ["while"] = TokenType.WHILE,
            ["print"] = TokenType.PRINT
        };
        public Lexer()
        {
            //for (; ; )
            //{

            //}
        }

    }
}
