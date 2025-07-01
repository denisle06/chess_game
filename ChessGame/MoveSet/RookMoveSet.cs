using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Pieces;

namespace ChessGame.MoveSet
{
    public class RookMoveSet : PieceMoveSet
    {
        public RookMoveSet() : base()
        {
        }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            if ((dest_x == x || dest_y == y))
            {
                return true;
            }
            return false;
        }

        public override bool CheckObstruction(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            int stepX;
            if (dest_x > x)
                stepX = 1;
            else if (dest_x < x)
                stepX = -1;
            else
                stepX = 0;

            int stepY;
            if (dest_y > y)
                stepY = 1;
            else if (dest_y < y)
                stepY = -1;
            else
                stepY = 0;


            int currentX = x + stepX;
            int currentY = y + stepY;

            while (currentX != dest_x || currentY != dest_y)
            {
                if (board.ChessGrid[currentX, currentY] != null)
                    return false;

                currentX += stepX;
                currentY += stepY;
            }
            return true;
        }
    }
}

