using ChessGame.CommandSpace;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    
    public enum Color { White, Black}
    public class Player
    {
        MoveCommand _command;
        Stack<MoveCommand> _stack = new Stack<MoveCommand>();
        List<ChessPiece> _pieces = new List<ChessPiece> ();
        Color _playerColor;
        Board _board;
        public Player(Color color, Board board) 
        { 
            _playerColor = color;
            _board = board;
            initPieces();
        }

        public void initPieces() //create pieces dynamically
        {
            int backrow, frontrow;
            if (_playerColor == Color.White)
            {
                backrow = 0; frontrow = 1;
            }
            else { backrow = 7; frontrow = 6; }

            List<String> order = new List<string> () {"rook", "knight", "bishop", "queen", "king", "bishop", "knight", "rook"};
            

            for (int i = 0; i < 8; i++)
            {
                _pieces.Add(PieceBuilder.CreatePiece("pawn", i, frontrow, _playerColor));
                _pieces.Add(PieceBuilder.CreatePiece(order[i], i, backrow, _playerColor));
            }
        }

        public string ExecuteCommand(string input)
        {
            string output = _command.Execute(input);
            _stack.Push(_command); //for the redo feature in future
            return output;
        }

        public List<(int, int)> AvailableKingMove()
        {
            ChessPiece king = _pieces.OfType<Knight>().FirstOrDefault();
            List<(int, int)> available_location = new List<(int, int)> { };

            for (int location_x = 1; location_x < 9; location_x++) //check every cell on board to account for possible upgrades
            {
                for (int location_y = 1; location_y < 9; location_y++)
                {
                    if (king.MoveSet.ValidMove(king, _board, king.X, king.Y, location_x, location_y) || king.MoveSet.CheckObstruction(king, _board, king.X, king.Y, location_x, location_y) || king.MoveSet.CheckCapture(king, _board, location_x, location_y))
                    {
                        available_location.Add((location_x, location_y));
                    }
                }
            }
            return available_location;
        }

        public List<ChessPiece> Pieces
        {
            get { return _pieces; }
        }

        public Board Board
        {
            get { return _board; }
        }
        public MoveCommand Command 
        {
            get { return _command; }
            set { _command = value; }
        }

        public Stack<MoveCommand> CommandList
        {
            get { return _stack; }
            set { _stack = value; }
        }
    }
}
