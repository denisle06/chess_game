using ChessGame.CommandSpace;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class GameManager
    {
        private Board _board;
        private Player _whiteP;
        private Player _blackP;

        public GameManager(Board board, Player whiteP, Player blackP) 
        {
            this._whiteP = whiteP;
            this._blackP = blackP;
            this._board = board;
        }

        public void Undo(Player p )
        {
            MoveCommand command = p.CommandList.Pop();
            command.MovedPiece.X = command.OldLocation.Item1;
            command.MovedPiece.Y = command.OldLocation.Item2;
            _board.ChessGrid[command.OldLocation.Item1, command.OldLocation.Item2] = command.MovedPiece;
            if (!(command.CapturedPiece == null))
            {
                command.CapturedPiece.X = command.NewLocation.Item1;
                command.CapturedPiece.Y = command.NewLocation.Item2;
                _board.ChessGrid[command.NewLocation.Item1, command.NewLocation.Item2] = command.CapturedPiece;
            } 
        }
    }
}
