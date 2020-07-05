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
        public Tile[,] mapGrid = new Tile[28, 13];
        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> objectsToAdd = new List<GameObject>();
        private List<GameObject> objectsToDelete = new List<GameObject>();
        Random generator = new Random();
        private Game game;
        public Map(Game game)
        {
            this.game = game;

        }
        public void Draw(Graphics g)
        {
            //TODO
        }
        public void Step()
        {
            //TODO
        }
        public void IsFree()
        {
            //TODO
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
        public void FreePlayer()
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
