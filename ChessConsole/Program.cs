using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.controllers;
using ChessLib.models;
using ChessLib.interfaces;

namespace ChessConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ChessController chess = new ChessController(args[0]);
            chess.PlayFromFile();
        }
    }
}
