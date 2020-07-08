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
        private int timeTillDentonation = 100;
        public Bomb(Game game, int bombStrenght) : base(game)
        {

            this.bombStrenght = bombStrenght;
            picture = game.pictureManager.bomb;
        }
        public override void Step()
        {
            timeTillDentonation--;
            if (timeTillDentonation <= 0)
            {
                game.map.DeleteObject(this);
            }
        }
    }
}
