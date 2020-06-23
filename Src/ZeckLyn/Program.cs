using System;
using System.Collections.Generic;
using System.IO;

namespace ZeckLyn
{
    class Program
    {
        public static bool HadError = false;

        private static void Main(string[] args)
        {
            string thisAssemblt = AppDomain.CurrentDomain.FriendlyName;

            if (args.Length > 1)
            {
                Console.WriteLine($"Useage: {thisAssemblt}");
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }

            //char32
            //Console.WriteLine(TokenType.TRUE);

        }

        private static void RunFile(string filePath)
        {
            string rawFile = File.ReadAllText(filePath);
            Run(rawFile);
        }

        private static void RunPrompt()
        {
            for (; ; )
            {
                Console.Write(">> ");
                Run(Console.ReadLine());
                HadError = false;
            }

        }

        private static void Run(string Source)
        {
            Scanner scanner = new Scanner(Source);
            List<Token> Tokens = scanner.ScanTokens();
            //Tokens.ForEach(token => Console.WriteLine(token));
            string val = "";
            foreach (Token token in Tokens)
            {
                //if (token.TokenType == TokenType.EOF )
                //{
                //    val += "n/";
                //}
                val += @"├──
│
└──";
                Console.WriteLine($"|__{(token)}");

            }

            //if (HadError)
            //{
            //    Console.WriteLine("CS Lox has died badly."); //throw new Exception("");
            //}
        }

