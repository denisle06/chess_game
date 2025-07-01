using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Pieces;

namespace ChessGame.MoveSet
{
     public class PawnMoveSet : PieceMoveSet
    {
        public PawnMoveSet() : base()
        {
        }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            int direction;
            if (piece.Color == Color.White) direction = 1;
            else direction = -1;

            if (x == dest_x)
            {
                if (!piece.Moved && (y + 2 * direction == dest_y || y + direction == dest_y))
                {
                    piece.Moved = true;
                    return true;
                }
                else if (y + direction == dest_y) return true;
            }

            if (Math.Abs(dest_x - x) == 1 && Math.Abs(dest_y - y) == direction)
            {
                if (CheckCapture(piece, board, dest_x, dest_y) == true)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CheckObstruction(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y) //check pawn position from the pawn up
        {
            if (piece.Color == Color.White)
            {

                if (dest_y == y + 2)
                {
                    return board.ChessGrid[x, y + 1] == null && board.ChessGrid[x, y + 2] == null;
                }

                else if (dest_y == y + 1)
                {
                    return board.ChessGrid[x, y + 1] == null;
                }
            }
            else 
            {
                if (dest_y == y - 2)
                {
                    return board.ChessGrid[x, y - 1] == null && board.ChessGrid[x, y - 2] == null;
                }
                else if (dest_y == y - 1)
                {
                    return board.ChessGrid[x, y - 1] == null;
                }
            }

            return false; 
        }
    }
}
