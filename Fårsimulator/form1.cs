using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;

namespace Fårsimulator
{
    public partial class Form1 : Form
    {
        // Global variables
        public static Random r = new Random(); // lets make it static so everyone can use the same class
        //************* VEKTOR MED OBJEKT *************
        public static List<Animal> animals = new List<Animal>();
        public static List<Shepherd> shepherds = new List<Shepherd>();
        public static Terrain[,] terrains = new Terrain[60, 30];
        public static Bitmap b; // double buffered canvas to draw on
        public static bool[,] occupied; // a map showing occupied space
        public static bool logIsOpen = false; // determine whether the log form is opened by the user

        public static bool hovering = false; // determine whether the mouse is hovering over our pictureBox
        public static bool mouseActivated = false;
        public static Point mousePos; // holds the position of the mouse pointer

        public static int numberOfSheep = 300;
        public static int numberOfWolfs = 100;
        public static int numberOfShepherds = 0;

        public static int fertilityAgeForSheep = 10;
        public static int fertilityAgeForWolf = 25;

        public static double radius = 100; // radien på vargens synfält
        public static bool fieldOfVision = false;
        public static int flockRadius = 50;

        public static bool trace = false;
        public static bool killWeakAnimals = false;
        public static bool nightMode = false;

        public static Color shepherdColor = Color.Violet;
        public static Color wolfColor = Color.Red;
        public static Color sheepColor = Color.DarkGreen;

        public static int poolProb = 20;
        public static int grassProb = 20;
        public static int growth = 3;
        public static int healthProb = 5;
        public static int wolfProb = 10;
        public static int sheepProb = 5;

        public static int maxAgeForWolf = 100;
        public static int maxAgeForSheep = 60;
        public static int maxThirst = 100;
        public static int maxHunger = 100;
        public static int maxEros = 100;

        public static int sheepSpeed = 1; 
        public static int wolfSpeed = 2;
        public static int shepherdSpeed = 3;
        public static bool constantSpeed = true;

        public static int moveTimerInterval;
        public static int ageTimerInterval;
        public static int logTimerInterval;

        public static int erosThresholdForWolf = 90;
        public static int htThresholdForWolf = 30;
        public static int erosValueForWolf = 50;
        public static int hungerValueForWolf = 20;
        public static int thirstValueForWolf = 1;

        public static int erosThresholdForSheep = 30;
        public static int htThresholdForSheep = 20;
        public static int erosValueForSheep = 10;
        public static int hungerValueForSheep = 1;
        public static int thirstValueForSheep = 1;

        private bool isRunning = false;
        private Log log = new Log();
        public static Pen tracePen;

        public static int sheepEatenByWolf = 0;
        public static int waterDrunkenByWolf = 0;
        public static int waterDrunkenBySheep = 0;
        public static int grassEatenBySheep = 0;
        public static int newWolfs = 0;
        public static int newSheep = 0;

        public static int hotRadiusForWolf = 10;
        public static int hotRadiusForSheep = 10;
        public static int hotRadiusForShepherd = 15;

        //************* STATISK FUNKTION *************
        public static void ResetAll()
        {
            numberOfSheep = 300;
            numberOfWolfs = 100;
            numberOfShepherds = 0;

            fertilityAgeForSheep = 15;
            fertilityAgeForWolf = 25;

            radius = 100; // radien på vargens synfält
            fieldOfVision = false;
            flockRadius = 50;

            trace = false;
            killWeakAnimals = false;
            nightMode = false;
            mouseActivated = false;

            shepherdColor = Color.Violet;
            wolfColor = Color.Red;
            sheepColor = Color.DarkGreen;

            poolProb = 20;
            grassProb = 20;
            growth = 3;
            healthProb = 5;
            wolfProb = 10;
            sheepProb = 5;

            maxAgeForWolf = 100;
            maxAgeForSheep = 60;
            maxThirst = 100;
            maxHunger = 100;
            maxEros = 100;

            sheepSpeed = 1; 
            wolfSpeed = 2;
            shepherdSpeed = 3;
            constantSpeed = true;

            moveTimerInterval = 100; /////////////////////////////////////////// change here if tick interval changed
            ageTimerInterval = 1000;
            logTimerInterval = 2000;

            erosThresholdForWolf = 90;
            htThresholdForWolf = 30;
            erosValueForWolf = 50;
            hungerValueForWolf = 20;
            thirstValueForWolf = 1;

            erosThresholdForSheep = 30;
            htThresholdForSheep = 20;
            erosValueForSheep = 10;
            hungerValueForSheep = 1;
            thirstValueForSheep = 1;

            hotRadiusForWolf = 10;
            hotRadiusForSheep = 10;
            hotRadiusForShepherd = 15;
        }

        public Form1()
        {
            InitializeComponent();
            b = new Bitmap(pb.Width, pb.Height); // set bitmap properties to the same as our pictureBox
            moveTimerInterval = moveTimer.Interval;
            ageTimerInterval = ageTimer.Interval;
            logTimerInterval = logTimer.Interval;
            tracePen = new Pen(shepherdColor, 1);
            occupied = NewBoolMatrix();
        }

