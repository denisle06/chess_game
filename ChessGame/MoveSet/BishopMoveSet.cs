using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.MoveSet
{
    public class BishopMoveSet : PieceMoveSet
    {
        public BishopMoveSet() : base()
        {
        }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            if (Math.Abs(x - dest_x) == Math.Abs(y - dest_y)) //if the absolute difference between x and destx, y and desty is the same -> valid move
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

                if (currentX < 0 || currentX >= 8 || currentY < 0 || currentY >= 8) //stop out of bound checking
                    return true;

                if (board.ChessGrid[currentX, currentY] != null)
                    return false;

                currentX += stepX;
                currentY += stepY;
            }
            return true;
        }
    }
}
