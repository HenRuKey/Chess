using Chess.Resources;
using ChessLib.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chess.converters
{
    class PieceToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Piece pieceObject = (Piece)value ?? null;
            // King
            if (pieceObject.GetType() == typeof(King))
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? PieceImages.dark_king : PieceImages.light_king;
            }
            // Queen
            else if (pieceObject.GetType() == typeof(Queen))
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? PieceImages.dark_queen : PieceImages.light_queen;
            }
            // Bishop
            else if (pieceObject.GetType() == typeof(Bishop))
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? PieceImages.dark_bishop : PieceImages.light_bishop;
            }
            // Knight
            else if (pieceObject.GetType() == typeof(Knight))
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? PieceImages.dark_knight : PieceImages.light_knight;
            }
            //Rook
            else if (pieceObject.GetType() == typeof(Rook))
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? PieceImages.dark_rook : PieceImages.light_rook;
            }
            // Pawn
            else if (pieceObject.GetType() == typeof(Pawn))
            {
                return pieceObject.Color == ChessLib.enums.Color.DARK ? PieceImages.dark_pawn : PieceImages.light_pawn;
            }
            // Blank square
            else
            {
                return PieceImages.blank;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
