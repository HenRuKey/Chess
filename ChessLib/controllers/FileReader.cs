using System;
using System.Collections.Generic;
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
        List<string> commands = new List<string>();
        Regex regex = new Regex("(^[KQBNRP][ld][a-h][1-8]$)");
        private string filename;


        public FileReader(string filename)
        {
            this.filename = filename;

            StreamReader reader = new StreamReader(filename);

            do
            {
                commands.Add(reader.ReadLine());
            } while (reader.Peek() != -1);

            commands.ForEach(c =>
            {
                if (regex.IsMatch(c))
                {
                    Console.WriteLine("Hello");
                }
            });
        }

    }
}
