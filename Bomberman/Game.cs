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
    class Game
    {
        public Map map;
        public Player player1;
        public Player player2;
        public bool gameOver;
        public PictureManager pictureManager;
        public Game()
        {
            pictureManager = new PictureManager();
            pictureManager.LoadPictures();
            player1 = new Player(this, 1);
            player2 = new Player(this, 2);
            map = new Map(this);
        }
        public void Draw(Graphics g)
        {
            map.Draw(g);
            player1.Draw(g);
            player2.Draw(g);
        }
        public void KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    player1.orientation = Player.MovementDirection.UP;
                    player1.picture = pictureManager.player1Up;
                    break;
                case Keys.S:
                    player1.orientation = Player.MovementDirection.DOWN;
                    player1.picture = pictureManager.player1Down;
                    break;
                case Keys.A:
                    player1.orientation = Player.MovementDirection.LEFT;
                    player1.picture = pictureManager.player1Left;
                    break;
                case Keys.D:
                    player1.orientation = Player.MovementDirection.RIGHT;
                    player1.picture = pictureManager.player1Right;
                    break;
                case Keys.Space:
                    player1.PlaceBomb();
                    break;
                case Keys.Up:
                    player2.orientation = Player.MovementDirection.UP;
                    player2.picture = pictureManager.player2Up;
                    break;
                case Keys.Down:
                    player2.orientation = Player.MovementDirection.DOWN;
                    player2.picture = pictureManager.player2Down;
                    break;
                case Keys.Left:
                    player2.orientation = Player.MovementDirection.LEFT;
                    player2.picture = pictureManager.player2Left;
                    break;
                case Keys.Right:
                    player2.orientation = Player.MovementDirection.RIGHT;
                    player2.picture = pictureManager.player2Right;
                    break;
                case Keys.ControlKey:
                    player2.PlaceBomb();
                    break;
                default:
                    break;
            }
        }
        public void KeyUp(Keys key)
        {
            if (key == Keys.W || key == Keys.S || key == Keys.A || key == Keys.D)
            {
                player1.orientation = Player.MovementDirection.NONE;
            }
            else if (key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right)
            {
                player2.orientation = Player.MovementDirection.NONE;
            }
        }
    }
}
