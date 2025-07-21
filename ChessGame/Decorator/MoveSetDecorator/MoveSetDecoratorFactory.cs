using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator.MoveSetDecorator
{
    public static class MoveSetDecoratorFactory
    {
        public static MoveSetDecorator CreateMoveSetDecorator(string type, PieceMoveSet baseMoveSet)
        {
            switch (type.ToLower())
            {
                case "diagonal_pawn":
                    return new DiagonalPawnDecorator(baseMoveSet);
                case "agile_king":
                    return new AgileKingDecorator(baseMoveSet);
                case "long_range":
                    return new LongRangeCatapultDecorator(baseMoveSet);
                case "retreating_queen":
                    return new RetreatingQueenDecorator(baseMoveSet);
                case "speedy_horse":
                    return new SpeedyHorseDecorator(baseMoveSet);
                case "alternating_bishop":
                    return new AlternatingBishopDecorator(baseMoveSet);
                default:
                    throw new ArgumentException($"Invalid piece type: {type}");
            }
        }
    }
}
