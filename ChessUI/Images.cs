using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using ChessGame;

namespace ChessUI
{
    public static class Images
    {
        private static readonly Dictionary<string, ImageSource> Sources = new(StringComparer.OrdinalIgnoreCase)
        {
            {"W Pawn", LoadImage("Assets/PawnW.png") },
            {"W Bishop", LoadImage("Assets/BishopW.png") },
            {"W Knight", LoadImage("Assets/KnightW.png") },
            {"W Rook", LoadImage("Assets/RookW.png") },
            {"W Queen", LoadImage("Assets/QueenW.png") },
            {"W King", LoadImage("Assets/KingW.png") },
            {"B Pawn", LoadImage("Assets/PawnB.png") },
            {"B Bishop", LoadImage("Assets/BishopB.png") },
            {"B Knight", LoadImage("Assets/KnightB.png") },
            {"B Rook", LoadImage("Assets/RookB.png") },
            {"B Queen", LoadImage("Assets/QueenB.png") },
            {"B King", LoadImage("Assets/KingB.png") },
        };

        private static ImageSource LoadImage(string filepath)
        {
            return new BitmapImage(new Uri(filepath, UriKind.Relative));
        }

        public static ImageSource GetImage(ChessPiece piece)
        {
            if (piece == null) return null;
            string name = piece.Name;
            return Sources[name];
        }
    }
}
