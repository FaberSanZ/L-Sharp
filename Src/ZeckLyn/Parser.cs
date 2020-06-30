using System.Collections.Generic;

namespace ZeckLyn
{
    public class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private readonly int _position;

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


        public SyntaxToken Curren => Peek(0);


        internal SyntaxToken Peek(int offset)
        {
            int index = _position + offset;

            if (index >= _tokens.Length)
            {
                return _tokens[_position - 1];
            }

            return _tokens[_position];
        }
    }
}
