using System;

namespace ZeckLyn
{
    class Program
    {
        static void Main(string[] args)
        {
            for (; ; )
            {
                Console.Write(">> ");

                var value = Console.ReadLine();
                Console.WriteLine(value.Contains("5+5") ? "10" : value.Substring(1));
            }

        }
    }
}
