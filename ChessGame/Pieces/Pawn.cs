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
        bool _JustMoveTwo;
        public Pawn (int x, int y, Color color) : base(x, y, color)
        {
            _moveset = new PawnMoveSet();
        }

        public bool JustMoveTwo
        {
            get { return _JustMoveTwo; }
            set { _JustMoveTwo = value; }
        }
    }
}
