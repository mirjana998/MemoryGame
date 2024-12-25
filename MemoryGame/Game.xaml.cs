using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Threading;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.IO;


namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public static List<ImageCard> images = new List<ImageCard>();

        public static ImageCard? selectedCard1;
        public static ImageCard? selectedCard2;

        public static TextBox nameText = new TextBox();

        public static DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public static TextBox timeText = new TextBox();

        public Game()
        {
            InitializeComponent();
            InitializeName();
            InitializeTimer();

        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            ImageCard.added = false;
            dispatcherTimer.Stop();
            this.Close();
        }

        
        public static int SelectedCards()
        {
            if (selectedCard1 == null && selectedCard2 == null)
            {
                return 2;
            }
            else if (selectedCard1 != null && selectedCard2 == null)
            {
                return 1;
            }
            return 0;
        }

        public async static void SetNotSelected()
        { 
            selectedCard1 = null;
            selectedCard2 = null;
        }

        public async static void CompareCards()
        {
            if (selectedCard1.imageSourcePrivate.Equals(selectedCard2.imageSourcePrivate))
            {
                selectedCard1.MarkCardMatched();
                selectedCard2.MarkCardMatched();
                SetNotSelected();
                if(checkAllMatched())
                {
                    dispatcherTimer.Stop();
                }
            }
            else
            {
                await Task.Delay(2000);
                selectedCard1.clickedCard = false;
                selectedCard2.clickedCard = false;
                selectedCard1.TurnCard();
                selectedCard2.TurnCard();
                SetNotSelected() ;

            }
        }

        public void InitializeName()
        {
            nameText.Name = "tbNameText";
            nameText.Text = ""; 
            nameText.FontSize = 25;
            nameText.FontWeight = FontWeights.Bold; 
            nameText.Height = 80; 
            nameText.VerticalAlignment = VerticalAlignment.Top;
            nameText.Margin = new Thickness(0, 350, 0, 0); 
            nameText.Background = Brushes.Transparent;
            if (nameText.Parent != null)
            {
                ((Grid)nameText.Parent).Children.Remove(nameText);
            }
            Grid.SetColumn(nameText, 2); 
            myGrid.Children.Add(nameText);
        }

        public void InitializeTimer()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();

            timeText.Name = "tbTimeText";
            timeText.Text = "0";
            timeText.FontSize = 25;
            timeText.FontWeight = FontWeights.Bold;
            timeText.Height = 80;
            timeText.VerticalAlignment = VerticalAlignment.Top;
            timeText.Margin = new Thickness(0, 250, 0, 0);
            timeText.Background = Brushes.Transparent;
            timeText.BorderBrush = Brushes.Transparent;
            timeText.IsEnabled = false;
            if (timeText.Parent != null)
            {
                ((Grid)timeText.Parent).Children.Remove(timeText);
            }
            Grid.SetColumn(timeText, 2);
            myGrid.Children.Add(timeText);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            timeText.Text = ((int.Parse(timeText.Text)) + 1).ToString();
            CommandManager.InvalidateRequerySuggested();
        }

        public static bool checkAllMatched()
        {
            int matchedNumber = 0;
            foreach(ImageCard imageCard in images)
            {
                if(imageCard.matchedCard)
                    matchedNumber++;
            }
            if (matchedNumber == 16)
                return true;
            return false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkAllMatched())
            {
                if(String.IsNullOrEmpty(nameText.Text))
                {
                    MessageBox.Show("Name missing!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    WriteToFile("C:\\Users\\LENOVO\\Projects\\MemoryGame\\MemoryGame\\assets\\Score.txt", nameText.Text + "#" + timeText.Text);
                    MessageBox.Show("Saved successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else  
            {
                MessageBox.Show("All cards not matched!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
        }

        static void WriteToFile(string filePath, string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.Write("\n" + content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }

}
