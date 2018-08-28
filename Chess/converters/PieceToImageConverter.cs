using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess.converters
{
    class PieceToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return value;
            }

            Piece pieceObject = (Piece)value;
            // King
            if (pieceObject is King)
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? FindImage("dark_king") : FindImage("light_king");
            }
            // Queen
            else if (pieceObject is Queen)
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? FindImage("dark_queen") : FindImage("light_queen");
            }
            // Bishop
            else if (pieceObject is Bishop)
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? FindImage("dark_bishop") : FindImage("light_bishop");
            }
            // Knight
            else if (pieceObject is Knight)
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? FindImage("dark_knight") : FindImage("light_knight");
            }
            //Rook
            else if (pieceObject is Rook)
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? FindImage("dark_rook") : FindImage("light_rook");
            }
            // Pawn
            else if (pieceObject is Pawn)
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? FindImage("dark_pawn") : FindImage("light_pawn");
            }
            // Blank square
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static BitmapImage FindImage(string imageName)
        {
            string path = $"pack://application:,,,/resources/pieces/{imageName}.png";
            BitmapImage image = new BitmapImage(new Uri(path));
            //image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(path);
            return image;
        }
    }
}