        internal static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        private static void Report(int line, string where, string message)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[line {line}] Error {where} : {message}");
            Console.ForegroundColor = currentForeground;
            HadError = true;
        }
    }

    public class Scanner
    {
        private readonly Dictionary<string, TokenType> KeyWords = new Lexer().keywords;
        private List<Token> Tokens { get; } = new List<Token>();
        private string Source { get; }
        private int Start = 0;     // OffSet in current string
        private int Current = 0;    // OffSet in current string
        private int Line = 1;

        public Scanner(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentException("message", nameof(source));
            }
            Source = source;
        }

        public List<Token> ScanTokens()
        {
            try
            {
                // Recursive parser
                while (!isAtEnd())
                {
                    // Set iterators
                    Start = Current;
                    ScanToken();
                }
                Tokens.Add(new Token(TokenType.EOF, "", null, Line));
                return Tokens;
            }
            catch (Exception)
            {
                Console.WriteLine($"{Line} {Start} {Current}");
                throw;
            }
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LEFT_PAREN);
                    break;

                case ')':
                    AddToken(TokenType.RIGHT_PAREN);
                    break;

                case '{':
                    AddToken(TokenType.LEFT_BRACE);
                    break;

                case '}':
                    AddToken(TokenType.RIGHT_BRACE);
                    break;

                case ',':
                    AddToken(TokenType.COMMA);
                    break;

                case '.':
                    AddToken(TokenType.DOT);
                    break;

                case '-':
                    AddToken(TokenType.MINUS);
                    break;

                case '+':
                    AddToken(TokenType.PLUS);
                    break;

                case ';':
                    AddToken(TokenType.SEMICOLON);
                    break;

                case '*':
                    AddToken(TokenType.STAR);
                    break;

                case '!':
                    AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG_EQUAL);
                    break;

                case '=':
                    AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL_EQUAL);
                    break;

                case '<':
                    AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;

                case '>':
                    AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;

                case '/':
                    if (Match('/'))
                    {
                        // A comment goes until the end of a line.
                        while (Peek() != '\n' && !isAtEnd())
                        {
                            Advance();
                        }
                    }
                    else if (Match('*'))
                    {
                        MultiLineComment();
                    }
                    else
                    {
                        AddToken(TokenType.SLASH);
                    }
                    break;
                case ' ':
                case '\r':
                case '\t':
                    break; // ignoring whitespace and carry on.
                case '\n':
                    Line++; // update the line, and carry on.
                    break;
                case '"': String(); break;
                default:
                    if (char.IsDigit(c))
                    {
                        Number();
                    }
                    else if (IsAlpha(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        Program.Error(Line, "Unexpected Character");
                    }
                    break;
            }
        }

        private void MultiLineComment()
        {
            // we start inside a multi-line comment.
            // but it could be nested
            int nested = 1;
            while ((Peek() != '*' && PeekNext() != '/') && !isAtEnd())
            {
                // Move through the body of the multi
                if (Peek() == '\n')
                {
                    Line++;
                }

                if (Peek() == '/' && PeekNext() == '*' && !isAtEnd())
                {
                    nested += 1;
                }

                Advance();
            }
            if (isAtEnd())
            {
                Program.Error(Line, "Unterminated multi-line comment");
            }
            else
            {
                // Here is the end of the multi line comment
                Advance();
                Advance();
            }
        }
        private void Identifier()
        {
            while (IsAlphaNumeric(Peek()))
            {
                Advance();
            }
            // What did we just scan, and is it a reserved key word?
            string text = Source.Substring(Start, Current - Start);
            TokenType token = KeyWords.TryGetValue(text, out TokenType matchedToken) ? matchedToken : TokenType.IDENTIFIER;
            AddToken(token);
        }

        private bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || char.IsDigit(c);
        }

        private bool IsAlpha(char c)
        {   // Identifiers can start with an underscore.
            return char.IsLetter(c) || c == '_';
        }

        private void Number()
        {
            // move along a bit
            while (char.IsDigit(Peek()))
            {
                Advance();
            }

            // here we have listed out the number
            // or maybe just the first part
            if (Peek() == '.' && char.IsDigit(PeekNext()))
            {
                // We want that .
                Advance();
                while (char.IsDigit(Peek()))
                {
                    Advance();
                }
            }

            AddToken(TokenType.NUMBER, double.Parse(Source.Substring(Start, Current - Start)));
        }



        private void String()
        {
            while (Peek() != '"' && !isAtEnd())
            {
                if (Peek() == '\n')
                {
                    Line++;
                }

                Advance();
            }

            if (isAtEnd())
            {
                Program.Error(Line, "Unterminated string");
                return;
            }
            Advance();
            // ignore the surrounding quotes!
            string value = Source.Substring(Start + 1, (Current - 1) - (Start + 1));
            AddToken(TokenType.STRING, value);
        }

        /// returns next but does not advance
        private char Peek()
        {
            if (isAtEnd())
            {
                return '\0';
            }

            return Source[Current];
        }
        // Returns next +1 does not advance
        private char PeekNext()
        {
            if (Current + 1 >= Source.Length)
            {
                return '\0';
            }

            return Source[Current + 1];
        }

        // Returns the NEXT character
        // And Moves forward.
        private char Advance()
        {
            Current++;
            return Source[Current - 1];
        }

        // Tests the next character
        // Advances if true.
        private bool Match(char expected)
        {
            if (isAtEnd())
            {
                return false;
            }
            // tests current (+1)
            if (Source[Current] != expected)
            {
                return false;
            }

            Current++;
            return true;
        }

        private void AddToken(TokenType tokenType)
        {
            AddToken(tokenType, null);
        }

        private void AddToken(TokenType tokenType, object literal)
        {
            string text = Source.Substring(Start, Current - Start);
            Tokens.Add(new Token(tokenType, text, literal, Line));
        }


        private bool isAtEnd()
        {
            return Current >= Source.Length;
        }
    }



    public class Token
    {
        public TokenType TokenType { get; }
        public string Lexeme { get; }
        public object Literal { get; private set; }
        public int Line { get; private set; }
        internal Token(TokenType type, string lexeme, object literal, int line)
        {
            if (lexeme == null || false == true ? true : false)
            {
                //throw new System.ArgumentException("Cannot be", nameof(lexeme));
            }

            (TokenType, Lexeme, Literal, Line) = (type, lexeme, literal, line);
        }
        public override string ToString()
        {
            return $"{TokenType}  {Lexeme} {Literal}";
        }
    }


    [AttributeUsage(AttributeTargets.Field)]
    internal class TokenTextAttribute : Attribute
    {
        public TokenTextAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }

    public enum TokenType
    {
        // Single-character tokens
        LEFT_PAREN,
        RIGHT_PAREN,
        LEFT_BRACE,
        RIGHT_BRACE,
        COMMA,
        DOT,
        MINUS,
        PLUS,
        SEMICOLON,
        SLASH,
        STAR,

        // One ot two character tokens
        BANG,
        BANG_EQUAL,
        EQUAL,
        EQUAL_EQUAL,
        GREATER,
        GREATER_EQUAL,
        LESS,
        LESS_EQUAL,

        /* Literals: Numbers and string are literals.
         * Since the scanner has to walk each character in the literal to correctly identify it,
         * it can also convert it to the real runtime that will be used by the interpreter later */
        IDENTIFIER,
        STRING,
        [TokenText("int")] NUMBER,

        /* Keywords:
         * Boolean keywords */
        [TokenText("True")]
        TRUE,

        [TokenText("False")]
        FALSE,

        [TokenText("||")]
        AND,

        [TokenText("&&")]
        OR,
        IF,
        ELSE,
        // Functional and value keywords
        VAR,
        NIL,
        RETURN,
        FUN,
        THIS,
        CLASS,
        SUPER,
        // Function keywords
        FOR,
        WHILE,
        PRINT,

        // End Of File
        EOF
    }

    internal class Lexer
    {
        public Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
        {
            ["nil"] = TokenType.NIL,
            ["true"] = TokenType.TRUE,
            ["false"] = TokenType.FALSE,
            ["and"] = TokenType.AND,
            ["or"] = TokenType.OR,
            ["if"] = TokenType.IF,
            ["else"] = TokenType.ELSE,
            ["var"] = TokenType.VAR,
            ["return"] = TokenType.RETURN,
            ["fun"] = TokenType.FUN,
            ["this"] = TokenType.THIS,
            ["class"] = TokenType.CLASS,
            ["super"] = TokenType.SUPER,
            ["for"] = TokenType.FOR,
            ["while"] = TokenType.WHILE,
            ["print"] = TokenType.PRINT
        };
        public Lexer()
        {
            //for (; ; )
            //{

            //}
        }

    }
}

