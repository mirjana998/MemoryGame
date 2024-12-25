using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for RangList.xaml
    /// </summary>
    public partial class Leaderboard : Window
    {
        List<Player> playerList = new List<Player>();
        
        public Leaderboard()
        {
            InitializeComponent();
            InitializeList("C:\\Users\\LENOVO\\Projects\\MemoryGame\\MemoryGame\\assets\\Score.txt");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void InitializeList(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('#');
                    Player player = new Player(parts[0], int.Parse(parts[1]));
                    playerList.Add(player);
                }
                var sortedPlayer = playerList.OrderBy(x => x.Time);
                lvLeaderboard.ItemsSource = sortedPlayer;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                
            }
        }

    }
}
