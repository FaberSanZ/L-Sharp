// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	SyntaxKind.cs
====================================================================================*/

namespace LSharp.Compiler.Syntax
{
    public enum SyntaxKind
    {
        [TokenText("#")]
        NumberToken,

        [TokenText("+")]
        PlusToken,

        [TokenText("-")]
        MinusToken,

        [TokenText("/")]
        SlashToken,

        [TokenText("*")]
        StarToken,

        [TokenText("(")]
        OpenParenthesisToken,

        [TokenText(")")]
        CloseParenthesisToken,

        WhitespaceToken,

        BadToken,

        EndOfFileToken,

        BinaryExpression,

        ParenthesizedExpression,

        LiteralExpression,

        NumberExpression,
    }
}
