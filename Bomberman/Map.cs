using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    class Map
    {
        public Tile[,] mapGrid { get; set; }
        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> objectsToAdd = new List<GameObject>();
        private List<GameObject> objectsToDelete = new List<GameObject>();
        private int width;
        private int height;
        private Random generator = new Random();
        private Game game;
        private int tileSize;
        public Map(Game game, int tileSize, string pathToPlan)
        {
            this.game = game;
            this.tileSize = tileSize;

            System.IO.StreamReader sr = new System.IO.StreamReader(pathToPlan);

            width = int.Parse(sr.ReadLine());
            height = int.Parse(sr.ReadLine());

            mapGrid = new Tile[width, height];

            for (int y = 0; y < height; y++)
            {
                string line = sr.ReadLine();
                for(int x = 0; x < width; x++)
                {
                    char character = line[x];//reads the line character by character
                    switch (character)
                    {
                        case 's': //sand
                            mapGrid[x, y] = new Tile(game.pictureManager.sand, true, false);
                            break;
                        case 'w'://wall
                            mapGrid[x, y] = new Tile(game.pictureManager.wall, false, false);
                            break;
                        case 'c': //crate
                            mapGrid[x, y] = new Tile(game.pictureManager.crate, false, true);
                            int whatsInTheCrate = generator.Next(7);
                            if(whatsInTheCrate == 0)
                            {
                                BonusExplosion expl = new BonusExplosion(game);
                                expl.position = new Point(x * tileSize, y * tileSize);
                                objects.Add(expl);
                            }
                            else if (whatsInTheCrate == 1)
                            {
                                BonusBomb bomb = new BonusBomb(game);
                                bomb.position = new Point(x * tileSize, y * tileSize);
                                objects.Add(bomb);
                            }
                            else if (whatsInTheCrate == 2)
                            {
                                BonusSpeed speed = new BonusSpeed(game);
                                speed.position = new Point(x * tileSize, y * tileSize);
                                objects.Add(speed);
                            }
                            else//nothing inside
                            {

                            }
                            break;
                        case '1'://player1
                            mapGrid[x, y] = new Tile(game.pictureManager.sand, true, false);//on the spot will be sand
                            game.players[0].position = new Point(x * tileSize, y * tileSize);
                            break;
                        case '2'://player2
                            mapGrid[x, y] = new Tile(game.pictureManager.sand, true, false);//on the spot will be sand
                            game.players[1].position = new Point(x * tileSize, y * tileSize);
                            break;
                        default:
                            mapGrid[x, y] = new Tile(game.pictureManager.sand, true, false); //neznamy znak v planu
                            break;
                    }
                }
            }

        }
        public void Draw(Graphics g)
        {
            for (int y = 0; y < mapGrid.GetLength(1); y++)//draw tiles
            {
                {
                    for (int x = 0; x < mapGrid.GetLength(0); x++)
                    {
                            mapGrid[x, y].Draw(x * tileSize, y * tileSize, g);
                    }
                }
            }
            foreach (GameObject obj in objects)//draw objects
            {
                if (obj.visible)
                {
                    obj.Draw(g);
                }
            }
        }
        public void Step()
        {
            foreach(GameObject obj in objects)
            {
                obj.Step();
                foreach(Player player in game.players)
                {
                    if(obj.pickable && player.Collision(obj))//player picked bonus
                    {
                        game.soundManager.PlayBonus();
                        DeleteObject(obj);
                    }
                }
            }
            foreach(GameObject obj in objectsToDelete)
            {
                objects.Remove(obj);
            }
            objectsToDelete.Clear();//deleted all of them
            foreach(GameObject obj in objectsToAdd)
            {
                objects.Add(obj);
            }
            objectsToAdd.Clear();
            
        }
        public bool IsStepable(int x, int y)
        {
            int gridX = x / tileSize;
            int gridY = y / tileSize;
            if(mapGrid[gridX, gridY].stepable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDestroyable(int x, int y)
        {
            int gridX = x / tileSize;
            int gridY = y / tileSize;
            if (mapGrid[gridX, gridY].destroyable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteObject(GameObject obj)
        {
            objectsToDelete.Add(obj);
        }
        public void AddObject(GameObject obj)
        {
            objectsToAdd.Add(obj);
        }
        public List<GameObject> ReturnGameObjects()
        {
            return objects;
        }
    }
}
