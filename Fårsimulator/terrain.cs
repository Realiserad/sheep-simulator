using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace Fårsimulator
{
    public abstract class Terrain
    {
        // All virtual functions assume that the terrain is grass by default. Once a pool object is
        // created those functions will be overridden by corresponding functions in the pool class

        protected Point pos;
        protected Bitmap b = Form1.b;
        protected bool isWater = false; // type of terrain
        protected int health; // health of pool or grass area varies between 0 and maxHealth
        protected const int maxHealth = 100; // maximal health

        public Terrain(Point position)
        {
            // Assign pos according to the value provided
            this.pos = position;
        }

        public Terrain(Point position, int health)
        {
            // Assign pos according to the value provided
            this.pos = position;
        }

        public virtual void Draw()
        {
            Graphics g = Graphics.FromImage(b);
            SolidBrush brush = new SolidBrush(GetColor());
            g.FillRectangle(brush, pos.X, pos.Y, 10, 10); // draw filled rectangle on pos with a height and width of 10
            // dispose objects to avoid memory leak
            brush.Dispose();
            g.Dispose();
        }

        protected virtual Color GetColor() // returns a corresponding color depending on the value of health
        {
            int factor = Convert.ToInt32(Math.Round(health / (double)maxHealth * 100));
            Color color = Color.FromArgb(255 - factor, 255, 120);
            if (Form1.nightMode) // invert color
                color = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
            return color;
        }

        public void ChangeHealth(int value) // increase or decrease health
        {
            health += value;

            if (health < 1) // avoid division with zero
                health = 1;
        }

        public int Health // get health
        {
            get { return health; }
        }

        public bool IsWater // get the type of terrain
        {
            get { return isWater; }
        }

        public Point Position // get pos
        {
            get { return pos; }
        }
    }
}
