using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Decorator.MoveSetDecorator;

namespace ChessGame.Upgrade.PieceUpgrade
{
    internal class LongPawn : PieceUpgrade
    {
        public LongPawn(Board board) : base(board)
        {
            _identifier.Add("pawn");
            _description = "Pawn can now always move 2-tiles forward";
            GetAffectedPiece();
            
        }
    }
}
