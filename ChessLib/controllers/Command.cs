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
                    string piece = GetPiece(CommandSplit[0]);
                    return $"Places a {piece} on space {command[2]}{command[3]}";
                case 2:
                    return $"Moves a peice from {CommandSplit[0]} to {CommandSplit[1]}";
                case 4:
                    return $"Moves a peice from {CommandSplit[0]} to {CommandSplit[1]} and another piece from {CommandSplit[2]} to {CommandSplit[3]}";

            }
            return "Fail";
        }

       

        private static string GetPiece(string v)
        {
            string piece = "";
            string color;
            color = (v[1] == 'l') ? "light" : "dark";
            switch (v[0])
            {
                case 'K':
                    piece = "King";
                    break;
                case 'Q':
                    piece = "Queen";
                    break;
                case 'B':
                    piece = "Bishop";
                    break;
                case 'N':
                    piece = "Knight";
                    break;
                case 'R':
                    piece = "Queen";
                    break;
                case 'P':
                    piece = "Queen";
                    break;
               
            }



            return color + " " + piece;
        }
    }
}
