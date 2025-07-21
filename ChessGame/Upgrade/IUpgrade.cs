using ChessGame.Decorator;
using ChessGame.Decorator.MoveSetDecorator;
using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Upgrade
{
    internal interface IUpgrade
    {
        public abstract void Apply();
    }
}
