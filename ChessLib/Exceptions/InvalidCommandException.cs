using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.Exceptions
{
    class InvalidCommandException : Exception
    {
        public InvalidCommandException()
        {

        }

        public InvalidCommandException(string comm)
            : base(String.Format("Invalid Command: {0}", comm))
        {
    
        }
    }
}
