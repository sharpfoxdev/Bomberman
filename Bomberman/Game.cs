using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    public enum Direction
    {
        NONE,
        LEFT,
        RIGHT,
        DOWN,
        UP,
    }
    class Game
    {
        public Map map;
        //private Player player1, player2;
        public List<Player> players = new List<Player>();
        public bool gameOver;
        public PictureManager pictureManager;
        public int tileSize = 46;
        public int gameObjectSize = 32;
        private string pathToPlan = "plan.txt";
        private int amountOfPlayers = 2;
        public Game()
        {
            pictureManager = new PictureManager();
            pictureManager.LoadPictures();
            for(int i = 0; i < amountOfPlayers; i++)
            {
                Player player = new Player(this, i); //i = cislo hrace
                players.Add(player);
            }
            /*player1 = new Player(this, 0);
            player2 = new Player(this, 1);
            players.Add(player1);
            players.Add(player2);*/
            map = new Map(this, tileSize, gameObjectSize, pathToPlan);
        }
        public void Draw(Graphics g)
        {
            map.Draw(g);
            foreach(Player player in players) //vykresli vsechny hrace
            {
                player.Draw(g);
            }
        }
        public void KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    players[0].orientation = Direction.UP;
                    players[0].picture = pictureManager.player1Up;
                    break;
                case Keys.S:
                    players[0].orientation = Direction.DOWN;
                    players[0].picture = pictureManager.player1Down;
                    break;
                case Keys.A:
                    players[0].orientation = Direction.LEFT;
                    players[0].picture = pictureManager.player1Left;
                    break;
                case Keys.D:
                    players[0].orientation = Direction.RIGHT;
                    players[0].picture = pictureManager.player1Right;
                    break;
                case Keys.Space:
                    players[0].PlaceBomb();
                    break;
                case Keys.Up:
                    players[1].orientation = Direction.UP;
                    players[1].picture = pictureManager.player2Up;
                    break;
                case Keys.Down:
                    players[1].orientation = Direction.DOWN;
                    players[1].picture = pictureManager.player2Down;
                    break;
                case Keys.Left:
                    players[1].orientation = Direction.LEFT;
                    players[1].picture = pictureManager.player2Left;
                    break;
                case Keys.Right:
                    players[1].orientation = Direction.RIGHT;
                    players[1].picture = pictureManager.player2Right;
                    break;
                case Keys.ControlKey:
                    players[1].PlaceBomb();
                    break;
                default:
                    break;
            }
        }
        public void KeyUp(Keys key)
        {
            if (key == Keys.W || key == Keys.S || key == Keys.A || key == Keys.D)
            {
                players[0].orientation = Direction.NONE;
            }
            else if (key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right)
            {
                players[1].orientation = Direction.NONE;
            }
        }
        public void Step()
        {
            map.Step();
            if (players[0].IsDead() || players[1].IsDead())
            {
                gameOver = true;
            }
            foreach(Player player in players) //move all players
            {
                player.Step();
            }
        }
    }
}
