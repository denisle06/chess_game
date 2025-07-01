using ChessGame.MoveSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    internal class Knight : ChessPiece
    {
        public Knight(int x, int y, Color color) : base(x, y, color)
        {
            _moveset = new KnightMoveSet();
        }
    }
}
