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
        /// <returns>True if command was performed.</returns>
        public static bool Perform(string command)
        {
            string[] CommandSplit = command.Split(' ');
            int numOfInstructions = CommandSplit.Length;
            switch (numOfInstructions)
            {
                case 1: 
                    Piece piece = CreatePiece(CommandSplit[0]);
                    // TODO: Add created piece to the board at the specified position.
                    break;
                case 2:
                    // TODO: Move piece at given coordinates to the new coordinates.
                    break;
                case 4:
                    // TODO: Move multiple pieces to the specified coordinates.
                    break;
            }
            // TODO: Find a better way to validate performance completion.
            return numOfInstructions == 1 || numOfInstructions == 2 || numOfInstructions == 4;
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
        private static Piece CreatePiece(string instruction)
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
