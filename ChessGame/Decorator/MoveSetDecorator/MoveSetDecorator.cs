using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    public abstract class MoveSetDecorator : PieceMoveSet, IDecorator
    {
        protected PieceMoveSet _baseMoveSet;

        public MoveSetDecorator(PieceMoveSet baseMoveSet)
        {
            _baseMoveSet = baseMoveSet;
        }

        public override bool ValidMove(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            return _baseMoveSet.ValidMove(piece, board, x, y, dest_x, dest_y);
        }

        public override bool CheckObstruction(ChessPiece piece, Board board, int x, int y, int dest_x, int dest_y)
        {
            return _baseMoveSet.CheckObstruction(piece, board, x, y, dest_x, dest_y);
        }

        public void Apply(ChessPiece p)
        {
            p.MoveSet = this;
        }
    }
}
