using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.MoveSet
{
    public class KingMoveSet : PieceMoveSet
    {
        public KingMoveSet() : base()
        {
        }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            if (Math.Abs(dest_x - x) <= 1 && Math.Abs(dest_y - y) <= 1) //if the absolute difference is smaller than 1
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
