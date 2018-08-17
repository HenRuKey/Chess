using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLib.controllers;
using ChessLib.models;

namespace Chess
{
    public partial class MainWindow : Window
    {
        List<Tile> tiles = new List<Tile>();
        Game game;
        int CommandItr = 0;
        List<string> commands;

        #region Color
        Color softWhite = new Color()
        {
            R = 242,
            B = 242,
            G = 242,
            A = 255
        };
        Color darkSlate = new Color()
        {
            R = 38,
            B = 38,
            G = 38,
            A = 255
        };
        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            game = new Game();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color color = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? darkSlate : softWhite;
                    Tuple<int, int> position = new Tuple<int, int>(i, j);
                    Tile tile = new Tile(color, position);
                    tiles.Add(tile);
                    gridBoard.Children.Add(tile.Grid);
                }
            }


            

        }

    }
}