        private bool[,] NewBoolMatrix()
        {
            bool[,] matrix = new bool[600, 300];
            for (int x = 0; x < 600; ++x)
                for (int y = 0; y < 300; y++)
                    matrix[x, y] = false;
            return matrix;
        }

        void Start() // start simulation
        {
            // Clear old data
            animals.Clear();
            shepherds.Clear();
            terrains = new Terrain[60, 30];
            occupied = NewBoolMatrix();
            // Reset variables
            sheepEatenByWolf = 0;
            waterDrunkenByWolf = 0;
            waterDrunkenBySheep = 0;
            grassEatenBySheep = 0;
            newWolfs = 0;
            newSheep = 0;
            // Create terrain of type grass
            for (int x = 0; x < 60; x++)
                for (int y = 0; y < 30; y++)
                {
                    terrains[x, y] = new Grass(new Point(x * 10, y * 10));
                }
            // Convert random areas to water pools
            for (int x = 0; x < 60; x++)
                for (int y = 0; y < 30; y++)
                    if (Form1.r.Next(poolProb) == Form1.r.Next(poolProb))
                    {
                        Point currentPos = terrains[x, y].Position;
                        terrains[x, y] = new Pool(currentPos);
                    }
            // Create sheep
            for (int i = 0; i < numberOfSheep; i++)
            {
                Point p = new Point(r.Next(600), r.Next(300));
                animals.Add(new Sheep(p));
            }
            // Create wolfs
            for (int i = 0; i < numberOfWolfs; i++)
            {
                Point p = new Point(r.Next(600), r.Next(300));
                animals.Add(new Wolf(p));
            }
            // Create shepherds
            for (int i = 0; i < numberOfShepherds; i++)
            {
                Point p = new Point(r.Next(600), r.Next(300));
                shepherds.Add(new Shepherd(p));
            }
        }

        void Run() // run simulation
        {
            // Upgrade grasses to pools
            for (int x = 0; x < 60; x++)
                for (int y = 0; y < 30; y++)
                    if (!terrains[x, y].IsWater && terrains[x, y].Health >= 100 && 
                        Form1.r.Next(poolProb) == Form1.r.Next(poolProb))
                        terrains[x, y] = new Pool(new Point(x * 10, y * 10), 10); // create pool object with a health of 10
            // Downgrade pools to grasses
            for (int x = 0; x < 60; x++)
                for (int y = 0; y < 30; y++)
                    if (terrains[x, y].IsWater && terrains[x, y].Health <= 1 &&
                        Form1.r.Next(grassProb) == Form1.r.Next(grassProb))
                        terrains[x, y] = new Grass(new Point(x * 10, y * 10), 10); // create grass object with a health of 10
            // Redraw terrain areas
            foreach (Terrain terrain in terrains)
            {
                terrain.Draw();
            }
            // Move animals
            for (int i = 0; i < animals.Count; ++i) // since new elements can be added during the loop
                // we cannot use a foreach statement
            {
                animals[i].Move();
            }
            // Increase hunger, thirst and sex drive for every animal by taking the mean of hunger,
            // thirst and eros decreased by procreation, eating of grass/sheep and drinking
            // from the water pools.
            double totalErosForWolfs = newWolfs * erosValueForWolf;
            double totalErosForSheep = newSheep * erosValueForSheep;

            // Get number of sheep
            double sheep = 0;
            for (int i = 0; i < animals.Count; i++)
                if (animals[i].IsSheep)
                    sheep++;
            // Get number of wolfs
            double wolfs = animals.Count - sheep;

            // Calculate the mean values
            int erosMeanForSheep = 0;
            int hungerMeanForSheep = 0;
            int thirstMeanForSheep = 0;
            if (sheep != 0)
            {
                erosMeanForSheep = Convert.ToInt32(Math.Round(totalErosForSheep / sheep, 0));
                hungerMeanForSheep = Convert.ToInt32(Math.Round(grassEatenBySheep / sheep, 0));
                thirstMeanForSheep = Convert.ToInt32(Math.Round(waterDrunkenBySheep / sheep, 0));
            }
            int erosMeanForWolf = 0;
            int thirstMeanForWolf = 0;
            int hungerMeanForWolf = 0;
            if (wolfs != 0)
            {
                erosMeanForWolf = Convert.ToInt32(Math.Round(totalErosForWolfs / wolfs, 0));
                thirstMeanForWolf = Convert.ToInt32(Math.Round(waterDrunkenByWolf / wolfs, 0));
                hungerMeanForWolf = Convert.ToInt32(Math.Round(sheepEatenByWolf * hungerValueForWolf / wolfs, 0));
            }
            // Increase
            for (int i = 0; i < animals.Count; i++)
            {
                if (animals[i].IsSheep)
                {
                    animals[i].Eros += erosMeanForSheep;
                    animals[i].Thirst += thirstMeanForSheep;
                    animals[i].Hunger += hungerMeanForSheep;
                }
                else
                {
                    animals[i].Eros += erosMeanForWolf;
                    animals[i].Thirst += thirstMeanForWolf;
                    animals[i].Hunger += hungerMeanForWolf;
                }
            }
            // Reset variables
            sheepEatenByWolf = 0;
            waterDrunkenByWolf = 0;
            waterDrunkenBySheep = 0;
            grassEatenBySheep = 0;
            newWolfs = 0;
            newSheep = 0;
            // Move shepherds
            Graphics traceGraphics = Graphics.FromImage(b);
            foreach (Shepherd shepherd in shepherds)
            {
                shepherd.Move();
                if (trace)
                {
                    if (nightMode)
                    {
                        Pen invertedTracePen = new Pen(InvertColor(shepherdColor));
                        traceGraphics.DrawLine(invertedTracePen, 0,
                            shepherd.Position.Y, 599, shepherd.Position.Y);
                        traceGraphics.DrawLine(invertedTracePen, shepherd.Position.X, 0,
                            shepherd.Position.X, 300);
                    }
                    else
                    {
                        traceGraphics.DrawLine(tracePen, 0, 
                            shepherd.Position.Y, 599, shepherd.Position.Y);
                        traceGraphics.DrawLine(tracePen, shepherd.Position.X, 0, 
                            shepherd.Position.X, 300);
                    }
                }
            }
            traceGraphics.Dispose();
            // Push double buffer to screen
            Graphics visible = pb.CreateGraphics();
            visible.DrawImageUnscaled(b, 0, 0);
            visible.Dispose();
        }

