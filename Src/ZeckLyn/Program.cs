using System;
using System.Collections.Generic;
using System.IO;

namespace ZeckLyn
{

    class Program
    {

        private static void Main(string[] args)
        {
            string thisAssemblt = AppDomain.CurrentDomain.FriendlyName;
            Compiler compiler = new Compiler();

            if (args.Length > 1)
            {
                Console.WriteLine($"Useage: {thisAssemblt}");
            }
            else if (args.Length == 1)
            {
                compiler.RunFile(args[0]);
            }
            else
            {
                compiler.RunPrompt();
            }
        }

        
    }
}

