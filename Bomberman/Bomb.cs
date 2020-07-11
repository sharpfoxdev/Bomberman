using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Bomb : GameObject
    {
        private int bombStrenght;
        private int timeTillDentonation = 200;
        private int whoPlacedIt;
        public Bomb(Game game, int bombStrenght, int whoPlacedIt)
        {
            this.game = game;
            visible = true;
            pickable = false;
            this.whoPlacedIt = whoPlacedIt;
            this.bombStrenght = bombStrenght;
            picture = game.pictureManager.bomb;
        }
        public override void Step()
        {
            timeTillDentonation--;
            if (timeTillDentonation <= 0)
            {
                game.players[whoPlacedIt].amountOfBombs++; //whoPlaced it tells us the number of player with which we can index into list of players
                game.map.DeleteObject(this);
                Explosion explosion = new Explosion(game);
                game.map.AddObject(explosion);
                explosion.position = position;//explosion in a place of bomb
                MakeExplosion(Direction.UP);
                MakeExplosion(Direction.DOWN);
                MakeExplosion(Direction.LEFT);
                MakeExplosion(Direction.RIGHT);
            }
        }
        void MakeExplosion(Direction direction)
        {
            for(int i = 0; i < bombStrenght; i++)
            {
                Explosion explosion = new Explosion(game);
                switch (direction)//find the position where to place the explosion
                {
                    case Direction.UP:
                        explosion.position.X = position.X;//doesnt change on x axis
                        explosion.position.Y = position.Y - ((i + 1) * game.tileSize);
                        break;
                    case Direction.DOWN:
                        explosion.position.X = position.X;
                        explosion.position.Y = position.Y + ((i + 1) * game.tileSize);
                        break;
                    case Direction.LEFT:
                        explosion.position.X = position.X - ((i + 1) * game.tileSize);
                        explosion.position.Y = position.Y;
                        break;
                    case Direction.RIGHT:
                        explosion.position.X = position.X + ((i + 1) * game.tileSize);
                        explosion.position.Y = position.Y;
                        break;
                    default:
                        break;
                }
                if (game.map.IsStepable(explosion.position.X, explosion.position.Y))//check if you can place it there
                {
                    game.map.AddObject(explosion);
                }
                else if (game.map.IsDestroyable(explosion.position.X, explosion.position.Y))//stops with first destroyable tile
                {
                    game.map.AddObject(explosion);
                    break;
                }
                else//wall, insnt stepable nor derstroyable, we dont put explosion there
                {
                    break;
                }
            }
        }
    }
}
