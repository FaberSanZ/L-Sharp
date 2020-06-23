using System;
using System.Collections.Generic;
using System.Text;

namespace ZeckLyn
{
    public enum TokenType
    {
        // Single-character tokens
        LEFT_PAREN,
        RIGHT_PAREN,
        LEFT_BRACE,
        RIGHT_BRACE,
        COMMA,
        DOT,
        MINUS,
        PLUS,
        SEMICOLON,
        SLASH,
        STAR,

        // One ot two character tokens
        BANG,
        BANG_EQUAL,
        EQUAL,
        EQUAL_EQUAL,
        GREATER,
        GREATER_EQUAL,
        LESS,
        LESS_EQUAL,

        /* Literals: Numbers and string are literals.
         * Since the scanner has to walk each character in the literal to correctly identify it,
         * it can also convert it to the real runtime that will be used by the interpreter later */
        IDENTIFIER,
        STRING,
        [TokenText("int")] NUMBER,

        /* Keywords:
         * Boolean keywords */
        [TokenText("True")]
        TRUE,

        [TokenText("False")]
        FALSE,

        [TokenText("||")]
        AND,

        [TokenText("&&")]
        OR,
        IF,
        ELSE,
        // Functional and value keywords
        VAR,
        NIL,
        RETURN,
        FUN,
        THIS,
        CLASS,
        SUPER,
        // Function keywords
        FOR,
        WHILE,
        PRINT,

        // End Of File
        EOF
    }
}
