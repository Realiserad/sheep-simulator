namespace Fårsimulator
{
    partial class Log
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.totalAmountOfWater = new System.Windows.Forms.Label();
            this.totalAmountOfGrass = new System.Windows.Forms.Label();
            this.numberOfWolfs = new System.Windows.Forms.Label();
            this.numberOfSheep = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.totalAmountOfWater);
            this.panel1.Controls.Add(this.totalAmountOfGrass);
            this.panel1.Controls.Add(this.numberOfWolfs);
            this.panel1.Controls.Add(this.numberOfSheep);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(431, 104);
            this.panel1.TabIndex = 0;
            // 
            // totalAmountOfWater
            // 
            this.totalAmountOfWater.AutoSize = true;
            this.totalAmountOfWater.Location = new System.Drawing.Point(135, 77);
            this.totalAmountOfWater.Name = "totalAmountOfWater";
            this.totalAmountOfWater.Size = new System.Drawing.Size(87, 13);
            this.totalAmountOfWater.TabIndex = 9;
            this.totalAmountOfWater.Text = "Waiting for info...";
            // 
            // totalAmountOfGrass
            // 
            this.totalAmountOfGrass.AutoSize = true;
            this.totalAmountOfGrass.Location = new System.Drawing.Point(135, 54);
            this.totalAmountOfGrass.Name = "totalAmountOfGrass";
            this.totalAmountOfGrass.Size = new System.Drawing.Size(87, 13);
            this.totalAmountOfGrass.TabIndex = 8;
            this.totalAmountOfGrass.Text = "Waiting for info...\r\n";
            // 
            // numberOfWolfs
            // 
            this.numberOfWolfs.AutoSize = true;
            this.numberOfWolfs.Location = new System.Drawing.Point(135, 32);
            this.numberOfWolfs.Name = "numberOfWolfs";
            this.numberOfWolfs.Size = new System.Drawing.Size(87, 13);
            this.numberOfWolfs.TabIndex = 7;
            this.numberOfWolfs.Text = "Waiting for info...";
            // 
            // numberOfSheep
            // 
            this.numberOfSheep.AutoSize = true;
            this.numberOfSheep.Location = new System.Drawing.Point(135, 9);
            this.numberOfSheep.Name = "numberOfSheep";
            this.numberOfSheep.Size = new System.Drawing.Size(87, 13);
            this.numberOfSheep.TabIndex = 6;
            this.numberOfSheep.Text = "Waiting for info...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Total mängd vatten:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Total mängd gräs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Antal vargar:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Antal får:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 110);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(431, 468);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 580);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Log";
            this.Opacity = 0.8D;
            this.Text = "Logg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label totalAmountOfWater;
        private System.Windows.Forms.Label totalAmountOfGrass;
        private System.Windows.Forms.Label numberOfWolfs;
        private System.Windows.Forms.Label numberOfSheep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}