using ChessLib.enums;
using ChessLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    public class King : Piece, IMoveable, ICastleable
    {
        public King(Color color) : base(color) {
            InCheck = false;
        }

        // TODO: Implement IMoveable interface in King.cs
        // TODO: Implement ICastleable interface in King.cs


        public bool HasMoved => throw new NotImplementedException();

        public bool InCheck { get; set; }


        public bool IsChecking(Chessboard board, King king)
        {
            return IsValidMove(board, king.Position);
        }

        public bool IsValidMove(Chessboard board, Tuple<int, int> position)
        {
            int YDIff = Math.Abs(this.Position.Item1 - position.Item1);
            int XDiff = Math.Abs(this.Position.Item2 - position.Item2);

            if (XDiff > 1 || YDIff > 1)
            {
                return false;
            }
            if (board.GetPiece(position) != null && board.GetPiece(position).Color == this.Color)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "K";
        }

    }
}
