using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fårsimulator
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        public string RichTextBox1 // set the text in the richTextBox
        {
            set { richTextBox1.Text = value; }
        }

        // Set labels
        public string NumberOfSheep
        {
            set { numberOfSheep.Text = value; }
        }

        public string NumberOfWolfs
        {
            set { numberOfWolfs.Text = value; }
        }

        public string TotalAmountOfWater
        {
            set { totalAmountOfWater.Text = value; }
        }

        public string TotalAmountOfGrass
        {
            set { totalAmountOfGrass.Text = value; }
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            // This code will execute when the user clicks the [x] in the top right corner
            // of the window. Instead of closing the form as usual, we'll try to hide it, which means
            // that we'll have it running in the background. If we close the window as usual, we'll have
            // some problems if the user open the log, close it and then tries to open the log again.
            // This action will throw an exception since Form1 will be unable to start an instance of a
            // disposed object. So, instead of disposing (closing) the object, we hide it and then cancel
            // the Log close action by setting e.Cancel to true (canceled).
            this.Hide(); // hide the window
            Form1.logIsOpen = false;
            e.Cancel = true; // this cancels the close event
        }
    }
}
