using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.CommandSpace
{
    public abstract class Command
    {
        protected Board _board;
        protected Player _player;
        public Command(Board board)
        {
            _board = board;
        }
        public abstract string Execute(string input);

        public abstract string CheckValidInput(string check);

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
    }
}
