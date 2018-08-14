using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.Exceptions
{
    class PieceNotFoundException : Exception
    {
        public PieceNotFoundException()
        {

        }

        public PieceNotFoundException(string comm)
            : base(String.Format("Piece Not Found At: {0}", comm))
        {

        }
    }
}
