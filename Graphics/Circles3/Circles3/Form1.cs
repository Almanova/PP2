using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circles3
{
    public partial class Form1 : Form
    {
        Point center;
        Bitmap bitmap;
        Graphics graphics;
        Pen pen = new Pen(Color.Maroon, 3);
        int r = 0;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(pen, center.X - r, center.Y - r, r * 2, r * 2);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            graphics.DrawEllipse(pen, center.X - r, center.Y - r, r * 2, r * 2);
            pictureBox1.Refresh();
            r = 0;
            center = e.Location;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            r += 5;
            pen.Color = Color.FromArgb(new Random().Next());
            pictureBox1.Refresh();
        }
    }
}
