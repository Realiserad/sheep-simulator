using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace Fårsimulator
{
    public class Wolf : Animal
    {
        public Wolf(Point pos) : base(pos)
        {
            maxAge = Form1.r.Next(Form1.maxAgeForWolf); // set maxAge
            speed = Form1.wolfSpeed;
        }

        public Wolf()
        {
            maxAge = Form1.r.Next(Form1.maxAgeForWolf); // set maxAge
            speed = Form1.wolfSpeed;
        }
    }
}
