using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public enum GameState { White, Black, Over }
    public class GameStateMachine 
    {

        public  GameState _state;
        public GameStateMachine() 
        {
            _state = GameState.Black; //change turn will be called before every move
        }


        public string ChangeTurn()
        {
            if (_state == GameState.White) { _state = GameState.Black; return "It's Black's turn"; }
            else { _state = GameState.White; return "It's White's turn"; }
        }

        public string GameOver()
        {
            _state = GameState.Over;
            return "Game Over. Press any key to continue";
        }
        public GameState State
        {
            get { return _state; }
        }
    }
}
