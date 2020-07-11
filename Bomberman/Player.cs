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
        public int bombStrenght { get; set; }
        public bool dead { get; set; }
        public Direction orientation { get; set; }
        public int amountOfBombs { get; set; }
        public int timeSpeededUp { get; set; }
        private int speed;
        private int numberOfPlayer;
        public Player(Game game, int numberOfPlayer) //: base(game)//BASE hra?
        {
            this.game = game;
            visible = true;
            pickable = false;
            this.numberOfPlayer = numberOfPlayer;
            bombStrenght = 1;
            dead = false;
            orientation = Direction.NONE;
            amountOfBombs = 1;
            speed = 2;
            if (numberOfPlayer == 0)
            {
                picture = game.pictureManager.player1Down;
            }
            else
            {
                picture = game.pictureManager.player2Down;
            }
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
                //check whether both corners fit in where Im walking
                case Direction.DOWN:
                    if (game.map.IsStepable(position.X, position.Y + speed + (game.playerSize - 1)) && game.map.IsStepable(position.X + (game.playerSize - 1), position.Y + speed + (game.playerSize - 1)))
                    {
                        position.Y += speed;
                    }
                    break;
                case Direction.UP:
                    if (game.map.IsStepable(position.X, position.Y - speed) && game.map.IsStepable(position.X + (game.playerSize - 1), position.Y - speed))
                        position.Y -= speed;
                    break;
                case Direction.LEFT:
                    if (game.map.IsStepable(position.X - speed, position.Y) && game.map.IsStepable(position.X - speed, position.Y + (game.playerSize - 1)))
                        position.X -= speed;
                    break;
                case Direction.RIGHT:
                    if (game.map.IsStepable(position.X + speed + (game.playerSize - 1), position.Y) && game.map.IsStepable(position.X + speed + (game.playerSize - 1), position.Y + (game.playerSize - 1)))
                        position.X += speed;
                    break;
                case Direction.NONE:
                    break;
                default:
                    break;
            }
        }
        public void PlaceBomb()
        {
            if (amountOfBombs > 0)
            {
                amountOfBombs--;
                Bomb bomb = new Bomb(game, bombStrenght, this);
                bomb.position = new Point(((int)Math.Round((double)position.X / game.tileSize)) * game.tileSize, ((int)Math.Round((double)position.Y / game.tileSize)) * game.tileSize); //to fit in the grid
                game.map.AddObject(bomb);
            } 
        }

    }
}
