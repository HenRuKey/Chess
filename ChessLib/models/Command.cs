using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    delegate void CommandDelegate();

    class Command
    {

        CommandDelegate perform;

        public Command(string command)
        {

        }

        /// <summary>
        /// Returns the translation of the command in plain English.
        /// </summary>
        /// <returns>Translation of the command used to initialize the object.</returns>
        public override string ToString()
        {
            // TODO: Provide a translation of the command.
            return "";
        }
    }
}
