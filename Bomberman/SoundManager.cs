using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Bomberman
{
    class SoundManager
    {
        public SoundPlayer backgroundMusic { get; set; }
        public void LoadSounds()
        {
            backgroundMusic = new SoundPlayer("soundtrack.wav");
        }
        public void PlayBomb()
        {
            MediaPlayer bomb = new MediaPlayer();
            bomb.Open(new System.Uri("bomb.wav", UriKind.Relative));
            bomb.Play();
        }
        public void PlayBonus()
        {
            MediaPlayer bonus = new MediaPlayer();
            bonus.Open(new System.Uri("bonus.wav", UriKind.Relative));
            bonus.Play();
        }
    }
}
