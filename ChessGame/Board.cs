using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Pieces;

//Plan: movecommand check if legal move, aka if the move is in check undo it. board will implement the actual checkmate logic, aka get legal moves -> check if player have any legal move left.

namespace ChessGame
{
    public class Board
    {
        ChessPiece[,] ChessBoard = new ChessPiece[8, 8];

        public Board() { }

        public void createGrid(List<Player> playerList)
        {

            foreach (Player player in playerList)
            {
                foreach (ChessPiece piece in player.Pieces)
                {
                    ChessBoard[piece.X, piece.Y] = piece;
                }
            }
        }

        public void Draw()
        {
            int cellWidth = 9;
            // write a -> h
            Console.Write("   ");
            for (char c = 'a'; c <= 'h'; c++)
            {
                Console.Write(c.ToString().PadRight(cellWidth));
            }
            Console.WriteLine();

            for (int y = 7; y >= 0; y--)
            {
                Console.Write($"{y + 1}  ");

                // Loop over each column (a to h)
                for (int x = 0; x < 8; x++)
                {
                    ChessPiece piece = ChessBoard[x, y];

                    string displayText;
                    if (piece != null)
                    {
                        displayText = piece.Name;
                    }
                    else
                    {
                        displayText = ".";
                    }

                    // Print the text with padding to align columns
                    Console.Write(displayText.PadRight(cellWidth));
                }
                Console.WriteLine();
            }
        }

        public List<ChessPiece> GetAllPieces()
        {
            List<ChessPiece> chessPieces = new List<ChessPiece>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (ChessBoard[x,y] != null) chessPieces.Add(ChessBoard[x,y]);
                }
            }

            return chessPieces;
        }
        public ChessPiece[,] ChessGrid { get { return ChessBoard; } }
    }
}
