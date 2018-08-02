using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Tile> tiles = new List<Tile>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color color = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? Colors.Black : Colors.White;
                    Tuple<int, int> position = new Tuple<int, int>(i, j);
                    Tile tile = new Tile(color, position);
                    tiles.Add(tile);
                    gridBoard.Children.Add(tile.Rectangle);
                }
            }
        }
    }
}
