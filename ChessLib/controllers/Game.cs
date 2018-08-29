using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.interfaces;
using ChessLib.Exceptions;
using static ChessLib.models.Delegates;

namespace ChessLib.controllers
{
    public class Game
    {

        public event MovementFailureHandler OnMoveFailure;

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
            if (piece.GetType() == typeof(King))
            {
                if (piece.Color == enums.Color.LIGHT)
                {
                    board.LightKing = (King)piece;

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
            IMoveable moveable = (IMoveable)piece;
            bool moveSucceeded = false;
            if (moveable != null)
            {
                moveSucceeded = board.TryMove(moveable, tuple);
                if (moveSucceeded)
                {
                    board.UpdatePosition(piece, tuple[1]);
                }
                else
                {
                    OnMoveFailure(this, new MovementFailureEventArgs(MoveFailureReason.ILLEGAL_MOVE, piece, tuple[0]));
                }
            }
            else
            {
                OnMoveFailure(this, new MovementFailureEventArgs(MoveFailureReason.NO_PIECE_TO_MOVE, piece, tuple[0]));
            }
            return (moveable != null && moveSucceeded);



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
            if (board.LightKing.InCheck)
            {
                foreach (Piece p in pieces)
                {
                    if (p.Color == enums.Color.LIGHT)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; i < 8; i++)
                            {
                                Tuple<int, int> position = new Tuple<int, int>(i, j);
                                Tuple<int, int>[] coordinates = new Tuple<int, int>[] { p.Position, position };

                                if (board.TryMove((IMoveable)p, coordinates))
                                {
                                    return false;
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
                                Tuple<int, int> position = new Tuple<int, int>(i, j);
                                Tuple<int, int>[] coordinates = new Tuple<int, int>[] { p.Position, position };

                                if (board.TryMove((IMoveable)p, coordinates))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }


        public Piece GetPieceAtCoord(Tuple<int, int> tuple)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.Position.Equals(tuple))
                {
                    return piece;
                }
            }
            return null;
        }

    }

    public enum MoveFailureReason
    {
        NO_PIECE_TO_MOVE,
        ILLEGAL_MOVE,
        PIECE_BELONGS_TO_ENEMY
    }

    public class MovementFailureEventArgs : EventArgs
    {
        private MoveFailureReason reason;
        public MoveFailureReason Reason
        {
            get { return reason; }
            private set { reason = value; }
        }

        private Piece failedPiece;
        public Piece MyProperty
        {
            get { return failedPiece; }
            private set { failedPiece = value; }
        }

        private Tuple<int,int> failedCoords;
        public Tuple<int,int> FailedCoords
        {
            get { return failedCoords; }
            private set { failedCoords = value; }
        }

        public string Message
        {
            get
            {
                switch (Reason)
                {
                    case MoveFailureReason.ILLEGAL_MOVE:
                        return "Move attempted was illegal.";
                    case MoveFailureReason.NO_PIECE_TO_MOVE:
                        return $"There was no piece at coordinate {Commander.NumberToLetter(failedCoords.Item2)}{failedCoords.Item1}";
                    case MoveFailureReason.PIECE_BELONGS_TO_ENEMY:
                        return "Unable to move opponent's piece.";
                    default:
                        return "Move Failed.";
                }
            }    
        }

        public MovementFailureEventArgs(MoveFailureReason reason, Piece failedPiece, Tuple<int, int> failedCoords)
        {
            Reason = reason;
        }
    }
}


//Stream myStream = myAssembly.GetManifestResourceStream( "MyNamespace.SubFolder.MyImage.bmp" );
