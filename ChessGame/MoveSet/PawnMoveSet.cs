using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public override bool ValidMove(ChessPiece piece, Board board, int dest_x, int dest_y)
        {
            int x = piece.X;
            int y = piece.Y;
            
            int direction;
            if (piece.Color == Color.White) direction = 1;
            else direction = -1;

            if (x == dest_x)
            {
                if (piece.Color == Color.White && y == 1 && dest_y == 3) return true;
                if (piece.Color == Color.Black && y == 6 && dest_y == 4) return true;
                if (y + direction == dest_y) return true;
            }

            if (Math.Abs(dest_x - x) == 1 && y + direction == dest_y)
            {
                
                if (CheckCapture(piece, board, dest_x, dest_y) == true && board.ChessGrid[dest_x, dest_y] != null)
                {
                    return true;
                }
            }
            
            return false;
        }

        public override bool CheckObstruction(ChessPiece piece, Board board, int dest_x, int dest_y) //check pawn position from the pawn up
        {
            int x = piece.X;
            int y = piece.Y;

            if (piece.Color == Color.White && x == dest_x)
            {
                if (dest_y - y == 2) return board.ChessGrid[x, y + 1] == null && board.ChessGrid[x, y + 2] == null;
                else return board.ChessGrid[x, y + 1] == null;
            }

            if (piece.Color == Color.Black && x == dest_x)
            {
                if (dest_y - y == -2) return board.ChessGrid[x, y - 1] == null && board.ChessGrid[x, y - 2] == null;
                else return board.ChessGrid[x, y - 1] == null;
            }
            
            return true;
        }
    }
}
