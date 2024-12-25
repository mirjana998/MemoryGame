using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for ImageCard.xaml
    /// </summary>
    public partial class ImageCard : UserControl
    {
        public static List<string> images = new List<string>();
        public static string[] imagesArray = {"\\MemoryGame;component\\assets\\pokemonCards\\1.png", "\\MemoryGame;component\\assets\\pokemonCards\\1.png", "\\MemoryGame;component\\assets\\pokemonCards\\2.png", "\\MemoryGame;component\\assets\\pokemonCards\\2.png", "\\MemoryGame;component\\assets\\pokemonCards\\3.png", "\\MemoryGame;component\\assets\\pokemonCards\\3.png", "\\MemoryGame;component\\assets\\pokemonCards\\4.png", "\\MemoryGame;component\\assets\\pokemonCards\\4.png", "\\MemoryGame;component\\assets\\pokemonCards\\5.png", "\\MemoryGame;component\\assets\\pokemonCards\\5.png", "\\MemoryGame;component\\assets\\pokemonCards\\6.png", "\\MemoryGame;component\\assets\\pokemonCards\\6.png", "\\MemoryGame;component\\assets\\pokemonCards\\7.png", "\\MemoryGame;component\\assets\\pokemonCards\\7.png", "\\MemoryGame;component\\assets\\pokemonCards\\8.png", "\\MemoryGame;component\\assets\\pokemonCards\\8.png" };
        public static bool added = false;

        public ImageCard()
        {
            if(!added)
            {
                images.AddRange(imagesArray);
                added = true;
            }
            this.imgCard = new Image();
            InitializeComponent();
            imageSource = "\\MemoryGame;component\\assets\\card.png";
            ChooseImage();
            InitializeImage(imageSource, imgCard);
            Game.images.Add(this);
            
        }
        public bool clickedCard = false;
        public bool matchedCard = false;
        public bool failedCard = false;

        public string imageSource { get; set; }
        public string imageSourcePrivate { get; set; }

        public void InitializeImage(string imageSource, Image image)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imageSource, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            image.Source = bitmapImage;
        }

        private void imgCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (SelectableCard())
            {
                switch(Game.SelectedCards())
                {
                    case 1:
                        Music.playCardFlip();
                        this.clickedCard = true;
                        this.TurnCard();
                        Game.selectedCard2 = this;
                        Game.CompareCards();
                        break;
                    case 2:
                        Music.playCardFlip();
                        this.clickedCard = true;
                        TurnCard();
                        Game.selectedCard1 = this;
                        break;
                    default:break;
                }
                
            }
            else
            {
                TurnCard();
            }
        }

        public void ChooseImage()
        {
            Random random = new Random();
            int index = random.Next(0,images.Count-1);
            imageSourcePrivate = images[index];
            images.Remove(imageSourcePrivate);
            
        }

        public void MarkCardMatched()
        {
            matchedCard = true;
        }

        public void MarkCardFailed()
        {
            failedCard = true;
        }

        public void TurnCard()
        {
            
            if (clickedCard)
            {
                //MessageBox.Show("TEKST", "Info", MessageBoxButton.OK, MessageBoxImage.None);
                InitializeImage(imageSourcePrivate, imgCard);
                
               
            }
            else if(matchedCard)
            {

                InitializeImage(imageSourcePrivate, imgCard);
                
            }
            else
            {
                
                InitializeImage(imageSource, imgCard);
            }
        }

        public bool SelectableCard()
        {
           
                if (clickedCard)
                    return false;
                if (matchedCard)
                    return false;

                return true;
            
        }

        public void PeekAtCard()
        {
            clickedCard = true;
            TurnCard();
        }

        public void ClosePeekAtCard()
        {
            clickedCard = false;
            failedCard = false;
            TurnCard();

        }


    }
}
