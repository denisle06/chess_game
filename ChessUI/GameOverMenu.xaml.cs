using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessGame;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;
        public GameOverMenu()
        {
            InitializeComponent();

            string winner = GetWinnerText();
            string reason = GetReasonText();

            WinnerText.Text = winner;
            ReasonText.Text = reason;
        }

        public static string GetWinnerText()
        {
            return GameManager.Instance.Winner switch
            {
                Winner.White => "White won!",
                Winner.Black => "Black won!",
                Winner.None => "Draw!"
            };
        }

        public static string GetReasonText()
        {
            return GameManager.Instance.Reason switch
            {
                EndReason.Checkmate => $"Checkmate, {GameManager.Instance.Turn} won!",
                EndReason.Stalemate => $"Stalemate, {GameManager.Instance.Turn} can't move!",
                EndReason.InsufficientMaterial => $"Draw due to insufficient materials",
                EndReason.FiftyMoveRule => $"Draw due to fifty move rule",
                EndReason.None => $"Unknown reason",
            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
    }
}
