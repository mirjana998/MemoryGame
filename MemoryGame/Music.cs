using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media;

namespace MemoryGame
{
    public class Music
    {
        public static MediaPlayer backgroundPlayer = new MediaPlayer();
        public static MediaPlayer player = new MediaPlayer();

      
        public static void PlayMusic(string filePath)
        {
            player.Open(new Uri(filePath));
            player.Play();
        }


        public static void playCardFlip()
        {
            PlayMusic("C:\\Users\\LENOVO\\Projects\\MemoryGame\\MemoryGame\\assets\\music\\cardFlip.wav");
        }
    

        public static void playBackgroundMusic()
        {
            backgroundPlayer.Open(new Uri("C:\\Users\\LENOVO\\Projects\\MemoryGame\\MemoryGame\\assets\\music\\backgroundSong.wav"));
            backgroundPlayer.MediaEnded += (sender, e) =>
            {
                backgroundPlayer.Position = TimeSpan.Zero; 
                backgroundPlayer.Play();
            };
            backgroundPlayer.Play();
        }

        
    }
}
