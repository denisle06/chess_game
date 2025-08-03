using ChessGame.Decorator.MoveSetDecorator;
using ChessGame.MoveSet;
using ChessGame.Upgrade.PieceUpgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Upgrade
{
    public static class UpgradeFactory
    {
        public static IUpgrade CreateUpgrade(int id)
        {
            switch (id)
            {
                case 1:
                    return new AgileKing(GameManager.Instance.Board);
                case 2:
                    return new AlternatingBishop(GameManager.Instance.Board);
                case 3:
                    return new DiagonalPawn(GameManager.Instance.Board);
                case 4:
                    return new LongRangeCatapult(GameManager.Instance.Board);
                case 5:
                    return new RetreatingQueen(GameManager.Instance.Board);
                case 6:
                    return new SpeedyHorse(GameManager.Instance.Board);
                default:
                    throw new ArgumentException($"Invalid upgrade");
            }
        }
    }
}
