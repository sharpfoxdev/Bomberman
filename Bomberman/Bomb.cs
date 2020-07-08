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
        public Bomb(Game game, int bombStrenght, int whoPlacedIt) : base(game)
        {
            this.whoPlacedIt = whoPlacedIt;
            this.bombStrenght = bombStrenght;
            picture = game.pictureManager.bomb;
        }
        public override void Step()
        {
            timeTillDentonation--;
            if (timeTillDentonation <= 0)
            {
                if(whoPlacedIt == 1)
                {
                    game.player1.amountOfBombs++;//gives back the bomb
                }
                else
                {
                    game.player2.amountOfBombs++;
                }
                game.map.DeleteObject(this);
                Explosion explosion = new Explosion(game);
                game.map.AddObject(explosion);
                explosion.position = position;
                //predelat tenhle humac
                for (int i = 0; i < bombStrenght; i++)
                {
                    Explosion explosionUp = new Explosion(game);
                    explosionUp.position.X = position.X;//doesnt change on x axis
                    explosionUp.position.Y = position.Y - ((i + 1) * 46);//zacnu o jednu dal od stredu
                    game.map.AddObject(explosionUp);
                    if (!game.map.IsFree(explosionUp.position.X, explosionUp.position.Y))//a tim skoncim jakmile dorazim k prvni prekazce
                    {
                        break;
                    }
                }
                for (int i = 0; i < bombStrenght; i++)
                {
                    Explosion explosionDown = new Explosion(game);
                    explosionDown.position.X = position.X;
                    explosionDown.position.Y = position.Y + ((i + 1) * 46);
                    game.map.AddObject(explosionDown);
                    if (!game.map.IsFree(explosionDown.position.X, explosionDown.position.Y))
                    {
                        break;
                    }
                }
                for (int i = 0; i < bombStrenght; i++)
                {
                    Explosion explosionLeft = new Explosion(game);
                    explosionLeft.position.X = position.X - ((i + 1) * 46);
                    explosionLeft.position.Y = position.Y;
                    game.map.AddObject(explosionLeft);
                    if (!game.map.IsFree(explosionLeft.position.X, explosionLeft.position.Y))
                    {
                        break;
                    }
                }
                for (int i = 0; i < bombStrenght; i++)
                {
                    Explosion explosionUp = new Explosion(game);
                    explosionUp.position.X = position.X + ((i + 1) * 46);
                    explosionUp.position.Y = position.Y;
                    game.map.AddObject(explosionUp);
                    if (!game.map.IsFree(explosionUp.position.X, explosionUp.position.Y))
                    {
                        break;
                    }
                }
            }
        }
    }
}
