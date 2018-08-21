using ChessLib.enums;
using ChessLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    public class Knight : Piece, IMoveable
    {
        public Knight(Color color) : base(color)
        {

        }

        // TODO: Implement IMoveable interface in Knight.cs

        public bool IsChecking(Chessboard board)
        {
            throw new NotImplementedException();
        }

        public bool IsValidMove(Chessboard board, Tuple<int, int> position)
        {
            int YDiff = Math.Abs(this.Position.Item1 - position.Item1);
            int XDiff = Math.Abs(this.Position.Item2 - position.Item2);

            if (board.GetPiece(position) != null && board.GetPiece(position).Color == this.Color)
            {
                return false;
            }

            if((XDiff == 2 && YDiff == 1) || (XDiff == 1 && YDiff == 2))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
