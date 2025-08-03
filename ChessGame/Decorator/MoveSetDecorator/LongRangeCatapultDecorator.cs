using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    internal class LongRangeCatapultDecorator : MoveSetDecorator //rook can capture the piece that is 2 tile away
    {
        public LongRangeCatapultDecorator(PieceMoveSet baseMoveSet) : base(baseMoveSet) { }

        public override bool ValidMove(ChessPiece piece, Board board, int dest_x, int dest_y)
        {
            int x = piece.X;
            int y = piece.Y;

            int dx = Math.Abs(dest_x - x);
            int dy = Math.Abs(dest_y - y);

            if ((dx == 2 && dy == 1) || (dx == 1 && dy == 2)) return true;
            return base.ValidMove(piece, board, dest_x, dest_y);
        }
    }
}
