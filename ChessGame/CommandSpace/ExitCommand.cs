using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.CommandSpace
{
    public class ExitCommand : Command
    {
        public ExitCommand(Board board) : base(board)
        {
        }

        public override string Execute(string input)
        {
            Console.WriteLine("Do you want to exit the game? Type y to confirm");
            string confirm = Console.ReadLine();
            if (confirm == "y") return "exit";
            else return null;
        }

        public override string CheckValidInput(string check)
        {
            throw new NotImplementedException();
        }

    }
}
