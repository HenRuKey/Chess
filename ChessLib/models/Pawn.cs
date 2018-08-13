﻿using ChessLib.enums;
using ChessLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    class Pawn : Piece, IMoveable
    {
        public Pawn(Color color) : base(color)
        {
            this.ImagePath = (color == Color.LIGHT) ? "C:/Users/Bryan/source/repos/Chess/ChessLib/resources/images" : "pack://application,,,/Images/darkpawn.png";
        }

        // Implement IMoveable interface in Pawn.cs
        public bool IsChecking(Chessboard board)
        {
            throw new NotImplementedException();
        }

        public bool IsValidMove(Chessboard board, Tuple<int, int> position)
        {
            Console.WriteLine("Pawn");
            return true;
        }

        public void UpdatePosition(Tuple<int, int> position)
        {
            throw new NotImplementedException();
        }
    }
}
