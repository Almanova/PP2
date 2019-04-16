using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circles2
{
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;
        int r = 0;
        Pen pen = new Pen(Color.Black, 3);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            r = 0;
            x = e.Location.X;
            y = e.Location.Y;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            r += 5;
            pen.Color = Color.FromArgb(new Random().Next());
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(pen, x - r, y - r, r * 2, r * 2);
        }
    }
}
