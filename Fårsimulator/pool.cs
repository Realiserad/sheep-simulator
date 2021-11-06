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
    public class Pool : Terrain
    {
        public Pool(Point pos) : base(pos)
        {
            // set terrain to water
            isWater = true;
            // set health
            health = Form1.r.Next(100);
        }

        public Pool(Point pos, int health) : base(pos, health)
        {
            // set terrain to water
            isWater = true;
            // set health
            this.health = health;
        }

        protected override Color GetColor() // Return a corresponding color depending on the value of health.
        {
            int factor = Convert.ToInt32(Math.Round(health / (double)maxHealth * 100));
            Color color = Color.FromArgb(255 - factor, 255 - factor, 255);
            if (Form1.nightMode) // invert color
                color = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
            return color;
        }

        public override void Draw()
        {
            Graphics g = Graphics.FromImage(b);
            SolidBrush brush = new SolidBrush(GetColor());
            g.FillRectangle(brush, pos.X, pos.Y, 10, 10); // draw filled rectangle on pos with a height and width of 10
            // dispose objects to avoid memory leak
            brush.Dispose();
            g.Dispose();
        }
    }
}
