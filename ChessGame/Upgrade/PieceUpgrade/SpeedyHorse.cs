using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Decorator.MoveSetDecorator;

namespace ChessGame.Upgrade.PieceUpgrade
{
    internal class SpeedyHorse : PieceUpgrade
    {
        public SpeedyHorse(Board board) : base(board)
        {
            _identifier.Add("speedy_horse");
        }
    }
}
