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
                        game.PerformMove(Commander.GetCoordinates(CommandSplit));
  
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
