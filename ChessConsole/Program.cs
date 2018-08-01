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
        static void Main(string[] args)
        {
            FileReader reader = new FileReader(args[0]);
            testDriver();
        }

        private static void testDriver()
        {
            
        }
    }
}
