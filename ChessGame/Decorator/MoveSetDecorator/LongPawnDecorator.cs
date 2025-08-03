using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    internal class LongPawnDecorator : MoveSetDecorator
    {
        public LongPawnDecorator(PieceMoveSet baseMoveSet): base(baseMoveSet) {} //pawn can move diagonally

        public override bool ValidMove(ChessPiece piece, Board board,  int dest_x, int dest_y)
        {
            int x = piece.X;
            int y = piece.Y;

            int direction;
            if (piece.Color == Color.White) direction = 1;
            else direction = -1;

            if (x == dest_x)
            {
                if (dest_y - y == direction * 2) return true;
            }
                
            return base.ValidMove(piece, board, dest_x, dest_y);
        }
    }
}
