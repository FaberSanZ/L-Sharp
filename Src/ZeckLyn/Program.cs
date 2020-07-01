using System;
using System.Linq;

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

                Parser parse = new Parser(line);

                ExpressionSyntax ex = parse.Parse();

                PrettyPrint(ex);

            }



        }

        private static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            string marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            SyntaxNode lastChild = node.GetChildren().LastOrDefault();

            foreach (SyntaxNode child in node.GetChildren())
            {
                PrettyPrint(child, indent, child == lastChild);
            }
        }

    }
}

