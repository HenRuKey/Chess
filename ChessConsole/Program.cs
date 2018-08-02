using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.controllers;

namespace ChessConsole
{
    class Program
    {
        static FileReader reader;
        static void Main(string[] args)
        {
            reader = new FileReader(args[0]);
            TestDriver();
        }

        private static void TestDriver()
        {
            List<string> commands = reader.Commands;

            foreach (string c in commands)
            {
                Console.WriteLine(Commander.Perform(c));
            }
        }
    }
}
