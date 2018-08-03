using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess
{
    class Tile
    {
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            private set { rectangle = value; }
        }

        private readonly Tuple<int, int> position;
        public Tuple<int, int> Position
        {
            get { return position; }
        }


        public Tile(Color color, Tuple<int,int> position)
        {
            Rectangle = new Rectangle
            {
                Width = 75,
                Height = 75,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(color)
            };
            this.position = position;
        }
    }
}
