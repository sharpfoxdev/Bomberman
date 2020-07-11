using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class BonusExplosion : GameObject
    {

        public BonusExplosion(Game game)
        {
            this.game = game;
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
        }
    }
}
