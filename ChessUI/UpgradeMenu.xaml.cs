using ChessGame;
using ChessGame.Upgrade;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace ChessUI
{
        /// <summary>
        /// Interaction logic for UpgradeMenu.xaml
        /// </summary>
        public partial class UpgradeMenu : UserControl
        {
            private IUpgrade _upgrade1;
            private IUpgrade _upgrade2;
            public event Action<IUpgrade> UpgradeSelected;
            private static readonly Random _rand = new Random();

            public UpgradeMenu()
            {
                InitializeComponent();
                GenerateRandomUpgrades(); // call on startup or manually later
            }

            public void GenerateRandomUpgrades()
            {
            // Generate 2 distinct upgrade IDs from 1–7
                int id1;
                do
                {
                    id1 = _rand.Next(1, 7);
                } while (!GameManager.Instance.ID_List.Contains(id1));
                
                int id2;
                do
                {
                    id2 = _rand.Next(1, 7);
                } while (id2 == id1 || !GameManager.Instance.ID_List.Contains(id2));

                GameManager.Instance.ID_List.Remove(id1);
                GameManager.Instance.ID_List.Remove(id2);
                // Create upgrades
                _upgrade1 = UpgradeFactory.CreateUpgrade(id1);
                _upgrade2 = UpgradeFactory.CreateUpgrade(id2);

                // Set upgrade info to text (replace with actual info if needed)
                FirstUpgradeText.Text = $"Upgrade #{id1}: {_upgrade1.Description}";
                SecondUpgradeText.Text = $"Upgrade #{id2}: {_upgrade2.Description}";
            }

            private void Choose_Upgrade_1(object sender, RoutedEventArgs e)
            {
                _upgrade1?.Apply();
                Debug.WriteLine($"Applied {_upgrade1.GetType().Name}!");
                UpgradeSelected?.Invoke(_upgrade1);
            }

            private void Choose_Upgrade_2(object sender, RoutedEventArgs e)
            {
                _upgrade2?.Apply();
                Debug.WriteLine($"Applied {_upgrade2.GetType().Name}!");
                UpgradeSelected?.Invoke(_upgrade2);
            }
        }
    }
