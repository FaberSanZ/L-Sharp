using System;
using System.Collections.Generic;
using System.Text;

namespace ZeckLyn
{
    public enum TokenType
    {
        // Single-character tokens
        NumberToken,

        WhitespaceToken,

        [TokenText("+")]
        PlusToken,

        [TokenText("-")]
        MinusToken,

        StarToken,

        SlashToken,

        [TokenText("(")]
        OpenParenthesisToken,

        [TokenText(")")]
        CloseParenthesisToken,

        BadToken,

        EndOfFileToken,

        NumberExpression,

        BinaryExpression,

        ParenthesizedExpression
    }
}
