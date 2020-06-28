// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	SyntaxKind.cs
=============================================================================*/


using System;
using System.Collections.Generic;
using System.Text;

namespace ZeckLyn
{
    public enum SyntaxKind
    {
        [TokenText("#")] NumberToken,

        [TokenText("+")] PlusToken,

        [TokenText("-")] MinusToken,

        [TokenText("/")] SlashToken,

        [TokenText("*")]  StarToken,

        [TokenText("(")] OpenParenthesisToken,

        [TokenText(")")] CloseParenthesisToken,

        WhitespaceToken,

        BadToken,

        EndOfFileToken,

        NumberExpression,

        BinaryExpression,

        ParenthesizedExpression
    }
}
