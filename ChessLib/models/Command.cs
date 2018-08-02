using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    class Command
    {
        delegate void CommandDelegate();

        public Command(string command)
        {

        }
    }
}
