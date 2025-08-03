using ChessGame.MoveSet;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChessGame.CommandSpace
{
    public enum MoveType{ Normal, Castling, Passant, Promotion }
    public class MoveCommand //command pattern
    {
        Board _board;
        MoveType _moveType;
        ChessPiece moved_piece = null;
        ChessPiece captured_piece = null;
        ChessPiece new_piece = null;
        (int, int) old_location; //for undo
        (int, int) new_location;


        public MoveCommand(Board board, ChessPiece piece, (int, int) location)
        {
            _board = board;
            moved_piece = piece;
            old_location = (piece.X, piece.Y);
            new_location = location;
            captured_piece = _board.ChessGrid[location.Item1, location.Item2];
            _moveType = CheckMoveType(piece, location);
        }

        


        public bool Execute(ChessPiece piece, (int, int) location)
        {
            if ((location.Item1, location.Item2) == (piece.X, piece.Y)) return false;
            bool check = ConditionCheck();
            if (!check) return false;

            switch (_moveType)
            {
                case (MoveType.Normal):
                    _board.ChessGrid[piece.X, piece.Y] = null;
                    piece.X = location.Item1;
                    piece.Y = location.Item2;
                    _board.ChessGrid[piece.X, piece.Y] = piece;
                    piece.Moved += 1;
                    return true;

                case (MoveType.Passant):
                    captured_piece = _board.ChessGrid[location.Item1, piece.Y];
                    _board.ChessGrid[piece.X, piece.Y] = null;
                    _board.ChessGrid[location.Item1, piece.Y] = null;
                    piece.X = location.Item1;
                    piece.Y = location.Item2;
                    _board.ChessGrid[piece.X, piece.Y] = piece;
                    return true;

                case (MoveType.Promotion):
                    _board.ChessGrid[piece.X, piece.Y] = null;
                    piece.X = location.Item1;
                    piece.Y = location.Item2;
                    _board.ChessGrid[piece.X, piece.Y] = piece;
                    piece.Moved += 1;
                    return true;

                case (MoveType.Castling):
                    int direction = captured_piece.X > piece.X ? 1 : -1;
                    _board.ChessGrid[piece.X, piece.Y] = null;
                    _board.ChessGrid[captured_piece.X, captured_piece.Y] = null;
                    piece.X = piece.X + 2 * direction;
                    captured_piece.X = piece.X + -1 * direction;
                    _board.ChessGrid[piece.X, piece.Y] = piece;
                    _board.ChessGrid[captured_piece.X, captured_piece.Y] = captured_piece;
                    piece.Moved += 1;
                    captured_piece.Moved += 1;
                    return true;

                default:
                    return false;

            }    
        }

        public MoveType CheckMoveType(ChessPiece piece, (int, int) location)
        {
            int dest_x = location.Item1;
            int dest_y = location.Item2;

            if (piece is Pawn)
            {
                if (piece.Color == Color.White)
                {
                    if (dest_y == 7) return MoveType.Promotion;
                    if (piece.Y == 4 &&
                        dest_y == 5 &&
                        _board.ChessGrid[dest_x, piece.Y] != null &&
                        _board.ChessGrid[dest_x, piece.Y].Color != piece.Color &&
                        _board.ChessGrid[dest_x, dest_y] == null &&
                        _board.ChessGrid[dest_x, piece.Y] is Pawn Wpawn &&
                        Wpawn.JustMoveTwo == true &&
                        Math.Abs(dest_x - piece.X) == 1) return MoveType.Passant; //condition to recognize en passant
                }

                if (piece.Color == Color.Black)
                {
                    if (dest_y == 0) return MoveType.Promotion;
                    if (piece.Y == 3 &&
                        dest_y == 2 &&
                        _board.ChessGrid[dest_x, piece.Y] != null &&
                        _board.ChessGrid[dest_x, piece.Y].Color != piece.Color &&
                        _board.ChessGrid[dest_x, dest_y] == null &&
                        _board.ChessGrid[dest_x, piece.Y] is Pawn Bpawn &&
                        Bpawn.JustMoveTwo == true
                        && Math.Abs(dest_x - piece.X) == 1
                        ) return MoveType.Passant;
                }
            }

            if (piece is King &&
                _board.ChessGrid[dest_x, dest_y] is Rook &&
                piece.Color == _board.ChessGrid[dest_x, dest_y].Color &&
                piece.Moved == 0 && _board.ChessGrid[dest_x, dest_y].Moved == 0) return MoveType.Castling;

            return MoveType.Normal;
        }


        public bool ConditionCheck()
        {
            if ((moved_piece.X, moved_piece.Y) == new_location) 
            {
                Debug.WriteLine("Rejected move: same tile");
                return false;
            }
            
            switch (_moveType)
            {
                case MoveType.Normal:
                    bool valid = moved_piece.MoveSet.ValidMove(moved_piece, _board, new_location.Item1, new_location.Item2);
                    bool clear = moved_piece.MoveSet.CheckObstruction(moved_piece, _board, new_location.Item1, new_location.Item2);
                    bool capture = moved_piece.MoveSet.CheckCapture(moved_piece, _board, new_location.Item1, new_location.Item2);

                    if (valid && clear && capture) return true;
                    else return false;

                case MoveType.Passant:
                        return true;

                case MoveType.Promotion:
                    valid = moved_piece.MoveSet.ValidMove(moved_piece, _board, new_location.Item1, new_location.Item2);
                    clear = moved_piece.MoveSet.CheckObstruction(moved_piece, _board, new_location.Item1, new_location.Item2);
                    capture = moved_piece.MoveSet.CheckCapture(moved_piece, _board, new_location.Item1, new_location.Item2);

                    if (valid && clear && capture) return true;
                    else return false;

                case MoveType.Castling:
                    Player p = GameManager.Instance.WhiteP.Color == MovedPiece.Color ? GameManager.Instance.WhiteP : GameManager.Instance.BlackP;    

                    int x = moved_piece.X;
                    int y = moved_piece.Y;

                    int stepX;
                    if (new_location.Item1 > x) stepX = 1;
                    else if (new_location.Item1 < x) stepX = -1;
                    else stepX = 0;

                    int stepY;
                    if (new_location.Item2 > y) stepY = 1;
                    else if (new_location.Item2 < y) stepY = -1;
                    else stepY = 0;


                    int currentX = x + stepX;
                    int currentY = y + stepY;

                    while (currentX != new_location.Item1 || currentY != new_location.Item2)
                    {
                        if (currentX < 0 || currentX >= 8 || currentY < 0 || currentY >= 8) //stop out of bound checking
                            return false;

                        if (_board.ChessGrid[currentX, currentY] != null) return false;

                        _board.ChessGrid[old_location.Item1, old_location.Item2] = null;
                        moved_piece.X = currentX;
                        moved_piece.Y = currentY;
                        _board.ChessGrid[moved_piece.X, moved_piece.Y] = moved_piece;

                        bool check = GameManager.Instance.InCheck(p);

                        _board.ChessGrid[moved_piece.X, moved_piece.Y] = null;
                        _board.ChessGrid[old_location.Item1, OldLocation.Item2] = moved_piece;
                        moved_piece.X = old_location.Item1;
                        moved_piece.Y = old_location.Item2;
                        

                        if (check) return false;

                        currentX += stepX;
                        currentY += stepY;
                    }
                    return true;

                default:
                    return false;
            }
        }
        

        public (int, int) OldLocation
        {
            get { return old_location; }
        }

        public (int, int) NewLocation
        {
            get { return new_location; }
        }

        public ChessPiece CapturedPiece
        {
            get { return captured_piece; }
        }

        public ChessPiece MovedPiece
        {
            get { return moved_piece; }
        }

        public MoveType MoveType
        {
            get { return _moveType; }
        }
    }
}

