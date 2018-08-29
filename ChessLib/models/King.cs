using ChessLib.enums;
using ChessLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessLib.models.Delegates;

namespace ChessLib.models
{
    public class King : Piece, IMoveable, ICastleable
    {
        public King(Color color) : base(color)
        {
            InCheck = false;
        }

        // TODO: Implement ICastleable interface in King.cs

        public event Checked OnCheck;

        public bool HasMoved => throw new NotImplementedException();

        private bool inCheck;
        public bool InCheck
        {
            get { return inCheck; }
            set
            {
                inCheck = value;
                if (inCheck)
                {
                    OnCheck(this, new CheckedArgs(this));
                }
            }
        }


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

    public class CheckedArgs : EventArgs
    {
        public King King { get; private set; }
        public Color Color
        {
            get { return King.Color; }
        }

        public CheckedArgs(King kingInCheck)
        {
            King = kingInCheck;
        }
    }
}
