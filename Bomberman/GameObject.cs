using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    abstract class GameObject
    {
        public Bitmap picture;
        public Point position;
        public int speed;
        protected Game game;
        public bool pickable { get; set; }
        public bool visible { get; set; }
        protected static Random generator = new Random();
        public GameObject(Game game)
        {
            visible = true;
            pickable = false;
            this.game = game;
        }
        public virtual void Draw(Graphics g)
        {
            g.DrawImage(picture, position); //sprite draws itself, is called from map draw
        }
        public abstract void Step();//takes care of what the sprite should do every step
        public bool Collision(GameObject obj)
        {
            Rectangle object1 = new Rectangle(position, picture.Size); //place and size of sprite on map
            Rectangle object2 = new Rectangle(obj.position, obj.picture.Size); //the other sprite
            return (object1.IntersectsWith(object2));
        }

    }
}
