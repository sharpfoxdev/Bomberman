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
        public int tileSize { get; set; }
        public int playerSize { get; set; }
        private string pathToPlan = "plan.txt";
        private int amountOfPlayers = 2;
        public bool gameOver { get; set; }
        public Map map { get; }
        public List<Player> players = new List<Player>();
        public PictureManager pictureManager { get; }
        public SoundManager soundManager { get; }
        public Game()
        {
            tileSize = 46;
            playerSize = 32;
            pictureManager = new PictureManager();
            pictureManager.LoadPictures();
            soundManager = new SoundManager();
            soundManager.LoadSounds();
            soundManager.backgroundMusic.PlayLooping();
            for(int i = 0; i < amountOfPlayers; i++)
            {
                Player player = new Player(this, i); //i = gives player its number
                players.Add(player);
            }
            map = new Map(this, tileSize, pathToPlan);
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
            if (players[0].dead || players[1].dead)
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
