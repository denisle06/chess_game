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
            GameStateMachine game_state = new GameStateMachine();

            Player whitePlayer = new Player(Color.White, board);
            Player blackPlayer = new Player(Color.Black, board);

            players.Add(whitePlayer);
            players.Add(blackPlayer);

            // Place pieces on the board
            board.createGrid(players);

            while(true)
            {
                board.Draw();
                string turn = game_state.ChangeTurn();
                Console.WriteLine(turn);
                GameState state = game_state.State;
                Console.WriteLine("Choose one of the command types. The folowing types exist: move, exit.");

                string? commandType = Console.ReadLine(); //type of command

                while (string.IsNullOrEmpty(commandType) || CommandBuilder.CreateCommand(commandType, board) == null)
                {
                    Console.WriteLine("Command type is invalid. Please enter a valid command type:");
                    commandType = Console.ReadLine();
                }

                Command action = CommandBuilder.CreateCommand(commandType, board);
                Console.WriteLine("Create command with type: " + commandType);

                if (state == GameState.White)
                {
                    action.Player = whitePlayer;
                    whitePlayer.Command = action;
                    string? input = Console.ReadLine();
                    string output = whitePlayer.ExecuteCommand(input);

                    while (!output.StartsWith("Move executed"))
                    {
                        Console.WriteLine(output);
                        input = Console.ReadLine();
                        output = whitePlayer.ExecuteCommand(input);
                    }

                    Console.WriteLine(output);
                }

                if (state == GameState.Black)
                {
                    action.Player = blackPlayer;
                    blackPlayer.Command = action;
                    string? input = Console.ReadLine();
                    string output = blackPlayer.ExecuteCommand(input);

                    while (!output.StartsWith("Move executed"))
                    {
                        Console.WriteLine(output);
                        input = Console.ReadLine();
                        output = blackPlayer.ExecuteCommand(input);
                    }

                    Console.WriteLine(output);
                }
            }
        }
    }
}
