using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Tile
    {
        private Bitmap picture;
        public bool destroyable { get; set; }
        public bool stepable { get; set; } //can I step on it
        public Tile(Bitmap picture, bool stepable, bool destroyable)
        {
            this.stepable = stepable;
            this.destroyable = destroyable;
            this.picture = picture;
        }
        public void Draw(int x, int y, Graphics g)//tile draws itself
        {
            g.DrawImage(picture, x, y);
        }
    }
}
