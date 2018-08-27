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
            // Do we need to check if there's a piece currently occupying that position?
            pieces.Add(piece);
            board.PlacePiece(piece);
            if(piece.GetType() == typeof(King))
            {
                if(piece.Color == enums.Color.LIGHT)
                {
                    board.LightKing = (King) piece;

                }
                else
                {
                    board.DarkKing = (King)piece;
                }
            };
        }

        internal bool PerformMove(Tuple<int, int>[] tuple)
        {
            IMoveable moveable = (IMoveable)GetPieceAtCoord(tuple[0]);
            return (moveable != null && board.TryMove(moveable, tuple));



            //if (moveable != null && moveable.IsValidMove(board, tuple[1]))
            //{

            //    board.UpdatePosition(piece, tuple[1]);
            //    return true;
            //}
            //return false;

        }

        internal void SpecialMove(Tuple<int, int>[] tuple)
        {

        }

        public void DetectCheck()
        {
            foreach (Piece p in pieces)
            {
                IMoveable piece = (IMoveable)p;
                if (piece.IsChecking(board, p.Color == enums.Color.LIGHT ? board.DarkKing : board.LightKing))
                {
                    Console.WriteLine("Check!");
                };
            }
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
