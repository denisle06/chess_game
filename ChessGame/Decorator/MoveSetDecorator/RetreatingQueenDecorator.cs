using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    internal class RetreatingQueenDecorator : MoveSetDecorator //queen can retreat like a knight
    {
        public RetreatingQueenDecorator(PieceMoveSet baseMoveSet) : base(baseMoveSet) { }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            int y_change = dest_y - y;
            bool valid_direction = false; 

            if (piece.Color == Color.White) if (y_change < 0) valid_direction = true;
            if (piece.Color == Color.Black) if (y_change > 0) valid_direction = true;

            if (valid_direction &&
               (Math.Abs(dest_x - x) == 2 && Math.Abs(dest_y - y) == 1 ||
               Math.Abs(dest_x - x) == 1 && Math.Abs(dest_y - y) == 2)) return true;

            return _baseMoveSet.ValidMove(piece, board, x, y, dest_x, dest_y);
        }
    }
}
