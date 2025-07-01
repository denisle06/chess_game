using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChessGame.CommandSpace
{   
    public class MoveCommand : Command//command pattern
    {
        string[] valid_input = new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "a", "b", "c", "d", "e", "f", "g", "h"};
        public MoveCommand(Board board) : base(board)
        {
        }

        public override string Execute(string input)
        {
            string[] command_list = input.Split(' ');
            if (command_list.Length != 2)
            {
                return ("Invalid command");
            }
            string start = command_list[0];
            string end = command_list[1];

            string start_valid = CheckValidInput(start);
            string end_valid = CheckValidInput(end);
            string additional_output = "";

            if (!(start_valid == "valid"))
            {
                return start_valid;
            }
            else if (!(end_valid == "valid"))
            {
                return end_valid;
            }
            else
            {
                List<string> start_position = start.Select(c => c.ToString()).ToList();
                List<string> end_position = end.Select(c => c.ToString()).ToList();


                int start_row = start_position[0][0] - 'a';
                int start_col = int.Parse(start_position[1]) - 1;
                int end_row = end_position[0][0] - 'a';
                int end_col = int.Parse(end_position[1]) - 1;

                ChessPiece piece = _board.ChessGrid[start_row, start_col];
                ChessPiece target = _board.ChessGrid[end_row, end_col];

                if (piece == null) return "There is no piece in the starting location";
                if (!(_player.Pieces.Contains(piece))) return "The piece you want to move is not yours";

                bool valid_move = piece.MoveSet.ValidMove(piece, _board, start_row, start_col, end_row, end_col);
                bool check_obstruction = piece.MoveSet.CheckObstruction(piece, _board, start_row, start_col, end_row, end_col);
                
                if (valid_move == false || check_obstruction == false) return "The move is invalid";
                if (target != null)
                {
                    bool capture = piece.MoveSet.CheckCapture(piece, _board, end_row, end_col);
                    if (capture == false) return "The end location is occupied by another piece";
                    else { additional_output = ".Captured the " + target.Name; }
                }

                piece.X = end_row;
                piece.Y = end_col;
                _board.ChessGrid[start_row, start_col] = null;
                _board.ChessGrid[end_row, end_col] = piece;
                return "Move executed" + additional_output;
            }
        }

        public override string CheckValidInput(string check)
        {
            char[] char_list = check.ToCharArray();
            if (char_list.Length != 2) return "Invalid command length";
            if (!char.IsLetter(char_list[0]) || !char.IsDigit(char_list[1])) return "Invalid command format";
            if (!valid_input.Contains(char_list[1].ToString()) || !valid_input.Contains(char_list[0].ToString())) return "Invalid position";
            return "valid";
        }
    }
}
