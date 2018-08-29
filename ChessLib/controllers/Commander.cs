using ChessLib.enums;
using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.Exceptions;
using ChessLib.interfaces;

namespace ChessLib.controllers
{

    public static class Commander
    {
        private const int UNICODE_LETTER_OFFSET = 97;

        /// <summary>
        /// Returns a piece object based on formatted instructions to create it.
        /// </summary>
        /// <param name="instruction">The instructions which include the type of piece, and its color.</param>
        /// <returns>The piece created from the supplied instructions.</returns>
        public static Piece CreatePiece(string instruction)
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
            piece.Position = new Tuple<int, int>(instruction[3] - '1', CharToInt(instruction[2]));

            return piece;
        }

        public static Tuple<int, int>[] GetSpecialCoordinates(string[] CommandSplit)
        {
            int y1 = CharToInt(CommandSplit[0][0]);
            int x1 = CommandSplit[0][1] - '1';
            int NewY1 = CharToInt(CommandSplit[1][0]);
            int NewX1 = CommandSplit[1][1] - '1';

            int y2 = CharToInt(CommandSplit[0][0]);
            int x2 = CommandSplit[0][1] - '1';
            int NewY2 = CharToInt(CommandSplit[1][0]);
            int NewX2 = CommandSplit[1][1] - '1';

            Tuple<int, int> OldPos1 = new Tuple<int, int>(x1, y1);
            Tuple<int, int> OldPos2 = new Tuple<int, int>(x2, y2);
            Tuple<int, int> NewPos1 = new Tuple<int, int>(NewX1, NewY1);
            Tuple<int, int> NewPos2 = new Tuple<int, int>(NewX2, NewY2);


            return new Tuple<int, int>[]
            {
                OldPos1,
                NewPos1,
                OldPos2,
                NewPos2
            };
        }

        /// <summary>
        /// Translates coordinates from a string to an integer tuple.
        /// </summary>
        /// <param name="CommandSplit">A string array containing chess coordinates.</param>
        /// <returns>The coordinates as an integer tuple.</returns>
        public static Tuple<int, int>[] GetCoordinates(string[] CommandSplit)
        {
            int y = CharToInt(CommandSplit[0][0]);
            int x = CommandSplit[0][1] - '1';
            int NewY = CharToInt(CommandSplit[1][0]);
            int NewX = CommandSplit[1][1] - '1';

            Tuple<int, int> OldPos = new Tuple<int, int>(x, y);
            Tuple<int, int> NewPos = new Tuple<int, int>(NewX, NewY);

            return new Tuple<int, int>[]
            {
                OldPos,
                NewPos
            };
        }

        private static int CharToInt(char v)
        {
            return v - UNICODE_LETTER_OFFSET;
        }

        /// <summary>
        /// Converts an integer to a cooresponding letter (A=1, B=2, etc.).
        /// </summary>
        /// <param name="num">An integer to convert to char.</param>
        /// <returns>The cooresponding char letter of the passed integer.</returns>
        public static char NumberToLetter(int num)
        {
            return (char)(65 + (num - 1));
        }
    }
}
