using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.enums;

namespace ChessLib.models
{
    class Piece
    {
        #region Position
        private Tuple<int> position;

        /// <summary>
        /// The coordinates of the piece on the chessboard.
        /// </summary>
        public Tuple<int> Position
        {
            get { return position; }
            internal set { position = value; }
        }
        #endregion

        #region Color
        private Color color;

        /// <summary>
        /// The color of the piece (light or dark).
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        #endregion

        #region InPlay

        private bool isInPlay;

        /// <summary>
        /// Whether or not the piece has been killed and removed from play.
        /// </summary>
        public bool IsInPlay
        {
            get { return isInPlay; }
            private set { isInPlay = value; }
        }

        #endregion

        /// <summary>
        /// Removes piece from play, setting it inactive and removing its position.
        /// </summary>
        /// <remarks>
        /// This does not remove piece from the board object. That must be done independently.
        /// </remarks>
        void RemoveFromPlay()
        {
            IsInPlay = false;
            Position = null;
        }
    }
}
