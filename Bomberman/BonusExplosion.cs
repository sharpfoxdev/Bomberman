using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class BonusExplosion : GameObject
    {

        public BonusExplosion(Game game) : base(game)
        {
            picture = game.pictureManager.bonusExplosion;
            visible = false;
            pickable = true;
        }
        public override void Step()
        {
            foreach(Player player in game.players)//zjistim, ktery z hracu sebral bonus
            {
                if (Collision(player))
                {
                    player.bombStrenght++;
                }
            }
            /*if (Collision(game.player1))
            {
                game.player1.bombStrenght++;
            }
            else if (Collision(game.player2))
            {
                game.player2.bombStrenght++;
            }*/
        }
    }
}
