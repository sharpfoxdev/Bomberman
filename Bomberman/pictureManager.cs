using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class PictureManager
    {
        public Bitmap wall {get; set;}
        public Bitmap wood { get; set; }
        public Bitmap sand { get; set; }
        public Bitmap playerUp { get; set; }
        public Bitmap playerDown { get; set; }
        public Bitmap playerLeft { get; set; }
        public Bitmap playerRight { get; set; }
        public Bitmap bomb { get; set; }
        public Bitmap explosion { get; set; }
        public void LoadPictures()
        {
            wall = new Bitmap("wall.png");
            wood = new Bitmap("wood.png");
            sand = new Bitmap("sand.png");
            playerUp = new Bitmap("playerUp.png");
            playerDown = new Bitmap("playerDown.png");
            playerLeft = new Bitmap("playerLeft.png");
            playerRight = new Bitmap("playerRight.png");
            bomb = new Bitmap("bomb.png");
            explosion = new Bitmap("explosion.png");
        }
    }
}
