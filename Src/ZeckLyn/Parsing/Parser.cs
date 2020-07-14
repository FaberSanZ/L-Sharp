// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	Parser.cs
=============================================================================*/


using System.Collections.Generic;

namespace ZeckLyn
{
    public class Parser
    {
        private readonly SyntaxToken[] _tokens;

        private readonly List<string> _diagnostics = new List<string>();
        private int _position;

        public Parser(string text)
        {
            _position = 0;
            List<SyntaxToken> tokens = new List<SyntaxToken>();

            Lexer lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();

                if (token.Kind != SyntaxKind.WhitespaceToken && token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }
            } while (token.Kind != SyntaxKind.EndOfFileToken);

            _tokens = tokens.ToArray();
            //_diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Peek(int offset)
        {
            int index = _position + offset;
            if (index >= _tokens.Length)
            {
                return _tokens[_tokens.Length - 1];
            }

            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        internal SyntaxToken NextToken()
        {
            SyntaxToken current = Current;
            _position++;
            return current;
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
            {
                return NextToken();
            }

            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        public SyntaxTree Parse()
        {
            ExpressionSyntax expresion = ParseTerm();
            SyntaxToken endOfFileToken = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(_diagnostics, expresion, endOfFileToken);
        }

        private ExpressionSyntax ParseTerm()
        {
            ExpressionSyntax left = ParseFactor();

            while (Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken)
            {
                SyntaxToken operatorToken = NextToken();
                ExpressionSyntax right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()
        {
            ExpressionSyntax left = ParsePrimaryExpression();

            while (Current.Kind == SyntaxKind.StarToken || Current.Kind == SyntaxKind.SlashToken)
            {
                SyntaxToken operatorToken = NextToken();
                ExpressionSyntax right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                SyntaxToken left = NextToken();
                ExpressionSyntax expression = ParseExpression();
                SyntaxToken right = Match(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            SyntaxToken numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}
