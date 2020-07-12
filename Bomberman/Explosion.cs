using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Explosion : GameObject
    {
        private int timeOfExploding = 50;
        public Explosion(Game game)
        {
            this.game = game;
            picture = game.pictureManager.explosion;
            visible = true;
            pickable = false;
        }
        
        public override void Step()
        {
            timeOfExploding--;

            foreach (Player player in game.players)
            {
                if (Collision(player))
                {
                    player.dead = true;
                }
            }
            if (timeOfExploding <= 0)
            {
                if (!game.map.mapGrid[position.X / game.tileSize, position.Y / game.tileSize].stepable && game.map.mapGrid[position.X / game.tileSize, position.Y / game.tileSize].destroyable)
                {
                    game.map.mapGrid[position.X / game.tileSize, position.Y / game.tileSize] = new Tile(game.pictureManager.sand, true, false);//becomes sand
                }
                List<GameObject> objects = game.map.ReturnGameObjects();
                game.map.DeleteObject(this);
                foreach(GameObject obj in objects)
                {
                    
                    if (Collision(obj) && obj.visible && obj.pickable)
                    {
                        game.map.DeleteObject(obj);
                    }
                    if (Collision(obj) && !obj.visible)
                    {
                        obj.visible = true;
                    }
                }

            }
        }
    }
}
