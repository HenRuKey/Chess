﻿using ChessLib.enums;
using ChessLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    class Queen : Piece, IMoveable
    {

        public Queen(Color color) : base(color)
        {

        }

        // Implement IMoveable interface in Queen.cs

        public bool IsChecking(Chessboard board)
        {
            throw new NotImplementedException();
        }

        public bool IsValidMove(Chessboard board, Tuple<int, int> position)
        {

            int XDiff = position.Item1 - this.Position.Item1;
            int YDiff = position.Item2 - this.Position.Item2;

            if (Math.Abs(XDiff / YDiff) == 1)
            {
                for (int i = 0; i < XDiff; i++)
                {
                    if (board.GetPiece(new Tuple<int, int>(this.Position.Item1 + XDiff, this.Position.Item2 + YDiff)) != null)
                    {
                        return false;
                    }
                }

                if (board.GetPiece(position) != null && board.GetPiece(position).Color == this.Color)
                {
                    return false;
                }

                return true;
            }

            if (XDiff == 0 || YDiff == 0)
            {
                for (int i = 0; i < XDiff; i++)
                {
                    if (board.GetPiece(new Tuple<int, int>(this.Position.Item1 + i, this.Position.Item2)) != null)
                    {
                        return false;
                    }
                }
                for (int i = 0; i < YDiff; i++)
                {
                    if (board.GetPiece(new Tuple<int, int>(this.Position.Item1, this.Position.Item2 + i)) != null)
                    {
                        return false;
                    }
                }

                if (board.GetPiece(position) != null && board.GetPiece(position).Color == this.Color)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void UpdatePosition(Tuple<int, int> position)
        {
            throw new NotImplementedException();
        }
    }
}
