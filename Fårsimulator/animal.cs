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
    public abstract class Animal
    {
        // All virtual functions assume that the animal class is a wolf by default. Once a sheep is created
        // those functions will be overridden by corresponding functions in the sheep class
        protected int speed; // the number of pixels to move for each frame
        protected Point pos; // holds the current position
        protected Point dir; // holds the position of the next pixel to move to
        protected Point endPos; // the end location
        protected Bitmap b = Form1.b; // our canvas to draw on
        protected bool isSheep = false; // determine whether the object is a sheep or a wolf
        protected bool isMale;
        protected int thirst;
        protected int hunger;
        protected int eros;
        protected int age = 0;
        protected int maxAge;

        public Animal(Point position)
        {
            // Assign pos according to the value provided
            this.pos = position;
            // Randomize typo
            isMale = GetRandomBool();
            thirst = Form1.r.Next(Form1.maxThirst);
            hunger = Form1.r.Next(Form1.maxHunger);
            eros = Form1.r.Next(Form1.maxEros);
        }

        public Animal()
        {
            // Assign pos according to the value provided
            this.pos = new Point(Form1.r.Next(600), Form1.r.Next(300));
            // Randomize typo
            isMale = GetRandomBool();
            thirst = Form1.r.Next(Form1.maxThirst);
            hunger = Form1.r.Next(Form1.maxHunger);
            eros = Form1.r.Next(Form1.maxEros);
        }

        protected Point Search(string name) // look after the closest object of type provided and returns the position of that object
        {
            // Use the Pythagorean theroem to find the closest sheep. Since we are not
            // interested in the actual distance, we can skip the square root to save
            // some processor cycles
            double h = int.MaxValue; // where h is the distance^2 to the closest object
            Point closest = pos; // we set closest position to current position so we don't return an empty value if no objects are found
            if (name == "pool") // search for pools
            {
                foreach (Terrain terrain in Form1.terrains)
                {
                    if (terrain.IsWater)
                    {
                        double temp = Math.Pow(terrain.Position.X - pos.X, 2) + Math.Pow(terrain.Position.Y - pos.Y, 2);
                        if (temp < h)
                        {
                            closest = terrain.Position;
                            h = temp;
                        }
                    }
                }
            }
            else if (name == "grass") // search for grasses with the best health
            {
                int maxHealth = int.MinValue;
                foreach (Terrain terrain in Form1.terrains)
                {
                    if (!terrain.IsWater)
                    {
                        int temp = terrain.Health;
                        if (temp > maxHealth)
                        {
                            closest = terrain.Position;
                            maxHealth = temp;
                        }
                    }
                }
            }
            else if (name == "sheep") // search for sheep
            {
                foreach (Animal animal in Form1.animals)
                {
                    if (animal.IsSheep)
                    {
                        double temp = Math.Pow(animal.Position.X - pos.X, 2) + Math.Pow(animal.Position.Y - pos.Y, 2);
                        if (temp < h) // if distance is less than previous distance
                        {
                            closest = animal.Position; // set new target
                            h = temp; // update h to our new value
                        }
                    }
                }
            }
            else if (name == "dependentSheep") // search for closest sheep with regard taken to whether the object is a male/female
            {
                foreach (Animal animal in Form1.animals)
                {
                    if (animal.IsSheep && animal.IsMale != isMale) // if the animal is a sheep and sexes are different
                    {
                        double temp = Math.Pow(animal.Position.X - pos.X, 2) + Math.Pow(animal.Position.Y - pos.Y, 2);
                        if (temp < h) // if distance is less than previous distance
                        {
                            closest = animal.Position; // set new target
                            h = temp; // update h to our new value
                        }
                    }
                }
            }
            else if (name == "dependentWolf")
            {
                foreach (Animal animal in Form1.animals)
                {
                    if (!animal.IsSheep && animal.IsMale != isMale) // if the animal is a sheep and sexes are different
                    {
                        double temp = Math.Pow(animal.Position.X - pos.X, 2) + Math.Pow(animal.Position.Y - pos.Y, 2);
                        if (temp < h) // if distance is less than previous distance
                        {
                            closest = animal.Position; // set new target
                            h = temp; // update h to our new value
                        }
                    }
                }
            }
            else if (name == "shepherd") // look for shepherds
            {
                foreach (Shepherd shepherd in Form1.shepherds)
                {
                    double temp = Math.Pow(shepherd.Position.X - pos.X, 2) + Math.Pow(shepherd.Position.Y - pos.Y, 2);
                    if (temp < h) // if distance is less than previous distance
                    {
                        closest = shepherd.Position; // set new target
                        h = temp; // update h to our new value
                    }
                }
            }
            return closest;
        }

        protected Point Search(string name, bool hasFieldOfVision) // look after the closest object within the range of radius of type provided and returns the position of that object
        {
            double h = int.MaxValue;
            Point closest = pos;
            if (hasFieldOfVision)
            {
                if (name == "pool") // search for pools
                {
                    foreach (Terrain terrain in Form1.terrains)
                    {
                        if (terrain.IsWater)
                        {
                            double temp = Math.Pow(terrain.Position.X - pos.X, 2) + Math.Pow(terrain.Position.Y - pos.Y, 2);
                            if (temp < h && temp < Form1.radius * Form1.radius) // if current distance is shorter than previous distance and within the field of vision defined by radius
                            {
                                closest = terrain.Position;
                                h = temp;
                            }
                        }
                    }
                }
                else if (name == "sheep") // search for sheep
                {
                    foreach (Animal animal in Form1.animals)
                    {
                        if (animal.IsSheep)
                        {
                            double temp = Math.Pow(animal.Position.X - pos.X, 2) + Math.Pow(animal.Position.Y - pos.Y, 2);
                            if (temp < h && temp < Form1.radius * Form1.radius)
                            {
                                closest = animal.Position;
                                h = temp;
                            }
                        }
                    }
                }
                else if (name == "dependentWolf")
                {
                    foreach (Animal animal in Form1.animals)
                    {
                        if (!animal.IsSheep && animal.IsMale != isMale) // if the animal is a sheep and sexes are different
                        {
                            double temp = Math.Pow(animal.Position.X - pos.X, 2) + Math.Pow(animal.Position.Y - pos.Y, 2);
                            if (temp < h && temp < Form1.radius * Form1.radius)
                            {
                                closest = animal.Position;
                                h = temp;
                            }
                        }
                    }
                }
            }
            else
            {
                if (name == "pool") // search for pools
                {
                    foreach (Terrain terrain in Form1.terrains)
                    {
                        if (terrain.IsWater)
                        {
                            double temp = Math.Pow(terrain.Position.X - pos.X, 2) + Math.Pow(terrain.Position.Y - pos.Y, 2);
                            if (temp < h)
                            {
                                closest = terrain.Position;
                                h = temp;
                            }
                        }
                    }
                }
                else if (name == "sheep") // search for sheep
                {
                    foreach (Animal animal in Form1.animals)
                    {
                        if (animal.IsSheep)
                        {
                            double temp = Math.Pow(animal.Position.X - pos.X, 2) + Math.Pow(animal.Position.Y - pos.Y, 2);
                            if (temp < h) // if distance is less than previous distance
                            {
                                closest = animal.Position; // set new target
                                h = temp; // update h to our new value
                            }
                        }
                    }
                }
                else if (name == "dependentWolf")
                {
                    foreach (Animal animal in Form1.animals)
                    {
                        if (!animal.IsSheep && animal.IsMale != isMale) // if the animal is a sheep and sexes are different
                        {
                            double temp = Math.Pow(animal.Position.X - pos.X, 2) + Math.Pow(animal.Position.Y - pos.Y, 2);
                            if (temp < h) // if distance is less than previous distance
                            {
                                closest = animal.Position; // set new target
                                h = temp; // update h to our new value
                            }
                        }
                    }
                }
            }
            return closest;
        }

        public virtual void Move() // moves the object on our static bitmap
        {
            if (Form1.mouseActivated && Form1.hovering)
                endPos = Form1.mousePos;
            else if (age > Form1.fertilityAgeForWolf &&
                (eros > Form1.erosThresholdForWolf ||
                (hunger < Form1.htThresholdForWolf && thirst < Form1.htThresholdForWolf)))
                endPos = Search("dependentWolf", Form1.fieldOfVision);
            else if (hunger > thirst)
                endPos = Search("sheep", Form1.fieldOfVision);
            else if (thirst >= hunger)
                endPos = Search("pool", Form1.fieldOfVision);
            
            dir = GetDirection(pos, endPos, speed); // get next pixel
            if (HoldsSheep(dir)) // if there is a sheep on the position to move to
            {
                // Remove the sheep on this position
                for (int i = 0; i < Form1.animals.Count; ++i)
                {
                    if (IsSheep && Form1.animals[i].Position == dir)
                    {
                        Form1.occupied[Form1.animals[i].Position.X, Form1.animals[i].Position.Y] = false; 
                        Form1.animals.RemoveAt(i);
                        hunger -= Form1.hungerValueForWolf;
                        Form1.sheepEatenByWolf++;
                        if (hunger < 1) hunger = 1;
                        break;
                    }
                }
            }
            else if (HoldsWolf(dir) // if there is a wolf on the position to move to
                && Form1.animals[GetIndex(dir)].IsMale != isMale
                && age > Form1.fertilityAgeForWolf
                && hunger < Form1.htThresholdForWolf
                && thirst < Form1.htThresholdForWolf)
            {
                if (Form1.r.Next(Form1.wolfProb) == Form1.r.Next(Form1.wolfProb))
                {
                    Point tempPos = GetEmptyPosition(pos);
                    if (tempPos != pos) // if there is space enough to create a new wolf
                    {
                        Form1.animals.Add(new Wolf(tempPos));
                        Form1.occupied[tempPos.X, tempPos.Y] = true;
                    }
                }
                eros -= Form1.erosValueForWolf;
                Form1.newWolfs++;
                if (eros < 1) eros = 1;
            }

            if (!Form1.occupied[dir.X, dir.Y]) // if the pixel on the position dir is not occupied, move to that pixel
            {
                if (Form1.nightMode) b.SetPixel(dir.X, dir.Y, InvertColor(Form1.wolfColor));
                else b.SetPixel(dir.X, dir.Y, Form1.wolfColor); // move
                // Drink water if there is is water on current position
                if (Form1.terrains[pos.X / 10, pos.Y / 10].IsWater)
                {
                    Form1.terrains[pos.X / 10, pos.Y / 10].ChangeHealth(Form1.thirstValueForWolf * -1);
                    thirst -= Form1.thirstValueForWolf;
                    Form1.waterDrunkenByWolf += Form1.thirstValueForWolf;
                    if (thirst < 1) thirst = 1;
                }
                Form1.occupied[pos.X, pos.Y] = false; // update occupation map
                Form1.occupied[dir.X, dir.Y] = true;
                pos = dir; // update position
            }
            else
            {
                if (Form1.nightMode) b.SetPixel(pos.X, pos.Y, InvertColor(Form1.wolfColor));
                else b.SetPixel(pos.X, pos.Y, Form1.wolfColor); // redraw pixel on pos
            }
            if (!Form1.constantSpeed && pos.X - endPos.X != 0) // calculate new speed
            {
                speed = Convert.ToInt32(1 / Math.Sqrt(Math.Pow(pos.X - endPos.X, 2) + Math.Pow(pos.Y - endPos.Y, 2)));
                if (speed < 1) speed = 1;
            }
        }

        protected virtual Point GetEmptyPosition(Point p) // get an empty position close to p
        {
            for (int i = 1; i < Form1.hotRadiusForWolf; i++)
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

        protected Color InvertColor(Color colorToInvert)
        {
            return Color.FromArgb(255 - colorToInvert.R, 255 - colorToInvert.G, 255 - colorToInvert.B);
        }

        public Point Position // get or set pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public bool IsSheep // get the type of animal
        {
            get { return isSheep; }
        }

        public bool IsMale // get or set the sex
        {
            get { return isMale; }
            set { isMale = value; }
        }

        protected bool GetRandomBool() // randomizes a boolean variable
        {
            if (Form1.r.Next(2) == 1) return true;
            else return false;
        }

        protected int GetIndex(Point position) // get the index of the first object with the position provided 
        {
            for (int i = 0; i < Form1.animals.Count; i++)
            {
                if (Form1.animals[i].Position == position)
                    return i;
            }
            return -1; // no index found
        }

        protected Point GetDirection(Point start, Point end, int numberOfPixels) // calculates the next pixel to move to
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

        public void IncreaseAge()
        {
            age++;
        }

        public int Age
        {
            get { return age; }
        } // get age

        public int MaxAge
        {
            get { return maxAge; }
        } // get maxAge

        public string GetInfo() // returns info about the object
        {
            string build = "";
            if (isSheep) 
                build += "Djuret är ett får av ";
            else build += "Djuret är en varg av ";
            if (isMale) build += "hankön som rör sig med hastigheten " + speed +
                " pixlar per tick mot punkten (" + endPos.X + ", " + endPos.Y + ").";
            else build += "honkön som rör sig med hastigheten " + speed +
                " pixlar per tick mot punkten (" + endPos.X + ", " + endPos.Y + ").";
            build += " Djuret har en hunger på " + Math.Round(hunger / (double)Form1.maxHunger, 2) * 100 +
                "%, en törst på " + Math.Round(thirst / (double)Form1.maxThirst, 2) * 100 +
                "% och en sexualdrift på " + Math.Round(eros / (double)Form1.maxEros, 2) * 100 + "%.";
            if (isSheep) build += "Fåret är " + age + " år gammalt och kommer leva i ytterligare "
                + Convert.ToInt32(maxAge - age) + " år till.";
            else build += "Vargen är " + age + " år gammal och kommer leva i ytterligare "
                + Convert.ToInt32(maxAge - age) + " år till.";
            return build;
        }

        protected bool HoldsSheep(Point onPosition)
        {
            foreach (Animal a in Form1.animals)
                if (a.Position == onPosition && a.IsSheep)
                    return true;
            return false;
        }

        protected bool HoldsWolf(Point onPosition)
        {
            foreach (Animal a in Form1.animals)
                if (a.Position == onPosition && !a.IsSheep)
                    return true;
            return false;
        }

        public int Hunger
        {
            get { return hunger; }
            set { hunger = value; }
        }

        public int Thirst
        {
            get { return thirst; }
            set { thirst = value; }
        }

        public int Eros
        {
            get { return eros; }
            set { eros = value; }
        }

        public virtual bool Lonely
        {
            get { return false; }
        }
    }
}
