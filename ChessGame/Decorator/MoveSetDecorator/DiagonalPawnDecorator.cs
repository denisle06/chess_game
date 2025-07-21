using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    internal class DiagonalPawnDecorator : MoveSetDecorator
    {
        public DiagonalPawnDecorator(PieceMoveSet baseMoveSet): base(baseMoveSet) {} //pawn can move diagonally

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            int direction;
            if (piece.Color == Color.White) direction = 1;
            else direction = -1;

            if (Math.Abs(x - dest_x) == 1 && (y - dest_y) == direction) return true;
            return base.ValidMove(piece, board, x, y, dest_x, dest_y);
        }
    }
}
