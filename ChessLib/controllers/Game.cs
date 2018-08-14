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
        private Chessboard board;
        private List<Piece> pieces;


        public Game()
        {
            board = new Chessboard();
        }


        public void PlacePiece(Piece piece)
        {
        
        }

        internal void PerformMove(Tuple<int, int>[] tuple)
        {
            Piece piece = GetPieceAtCoord(tuple[0]);
            IMoveable moveable = (IMoveable)piece;
            if (moveable.IsValidMove(board, tuple[1]))
            {
                piece.Position = tuple[1];
            }
            
        }

        internal void SpecialMove(Tuple<int, int>[] tuple)
        {

        }

        private Piece GetPieceAtCoord(Tuple<int, int> tuple)
        {
            foreach (Piece piece in pieces)
            {
                if(piece.Position == tuple)
                {
                    return piece;
                }
            }
            throw new PieceNotFoundException(tuple.ToString());
        }


    }
}


//Stream myStream = myAssembly.GetManifestResourceStream( "MyNamespace.SubFolder.MyImage.bmp" );
