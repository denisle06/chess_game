using ChessGame.CommandSpace;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public enum Color { White, Black }
    public class GameManager
    {
        private Board _board;
        private Player _whiteP;
        private Player _blackP;
        private Color _turn;

        public GameManager() 
        {
            this._board = new Board();
            this._whiteP = new Player(Color.White, _board);
            this._blackP = new Player(Color.Black, _board);
            this._turn = Color.White;
            List<Player> players = new List<Player> { _whiteP, _blackP };

            _board.createGrid(players);
        }

        public void ChangeTurn()
        {
            if (_turn == Color.White) _turn = Color.Black;
            else _turn = Color.White;
        }

        public void Undo(Player p)
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

        public void Undo(MoveCommand command) //undo the command
        {
            command.MovedPiece.X = command.OldLocation.Item1;
            command.MovedPiece.Y = command.OldLocation.Item2;
            _board.ChessGrid[command.OldLocation.Item1, command.OldLocation.Item2] = command.MovedPiece;
            _board.ChessGrid[command.NewLocation.Item1, command.NewLocation.Item2] = null;
            if (!(command.CapturedPiece == null))
            {
                command.CapturedPiece.X = command.NewLocation.Item1;
                command.CapturedPiece.Y = command.NewLocation.Item2;
                _board.ChessGrid[command.NewLocation.Item1, command.NewLocation.Item2] = command.CapturedPiece;
            }
        }

        public bool InCheck(Player p)
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

            ChessPiece king = p.Pieces.OfType<King>().FirstOrDefault();
            foreach (ChessPiece piece in opponent.Pieces) 
            {
                if (piece.MoveSet.ValidMove(piece, _board, piece.X, piece.Y, king.X, king.Y) &&
                    piece.MoveSet.CheckObstruction(piece, _board, piece.X, piece.Y, king.X, king.Y)) 
                    return true;
            }
            return false;
        }

        public List<(int, int)> PieceLegalMove(ChessPiece piece)
        {
            List<(int, int)> pos_list = new List<(int, int)>();
            Player p;

            if (_turn != piece.Color) return pos_list;
            if (WhiteP.Color == piece.Color) p = WhiteP;
            else p = BlackP;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((x, y) == (piece.X, piece.Y)) continue;

                    bool valid = piece.MoveSet.ValidMove(piece, _board, piece.X, piece.Y, x, y);
                    bool clear = piece.MoveSet.CheckObstruction(piece, _board, piece.X, piece.Y, x, y);
                    bool capture = piece.MoveSet.CheckCapture(piece, _board, x, y);

                    if (valid && clear)
                    {
                        MoveCommand move = new MoveCommand(_board);
                        move.Execute(piece, (x, y));
                        if (InCheck(p)) Undo(move);
                        else
                        {
                            pos_list.Add((x, y));
                            Undo(move);
                        }
                    }
                    
                }
            }
            return pos_list;
        }

        public bool HaveValidMove(Player p)
        {
            foreach (ChessPiece piece in p.Pieces)
            {
                List<(int, int)> legal_moves = PieceLegalMove(piece);
                if (legal_moves.Count > 0) return true;
            }
            return false;
        }


        public bool Checkmate(Player p)
        {
            if (InCheck(p) && !(HaveValidMove(p))) 
            {
                return true;
            }
            return false;
        }

        public bool Stalemate(Player p)
        {
            if (!InCheck(p) && !(HaveValidMove(p)))
            {
                return true;
            }
            return false;
        }

        public Board Board
        {
            get { return _board; }
        }

        public Player WhiteP
        {
            get { return _whiteP; }
        }

        public Player BlackP
        {
            get { return _blackP; }
        }
    }
}