//string[] valid_input = new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "a", "b", "c", "d", "e", "f", "g", "h"};

//public override string Execute(string input)
//{
//    string[] command_list = input.Split(' ');
//    if (command_list.Length != 2)
//    {
//        return ("Invalid command");
//    }
//    string start = command_list[0];
//    string end = command_list[1];

//    string start_valid = CheckValidInput(start);
//    string end_valid = CheckValidInput(end);
//    string additional_output = "";

//    if (!(start_valid == "valid"))
//    {
//        return start_valid;
//    }
//    else if (!(end_valid == "valid"))
//    {
//        return end_valid;
//    }
//    else
//    {
//        List<string> start_position = start.Select(c => c.ToString()).ToList();
//        List<string> end_position = end.Select(c => c.ToString()).ToList();


//        int start_row = start_position[0][0] - 'a';
//        int start_col = int.Parse(start_position[1]) - 1;
//        int end_row = end_position[0][0] - 'a';
//        int end_col = int.Parse(end_position[1]) - 1;

//        ChessPiece piece = _board.ChessGrid[start_row, start_col];
//        ChessPiece target = _board.ChessGrid[end_row, end_col];

//        if (piece == null) return "There is no piece in the starting location";
//        if (!(_player.Pieces.Contains(piece))) return "The piece you want to move is not yours";

//        bool valid_move = piece.MoveSet.ValidMove(piece, _board, start_row, start_col, end_row, end_col);
//        bool check_obstruction = piece.MoveSet.CheckObstruction(piece, _board, start_row, start_col, end_row, end_col);

//        if (valid_move == false || check_obstruction == false) return "The move is invalid";
//        if (target != null)
//        {
//            bool capture = piece.MoveSet.CheckCapture(piece, _board, end_row, end_col);
//            if (capture == false) return "The end location is occupied by another piece";
//            else { additional_output = ".Captured the " + target.Name; }
//        }

//        //for undo
//        old_location = (start_row, start_col);
//        new_location = (end_row, end_col);
//        captured_piece = _board.ChessGrid[end_row, end_col];
//        moved_piece = piece;

//        piece.X = end_row;
//        piece.Y = end_col;
//        _board.ChessGrid[start_row, start_col] = null;
//        _board.ChessGrid[end_row, end_col] = piece;
//        return "Move executed" + additional_output;
//    }
//}
//public override string CheckValidInput(string check)
//{
//    char[] char_list = check.ToCharArray();
//    if (char_list.Length != 2) return "Invalid command length";
//    if (!char.IsLetter(char_list[0]) || !char.IsDigit(char_list[1])) return "Invalid command format";
//    if (!valid_input.Contains(char_list[1].ToString()) || !valid_input.Contains(char_list[0].ToString())) return "Invalid position";
//    return "valid";
//}