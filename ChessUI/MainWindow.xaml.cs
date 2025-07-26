using ChessGame;
using ChessGame.CommandSpace;
using ChessGame.Pieces;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    ///due to logic mismatch between the wdf display, which 
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];

        private GameManager _game;
        private (int, int)? selected_pos;
        private List<(int, int)> avai_moves;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            _game = new GameManager();
            DrawBoard(_game.Board);
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Image image = new Image();
                    pieceImages[row, col] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[row, col] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    ChessPiece piece = board.ChessGrid[row, col];
                    pieceImages[row, col].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double square_size = BoardGrid.ActualWidth / 8;

            int y = (int)(e.GetPosition(BoardGrid).X / square_size);
            int x = (int)(e.GetPosition(BoardGrid).Y / square_size);

            Debug.WriteLine(x.ToString() + y.ToString());

            if (selected_pos == null) SelectPosition(x, y);
            else MoveToPosition(x, y);
        }

        private void SelectPosition(int x, int y)
        {
            if (_game.Board.ChessGrid[x, y] == null)
            {
                Debug.WriteLine("no piece at location");
                return; 
            }
            List<(int, int)> moves = _game.PieceLegalMove(_game.Board.ChessGrid[x, y]);

            foreach ((int, int) move in moves) Debug.WriteLine($"Legal moves in location {move.Item1}, {move.Item2}");


            if (moves.Any())
            {
                selected_pos = (x, y);
                avai_moves = moves;
                ShowHighlights();
            }

        }

        private void MoveToPosition(int x, int y)
        {
            if (avai_moves.Contains((x, y))) 
            {
                MoveCommand move = new MoveCommand(_game.Board);
                if (selected_pos is (int row, int col))
                {
                    move.Execute(_game.Board.ChessGrid[row, col], (x, y));
                    DrawBoard(_game.Board);
                    
                }
            }

            selected_pos = null;
            HideHighlights();
        }

        private void ShowHighlights()
        {
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(150, 125, 255, 125);
            //avai_moves = _game.PieceLegalMove(piece);

            foreach ((int, int) pos in avai_moves)
            {
                highlights[pos.Item1, pos.Item2].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach ((int, int) pos in avai_moves)
            {
                highlights[pos.Item1, pos.Item2].Fill = Brushes.Transparent;
            }
        }


    }
}