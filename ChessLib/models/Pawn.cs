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
    public class Pawn : Piece, IMoveable
    {
        public Pawn(Color color) : base(color) { }

        public event PawnPromotionHandler OnPromotion;

        // Implement IMoveable interface in Pawn.cs
        public bool IsChecking(Chessboard board, King king)
        {
            return IsValidMove(board, king.Position);
        }

        public void ValidatePromotion()
        {
            if ((this.Color == Color.LIGHT && this.Position.Item1 == 7) || 
                (this.Color == Color.DARK && this.Position.Item1 == 0))
            {
                OnPromotion(this, new PawnPromotionArgs(this));
            }
        }

        public bool IsValidMove(Chessboard board, Tuple<int, int> position)
        {
            int limit;
            int YDiff = position.Item1 - this.Position.Item1;
            int XDiff = position.Item2 - this.Position.Item2;

            switch (this.Color)
            {
                case Color.LIGHT:
                    limit = 1;
                    if (this.Position.Item1 == 1)
                    {
                        limit = 2;
                    }
                    if (XDiff == 0 && (Math.Abs(YDiff) > 0 && Math.Abs(YDiff) <= limit) && board.GetPiece(position) == null)
                    {
                        return true;
                    }

                    if (this.Position.Item1 - position.Item1 == -1 && board.GetPiece(position) != null && board.GetPiece(position).Color != this.Color)
                    {
                        return true;
                    }
                    break;
                case Color.DARK:
                    limit = -1;
                    if (this.Position.Item1 == 6)
                    {
                        limit = -2;
                    }
                    if (XDiff == 0 && (YDiff < 0 && YDiff >= limit) && board.GetPiece(position) == null)
                    {
                        return true;
                    }

                    if (this.Position.Item1 - position.Item1 == 1 && board.GetPiece(position) != null && board.GetPiece(position).Color != this.Color)
                    {
                        return true;
                    }
                    break;
            }


            return false;
        }

        public override string ToString()
        {
            return "P";
        }
    }

    public class PawnPromotionArgs : EventArgs
    {
        public Pawn Pawn { get; private set; }

        public PawnPromotionArgs(Pawn pawn)
        {
            Pawn = pawn;
        }
    }
}
