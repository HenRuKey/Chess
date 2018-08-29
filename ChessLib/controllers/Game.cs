﻿using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.interfaces;
using ChessLib.Exceptions;
using static ChessLib.models.Delegates;
using ChessLib.enums;

namespace ChessLib.controllers
{
    public class Game
    {

        public event MovementFailureHandler OnMoveFailure;
        public event CheckMateHandler OnCheckMate;

        // TODO: Find alternate ways of implementing without exposing chessboard directly.
        private Chessboard board;
        public Chessboard ChessBoard
        {
            get { return board; }
        }

        public Game()
        {
            board = new Chessboard();
        }


        public void PlacePiece(Piece piece)
        {
            // Do we need to check if there's a piece currently occupying that position?
            board.PlacePiece(piece);
            if (piece.GetType() == typeof(King))
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
                    OnMoveFailure(this, new MovementFailureArgs(MoveFailureReason.ILLEGAL_MOVE, piece, tuple[0]));
                }
            }
            else
            {
                OnMoveFailure(this, new MovementFailureArgs(MoveFailureReason.NO_PIECE_TO_MOVE, piece, tuple[0]));
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
                        if (king.InCheck)
                        {
                            board.UpdatePosition(piece, oldPosition);
                            if (occupyingPiece != null) { board.PlacePiece(occupyingPiece); };
                            return;
                        }
                        else
                        {
                            board.UpdatePosition(piece, oldPosition);
                            if (occupyingPiece != null) { board.PlacePiece(occupyingPiece); };
                        }
                    }
                }
            }
            OnCheckMate(this, new CheckMateArgs(king));
        }

        internal bool IsCheckmate()
        {
            bool Checkmate = true;
            if (board.LightKing.InCheck)
            {
                foreach (Piece p in board.Pieces)
                {
                    if (p.Color == enums.Color.LIGHT)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                Tuple<int, int> oldPosition = p.Position;
                                Tuple<int, int> position = new Tuple<int, int>(i, j);
                                Piece opponentPiece = board.GetPiece(new Tuple<int, int>(i, j));
                                Tuple<int, int>[] coordinates = new Tuple<int, int>[] {p.Position, position };

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
            if (board.DarkKing.InCheck)
            {
                foreach (Piece p in board.Pieces)
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
        Color WinningColor
        {
            get { return King.Color == Color.LIGHT ? Color.DARK : Color.LIGHT; }
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


//Stream myStream = myAssembly.GetManifestResourceStream( "MyNamespace.SubFolder.MyImage.bmp" );
