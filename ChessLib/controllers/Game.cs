using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib.interfaces;

namespace ChessLib.controllers
{
    public class Game
    {
        private Chessboard board;
        private List<Piece> pieces;


        public Game(string gameFile)
        {
            FileReader reader = new FileReader(gameFile);
            board = new Chessboard();
        }

        public void PerformMove()
        {
            IMoveable p
        }

        public void PlacePiece(Piece piece)
        {
        
        }
        
    }
}


//Stream myStream = myAssembly.GetManifestResourceStream( "MyNamespace.SubFolder.MyImage.bmp" );
