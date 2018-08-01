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
       
        Regex regex = new Regex(@"(^[KQBNRP][ld][a-h][1-8]$)|(^[a-h][1-8]\s[a-h][1-8]$)|(^[a-h][1-8]\s[a-h][1-8]\s[a-h][1-8]\s[a-h][1-8]$)");
        private string filename;

        public List<string> Commands { get;}

        public FileReader(string filename)
        {
            this.filename = filename;
            Commands = new List<string>();

            StreamReader reader = new StreamReader(filename);

            do
            {
                Commands.Add(reader.ReadLine());
            } while (reader.Peek() != -1);

            Commands.ForEach(c =>
            {
                if (regex.IsMatch(c))
                {
                    Console.WriteLine("Success");
                } else
                {
                    throw new FormatException();
                }
            });
        }




    }
}
