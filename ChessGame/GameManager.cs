using ChessGame.CommandSpace;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public enum Color { White, Black }
    public class GameManager
    {
        public static GameManager Instance { get; private set; } //singleton

        private Board _board;
        private Player _whiteP;
        private Player _blackP;
        private Color _turn;
        private bool _stop = false;

        public GameManager() 
        {
            if (Instance != null)
                throw new Exception("Only one GameManager allowed!");
            Instance = this;

            this._board = new Board();
            this._whiteP = new Player(Color.White, _board);
            this._blackP = new Player(Color.Black, _board);
            this._turn = Color.White;
            List<Player> players = new List<Player> { _whiteP, _blackP };

            _board.createGrid(players);
        }

        public void ChangeTurn()
        {
            if ((InCheck(WhiteP))) Debug.WriteLine($"{WhiteP.Color}'s king is threatened");
            if ((InCheck(BlackP))) Debug.WriteLine($"{BlackP.Color}'s king is threatened");

            if ((_turn == Color.White && Checkmate(WhiteP)) || (_turn == Color.Black && Checkmate(BlackP)))
            {
                    Debug.WriteLine($"Checkmate!");
                if (_turn == Color.White) Debug.WriteLine($"Black won");
                else Debug.WriteLine($"White won");
                _stop = true;
            }
            else if (_turn == Color.White && Stalemate(WhiteP) || _turn == Color.Black && Stalemate(BlackP))
            {
                Debug.WriteLine($"Stalemate!");
                Debug.WriteLine($"Draw");
                _stop = true;
            }

            if (_turn == Color.White) _turn = Color.Black;
            else _turn = Color.White;
        }

        

        public void Undo(MoveCommand command) //undo the command
        {
            switch (command.MoveType) 
            {
                case (MoveType.Normal):
                    command.MovedPiece.X = command.OldLocation.Item1;
                    command.MovedPiece.Y = command.OldLocation.Item2;
                    _board.ChessGrid[command.OldLocation.Item1, command.OldLocation.Item2] = command.MovedPiece;
                    _board.ChessGrid[command.NewLocation.Item1, command.NewLocation.Item2] = null;
                    
                    if (!(command.CapturedPiece == null))
                    {
                        Player opponent = command.CapturedPiece.Color == Color.White ? _whiteP : _blackP;   //re-add the piece back to the player
                        if (!opponent.Pieces.Contains(command.CapturedPiece)) opponent.Pieces.Add(command.CapturedPiece);

                        command.CapturedPiece.X = command.NewLocation.Item1;
                        command.CapturedPiece.Y = command.NewLocation.Item2;
                        _board.ChessGrid[command.NewLocation.Item1, command.NewLocation.Item2] = command.CapturedPiece;
                    }
                    command.MovedPiece.Moved -= 1;
                    break;

                case (MoveType.Passant):
                    command.MovedPiece.X = command.OldLocation.Item1;
                    command.MovedPiece.Y = command.OldLocation.Item2;
                    _board.ChessGrid[command.OldLocation.Item1, command.OldLocation.Item2] = command.MovedPiece;
                    _board.ChessGrid[command.NewLocation.Item1, command.NewLocation.Item2] = null;
                    if (!(command.CapturedPiece == null))
                    {
                        Player opponent = command.CapturedPiece.Color == Color.White ? _whiteP : _blackP;
                        if (!opponent.Pieces.Contains(command.CapturedPiece)) opponent.Pieces.Add(command.CapturedPiece);

                        command.CapturedPiece.X = command.NewLocation.Item1;
                        command.CapturedPiece.Y = command.OldLocation.Item2;
                        _board.ChessGrid[command.NewLocation.Item1, command.OldLocation.Item2] = command.CapturedPiece;
                    }
                    command.MovedPiece.Moved -= 1;
                    break;

                case (MoveType.Castling):
                    int direction = command.NewLocation.Item1 > command.OldLocation.Item1 ? 1 : -1;
                    int new_kingX = command.OldLocation.Item1 + 2 * direction;
                    int new_rookX = new_kingX + -1 * direction;

                    // Clear the castled positions
                    _board.ChessGrid[new_kingX, command.OldLocation.Item2] = null;
                    _board.ChessGrid[new_rookX, command.OldLocation.Item2] = null;

                    // Restore the king
                    command.MovedPiece.X = command.OldLocation.Item1;
                    command.MovedPiece.Y = command.OldLocation.Item2;
                    _board.ChessGrid[command.MovedPiece.X, command.MovedPiece.Y] = command.MovedPiece;

                    // Restore the rook
                    command.CapturedPiece.X = command.NewLocation.Item1;
                    command.CapturedPiece.Y = command.NewLocation.Item2;
                    _board.ChessGrid[command.CapturedPiece.X, command.CapturedPiece.Y] = command.CapturedPiece;

                    command.MovedPiece.Moved -= 1;
                    command.CapturedPiece.Moved -= 1;

                    Debug.WriteLine("Clearing positions:");
                    Debug.WriteLine($" - King new pos: {command.MovedPiece.X}, {command.MovedPiece.Y}");
                    Debug.WriteLine($" - Rook new pos: {command.CapturedPiece.X}, {command.CapturedPiece.Y}");
                    Debug.WriteLine($" - King old pos: {command.OldLocation.Item1}{command.OldLocation.Item2}");
                    Debug.WriteLine($" - Rook original: {command.NewLocation.Item1}, {command.NewLocation.Item2}");
                    break;

                default: break;
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
                if (piece.MoveSet.ValidMove(piece, _board, king.X, king.Y) &&
                    piece.MoveSet.CheckObstruction(piece, _board, king.X, king.Y)) 
                    return true;

            }
            return false;
        }

        public List<(int, int)> PieceLegalMove(ChessPiece piece)
        {
            List<(int, int)> pos_list = new List<(int, int)>();
            Player p;
            Player opp;

            if (_turn != piece.Color) return pos_list;
            if (WhiteP.Color == piece.Color)
            {
                p = WhiteP; 
                opp = BlackP;
            }
            else
            {
                p = BlackP;
                opp = WhiteP;
            }

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (CreateAndExecuteCommand(piece, x, y, p, opp, true)) pos_list.Add((x, y));
                }
            }
            return pos_list;
        }

        public bool CreateAndExecuteCommand(ChessPiece piece, int x, int y, Player p, Player opp, bool simulate) //destination x and y
            //simulate flag for the undo
        {
            if ((x, y) == (piece.X, piece.Y)) return false;
            MoveCommand move = new MoveCommand(_board, piece, (x, y));

            bool execute = move.Execute(piece, (x, y));
            if (!execute)
            {
               // Debug.WriteLine($"Test movement with move type {move.MoveType} from ({move.OldLocation.Item1}, {move.OldLocation.Item2}) to {(x, y)}. The piece type is {piece.Name}. Move not executed");
                return false;
            }
            if (move.CapturedPiece != null && move.MoveType != MoveType.Castling) opp.Pieces.Remove(move.CapturedPiece);  //remove the piece if capture a piece
            //Debug.WriteLine($"Test movement with move type {move.MoveType} from ({move.OldLocation.Item1}, {move.OldLocation.Item2}) to {(x, y)}. The piece type is {piece.Name}. Move executed");
            
            if (simulate == true)
            {
                if (InCheck(p))
                {
                    //Debug.WriteLine($"Return false, move is not added to pos_list");
                    Undo(move);
                    return false;
                }
                else
                {
                    //Debug.WriteLine($"Return true, move is added to pos_list");
                    Undo(move);
                    return true;
                }
            }
            else
            {
                if (InCheck(p))
                {
                    Undo(move);
                    return false;
                }
                else 
                {
                    foreach (var pawn in p.Pieces.Concat(opp.Pieces).OfType<Pawn>())
                        pawn.JustMoveTwo = false;
                    if (move.MovedPiece is Pawn moved_pawn && Math.Abs(move.OldLocation.Item2 - move.NewLocation.Item2) == 2) moved_pawn.JustMoveTwo = true;
                    
                    return true;
                }
                            
            }
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

        public Color Turn
        {
            get { return _turn; }
        }
    }
}




//public void Undo(Player p)
//{
//   MoveCommand command = p.CommandList.Pop();
//    command.MovedPiece.X = command.OldLocation.Item1;
//    command.MovedPiece.Y = command.OldLocation.Item2;
//    _board.ChessGrid[command.OldLocation.Item1, command.OldLocation.Item2] = command.MovedPiece;
//    if (!(command.CapturedPiece == null))
//    {
//        command.CapturedPiece.X = command.NewLocation.Item1;
//        command.CapturedPiece.Y = command.NewLocation.Item2;
//        _board.ChessGrid[command.NewLocation.Item1, command.NewLocation.Item2] = command.CapturedPiece;
//    } 
//}