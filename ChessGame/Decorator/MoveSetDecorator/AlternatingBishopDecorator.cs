using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    internal class AlternatingBishopDecorator : MoveSetDecorator
    {
        public AlternatingBishopDecorator(PieceMoveSet baseMoveSet) : base(baseMoveSet) { } //bishop can move 1 tile up or down

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            if (Math.Abs(y - dest_y) == 1) return true;
            return base.ValidMove(piece, board, x, y, dest_x, dest_y);
        }
    }
}
