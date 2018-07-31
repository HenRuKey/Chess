using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    class Chessboard
    {
        #region Squares
        // TODO: Determine access modifier for chessboard two-dimensional array.
        private Piece[][] squares = new Piece[8][];

        // TODO: Document squares after deciding access modifier.
        public Piece[][] Squares
        {
            get { return squares; }
        }
        #endregion

        #region Constructor(s) and Object Initialization
        public Chessboard()
        {
            InitializePieces();
            // TODO: Determine if the constructor requires more functionality.
        }

        /// <summary>
        /// Creates chess pieces and places them at their starting positions on the board.
        /// </summary>
        void InitializePieces()
        {
            // TODO: Generate pieces and add them to the two-dimensional array.
        }
        #endregion

        /// <summary>
        /// Sets a piece's position to a specified coordinate.
        /// </summary>
        /// <param name="piece">The piece to move.</param>
        /// <param name="position">The coordinates of the piece's new, legal position.</param>
        void UpdatePosition(Piece piece, Tuple<int> position)
        {
            // Assume controller has found the move is legal.

            // Get the current position of the piece.

            // Remove the piece from the two-dimensional array.

            // Use the UpdatePosition method of the IMoveable interface to change the piece's position.

            // Check if the piece's claimed position is occupied.
                // If so, set the occupying piece to inactive using the RemoveFromPlay method.
            
            // Use the piece's position property to add it to the square two-dimensional array. 
        }

        /// <summary>
        /// Retrieves the piece located on a specified coordinate.
        /// </summary>
        /// <param name="position">The coordinates of the piece.</param>
        /// <returns>The coordinate's chess piece, null if the square is empty.</returns>
        Piece GetPiece(Tuple<int> position)
        {
            // TODO: Return the chess piece from the two-dimensional array.
            return null;
        }

    }
}
