using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.models;
using ChessLib.Exceptions;

namespace ChessLib.controllers
{
    public class ChessController
    {
        Game game;
        FileReader reader;
        List<string> Commands;
        
        public ChessController(string filename)
        {
            game = new Game();
            reader = new FileReader(filename);
            Commands = reader.Commands;
        }


        /// <summary>
        ///   Temporary Debugging Method
        ///   ***DELETE WHEN FIXED***
        /// </summary>
        public void PrintBoard()
        {
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 7; j++)
                {
                    Piece p = game.GetPieceAtCoord(new Tuple<int, int>(i, j));
                    if (p != null)
                    {
                        Console.Write($"[{p.ToString()}]");
                    } else
                    {
                        Console.Write("[ ]");
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public void PlayFromFile()
        {
            foreach (string command in Commands)
            {
                string[] CommandSplit = command.Split(' ');
                int numOfInstructions = CommandSplit.Length;
                switch (numOfInstructions)
                {
                    case 1:
                        Piece piece = Commander.CreatePiece(CommandSplit[0]);
                        game.PlacePiece(piece);
                        break;
                    case 2:
                        if (game.PerformMove(Commander.GetCoordinates(CommandSplit)))
                        {
                            PrintBoard();
                        };
                        break;
                    case 4:
                        game.SpecialMove(Commander.GetSpecialCoordinates(CommandSplit));
                        break;
                    default: throw new InvalidCommandException(command);
                }
            }
        }

        //TODO Add manual gameplay methods




    }
}
