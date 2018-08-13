using ChessLib.enums;
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
            Console.WriteLine("Quen");
            return true;
        }

        public void UpdatePosition(Tuple<int, int> position)
        {
            throw new NotImplementedException();
        }
    }
}
