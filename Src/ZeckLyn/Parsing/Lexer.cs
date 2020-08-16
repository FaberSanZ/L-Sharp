// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	Lexer.cs
=============================================================================*/

namespace LSharp
{

    public class Lexer
    {
        private readonly string _text;
        private int _position;

        public Lexer(string text)
        {
            _text = text;
            _position = 0;
        }

        public char Current => _position >= _text.Length ? '\0' : _text[_position];

        public void Next()
        {
            _position++;
        }

        public SyntaxToken Lex()
        {
            if (_position >= _text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            }

            if (char.IsDigit(Current))
            {
                int start = _position;

                while (IsDigit(Current))
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
                    SyntaxKind plusToken = SyntaxKind.PlusToken;
                    return new SyntaxToken(plusToken, _position, plusToken.ToText(), plusToken.ToText());

                case '-':
                    Next();
                    SyntaxKind minusToken = SyntaxKind.MinusToken;
                    return new SyntaxToken(minusToken, _position, minusToken.ToText(), minusToken.ToText());


                case '*':
                    Next();
                    SyntaxKind starToken = SyntaxKind.StarToken;
                    return new SyntaxToken(starToken, _position, starToken.ToText(), starToken.ToText());


                case '/':
                    Next();
                    SyntaxKind slashToken = SyntaxKind.SlashToken;
                    return new SyntaxToken(slashToken, _position, slashToken.ToText(), null);


                case '(':
                    Next();
                    SyntaxKind openParenthesisToken = SyntaxKind.SlashToken;
                    return new SyntaxToken(openParenthesisToken, _position, openParenthesisToken.ToText(), null);


                case ')':
                    Next();
                    SyntaxKind closeParenthesisToken = SyntaxKind.SlashToken;
                    return new SyntaxToken(closeParenthesisToken, _position, closeParenthesisToken.ToText(), null);


                default:
                    Next();
                    return new SyntaxToken(SyntaxKind.BadToken, _position, _text.Substring(_position - 1, 1), null);

            }

        }


        internal bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
        }

        internal bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        internal bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }
    }
}
