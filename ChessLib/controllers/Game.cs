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
            Piece piece = GetPieceAtCoord(tuple[0]);
            IMoveable moveable = (IMoveable) piece;
            if (moveable != null)
            {
                if(board.TryMove(moveable, tuple))
                {
                    board.UpdatePosition(piece, tuple[1]);
                    return true;
                }
            }
            return false;



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

        public bool DetectCheck()
        {
            foreach (Piece p in pieces)
            {
                IMoveable piece = (IMoveable)p;
                King king = p.Color == enums.Color.LIGHT ? board.DarkKing : board.LightKing;
                if (piece.IsChecking(board, king))
                {
                    king.InCheck = true;
                    return true;
                }
                else
                {
                    king.InCheck = false;
                };
            }

            return false;
        }
        internal bool IsCheckmate()
        {
            bool Checkmate = true;
            if (board.LightKing.InCheck)
            {
                foreach (Piece p in pieces)
                {
                    if(p.Color == enums.Color.LIGHT)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                Tuple<int, int> oldPosition = p.Position;
                                Tuple<int, int> position = new Tuple<int, int>(i, j);
                                Piece opponentPiece = board.GetPiece(new Tuple<int, int>(i, j));
                                Tuple<int, int>[] coordinates = new Tuple<int, int>[] {p.Position, position };

                                if(board.TryMove((IMoveable)p, coordinates))
                                {

                                    board.UpdatePosition(p, position);
                                    Checkmate = DetectCheck();
                                    board.UpdatePosition(p, oldPosition);
                                    if (opponentPiece != null)
                                    {
                                        board.PlacePiece(opponentPiece);
                                    }
                                    if (!Checkmate)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (board.DarkKing.InCheck)
            {
                foreach (Piece p in pieces)
                {
                    if (p.Color == enums.Color.DARK)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; i < 8; i++)
                            {
                                Tuple<int, int> oldPosition = p.Position;
                                Tuple<int, int> position = new Tuple<int, int>(i, j);
                                Tuple<int, int>[] coordinates = new Tuple<int, int>[] { p.Position, position };
                                Piece opponentPiece = board.GetPiece(new Tuple<int, int>(i, j));

                                if (board.TryMove((IMoveable)p, coordinates))
                                {
                                    board.UpdatePosition(p, position);
                                    Checkmate = DetectCheck();
                                    board.UpdatePosition(p, oldPosition);
                                    if (opponentPiece != null)
                                    {
                                        board.PlacePiece(opponentPiece);
                                    }
                                    if (!Checkmate)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Checkmate;
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
