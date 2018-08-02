using ChessLib.enums;
using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.controllers
{

    public static class Commander
    {
      public static string Perform(string command)
        {
            string[] CommandSplit = command.Split(' ');
            switch (CommandSplit.Length)
            {
                case 1:
                    Piece piece = GetPiece(CommandSplit[0]);
                    return $"Places a {piece.GetType().Name} on space {command[2]}{command[3]}";
                case 2:
                    return $"Moves a peice from {CommandSplit[0]} to {CommandSplit[1]}";
                case 4:
                    return $"Moves a peice from {CommandSplit[0]} to {CommandSplit[1]} and another piece from {CommandSplit[2]} to {CommandSplit[3]}";

            }
            return "Fail";
        }

       

        private static Piece GetPiece(string instruction)
        {
            Piece piece = null;
            Color color;
            color = (instruction[1] == 'l') ? Color.LIGHT : Color.DARK;
            switch (instruction[0])
            {
                case 'K':
                    piece = new King(color);
                    break;
                case 'Q':
                    piece = new Queen(color);
                    break;
                case 'B':
                    piece = new Bishop(color);
                    break;
                case 'N':
                    piece = new Knight(color);
                    break;
                case 'R':
                    piece = new Rook(color);
                    break;
                case 'P':
                    piece = new Pawn(color);
                    break;
            }
            return piece;
        }
    }
}
