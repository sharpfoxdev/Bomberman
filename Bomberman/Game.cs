using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Game
    {
        public Map map;
        public Player player;
        public bool gameOver;
        public PictureManager pictureManager;
        public Game()
        {
            pictureManager = new PictureManager();
            pictureManager.LoadPictures();
            player = new Player(this);
            map = new Map(this);
        }
        public void Draw(Graphics g)
        {
            map.Draw(g);
            player.Draw(g);
        }
    }
}
