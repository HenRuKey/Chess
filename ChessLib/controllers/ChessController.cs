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
        public Game game;
        FileReader reader;
        List<string> Commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessController"/> class.
        /// </summary>
        /// <param name="filename">The path to the file containing the chess instructions.</param>
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
            
            /*for (int i = 7; i >= 0; i--)
            {
                Console.Write($"{1 + i}");
                for (int j = 0; j <= 7; j++)
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
            Console.WriteLine("  A  B  C  D  E  F  G  H  ");
            Console.ReadKey();
            */
        }

        /// <summary>
        /// Performs each command from the file in order.
        /// </summary>
        /// <exception cref="InvalidCommandException">Thrown when an illegal move is attempted.</exception>
        public void PlayFromFile()
        {
            foreach (string command in Commands)
            {
                PerformCommand(command);
            }
        }

        public void PerformCommand(string command)
        {
            string[] CommandSplit = command.Split(' ');
            int numOfInstructions = CommandSplit.Length;
            switch (numOfInstructions)
            {
                case 1:
                    Piece piece = Commander.CreatePiece(CommandSplit[0]);
                    game.PlacePiece(piece);
                    PrintBoard();
                    break;
                case 2:
                    if (game.PerformMove(Commander.GetCoordinates(CommandSplit)))
                    {
                        if (game.DetectCheck())
                        {
                            if (game.IsCheckmate())
                            {
                                throw new NotImplementedException();
                            }
                        }
                        PrintBoard();
                    }
                    else
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

        //TODO Add manual gameplay methods




    }
}
