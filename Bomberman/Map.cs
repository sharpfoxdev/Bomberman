using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    class Map
    {
        public Tile[,] mapGrid = new Tile[15, 10];
        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> objectsToAdd = new List<GameObject>();
        private List<GameObject> objectsToDelete = new List<GameObject>();
        int width;
        int height;
        Random generator = new Random();
        private Game game;
        public Map(Game game)
        {
            this.game = game;

            System.IO.StreamReader sr = new System.IO.StreamReader("plan.txt");

            width = int.Parse(sr.ReadLine());
            height = int.Parse(sr.ReadLine());
            //mapGrid = new Tile[width, height];

            //jedu tak jak ctu po radcich
            for(int y = 0; y < height; y++)//vyska - getlenght zjisti velikost pole v dane dimenzi
            {
                string line = sr.ReadLine();
                for(int x = 0; x < width; x++)//sirka
                {
                    char character = line[x];//prectu nacnteny radek po znacich
                    switch (character)
                    {
                        case 's': //sand
                            mapGrid[x, y] = new Tile(game.pictureManager.sand, true, false);
                            break;
                        case 'w'://wall
                            mapGrid[x, y] = new Tile(game.pictureManager.wall, false, false);
                            break;
                        case 'd': //wood
                            mapGrid[x, y] = new Tile(game.pictureManager.wood, false, true);
                            break;
                        case '1'://player1
                            mapGrid[x, y] = new Tile(game.pictureManager.sand, true, false);//on the spot will be sand
                            game.player1.position = new Point(x * 46, y * 46);
                            break;
                        case '2'://player2
                            mapGrid[x, y] = new Tile(game.pictureManager.sand, true, false);//on the spot will be sand
                            game.player2.position = new Point(x * 46, y * 46);
                            break;
                        default:
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
                        if (mapGrid[x,y] != null)//nenainicializovali jsme danou pozici (treba hrac)
                        {
                            mapGrid[x, y].Draw(x * 46, y * 46, g);
                        }
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
                if(obj.pickable && game.player1.Collision(obj))
                {
                    game.player1.Pick(obj);
                }
                else if (obj.pickable && game.player2.Collision(obj))
                {
                    game.player2.Pick(obj);
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

            game.player1.Step();
            game.player2.Step();
        }
        public bool IsFree(int x, int y)
        {
            int gridX = x / 46;
            int gridY = y / 46;
            if(mapGrid[gridX, gridY].stepable)
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
        public void PlacePlayer()
        {
            //TODO
        }
        public void FindWood()
        {
            //TODO
        }
        public List<GameObject> GetGameObjects()
        {
            return objects;
        }


    }
}
