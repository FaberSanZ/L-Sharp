using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZeckLyn
{
    public class Compiler
    {
        public Compiler()
        {
            HadError = false;
        }

        public bool HadError { get; private set; }


        public void RunFile(string filePath)
        {
            string rawFile = File.ReadAllText(filePath);
            Run(rawFile);
        }

        public void RunPrompt()
        {
            for (; ; )
            {
                Console.Write(">> ");
                Run(Console.ReadLine());
                HadError = false;
            }

        }

        public void Run(string Source)
        {
            Scanner scanner = new Scanner(Source);
            List<Token> Tokens = scanner.ScanTokens();
            //Tokens.ForEach(token => Console.WriteLine(token));

            foreach (Token token in Tokens)
            {
                //if (token.TokenType == TokenType.EOF )
                //{
                //    val += "n/";
                //}
                //val += @"├──
                //            │
                //            └──";

                Console.WriteLine($"|__{(token)}");

            }

            //if (HadError)
            //{
            //    Console.WriteLine("CS Lox has died badly."); //throw new Exception("");
            //}
        }

        internal void Error(int line, string message)
        {
            Report(line, "", message);
        }

        private void Report(int line, string where, string message)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[line {line}] Error {where} : {message}");
            Console.ForegroundColor = currentForeground;
            HadError = true;
        }
    }
}