        private void startaSimuleringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                startaSimuleringToolStripMenuItem.Text = "Avbryt simulering";
                // Create objects
                Start();
                // Start timers
                moveTimer.Enabled = true;
                ageTimer.Enabled = true;
            }
            else
            {
                moveTimer.Enabled = false;
                ageTimer.Enabled = false;
                startaSimuleringToolStripMenuItem.Text = "Starta simulering";
                isRunning = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            moveTimer.Interval = moveTimerInterval; // update interval tick value
            Run();
        }

        private void avslutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ageTimer_Tick(object sender, EventArgs e)
        {
            ageTimer.Interval = ageTimerInterval;
            if (animals.Count > 0)
            {   
                // Increase age and clean ded objects from list
                for (int i = 0; i < Form1.animals.Count; i++)
                {
                    Form1.animals[i].IncreaseAge();
                    if (Form1.animals[i].Age > animals[i].MaxAge)
                        animals.RemoveAt(GetIndex(animals[i].Position));
                    else if (killWeakAnimals)
                        if (animals[i].Hunger > maxHunger || animals[i].Thirst > maxThirst)
                            animals.RemoveAt(GetIndex(animals[i].Position));
                }
                // Increase health
                foreach (Terrain terrain in terrains)
                {
                    if (Form1.r.Next(healthProb) == Form1.r.Next(healthProb)) 
                        terrain.ChangeHealth(r.Next(growth));
                }
            }
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

        private void förstoringsglasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Starta Magnifier
            System.Diagnostics.Process.Start("magnify.exe");
        }

        private void ändraVariablerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void loggToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!logIsOpen) // Open log
            {
                log.Show();
                logIsOpen = true;
                logTimer.Enabled = true;
            }
        }

        private void logTimer_Tick(object sender, EventArgs e)
        {
            logTimer.Interval = logTimerInterval;
            if (isRunning)
            {
                // Get number of sheep
                int sheep = 0;
                foreach (Animal animal in animals)
                    if (animal.IsSheep)
                        sheep++;
                // Get number of wolfs
                int wolfs = animals.Count - sheep;
                // Get total amount of grass and water
                int grass = 0;
                int water = 0;
                foreach (Terrain terrain in terrains)
                {
                    if (terrain.IsWater)
                        water += terrain.Health;
                    else
                        grass += terrain.Health;
                }
                // Set values
                log.NumberOfSheep = Convert.ToString(sheep);
                log.NumberOfWolfs = Convert.ToString(wolfs);
                log.TotalAmountOfGrass = Convert.ToString(grass);
                log.TotalAmountOfWater = Convert.ToString(water);
                // Set richTextBox
                string build = "";
                foreach (Animal animal in animals)
                    build += animal.GetInfo() + "\n";
                log.RichTextBox1 = build;
            }
        }

        private void omFårsimulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private Color InvertColor(Color colorToInvert)
        {
            return Color.FromArgb(255 - colorToInvert.R, 255 - colorToInvert.G, 255 - colorToInvert.B);
        }

        private void pb_MouseEnter(object sender, EventArgs e)
        {
            hovering = true;
        }

        private void pb_MouseLeave(object sender, EventArgs e)
        {
            hovering = false;
        }

        //************* STRUCT *************
        public struct Coord // holds a coordinate pair
        {
            public int x;
            public int y;

            public Coord(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public double Hyp()
            {
                return Math.Sqrt(x * x + y * y);
            }
        }

        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            // Update mouse position
            mousePos.X = e.X;
            mousePos.Y = e.Y;
        }
    }
}
