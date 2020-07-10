using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
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
        public int bombStrenght = 1;
        int numberOfPlayer;
        private bool dead = false;
        public MovementDirection orientation = MovementDirection.NONE;
        public int amountOfBombs = 1;
        public int timeSpeededUp;
        List<GameObject> objects = new List<GameObject>();
        public Player(Game game, int numberOfPlayer) : base(game)//BASE hra?
        {
            this.numberOfPlayer = numberOfPlayer;
            if(numberOfPlayer == 0)
            {
                picture = game.pictureManager.player1Down;
            }
            else
            {
                picture = game.pictureManager.player2Down;
            }
            speed = 2;
        }
        public bool IsDead()
        {
            return dead;
        }
        public void Died()
        {
            dead = true;
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
            if(timeSpeededUp > 0)
            {
                speed = 3;
                timeSpeededUp--;
            }
            else
            {
                speed = 2;
            }
            switch (orientation)
            {
                case MovementDirection.DOWN:
                    if(game.map.IsStepable(position.X, position.Y + speed + 31) && game.map.IsStepable(position.X + 31, position.Y + speed + 31))//check if both corners fit in where Im walking
                    {
                        position.Y += speed;
                    }
                    break;
                case MovementDirection.UP:
                    if (game.map.IsStepable(position.X, position.Y - speed) && game.map.IsStepable(position.X + 31, position.Y - speed))
                        position.Y -= speed;
                    break;
                case MovementDirection.LEFT:
                    if (game.map.IsStepable(position.X - speed, position.Y) && game.map.IsStepable(position.X - speed, position.Y + 31))
                        position.X -= speed;
                    break;
                case MovementDirection.RIGHT:
                    if (game.map.IsStepable(position.X + speed + 31, position.Y) && game.map.IsStepable(position.X + speed + 31, position.Y + 31))
                        position.X += speed;
                    break;
                case MovementDirection.NONE:
                    break;
                default:
                    break;
            }
        }
        public void PlaceBomb()
        {
            if(amountOfBombs > 0)
            {
                amountOfBombs--;
                Bomb bomb = new Bomb(game, bombStrenght, numberOfPlayer);
                bomb.position = new Point(((int)Math.Round(position.X / 46.0)) * 46, ((int)Math.Round(position.Y / 46.0)) * 46); //to fit in the grid
                game.map.AddObject(bomb);
            } 
        }

    }
}
