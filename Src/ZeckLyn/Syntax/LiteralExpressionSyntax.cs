// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	Evaluator.cs
=============================================================================*/



using System;
using System.Collections.Generic;
using System.Text;

namespace ZeckLyn
{
    public sealed class LiteralExpressionSyntax : ExpressionSyntax
    {
        public LiteralExpressionSyntax(SyntaxToken literalToken)
        {
            LiteralToken = literalToken;
        }

        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public SyntaxToken LiteralToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}
