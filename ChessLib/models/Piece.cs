using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.enums;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChessLib.models
{
    public class Piece 
    {
        #region Position
        private Tuple<int, int> position;

        /// <summary>
        /// The coordinates of the piece on the chessboard.
        /// </summary>
        public Tuple<int, int> Position
        {
            get { return position; }
            internal set { position = value; }
        }
        #endregion

        #region Color

        /// <summary>
        /// The color of the piece (light or dark).
        /// </summary>
        public readonly enums.Color Color;

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
        /// Stores information about the piece, including its color, position, and in-play status.
        /// </summary>
        /// 
        public Piece(enums.Color color)
        {
            this.Color = color;
        }

        /// <summary>
        /// Removes piece from play, setting it inactive and removing its position.
        /// </summary>
        /// <remarks>
        /// This does not remove piece from the board object. That must be done independently.
        /// </remarks>
        public void RemoveFromPlay()
        {
            IsInPlay = false;
            Position = null;
        }
    }
}
