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
            FileReader reader = new FileReader(args[0]);

            List<string> comms = reader.Commands;

            List<Piece> pieces = new List<Piece>();

            foreach (var c in comms)
            {
                Commander.Perform(c, ref pieces);
            }

            foreach (Piece p in pieces)
            {
                IMoveable i = (IMoveable)p;
                i.IsValidMove(new Chessboard(), new Tuple<int, int>(1,9));
            }
        }
    }
}
