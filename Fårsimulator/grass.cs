using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Fårsimulator
{
    public class Grass : Terrain
    {
        public Grass(Point pos) : base(pos)
        {
            // set health
            health = 80;
        }

        public Grass(Point pos, int health) : base(pos, health)
        {
            this.health = health;
        }
    }
}
