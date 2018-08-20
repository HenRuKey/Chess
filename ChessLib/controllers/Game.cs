using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.interfaces;
using ChessLib.Exceptions;


namespace ChessLib.controllers
{
    public class Game
    {
        // TODO: Find alternate ways of implementing without exposing chessboard directly.
        private Chessboard board;
        public Chessboard ChessBoard
        {
            get { return board; }
        }

        private List<Piece> pieces;

        public Game()
        {
            board = new Chessboard();
            pieces = new List<Piece>();
        }


        public void PlacePiece(Piece piece)
        {
            // Do we need to track pieces on board?
            pieces.Add(piece);
            board.PlacePiece(piece);
        }

        internal bool PerformMove(Tuple<int, int>[] tuple)
        {
            Piece piece = GetPieceAtCoord(tuple[0]);
            IMoveable moveable = (IMoveable)piece;
            if (moveable != null && moveable.IsValidMove(board, tuple[1]))
            {
                if (piece.GetType() == typeof(King))
                {
                    //Console.Beep();
                }
                board.UpdatePosition(piece, tuple[1]);
                return true;
            }
            return false;
            
        }

        internal void SpecialMove(Tuple<int, int>[] tuple)
        {

        }

        public Piece GetPieceAtCoord(Tuple<int, int> tuple)
        {
            foreach (Piece piece in pieces)
            {
                if(piece.Position.Equals(tuple))
                {
                    return piece;
                }
            }
            return null;
        }


    }
}


//Stream myStream = myAssembly.GetManifestResourceStream( "MyNamespace.SubFolder.MyImage.bmp" );
