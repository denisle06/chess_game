    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Pieces;

namespace ChessGame.MoveSet
{
    public class KnightMoveSet : PieceMoveSet
    {
        public KnightMoveSet() : base()
        {
        }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            if ((Math.Abs(dest_x - x) == 2 && Math.Abs(dest_y - y) == 1) || (Math.Abs(dest_x - x) == 1 && Math.Abs(dest_y - y) == 2)) //2 in a direction and then 1
            {
                return true;
            }
            return false;
        }

        public override bool CheckObstruction(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            return true;
        }
    }
}
