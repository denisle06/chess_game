using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    internal class SpeedyHorseDecorator : MoveSetDecorator //knight can move 3 tile in one direction and 1 tile in another
    {
        public SpeedyHorseDecorator(PieceMoveSet baseMoveSet) : base(baseMoveSet) { }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            if (Math.Abs(dest_x - x) == 3 && Math.Abs(dest_y - y) == 1 || Math.Abs(dest_x - x) == 1 && Math.Abs(dest_y - y) == 3) //3 in a direction and then 1
            {
                return true;
            }
            return false;
        }
    }
}
