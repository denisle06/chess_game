using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    internal class AgileKingDecorator : MoveSetDecorator
    {
        public AgileKingDecorator(PieceMoveSet baseMoveSet) : base(baseMoveSet) { } //king can move 2 tile in any direction

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            int dx = Math.Abs(dest_x - x);
            int dy = Math.Abs(dest_y - y);

            if ((dx <= 2 && dy <= 2) && (dx != 0 || dy != 0) &&
                !(dx == 2 && dy == 1) && !(dx == 1 || dy == 2)
                ) 
            {
                return true;
            }
            return base.ValidMove(piece, board, x, y, dest_x, dest_y); ;
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
