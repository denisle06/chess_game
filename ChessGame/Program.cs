using ChessGame.CommandSpace;
using ChessGame.Pieces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Board board = new Board();
            List<Player> players = new List<Player>();


            Player whitePlayer = new Player(Color.White, board);
            Player blackPlayer = new Player(Color.Black, board);

            players.Add(whitePlayer);
            players.Add(blackPlayer);

            // Place pieces on the board
            board.createGrid(players);

        }
    }
}
