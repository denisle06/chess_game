using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    public static class PieceBuilder //factory pattern
    {
        public static ChessPiece CreatePiece(string type, int x, int y, Color color)
        {
            switch (type.ToLower())
            {
                case "rook":
                    return new Rook(x, y, color);
                case "pawn":
                    return new Pawn(x, y, color);
                case "knight":
                    return new Knight(x, y, color);
                case "bishop":
                    return new Bishop(x, y, color);
                case "queen":
                    return new Queen(x, y, color);
                case "king":
                    return new King(x, y, color);
                default:
                    throw new ArgumentException($"Invalid piece type: {type}");
            }
        }
    }
}
