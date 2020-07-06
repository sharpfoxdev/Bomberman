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
        public Bitmap wall { get; set;} //normal variable instead of get set property should work too
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
            playerUp = new Bitmap("player1Up.png");
            playerDown = new Bitmap("player1Down.png");
            playerLeft = new Bitmap("player1Left.png");
            playerRight = new Bitmap("player1Right.png");
            bomb = new Bitmap("bomb.png");
            explosion = new Bitmap("explosion.png");
        }
    }
}
