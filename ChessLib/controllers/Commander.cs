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

        /// <summary>
        /// Performs the instructions of a string command based on its format.
        /// </summary>
        /// <param name="command">The valid, formatted string command.</param>
        /// <returns>An English translation of what the method performed.</returns>
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

        /// <summary>
        /// Converts an integer to a cooresponding letter (A=1, B=2, etc.).
        /// </summary>
        /// <param name="num">An integer to convert to char.</param>
        /// <returns>The cooresponding char letter of the passed integer.</returns>
        private static char NumberToLetter(int num)
        {
            return (char)(65 + (num - 1));
        }

        /// <summary>
        /// Returns a piece object based on formatted instructions to create it.
        /// </summary>
        /// <param name="instruction">The instructions which include the type of piece, and its color.</param>
        /// <returns>The piece created from the supplied instructions.</returns>
        private static Piece GetPiece(string instruction)
        {
            Piece piece = null;
            Color color;
            color = (instruction[1] == 'l') ? Color.LIGHT : Color.DARK;
            switch (instruction[0].ToString().ToUpper()[0])
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
