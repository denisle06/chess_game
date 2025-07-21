using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Decorator.MoveSetDecorator;

namespace ChessGame.Upgrade.PieceUpgrade
{
    internal class LongRangeCatapult : PieceUpgrade
    {
        public LongRangeCatapult(Board board) : base(board)
        {
            _identifier.Add("long_range");
        }
    }
}
