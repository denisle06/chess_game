using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Decorator
{
    internal interface IDecorator
    {
        public abstract void Apply(ChessPiece p);
    }
}
