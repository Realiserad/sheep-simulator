namespace Fårsimulator
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arkivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avslutaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.körToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startaSimuleringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ändraVariablerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hjälpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.förstoringsglasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.omFårsimulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pb = new System.Windows.Forms.PictureBox();
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            this.ageTimer = new System.Windows.Forms.Timer(this.components);
            this.logTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arkivToolStripMenuItem,
            this.körToolStripMenuItem,
            this.hjälpToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(600, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arkivToolStripMenuItem
            // 
            this.arkivToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.avslutaToolStripMenuItem});
            this.arkivToolStripMenuItem.Name = "arkivToolStripMenuItem";
            this.arkivToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.arkivToolStripMenuItem.Text = "Arkiv";
            // 
            // avslutaToolStripMenuItem
            // 
            this.avslutaToolStripMenuItem.Name = "avslutaToolStripMenuItem";
            this.avslutaToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.avslutaToolStripMenuItem.Text = "Avsluta";
            this.avslutaToolStripMenuItem.Click += new System.EventHandler(this.avslutaToolStripMenuItem_Click);
            // 
            // körToolStripMenuItem
            // 
            this.körToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startaSimuleringToolStripMenuItem,
            this.ändraVariablerToolStripMenuItem,
            this.loggToolStripMenuItem});
            this.körToolStripMenuItem.Name = "körToolStripMenuItem";
            this.körToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.körToolStripMenuItem.Text = "Verktyg";
            // 
            // startaSimuleringToolStripMenuItem
            // 
            this.startaSimuleringToolStripMenuItem.Name = "startaSimuleringToolStripMenuItem";
            this.startaSimuleringToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.startaSimuleringToolStripMenuItem.Text = "Starta simulering";
            this.startaSimuleringToolStripMenuItem.Click += new System.EventHandler(this.startaSimuleringToolStripMenuItem_Click);
            // 
            // ändraVariablerToolStripMenuItem
            // 
            this.ändraVariablerToolStripMenuItem.Name = "ändraVariablerToolStripMenuItem";
            this.ändraVariablerToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.ändraVariablerToolStripMenuItem.Text = "Ändra variabler...";
            this.ändraVariablerToolStripMenuItem.Click += new System.EventHandler(this.ändraVariablerToolStripMenuItem_Click);
            // 
            // loggToolStripMenuItem
            // 
            this.loggToolStripMenuItem.Name = "loggToolStripMenuItem";
            this.loggToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.loggToolStripMenuItem.Text = "Öppna logg";
            this.loggToolStripMenuItem.Click += new System.EventHandler(this.loggToolStripMenuItem_Click);
            // 
            // hjälpToolStripMenuItem
            // 
            this.hjälpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.förstoringsglasToolStripMenuItem,
            this.omFårsimulatorToolStripMenuItem});
            this.hjälpToolStripMenuItem.Name = "hjälpToolStripMenuItem";
            this.hjälpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hjälpToolStripMenuItem.Text = "Hjälp";
            // 
            // förstoringsglasToolStripMenuItem
            // 
            this.förstoringsglasToolStripMenuItem.Name = "förstoringsglasToolStripMenuItem";
            this.förstoringsglasToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.förstoringsglasToolStripMenuItem.Text = "Förstoringsglas";
            this.förstoringsglasToolStripMenuItem.Click += new System.EventHandler(this.förstoringsglasToolStripMenuItem_Click);
            // 
            // omFårsimulatorToolStripMenuItem
            // 
            this.omFårsimulatorToolStripMenuItem.Name = "omFårsimulatorToolStripMenuItem";
            this.omFårsimulatorToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.omFårsimulatorToolStripMenuItem.Text = "Om Fårsimulator...";
            this.omFårsimulatorToolStripMenuItem.Click += new System.EventHandler(this.omFårsimulatorToolStripMenuItem_Click);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(0, 23);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(600, 300);
            this.pb.TabIndex = 1;
            this.pb.TabStop = false;
            this.pb.MouseEnter += new System.EventHandler(this.pb_MouseEnter);
            this.pb.MouseLeave += new System.EventHandler(this.pb_MouseLeave);
            this.pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            // 
            // moveTimer
            // 
            this.moveTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ageTimer
            // 
            this.ageTimer.Interval = 1000;
            this.ageTimer.Tick += new System.EventHandler(this.ageTimer_Tick);
            // 
            // logTimer
            // 
            this.logTimer.Interval = 2000;
            this.logTimer.Tick += new System.EventHandler(this.logTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 323);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fårsimulator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arkivToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avslutaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem körToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startaSimuleringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hjälpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem omFårsimulatorToolStripMenuItem;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Timer ageTimer;
        private System.Windows.Forms.ToolStripMenuItem förstoringsglasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ändraVariablerToolStripMenuItem;
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.ToolStripMenuItem loggToolStripMenuItem;
        private System.Windows.Forms.Timer logTimer;
    }
}

