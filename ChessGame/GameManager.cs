using ChessGame.CommandSpace;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
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

        public bool InCheck(Player p, Player opponent)
        {
            ChessPiece king = p.Pieces.OfType<King>().FirstOrDefault();
            foreach (ChessPiece piece in opponent.Pieces) 
            {
                if (piece.MoveSet.ValidMove(piece, _board, piece.X, piece.Y, king.X, king.Y) &&
                    piece.MoveSet.CheckObstruction(piece, _board, piece.X, piece.Y, king.X, king.Y)) 
                    return true;
            }
            return false;
        }

        public bool HaveValidMove(Player p)
        {
            Player opponent;
            if (p == _whiteP)
            {
                opponent = _blackP;
            }
            else
            {
                opponent = _whiteP;
            }

            foreach (ChessPiece piece in p.Pieces)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        (int, int) target = (x, y);
                        List<string> letters = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h" };
                        string input = letters[target.Item1] + y.ToString();

                        MoveCommand command = CommandBuilder.CreateCommand("move", _board) as MoveCommand;
                        p.Command = command;
                        string output = p.ExecuteCommand(input);

                        if (output.StartsWith("Move executed") && !(InCheck(p, opponent)))
                        {
                            Undo(p);
                            return true;
                        }
                        else
                        {
                            Undo(p);
                        }
                    }
                } 
            }
            return false;
        }
    }
}
