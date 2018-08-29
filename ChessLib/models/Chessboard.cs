using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.interfaces;
using static ChessLib.models.Delegates;

namespace ChessLib.models
{
    public class Chessboard
    {
        #region Events

        public event MovementHandler OnPieceMoved;
        public event PlacementHandler OnPiecePlaced;

        #endregion

        #region Board
        // TODO: Determine access modifier for chessboard two-dimensional array.
        private Piece[,] board = new Piece[8,8];

        // TODO: Document board after deciding access modifier.

        public Piece[,] Board { get => board; set => board = value; }
        #endregion

        #region Constructor(s) and Object Initialization
        public Chessboard()
        {
            // TODO: Determine if the constructor requires more functionality.
        }

        /// <summary>
        /// Creates chess pieces and places them at their starting positions on the board.
        /// </summary>
        public void InitializePieces()
        {
            // TODO: Generate pieces and add them to the two-dimensional array.
        }

        #endregion

        public King LightKing { get; set; }
        public King DarkKing { get; set; }
        public List<Piece> Pieces
        {
            get
            {
                List<Piece> pieces = new List<Piece>();
                foreach (Piece piece in board)
                {
                    if (piece != null) { pieces.Add(piece); }
                }
                return pieces;
            }
        }


        public Tuple<int, int>[] GetAllPossibleMoves(Piece piece)
        {
            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Tuple<int, int> position = new Tuple<int, int>(i, j);
                    Tuple<int, int>[] coordinates = new Tuple<int, int>[] { piece.Position, position };
                    if (TryMove((IMoveable) piece, coordinates))
                    {
                        possibleMoves.Add(position);
                    }
                }
            }
            return possibleMoves.ToArray();
        }

        /// <summary>
        /// Sets a piece's position to a specified coordinate.
        /// </summary>
        /// <param name="piece">The piece to move.</param>
        /// <param name="position">The coordinates of the piece's new, legal position.</param>
        public void UpdatePosition(Piece piece, Tuple<int, int> position)
        {
            // Record the piece's current position for supplying MovementHandler args.
            Tuple<int, int> oldPosition = piece.Position;


            board[piece.Position.Item1, piece.Position.Item2] = null;
            piece.Position = position;
            board[piece.Position.Item1, piece.Position.Item2] = piece;
            MovementArgs args = new MovementArgs(oldPosition, piece);
            OnPieceMoved?.Invoke(this, args);
        }

        public void PlacePiece(Piece piece)
        {
            Board[piece.Position.Item1, piece.Position.Item2] = piece;
            PlacementArgs args = new PlacementArgs(piece);
            OnPiecePlaced?.Invoke(this, args);
        }

        /// <summary>
        /// Retrieves the piece located on a specified coordinate.
        /// </summary>
        /// <param name="position">The coordinates of the piece.</param>
        /// <returns>The coordinate's chess piece, null if the square is empty.</returns>
        public Piece GetPiece(Tuple<int, int> position)
        {
            return board[position.Item1, position.Item2];
        }

        internal bool TryMove(IMoveable moveable, Tuple<int, int>[] tuple)
        {
            Piece movedPiece = GetPiece(tuple[0]);
            Tuple<int, int> oldPosition = movedPiece.Position;
            Piece OccupyingPiece = GetPiece(tuple[1]);
            if (moveable.IsValidMove(this, tuple[1]))
            {
                UpdatePosition(movedPiece, tuple[1]);
                foreach (Piece piece in Pieces)
                {
                    Piece opponentPiece = piece;
                    if (opponentPiece != null && opponentPiece.Color != movedPiece.Color)
                    {
                        IMoveable m = (IMoveable)opponentPiece;
                        if (m.IsChecking(this, movedPiece.Color == enums.Color.LIGHT ? LightKing : DarkKing))
                        {
                            UpdatePosition(movedPiece, oldPosition);
                            if (OccupyingPiece != null)
                            {
                                PlacePiece(OccupyingPiece);
                            }
                            return false;
                        }
                    }
                }
                UpdatePosition(movedPiece, oldPosition);
                return true;
            }

            return false;
        }

    }

     

    public class MovementArgs : EventArgs
    {
        private Tuple<int,int> oldPosition;
        public Tuple<int,int> OldPosition
        {
            get { return oldPosition; }
        }

        private Piece pieceMoved;
        public Piece PieceMoved
        {
            get { return pieceMoved; }
        }

        public MovementArgs(Tuple<int, int> oldPosition, Piece pieceMoved)
        {
            this.oldPosition = oldPosition;
            this.pieceMoved = pieceMoved;
        }
    }

    public class PlacementArgs : EventArgs
    {
        private Piece piecePlaced;
        public Piece PiecePlaced
        {
            get { return piecePlaced; }
        }

        public PlacementArgs(Piece piecePlaced)
        {
            this.piecePlaced = piecePlaced;
        }
    }

}
