using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Player: GameObject
    {
        public int bombStrenght = 2;
        //TODO several bombs, speed
        List<GameObject> objects = new List<GameObject>();
        public Player(Game game) : base(game)//BASE hra?
        {
            picture = game.pictureManager.playerDown;
        }
        public void Pick(GameObject obj)
        {
            objects.Add(obj);
            game.map.DeleteObject(obj); //we picked it so it disappears
        }
        public override void Draw(Graphics g)//was missing system drawing in the top of source
        {
            base.Draw(g);
            //maybe TODO
        }
        public override void Step()
        {

        }
        public void PlaceBomb()
        {
            //TODO
        }

    }
}
