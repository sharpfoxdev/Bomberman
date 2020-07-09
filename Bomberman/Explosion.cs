﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Explosion : GameObject
    {
        private int timeOfExploding = 50;
        public Explosion(Game game) : base(game)
        {
            picture = game.pictureManager.explosion;
        }
        public override void Step()
        {
            timeOfExploding--;
            if (!game.map.mapGrid[position.X / 46, position.Y / 46].stepable && game.map.mapGrid[position.X / 46, position.Y / 46].destroyable)
            {
                game.map.mapGrid[position.X / 46, position.Y / 46] = new Tile(game.pictureManager.sand, true, false);//becomes sand
            }
            if (timeOfExploding <= 0)
            {
                game.map.DeleteObject(this);
            }
            if (Collision(game.player1))
            {
                game.player1.dead = true;
            }
            if (Collision(game.player2))
            {
                game.player2.dead = true;
            }
        }

    }
}