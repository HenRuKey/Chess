using ChessLib.enums;
using ChessLib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    public class Bishop : Piece, IMoveable
    {
        public Bishop(Color color) : base(color)
        {

        }

        // Implement IMoveable interface in Bishop.cs

        public bool IsChecking(Chessboard board, King king)
        {
            return IsValidMove(board, king.Position);
        }


        public bool IsValidMove(Chessboard board, Tuple<int, int> position)
        {
            int YDiff = this.Position.Item1 - position.Item1;
            int XDiff = this.Position.Item2 - position.Item2;

            if(XDiff != 0 && YDiff != 0 && Math.Abs((float)XDiff / (float)YDiff) == 1)
            {

                if (board.GetPiece(position) != null && board.GetPiece(position).Color == this.Color)
                {
                    return false;
                }
                
                if (XDiff > 0)
                {
                    if (YDiff > 0)
                    {
                        for (int i = 1; i < Math.Abs(XDiff); i++)
                        {

                            if (board.GetPiece(new Tuple<int, int>(this.Position.Item1 - i, this.Position.Item2 - i)) != null)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < Math.Abs(XDiff); i++)
                        {

                            if (board.GetPiece(new Tuple<int, int>(this.Position.Item1 + i, this.Position.Item2 - i)) != null)
                            {
                                return false;
                            }
                        }
                    }


                } else
                {
                    if (YDiff > 0)
                    {
                        for (int i = 1; i < Math.Abs(XDiff); i++)
                        {

                            if (board.GetPiece(new Tuple<int, int>(this.Position.Item1 - i, this.Position.Item2 + i)) != null)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < Math.Abs(XDiff); i++)
                        {

                            if (board.GetPiece(new Tuple<int, int>(this.Position.Item1 + i, this.Position.Item2 + i)) != null)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
