using ChessGame.Decorator;
using ChessGame.Decorator.MoveSetDecorator;
using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Upgrade.PieceUpgrade
{
    internal abstract class PieceUpgrade : IUpgrade
    {
        protected Board _board;
        protected List<string> _identifier = new List<string> { };
        protected MoveSetDecorator _decorator;
        protected List<ChessPiece> _chessPieces;
        public PieceUpgrade(Board board)
        {
            _board = board;
            _identifier.Add("piece");
        }


        public void GetAffectedPiece()
        {
            List<ChessPiece> all_pieces = _board.GetAllPieces();

            foreach (ChessPiece piece in all_pieces) 
            {
                foreach (string id in _identifier)
                {
                    if (piece.Name.ToLower().Contains(id)) _chessPieces.Add(piece);
                }
            }        
        }

        public void Apply()
        {
            foreach (ChessPiece piece in _chessPieces)
            {
                MoveSetDecorator decorator = MoveSetDecoratorFactory.CreateMoveSetDecorator(_identifier[1], piece.MoveSet);
                decorator.Apply(piece);
            }
        }
    }
}
