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
    public class Shepherd
    {
        private Point pos;
        private Point dir;
        private Point endPos;
        private Bitmap b = Form1.b;
        private bool hasSheep = false;
        private Animal backup;
        private int speed = Form1.shepherdSpeed;

        public Shepherd(Point position)
        {
            // Assign position
            this.pos = position;
        }

        public Shepherd()
        {
            // Assign position
            this.pos = new Point(Form1.r.Next(600), Form1.r.Next(300));
        }

        private int GetIndex(Point position) // get the index of the first object with the position provided 
        {
            for (int i = 0; i < Form1.animals.Count; i++)
            {
                if (Form1.animals[i].Position == position)
                    return i;
            }
            return -1; // no index found
        }

        public void Move() // move the object on our static bitmap
        {
            if (hasSheep) // Look for the closest sheep that is lonely
            {
                double h = int.MaxValue;
                for (int i = 0; i < Form1.animals.Count; i++)
                {
                    if (Form1.animals[i].IsSheep)
                    {
                        double temp = Math.Pow(Form1.animals[i].Position.X - pos.X, 2) +
                            Math.Pow(Form1.animals[i].Position.Y - pos.Y, 2);
                        if (temp < h) // if distance is less than previous distance
                        {
                            endPos = Form1.animals[i].Position; // set new target
                            h = temp; // update h to our new value
                        }
                    }
                }
            }
            else // Look for the closest sheep
            {
                double h = int.MaxValue;
                for (int i = 0; i < Form1.animals.Count; i++)
                {
                    if (Form1.animals[i].IsSheep && Form1.animals[i].Lonely)
                    {
                        double temp = Math.Pow(Form1.animals[i].Position.X - pos.X, 2) +
                            Math.Pow(Form1.animals[i].Position.Y - pos.Y, 2);
                        if (temp < h) // if distance is less than previous distance
                        {
                            endPos = Form1.animals[i].Position; // set new target
                            h = temp; // update h to our new value
                        }
                    }
                }
            }
            dir = GetDirection(pos, endPos, speed);
            
            if (HoldsSheep(dir)) // if there is a sheep on the pixel to move to
            {
                if (hasSheep) // if shephard already has a sheep
                {
                    // Calculate the position of the sheep to put down
                    Point tempPos = GetEmptyPosition(pos);
                    if (tempPos != pos) // if there is space enough to put the sheep down
                    {
                        // Change values for this sheep
                        backup.Position = tempPos;
                        // Insert backup sheep in list
                        Form1.animals.Add(backup);
                        // Occupy space
                        Form1.occupied[tempPos.X, tempPos.Y] = true;
                        hasSheep = false;
                    }
                }
                else // has no sheep
                {
                    int index = GetIndex(dir);
                    // Backup sheep on position
                    backup = Form1.animals[index];
                    // Erase sheep from list
                    Form1.animals.RemoveAt(index);
                    // Make this space free
                    Form1.occupied[backup.Position.X, backup.Position.Y] = false;
                    hasSheep = true;
                }
            }

            if (!Form1.occupied[dir.X, dir.Y])
            {
                if (Form1.nightMode) b.SetPixel(dir.X, dir.Y, InvertColor(Form1.shepherdColor));
                else b.SetPixel(dir.X, dir.Y, Form1.shepherdColor); // move
                Form1.occupied[pos.X, pos.Y] = false; // update occupation map
                Form1.occupied[dir.X, dir.Y] = true;
                pos = dir; // update position
            }
            else
            {
                if (Form1.nightMode) b.SetPixel(pos.X, pos.Y, InvertColor(Form1.shepherdColor));
                else b.SetPixel(pos.X, pos.Y, Form1.shepherdColor); // redraw pixel on pos
            }
        }

        private Point GetEmptyPosition(Point p) // get an empty position close to p
        {
            for (int i = 1; i < Form1.hotRadiusForShepherd; i++)
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

        private Color InvertColor(Color colorToInvert)
        {
            return Color.FromArgb(255 - colorToInvert.R, 255 - colorToInvert.G, 255 - colorToInvert.B);
        }

        private bool HoldsSheep(Point onPosition)
        {
            foreach (Animal a in Form1.animals)
                if (a.Position == onPosition && a.IsSheep)
                    return true;
            return false;
        }

        private Point GetDirection(Point start, Point end, int numberOfPixels) // calculates the next pixel to move to
        {
            if (Math.Abs(start.X - end.X) <= numberOfPixels && Math.Abs(start.Y - end.Y) <= numberOfPixels)
                return end;
            Point p = start;
            for (int i = 0; i < numberOfPixels; i++)
            {
                // Every pixel has eight neighbours. Check each case individually to determine 
                // which pixel that is closest to end and return a Point class. We assume
                // that start is not equal with end.
                if (start.X == end.X && start.Y < end.Y) p = new Point(start.X, start.Y + 1);
                else if (start.X == end.X && start.Y > end.Y) p = new Point(start.X, start.Y - 1);
                else if (start.Y == end.Y && start.X < end.X) p = new Point(start.X + 1, start.Y);
                else if (start.Y == end.Y && start.X > end.X) p = new Point(start.X - 1, start.Y);
                else if (start.Y < end.Y && start.X < end.X) p = new Point(start.X + 1, start.Y + 1);
                else if (start.Y < end.Y && start.X > end.X) p = new Point(start.X - 1, start.Y + 1);
                else if (start.Y > end.Y && start.X < end.X) p = new Point(start.X + 1, start.Y - 1);
                else if (start.Y > end.Y && start.X > end.X) p = new Point(start.X - 1, start.Y - 1);
                else break;
            }
            return p;
        }

        public Point Position
        {
            get { return pos; }
            set { pos = value; }
        }
    }
}
