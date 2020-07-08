﻿using System;
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
        public Bitmap player1Up { get; set; }
        public Bitmap player1Down { get; set; }
        public Bitmap player1Left { get; set; }
        public Bitmap player1Right { get; set; }
        public Bitmap player2Up { get; set; }
        public Bitmap player2Down { get; set; }
        public Bitmap player2Left { get; set; }
        public Bitmap player2Right { get; set; }
        public Bitmap bomb { get; set; }
        public Bitmap explosion { get; set; }
        public void LoadPictures()
        {
            wall = new Bitmap("wall.png");
            wood = new Bitmap("wood.png");
            sand = new Bitmap("sand.png");
            player1Up = new Bitmap("player1Up.png");
            player1Down = new Bitmap("player1Down.png");
            player1Left = new Bitmap("player1Left.png");
            player1Right = new Bitmap("player1Right.png");
            player2Up = new Bitmap("player2Up.png");
            player2Down = new Bitmap("player2Down.png");
            player2Left = new Bitmap("player2Left.png");
            player2Right = new Bitmap("player2Right.png");
            bomb = new Bitmap("bomb.png");
            explosion = new Bitmap("explosion.png");
        }
    }
}
