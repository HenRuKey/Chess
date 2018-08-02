using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessLib.controllers
{
    /// <summary>
    /// Reads files to update the chessboard an pieces throught placement and movement
    /// </summary>
    public class FileReader
    {
       
        Regex regex = new Regex(@"(^[KQBNRP][ld][a-i][0-9]$)|(^[a-i][0-9]\s[a-i][0-9]$)|(^[a-i][0-9]\s[a-i][0-9]\s[a-i][0-9]\s[a-i][0-9]$)");
        private string filename;
        public List<string> Commands { get; }

        public FileReader(string filename)
        {
            this.filename = filename;
            Commands = new List<string>();
            ReadCommandsFromFile();
            ValidateCommands();
        }

        private void ReadCommandsFromFile()
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                do {
                    Commands.Add(reader.ReadLine());
                } while (reader.Peek() != -1) ;
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }

        private void ValidateCommands()
        {
            Commands.ForEach(c =>
            {

                if (c == "")
                {
                    Trace.Write(Commands.IndexOf(c));
                }
                if (!regex.IsMatch(c))
                { 
                    throw new FormatException();
                }
            });
        }




    }
}
