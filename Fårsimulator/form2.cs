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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            // Field of vision
            fieldOfVision.Checked = Form1.fieldOfVision;
            fieldOfVisionValue.Enabled = Form1.fieldOfVision;
            fieldOfVisionValue.Text = Convert.ToString(Form1.radius);
            // Set color picker buttons
            setShepherdColor.BackColor = Form1.shepherdColor;
            setSheepColor.BackColor = Form1.sheepColor;
            setWolfColor.BackColor = Form1.wolfColor;
            // Set start values
            sheep.Text = Convert.ToString(Form1.numberOfSheep);
            wolfs.Text = Convert.ToString(Form1.numberOfWolfs);
            shepherds.Text = Convert.ToString(Form1.numberOfShepherds);
            // Speeds
            sheepSpeed.Text = Convert.ToString(Form1.sheepSpeed);
            wolfSpeed.Text = Convert.ToString(Form1.wolfSpeed);
            shepherdSpeed.Text = Convert.ToString(Form1.shepherdSpeed);
            // Probabilities
            poolProb.Text = Convert.ToString(1 / Convert.ToDouble(Form1.poolProb));
            grassProb.Text = Convert.ToString(1 / Convert.ToDouble(Form1.grassProb));
            healthProb.Text = Convert.ToString(1 / Convert.ToDouble(Form1.healthProb));
            wolfProb.Text = Convert.ToString(1 / Convert.ToDouble(Form1.wolfProb));
            sheepProb.Text = Convert.ToString(1 / Convert.ToDouble(Form1.sheepProb));
            // Max values
            growth.Text = Convert.ToString(Form1.growth);
            maxAgeForWolf.Text = Convert.ToString(Form1.maxAgeForWolf);
            maxAgeForSheep.Text = Convert.ToString(Form1.maxAgeForSheep);
            maxThirst.Text = Convert.ToString(Form1.maxThirst);
            maxHunger.Text = Convert.ToString(Form1.maxHunger);
            maxEros.Text = Convert.ToString(Form1.maxEros);
            // Fertility ages
            fertilityAgeForSheep.Text = Convert.ToString(Form1.fertilityAgeForSheep);
            fertilityAgeForWolf.Text = Convert.ToString(Form1.fertilityAgeForWolf);
            // Update frequencies
            moveTimerInterval.Text = Convert.ToString(Form1.moveTimerInterval);
            ageTimerInterval.Text = Convert.ToString(Form1.ageTimerInterval);
            logTimerInterval.Text = Convert.ToString(Form1.logTimerInterval);
            // Behavior for wolfs
            erosThresholdForWolf.Text = Convert.ToString(Form1.erosThresholdForWolf);
            htThresholdForWolf.Text = Convert.ToString(Form1.htThresholdForWolf);
            erosValueForWolf.Text = Convert.ToString(Form1.erosValueForWolf);
            thirstValueForWolf.Text = Convert.ToString(Form1.thirstValueForWolf);
            hungerValueForWolf.Text = Convert.ToString(Form1.hungerValueForWolf);
            // Behavior for sheep
            erosThresholdForSheep.Text = Convert.ToString(Form1.erosThresholdForSheep);
            htThresholdForSheep.Text = Convert.ToString(Form1.htThresholdForSheep);
            erosValueForSheep.Text = Convert.ToString(Form1.erosValueForSheep);
            thirstValueForSheep.Text = Convert.ToString(Form1.thirstValueForSheep);
            hungerValueForSheep.Text = Convert.ToString(Form1.hungerValueForSheep);
            flockRadius.Text = Convert.ToString(Form1.flockRadius);
            // Misc
            trace.Checked = Form1.trace;
            killWeakAnimals.Checked = Form1.killWeakAnimals;
            nightMode.Checked = Form1.nightMode;
            mouseActivated.Checked = Form1.mouseActivated;
            // Hot radius
            hotRadiusForSheep.Text = Convert.ToString(Form1.hotRadiusForSheep);
            hotRadiusForShepherd.Text = Convert.ToString(Form1.hotRadiusForShepherd);
            hotRadiusForWolf.Text = Convert.ToString(Form1.hotRadiusForWolf);
            // Speed type
            if (Form1.constantSpeed)
            {
                constantSpeed.Checked = true;
                accelerating.Checked = false;
            }
            else
            {
                constantSpeed.Checked = false;
                accelerating.Checked = true;
            }
        }

        private void setSheepColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Form1.sheepColor;

            if (colorDlg.ShowDialog() == DialogResult.OK) Form1.sheepColor = colorDlg.Color;

            setSheepColor.BackColor = Form1.sheepColor;
        }

        private void setWolfColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Form1.wolfColor;

            if (colorDlg.ShowDialog() == DialogResult.OK) Form1.wolfColor = colorDlg.Color;

            setWolfColor.BackColor = Form1.wolfColor;
        }

        private void setShepherdColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Form1.shepherdColor;

            if (colorDlg.ShowDialog() == DialogResult.OK) Form1.shepherdColor = colorDlg.Color;

            setShepherdColor.BackColor = Form1.shepherdColor;
            Form1.tracePen = new Pen(Form1.shepherdColor, 1);
        }

        private void sheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.numberOfSheep = Convert.ToInt32(sheep.Text);
            }
            catch
            {

            }
        }

        private void wolfs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.numberOfWolfs = Convert.ToInt32(wolfs.Text);
            }
            catch
            {
            }
        }

        private void shepherds_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.numberOfShepherds = Convert.ToInt32(shepherds.Text);
            }
            catch
            {
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.poolProb = Convert.ToInt32(Math.Round(1 / Convert.ToDouble(poolProb.Text), 0));
            }
            catch
            {
            }
        }

        private void grassProb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.grassProb = Convert.ToInt32(Math.Round(1 / Convert.ToDouble(grassProb.Text), 0));
            }
            catch
            {
            }
        }

        private void healthProb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.healthProb = Convert.ToInt32(Math.Round(1 / Convert.ToDouble(healthProb.Text), 0));
            }
            catch
            {
            }
        }

        private void growth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.growth = Convert.ToInt32(growth.Text);
            }
            catch
            {
            }
        }

        private void maxAgeForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.maxAgeForWolf = Convert.ToInt32(maxAgeForWolf.Text);
            }
            catch
            { 
            }
        }

        private void maxThirst_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.maxThirst = Convert.ToInt32(maxThirst.Text);
            }
            catch
            {
            }
        }

        private void maxHunger_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.maxHunger = Convert.ToInt32(maxHunger.Text);
            }
            catch
            {
            }
        }

        private void maxEros_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.maxEros = Convert.ToInt32(maxEros.Text);
            }
            catch
            {
            }
        }

        private void sheepSpeed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.sheepSpeed = Convert.ToInt32(sheepSpeed.Text);
            }
            catch
            {
            }
        }

        private void wolfSpeed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.wolfSpeed = Convert.ToInt32(wolfSpeed.Text);
            }
            catch
            {
            }
        }

        private void shepherdSpeed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.shepherdSpeed = Convert.ToInt32(shepherdSpeed.Text);
            }
            catch
            {
            }
        }

        private void moveTimerInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.moveTimerInterval = Convert.ToInt32(moveTimerInterval.Text);
            }
            catch
            {
            }
        }

        private void ageTimerInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.ageTimerInterval = Convert.ToInt32(ageTimerInterval.Text);
            }
            catch
            {
            }
        }

        private void erosThresholdForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.erosThresholdForWolf = Convert.ToInt32(erosThresholdForWolf.Text);
            }
            catch
            {
            }
        }

        private void htThresholdForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.htThresholdForWolf = Convert.ToInt32(htThresholdForWolf.Text);
            }
            catch
            {
            }
        }

        private void erosValueForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.erosValueForWolf = Convert.ToInt32(erosValueForWolf.Text);
            }
            catch
            {
            }
        }

        private void hungerValueForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.hungerValueForWolf = Convert.ToInt32(hungerValueForWolf.Text);
            }
            catch
            {
            }
        }

        private void thirstValueForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.thirstValueForWolf = Convert.ToInt32(thirstValueForWolf.Text);
            }
            catch
            {
            }
        }

        private void erosThresholdForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.erosThresholdForSheep = Convert.ToInt32(erosThresholdForSheep.Text);
            }
            catch
            {
            }
        }

        private void htThresholdForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.htThresholdForSheep = Convert.ToInt32(htThresholdForSheep.Text);
            }
            catch
            {
            }
        }

        private void erosValueForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.erosValueForSheep = Convert.ToInt32(erosValueForSheep.Text);
            }
            catch
            {
            }
        }

        private void hungerValueForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.hungerValueForSheep = Convert.ToInt32(hungerValueForSheep.Text);
            }
            catch
            {
            }
        }

        private void thirstValueForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.thirstValueForSheep = Convert.ToInt32(thirstValueForSheep.Text);
            }
            catch
            {
            }
        }

        private void maxAgeForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.maxAgeForSheep = Convert.ToInt32(maxAgeForSheep.Text);
            }
            catch
            {
            }
        }

        private void fieldOfVision_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (fieldOfVision.Checked) // aktivera synfält
                {
                    Form1.fieldOfVision = fieldOfVision.Checked; // sätt synfält till sant
                    Form1.radius = Convert.ToDouble(fieldOfVisionValue.Text);
                    fieldOfVisionValue.Enabled = true; // aktivera textBox
                }
                else
                {
                    Form1.fieldOfVision = fieldOfVision.Checked; // sätt synfält till falskt
                    fieldOfVisionValue.Enabled = false; // inaktivera textBox
                }
            }
            catch
            {

            }
        }

        private void fieldOfVisionValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.radius = Convert.ToDouble(fieldOfVisionValue.Text);
            }
            catch
            {

            }
        }

        private void logTimerInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.logTimerInterval = Convert.ToInt32(logTimerInterval.Text);
            }
            catch
            {
            }
        }

        private void fertilityAgeForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.fertilityAgeForSheep = Convert.ToInt32(fertilityAgeForSheep.Text);
            }
            catch
            {
            }
        }

        private void fertilityAgeForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.fertilityAgeForWolf = Convert.ToInt32(fertilityAgeForWolf.Text);
            }
            catch
            {
            }
        }

        private void flockRadius_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.flockRadius = Convert.ToInt32(flockRadius.Text);
            }
            catch
            {
            }
        }

        private void trace_CheckedChanged(object sender, EventArgs e)
        {
            Form1.trace = trace.Checked;
        }

        private void killWeakAnimals_CheckedChanged(object sender, EventArgs e)
        {
            Form1.killWeakAnimals = killWeakAnimals.Checked;
        }

        private void nightMode_CheckedChanged(object sender, EventArgs e)
        {
            Form1.nightMode = nightMode.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.ResetAll();
            Close();
        }

        private void sheepProb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.sheepProb = Convert.ToInt32(Math.Round(1 / Convert.ToDouble(sheepProb.Text), 0));
            }
            catch
            {
            }
        }

        private void wolfProb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.wolfProb = Convert.ToInt32(Math.Round(1 / Convert.ToDouble(wolfProb.Text), 0));
            }
            catch
            {
            }
        }

        private void constantSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (constantSpeed.Checked)
                Form1.constantSpeed = true;
            else
                Form1.constantSpeed = false;
        }

        private void accelerating_CheckedChanged(object sender, EventArgs e)
        {
            if (constantSpeed.Checked)
                Form1.constantSpeed = true;
            else
                Form1.constantSpeed = false;
        }

        private void hotRadiusForWolf_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.hotRadiusForWolf = Convert.ToInt32(hotRadiusForWolf.Text);
            }
            catch
            {
            }
        }

        private void hotRadiusForSheep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.hotRadiusForSheep = Convert.ToInt32(hotRadiusForSheep.Text);
            }
            catch
            {
            }
        }

        private void hotRadiusForShepherd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1.hotRadiusForShepherd = Convert.ToInt32(hotRadiusForShepherd.Text);
            }
            catch
            {
            }
        }

        private void mouseActivated_CheckedChanged(object sender, EventArgs e)
        {
            Form1.mouseActivated = mouseActivated.Checked;
        }
    }
}
