using ChessLib.enums;
using ChessLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    public class Pawn : Piece, IMoveable
    {
        public bool FirstMove { get; set; }

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
            int limit = 1;
            if (this.FirstMove)
            {
                limit = 2;
            }
            int YDiff = position.Item1 - this.Position.Item1;
            int XDiff = position.Item2 - this.Position.Item2;

            if(XDiff == 0 && (YDiff > 0 && YDiff <= limit) && board.GetPiece(position) == null)
            {
                return true;
            }

            if(Math.Abs(this.Position.Item1 - position.Item1) == 1 && board.GetPiece(position) != null && board.GetPiece(position).Color != this.Color)
            {
                return true;
            }

            return false;
        }

        public void UpdatePosition(Tuple<int, int> position)
        {
            
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
