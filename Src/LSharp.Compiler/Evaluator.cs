// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	Evaluator.cs
====================================================================================*/

using LSharp.Compiler.Syntax;
using System;

namespace LSharp.Compiler
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
            if (node is LiteralExpressionSyntax n)
            {
                return (int)n.LiteralToken.Value;
            }

            if (node is BinaryExpressionSyntax b)
            {
                int left = EvaluateExpression(b.Left);
                int right = EvaluateExpression(b.Right);

                switch (b.OperatorToken.Kind)
                {
                    case SyntaxKind.PlusToken:
                        return left + right;

                    case SyntaxKind.MinusToken:
                        return left - right;

                    case SyntaxKind.StarToken:
                        return left * right;

                    case SyntaxKind.SlashToken:
                        return left / right;

                    default:
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
