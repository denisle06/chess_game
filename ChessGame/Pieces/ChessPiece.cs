using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.MoveSet;

namespace ChessGame.Pieces
{
    public abstract class ChessPiece
    {
        protected int _x;
        protected int _y;
        protected Color _color;
        protected string _name;
        protected bool _moved;
        protected PieceMoveSet _moveset;
        //protected MoveCommand _moveCommand;

        public ChessPiece(int x, int y, Color color)
        {
            _x = x;
            _y = y;
            _color = color;
            _name = makeName();
            _moved = false;
        }

        public string makeName()
        {
            return _color.ToString()[0] + " " + GetType().Name; //convert enum object to string + get its class to create a name for the piece
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Color Color { get { return _color; } }

        public bool Moved { get { return _moved; } set { _moved = value; } }

        public PieceMoveSet MoveSet { get { return _moveset; } }
    }
}
