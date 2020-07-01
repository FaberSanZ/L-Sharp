using System;
using System.Collections.Generic;

namespace ZeckLyn
{
    public class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;

        public Parser(string text)
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>();
            Lexer lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NexToken();

                if (token.Kind != SyntaxKind.WhitespaceToken && token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EndOfFileToken);


            _tokens = tokens.ToArray();
        }


        public SyntaxToken Current => Peek(0);


        internal SyntaxToken Peek(int offset)
        {
            int index = _position + offset;

            if (index >= _tokens.Length)
            {
                return _tokens[_position - 1];
            }

            return _tokens[_position];
        }


        public SyntaxToken NextToken()
        {
            SyntaxToken current = Current;
            _position++;

            return current;
        }


        public SyntaxToken Match(SyntaxKind kind)
        {

            if (Current.Kind == kind )
            {
                NextToken();
            }

            return new SyntaxToken(kind, Current.Position, null, null);
        }

        public ExpressionSyntax Parse()
        {
            var left = ParsePrymaryExpression();

            while (Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrymaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }


            return left;
        }

        internal ExpressionSyntax ParsePrymaryExpression()
        {
            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}
