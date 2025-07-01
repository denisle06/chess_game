using ChessGame.MoveSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    internal class Pawn : ChessPiece
    {
        
        public Pawn (int x, int y, Color color) : base(x, y, color)
        {
            _moveset = new PawnMoveSet();
        }

        
    }
}
