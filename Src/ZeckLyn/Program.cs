using System;

namespace ZeckLyn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (; ; )
            {
                Console.Write(">>");
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    return;
                }
                Lexer lexer = new Lexer(line);
                while (true)
                {
                    SyntaxToken token = lexer.NexToken();
                    if (token.Kind == SyntaxKind.EndOfFileToken)
                    {
                        break;
                    }

                    Console.Write($"{token.Kind}: '{token.Text}'");
                    if (token.Value != null)
                    {
                        Console.Write($"Value: {token.Value}");
                    }

                    Console.WriteLine();
                }


            }



        }

    }
}

