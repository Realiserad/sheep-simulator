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
    public class Sheep : Animal
    {
        bool lonely = false;

        public Sheep(Point pos) : base(pos)
        {
            maxAge = Form1.r.Next(Form1.maxAgeForSheep); // set maxAge
            isSheep = true; // set the animal to a sheep
            speed = Form1.sheepSpeed;
        }

        public Sheep()
        {
            maxAge = Form1.r.Next(Form1.maxAgeForSheep); // set maxAge
            isSheep = true; // set the animal to a sheep
            speed = Form1.sheepSpeed;
        }

        //************* DYNAMISK BINDNING *************
        public override void Move() // moves the object on our static bitmap
        {
            lonely = IsLonely();

            if (Form1.mouseActivated && Form1.hovering)
                endPos = Form1.mousePos;
            else if (age > Form1.fertilityAgeForSheep &&
                (eros > Form1.erosThresholdForSheep || 
                (thirst < Form1.htThresholdForSheep && 
                hunger < Form1.htThresholdForSheep)))           // find another sheep to duplicate
                   endPos = Search("dependentSheep");
            else if (hunger > thirst)                        // search after the grass with best health
                endPos = Search("grass");
            else if (hunger <= thirst)                       // search after the closest pool
                endPos = Search("pool"); 

            dir = GetDirection(pos, endPos, speed); // Get direction

            if (HoldsSheep(dir)                                 // if there is a sheep on the pixel to move to and the sheep is of a different sex
                && Form1.animals[GetIndex(dir)].IsMale != isMale
                && age > Form1.fertilityAgeForSheep
                && hunger < Form1.htThresholdForSheep
                && thirst < Form1.htThresholdForSheep)
            {
                // Create a new sheep on that pixel
                if (Form1.r.Next(Form1.sheepProb) == Form1.r.Next(Form1.sheepProb))
                {
                    Point tempPos = GetEmptyPosition(pos);
                    if (tempPos != pos)
                    {
                        Form1.animals.Add(new Sheep(tempPos));
                        Form1.occupied[tempPos.X, tempPos.Y] = true;
                    }
                }
                Form1.newSheep++;
                eros -= Form1.erosValueForSheep;
                if (eros < 1) eros = 1;
            }

            if (!Form1.occupied[dir.X, dir.Y])
            {
                if (Form1.nightMode) b.SetPixel(dir.X, dir.Y, InvertColor(Form1.sheepColor));
                else b.SetPixel(dir.X, dir.Y, Form1.sheepColor); // Move
                // Eat grass or drink water
                if (Form1.terrains[pos.X / 10, pos.Y / 10].IsWater)
                {
                    Form1.terrains[pos.X / 10, pos.Y / 10].ChangeHealth(Form1.thirstValueForSheep * -1);
                    thirst -= Form1.thirstValueForSheep;
                    Form1.waterDrunkenBySheep += Form1.thirstValueForSheep;
                    if (thirst < 1) thirst = 1;
                }
                else if (Form1.terrains[pos.X / 10, pos.Y / 10].Health > 1)
                {
                    Form1.terrains[pos.X / 10, pos.Y / 10].ChangeHealth(Form1.hungerValueForSheep * -1);
                    hunger -= Form1.hungerValueForSheep;
                    Form1.grassEatenBySheep += Form1.hungerValueForSheep;
                    if (hunger < 1) hunger = 1;
                }
                Form1.occupied[pos.X, pos.Y] = false; // update occupation map
                Form1.occupied[dir.X, dir.Y] = true;
                pos = dir; // Update current position
            }
            else
            {
                if (Form1.nightMode) b.SetPixel(pos.X, pos.Y, InvertColor(Form1.sheepColor));
                else b.SetPixel(pos.X, pos.Y, Form1.sheepColor); // redraw pixel on pos
            }
        }

        protected override Point GetEmptyPosition(Point p) // get an empty position close to p
        {
            for (int i = 1; i < Form1.hotRadiusForSheep; i++)
            {
                if (p.X - i >= 0 && !Form1.occupied[p.X - i, p.Y]) return new Point(p.X - i, p.Y);
                if (p.X - i >= 0 && p.Y + i < 300 && !Form1.occupied[p.X - i, p.Y + i]) return new Point(p.X - i, p.Y);
                if (p.Y + i < 300 && !Form1.occupied[p.X, p.Y + i]) return new Point(p.X, p.Y + i);
                if (p.X + i < 600 && p.Y + i < 300 && !Form1.occupied[p.X + i, p.Y + i]) return new Point(p.X + i, p.Y + i);
                if (p.X + i < 300 && !Form1.occupied[p.X + i, p.Y]) return new Point(p.X + i, p.Y);
                if (p.Y - i >= 0 && p.X + i < 300 && !Form1.occupied[p.X + i, p.Y - i]) return new Point(p.X + i, p.Y - i);
                if (p.Y - i >= 0 && !Form1.occupied[p.X, p.Y - i]) return new Point(p.X, p.Y - i);
                if (p.Y - i >= 0 && p.X - i >= 0 && !Form1.occupied[p.X - i, p.Y - i]) return new Point(p.X - i, p.Y - i);
            }
            return p;
        }

        private bool IsLonely()
        {
            // Determine whether the sheep is lonely
            double lonelyRadius = (double)Form1.flockRadius;
            for (int i = 0; i < Form1.animals.Count; i++)
                if (Form1.animals[i].IsSheep)
                {
                    double temp = Math.Sqrt(Math.Pow(pos.X - Form1.animals[i].Position.X, 2) +
                        Math.Pow(pos.Y - Form1.animals[i].Position.Y, 2));
                    if (temp != 0 && temp < lonelyRadius) return false;
                }
            return true;
        }

        public override bool Lonely
        {
            get { return lonely; }
        }
    }
}
