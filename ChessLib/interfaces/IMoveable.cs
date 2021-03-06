﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.models;

namespace ChessLib.interfaces
{
    public interface IMoveable
    {
        /// <summary>
        /// Determines if the piece is currently putting the opposing king in check.
        /// </summary>
        /// <param name="board">The board object being played on.</param>
        /// <returns>True if the king is in check.</returns>
        bool IsChecking(Chessboard board, King king);

        /// <summary>
        /// Determines if moving to a specified position from the piece's current position is a legal move.
        /// </summary>
        /// <param name="board">The board object being played on.</param>
        /// <param name="position">The desired position to move the piece to.</param>
        /// <returns>True if the desired move is legal.</returns>
        bool IsValidMove(Chessboard board, Tuple<int, int> position);

    }
}
