// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	Evaluator.cs
=============================================================================*/

using System;

namespace ZeckLyn
{
    public class Evaluator
    {
        internal ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is NumberExpressionSyntax)
            {
                return (int)((NumberExpressionSyntax)node).NumberToken.Value;
            }

            if (node is BinaryExpressionSyntax b)
            {
                int left = EvaluateExpression(b.Left);
                int right = EvaluateExpression(b.Right);

                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                {
                    return left + right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                {
                    return left - right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
                {
                    return left * right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.SlashToken)
                {
                    return left / right;
                }
                else
                {
                    throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                }
            }

            if (node is ParenthesizedExpressionSyntax p)
            {
                return EvaluateExpression(p.Expression);
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
