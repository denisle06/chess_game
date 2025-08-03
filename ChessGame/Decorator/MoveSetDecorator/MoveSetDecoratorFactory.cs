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
                case "pawn":
                    return new LongPawnDecorator(baseMoveSet);
                case "king":
                    return new AgileKingDecorator(baseMoveSet);
                case "rook":
                    return new LongRangeCatapultDecorator(baseMoveSet);
                case "queen":
                    return new RetreatingQueenDecorator(baseMoveSet);
                case "knight":
                    return new SpeedyHorseDecorator(baseMoveSet);
                case "bishop":
                    return new AlternatingBishopDecorator(baseMoveSet);
                default:
                    throw new ArgumentException($"Invalid piece type: {type}");
            }
        }
    }
}
