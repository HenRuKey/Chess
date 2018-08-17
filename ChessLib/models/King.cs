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
        public King(Color color) : base(color) { }

        // TODO: Implement IMoveable interface in King.cs
        // TODO: Implement ICastleable interface in King.cs


        public bool HasMoved => throw new NotImplementedException();

        public bool IsChecking(Chessboard board)
        {
            throw new NotImplementedException();
        }

        public bool IsValidMove(Chessboard board, Tuple<int, int> position)
        {
            
            return true;
        }

        public void UpdatePosition(Tuple<int, int> position)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "K";
        }

    }
}
