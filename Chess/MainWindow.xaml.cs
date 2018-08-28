using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using ChessLib.controllers;
using ChessLib.models;

namespace Chess
{
    public partial class MainWindow : Window
    {

        Dictionary<Tuple<int, int>, Tile> tiles = new Dictionary<Tuple<int, int>, Tile>();
        ChessController controller;
        #region Color
        Color softWhite = new Color()
        {
            R = 164,
            B = 9,
            G = 10,
            A = 255
        };
        Color darkSlate = new Color()
        {
            R = 115,
            B = 1,
            G = 3,
            A = 255
        };
        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controller = new ChessController("../../../ChessTest/test_data/start.txt");
            gridBoard.Columns = 8;
            gridBoard.Rows = 8;
            // Set-up event for when a piece is moved.
            controller.game.ChessBoard.OnPieceMoved += ChessBoard_OnPieceMoved;
            controller.game.ChessBoard.OnPiecePlaced += ChessBoard_OnPiecePlaced;

            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color color = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? darkSlate : softWhite;
                    Tuple<int, int> position = new Tuple<int, int>(i,j);
                    Tile tile = new Tile(color);
                    tiles.Add(position, tile);
                    gridBoard.Children.Add(tile.Grid);
                }
            }
            controller.PlayFromFile();
        }

        private void ChessBoard_OnPiecePlaced(object sender, PlacementArgs e)
        {
            tiles[e.PiecePlaced.Position].Piece = e.PiecePlaced;
        }

        private void ChessBoard_OnPieceMoved(object sender, MovementArgs e)
        {
            tiles[e.OldPosition].Piece = null;
            tiles[e.PieceMoved.Position].Piece = e.PieceMoved;
        }

        private void txtBoxCommand_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key is System.Windows.Input.Key.Enter)
            {
                string userInput = txtBoxCommand.Text;
                if (FileReader.CommandIsValid(userInput))
                {
                    txtBoxCommand.Clear();
                    controller.PerformCommand(userInput);
                }
            }
        }
    }
}
