using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.MoveSet
{
    public abstract class PieceMoveSet
    {


        public PieceMoveSet()
        {
        }

        public abstract bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y);

        public abstract bool CheckObstruction(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y);

        public virtual bool CheckCapture(ChessPiece piece, Board board, int dest_x, int dest_y)
        {
            ChessPiece target = board.ChessGrid[dest_x, dest_y];
            //if (target == null) return true; the logic in handled in movecommand
            if (piece.Color == target.Color)
            {
                return false;
            }
            return true;
        }
        
    }
}
