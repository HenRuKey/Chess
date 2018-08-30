using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.interfaces;
using ChessLib.Exceptions;
using static ChessLib.models.Delegates;
using ChessLib.enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChessLib.controllers
{
    public class Game : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public event MovementFailureHandler OnMoveFailure;
        public event CheckMateHandler OnCheckMate;

        // TODO: Find alternate ways of implementing without exposing chessboard directly.
        private Chessboard board;
        public Chessboard ChessBoard
        {
            get { return board; }
        }

        private bool whiteToMove;
        public bool WhiteToMove
        {
            get { return whiteToMove; }
            private set { whiteToMove = value; NotifyPropertyChanged(); }
        }

        public Game()
        {
            board = new Chessboard();
            WhiteToMove = true;
        }


        public void PlacePiece(Piece piece)
        {
            // Do we need to check if there's a piece currently occupying that position?
            board.PlacePiece(piece);
            if (piece is King)
            {
                King king = (King)piece;
                king.OnCheck += IsCheckmate;
                if (piece.Color == enums.Color.LIGHT)
                {
                    board.LightKing = (King)piece;

                }
                else
                {
                    board.DarkKing = (King)piece;
                }
            }
            else if (piece is Pawn)
            {
                Pawn pawn = (Pawn)piece;
                pawn.OnPromotion += Pawn_OnPromotion;
            }
        }

        private void Pawn_OnPromotion(object sender, PawnPromotionArgs e)
        {
            Queen queen = new Queen(e.Pawn.Color);
            queen.Position = e.Pawn.Position;
            board.PlacePiece(queen);
        }

        internal bool PerformMove(Tuple<int, int>[] tuple)
        {
            Piece piece = GetPieceAtCoord(tuple[0]);
            if ((piece?.Color == Color.LIGHT && !WhiteToMove) ||
                (piece?.Color == Color.DARK && WhiteToMove))
            {
                OnMoveFailure(this, new MovementFailureArgs(MoveFailureReason.PIECE_BELONGS_TO_ENEMY, piece, tuple[0]));
                return false;
            }
            IMoveable moveable = (IMoveable)piece;
            bool moveSucceeded = false;
            if (moveable != null)
            {
                moveSucceeded = board.TryMove(moveable, tuple);
                if (moveSucceeded)
                {
                    board.UpdatePosition(piece, tuple[1]);
                    if (piece is Pawn) { ((Pawn)piece).ValidatePromotion(); }
                }
                else
                {
                    OnMoveFailure(this, new MovementFailureArgs(MoveFailureReason.ILLEGAL_MOVE, piece, tuple[0]));
                }
            }
            else
            {
                OnMoveFailure(this, new MovementFailureArgs(MoveFailureReason.NO_PIECE_TO_MOVE, piece, tuple[0]));
                return false;
            }
            if (moveable != null && moveSucceeded)
            {
                WhiteToMove = !WhiteToMove;
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void SpecialMove(Tuple<int, int>[] tuple)
        {

        }

        public bool DetectCheck()
        {
            foreach (Piece p in board.Pieces)
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

        private void IsCheckmate(object sender, CheckedArgs e)
        {
            King king = e.King;
            foreach (Piece piece in board.Pieces)
            {
                if (piece.Color == e.Color)
                {
                    foreach (Tuple<int, int> move in board.GetAllPossibleMoves(piece))
                    {
                        Tuple<int, int> oldPosition = piece.Position;
                        Piece occupyingPiece = board.GetPiece(move);

                        board.UpdatePosition(piece, move);
                        DetectCheck();
                        if (king.InCheck)
                        {
                            board.UpdatePosition(piece, oldPosition);
                            if (occupyingPiece != null) { board.PlacePiece(occupyingPiece); };
                        }
                        else
                        {
                            board.UpdatePosition(piece, oldPosition);
                            if (occupyingPiece != null) { board.PlacePiece(occupyingPiece); };
                            return;
                        }
                    }
                }
            }
            OnCheckMate(this, new CheckMateArgs(king));
        }

        public Piece GetPieceAtCoord(Tuple<int, int> tuple)
        {
            foreach (Piece piece in board.Pieces)
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

    public class CheckMateArgs : EventArgs
    {
        public King King { get; private set; }
        public string WinningColor
        {
            get { return King.Color == Color.LIGHT ? "Black" : "White"; }
        }

        public CheckMateArgs(King king)
        {
            King = king;
        }
    }

    public class MovementFailureArgs : EventArgs
    {
        public MoveFailureReason Reason { get; private set; }
        public Piece FailedPiece { get; private set; }
        public Tuple<int,int> FailedCoords { get; private set; }

        public string Message
        {
            get
            {
                switch (Reason)
                {
                    case MoveFailureReason.ILLEGAL_MOVE:
                        return "The move attempted was illegal.";
                    case MoveFailureReason.NO_PIECE_TO_MOVE:
                        return $"There was no piece at coordinate {Commander.NumberToLetter(FailedCoords.Item2)}{FailedCoords.Item1 + 1}";
                    case MoveFailureReason.PIECE_BELONGS_TO_ENEMY:
                        return "You are unable to move your opponent's piece.";
                    default:
                        return "Move Failed.";
                }
            }    
        }

        public MovementFailureArgs(MoveFailureReason reason, Piece failedPiece, Tuple<int, int> failedCoords)
        {
            Reason = reason;
            FailedCoords = failedCoords;
            FailedPiece = failedPiece;
        }
    }
}