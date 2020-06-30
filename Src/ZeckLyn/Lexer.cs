// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	Lexer.cs
=============================================================================*/

namespace ZeckLyn
{

    public class Lexer
    {
        private readonly string _text;
        private int _position;

        public Lexer(string text)
        {
            _text = text;
        }

        public char Current => _position >= _text.Length ? '\0' : _text[_position];

        public void Next()
        {
            _position++;
        }

        public SyntaxToken NexToken()
        {
            if (_position >= _text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            }

            if (char.IsDigit(Current))
            {
                int start = _position;

                while (char.IsDigit(Current))
                {
                    Next();
                }

                int length = _position - start;
                string text = _text.Substring(start, length);
                int.TryParse(text, out int value);
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }
            if (char.IsWhiteSpace(Current))
            {
                int start = _position;

                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }

                int length = _position - start;
                string text = _text.Substring(start, length);
                //int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }


            switch (Current)
            {
                case '+':
                    Next();
                    return new SyntaxToken(SyntaxKind.PlusToken, _position, "+", null);

                case '-':
                    Next();
                    return new SyntaxToken(SyntaxKind.MinusToken, _position, "-", null);


                case '*':
                    Next();
                    return new SyntaxToken(SyntaxKind.StarToken, _position, "*", null);


                case '/':
                    Next();
                    return new SyntaxToken(SyntaxKind.SlashToken, _position, "/", null);


                case '(':
                    Next();
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position, "(", null);


                case ')':
                    Next();
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position, ")", null);


                default:
                    Next();
                    return new SyntaxToken(SyntaxKind.BadToken, _position, _text.Substring(_position - 1, 1), null);

            }

        }



    }
}
