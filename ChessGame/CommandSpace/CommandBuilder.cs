using ChessGame.Pieces;
using ChessGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.CommandSpace
{
    public class CommandBuilder
    {
        public static Command CreateCommand(string type, Board board)
        {
            switch (type.ToLower())
            {
                case "move":
                    return new MoveCommand(board);
                case "exit":
                    return new ExitCommand(board);
                default:
                    return null;
            }
        }
    }
}
