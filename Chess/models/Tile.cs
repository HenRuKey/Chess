using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess
{
    class Tile
    {

        private Grid grid;
        public Grid Grid
        {
            get { return grid; }
            private set { grid = value; }
        }

        private readonly Tuple<int, int> position;
        public Tuple<int, int> Position
        {
            get { return position; }
        }

        public Tile(Color color, Tuple<int,int> position)
        {
            this.Grid = new Grid()
            {
                Background = new SolidColorBrush(color)
            };
            this.position = position;
        }
    }
}
