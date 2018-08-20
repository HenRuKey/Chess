using Chess.converters;
using ChessLib.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess
{
    class Tile : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        private Grid grid;
        public Grid Grid
        {
            get { return grid; }
            private set { grid = value; }
        }

        private Piece piece;
        public Piece Piece
        {
            get { return piece; }
            set { piece = value; NotifyPropertyChanged(); }
        }

        public Tile(Color color)
        {
            this.Grid = new Grid()
            {
                Background = new SolidColorBrush(color),
                DataContext = this
            };
            // Create image to store in grid.
            Image image = new Image();
            // Bind image to tile's piece property.
            Binding imageBinding = new Binding("Piece");
            imageBinding.Mode = BindingMode.OneWay;
            imageBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            imageBinding.Converter = new PieceToImageConverter();
            image.SetBinding(Image.SourceProperty, imageBinding);
            grid.Children.Add(image);
        }
    }
}
