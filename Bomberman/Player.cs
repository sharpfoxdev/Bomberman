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
        public enum MovementDirection
        {
            NONE,
            LEFT,
            RIGHT,
            DOWN,
            UP,
        }
        public int bombStrenght = 2;
        int numberOfPlayer;
        public MovementDirection orientation = MovementDirection.NONE;
        //TODO several bombs, speed
        List<GameObject> objects = new List<GameObject>();
        public Player(Game game, int numberOfPlayer) : base(game)//BASE hra?
        {
            this.numberOfPlayer = numberOfPlayer;
            if(numberOfPlayer == 1)
            {
                picture = game.pictureManager.player1Down;
            }
            else
            {
                picture = game.pictureManager.player2Down;
            }
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
